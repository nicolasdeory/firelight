using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.ItemModules;
using Games.LeagueOfLegends.Model;
using FirelightCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Games.LeagueOfLegends
{
    // TODO: Refactor this into a separate project
    public class LeagueOfLegendsModule : BaseGameModule
    {
        public const string GAME_ID = "leagueoflegends";

        private static List<Type> ChampionControllers = GetChampionControllers();
        private static List<Type> ItemControllers = GetItemControllers();

        protected GameState GameState;

        public static List<string> ChampionNames = ChampionControllers
                .Select(x => ((ChampionAttribute)Attribute.GetCustomAttribute(x, typeof(ChampionAttribute))).ChampionName).ToList();

        // Constants

        public static HSVColor LoadingColor { get; } = new HSVColor(0.09f, 0.8f, 1f);

        public static HSVColor DeadColor { get; } = new HSVColor(0f, 0.8f, 0.77f);
        public static HSVColor NoManaColor { get; } = new HSVColor(0.52f, 0.66f, 1f);

        public static HSVColor KillColor { get; } = new HSVColor(0.06f, 0.96f, 1f);

        // Variables

        ChampionModule championModule;

        ItemModule[] ItemModules = new ItemModule[7];

        int lastExternalFrameReceivedTimer = 0;
        double currentGameTimestamp = 0;

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        string customKillAnimation = null; // can be changed by champion module
        bool wasDeadLastFrame = false;

        HUDModule hudModule = new HUDModule();
        LEDData lastHudFrame = LEDData.Empty;


        // Events

        /// <summary>
        /// Creates a new <see cref="LeagueOfLegendsModule"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        public static LeagueOfLegendsModule Create()
        {
            return new LeagueOfLegendsModule();
        }

        private static AbilityCastPreference GetCastPreference(string castMode)
        {
            return castMode switch
            {
                "quick" => AbilityCastPreference.Quick,
                "quickindicator" => AbilityCastPreference.QuickWithIndicator,
                _ => AbilityCastPreference.Normal,
            };
        }

        private new LeagueOfLegendsModuleAttributes ModuleAttributes;

        private LeagueOfLegendsModule() : base(GAME_ID) 
        {
            GameState = new GameState();
            ModuleAttributes = base.ModuleAttributes as LeagueOfLegendsModuleAttributes;
            // League of Legends integration Initialization
            // Init Item Attributes
            ItemUtils.Init();
            ItemCooldownController.Init();

            AddAnimatorEvent();

            PlayLoadingAnimation();
            _ = WaitForGameInitialization();
        }

        // plays it indefinitely
        private void PlayLoadingAnimation()
        {
            Animator.StopCurrentAnimation();
            Animator.FadeBetweenTwoColors(LightZone.All, HSVColor.Black, LoadingColor, 1.5f, 2f);
        }

        private async Task WaitForGameInitialization()
        {
            while (true)
            {
                if (masterCancelToken.IsCancellationRequested)
                    return;
                try
                {
                    if (await WebRequestUtil.IsLive("https://127.0.0.1:2999/liveclientdata/allgamedata"))
                    {
                        break;
                    }
                }
                catch (WebException e)
                {
                    // TODO: Account for League client disconnects, game ended, etc. without crashing the whole program
                    //throw new InvalidOperationException("Couldn't connect with the game client", e);
                }

                await Task.Delay(2000);
                PlayLoadingAnimation();
            }
            await OnGameInitialized();
        }

        private async Task OnGameInitialized() // TODO: Handle summoner spells
        {
            Animator.StopCurrentAnimation(); // stops the current anim

            // Queries the game information
            await QueryPlayerInfo(true);

            // Set trinket initial cooldowns
            double avgChampLevel = GameState.AverageChampionLevel;
            // note: this assumes the program is run BEFORE the game starts, or else it won't be very accurate.
            ItemCooldownController.SetCooldown(OracleLensModule.ITEM_ID, OracleLensModule.GetCooldownDuration(avgChampLevel));
            ItemCooldownController.SetCooldown(FarsightAlterationModule.ITEM_ID, FarsightAlterationModule.GetCooldownDuration(avgChampLevel));

            // Load champion module. Different modules will be loaded depending on the champion.
            // If there is no suitable module for the selected champion, just the health bar will be displayed.

            string champName = GameState.PlayerChampion.RawChampionName;

            Type champType = ChampionControllers.FirstOrDefault(x => champName.Contains(x.GetCustomAttribute<ChampionAttribute>().ChampionName));
            if (champType != null)
            {
                championModule = champType.GetConstructors().First()
                                    .Invoke(new object[] { GameState })
                                    as ChampionModule;
                championModule.NewFrameReady += NewFrameReadyHandler;
                championModule.TriedToCastOutOfMana += OnAbilityCastNoMana;
            }
            CurrentLEDSource = championModule;

            // Initialize endless player info checking check
            UpdatePlayerInfo();

            // start frame timer
            _ = FrameTimer();
        }

        private async void UpdatePlayerInfo()
        {
            while (true)
            {
                if (masterCancelToken.IsCancellationRequested)
                    return;
                await QueryPlayerInfo();
                await Task.Delay(150);
            }
        }

        /// <summary>
        /// Queries updated game data from the LoL live client API.
        /// </summary>
        private async Task QueryPlayerInfo(bool firstTime = false)
        {
            string json;
            try
            {
                json = await WebRequestUtil.GetResponse("https://127.0.0.1:2999/liveclientdata/allgamedata");
            }
            catch (WebException)
            {
                Console.WriteLine("InvalidOperationException: Game client disconnected");
                return;
                //throw new InvalidOperationException("Couldn't connect with the game client", e);
            }

            var gameData = JsonConvert.DeserializeObject<dynamic>(json);
            GameState.GameEvents = (gameData.events.Events as JArray).ToObject<List<Event>>();
            // Get active player info
            GameState.ActivePlayer = ActivePlayer.FromData(gameData.activePlayer);
            // Get player champion info (IsDead, Items, etc)
            GameState.Champions = (gameData.allPlayers as JArray).ToObject<List<Champion>>();
            GameState.PlayerChampion = GameState.Champions.Find(x => x.SummonerName == GameState.ActivePlayer.SummonerName);
            // Update active player based on player champion data
            GameState.ActivePlayer.IsDead = GameState.PlayerChampion.IsDead;
            // Update champion LED module information
            championModule?.UpdateGameState(GameState);
            // Update player ability cooldowns
            GameState.PlayerAbilityCooldowns = championModule?.AbilitiesOnCooldown;
            // Get player items
            //GameState.PlayerItemCooldowns = new bool[7];
            foreach (Item item in GameState.PlayerChampion.Items)
            {
                SetModuleForItem(item);
            }

            // Process game events
            ProcessGameEvents(firstTime);
        }

        private void SetModuleForItem(Item item)
        {
            ItemAttributes attrs = ItemUtils.GetItemAttributes(item.ItemID);
            // decide and create item module accordingly.
            Type itemType = ItemControllers.FirstOrDefault(x => item.ItemID == x.GetCustomAttribute<ItemAttribute>().ItemID);
            if (itemType != null)
            {
                if (ItemModules[item.Slot] == null || !(ItemModules[item.Slot].GetType().IsAssignableFrom(itemType)))
                {
                    ItemModules[item.Slot]?.Dispose();
                    ItemModules[item.Slot] = itemType.GetConstructors().First()
                                        .Invoke(new object[] { GameState, item.Slot, ModuleAttributes.ItemCastPreference })
                                        as ItemModule;
                    ItemModules[item.Slot].RequestActivation += OnItemActivated;
                    ItemModules[item.Slot].NewFrameReady += NewFrameReadyHandler;
                    ItemCooldownController.AssignItemIdToSlot(item.Slot, item.ItemID);
                    if (item.ItemID == WardingTotemModule.ITEM_ID)
                    {
                        WardingTotemModule.Current = ItemModules[item.Slot] as WardingTotemModule; // HACK to make it accessible to HUDModule
                    }
                    // TODO: Show an item buy animation here?
                }
                ItemModules[item.Slot].UpdateGameState(GameState);
            }
            else
            {
                ItemModules[item.Slot]?.Dispose();
                ItemModules[item.Slot] = null;
            }
        }

        /// <summary>
        /// Task that periodically updates the health bar.
        /// </summary>
        private async Task FrameTimer()
        {
            while (true)
            {
                if (masterCancelToken.IsCancellationRequested)
                    return;
                if (lastExternalFrameReceivedTimer == 0)
                {
                    if (!CheckIfDead())
                    {
                        LEDFrame frame = hudModule.DoFrame(GameState);
                        lastHudFrame = frame.Leds;
                        InvokeNewFrameReady(frame);
                    }
                }
                await Task.Delay(30);
                lastExternalFrameReceivedTimer -= 30;
                if (lastExternalFrameReceivedTimer < 0)
                    lastExternalFrameReceivedTimer = 0;
            }
        }

        /// <summary>
        /// Sets a custom champion kill animation. If the player kills a champion within <paramref name="duration"/> ms,
        /// this animation will play instead of the default one. Useful for garen ult or chogath, for example.
        /// </summary>
        /// <param name="animPath">The animation path</param>
        public void SetCustomKillAnim(string animPath, int duration)
        {
            customKillAnimation = animPath;
            Task.Run(async () =>
            {
                await Task.Delay(duration);
                customKillAnimation = null;
            });
        }

        /// <summary>
        /// Called by a <see cref="LEDModule"/> when a new frame is available to be processed.
        /// </summary>
        /// <param name="s">Module that sent the message</param>
        /// <param name="data">LED data</param>
        protected override void NewFrameReadyHandler(LEDFrame frame)
        {
            // TODO: Make sure frame Sender is ChampionModule and NOT animationModule
            if ((frame.LastSender is ChampionModule && CurrentLEDSource is ItemModule item && !item.IsPriorityItem)) // Champion modules take priority over item casts... for the moment
            {
                CurrentLEDSource = (LEDModule)frame.LastSender;
            }
            if (frame.LastSender != Animator && frame.LastSender != CurrentLEDSource)
                return; // If it's from a different source that what we're listening to, ignore it

            if (frame.Zones != LightZone.All) // TODO: Do proper layering
            {
                LEDData hud = lastHudFrame;

                if (!frame.Zones.HasFlag(LightZone.Keyboard))
                    frame.Leds.Keyboard = hud.Keyboard;
                if (!frame.Zones.HasFlag(LightZone.Mouse))
                    frame.Leds.Mouse = hud.Mouse;
                if (!frame.Zones.HasFlag(LightZone.Mousepad))
                    frame.Leds.Mousepad = hud.Mousepad;
                if (!frame.Zones.HasFlag(LightZone.Headset))
                    frame.Leds.Headset = hud.Headset;
                if (!frame.Zones.HasFlag(LightZone.Keypad))
                    frame.Leds.Keypad = hud.Keypad;
                if (!frame.Zones.HasFlag(LightZone.Strip))
                    frame.Leds.Strip = hud.Strip;
                if (!frame.Zones.HasFlag(LightZone.General))
                    frame.Leds.General = hud.General;

                // We merge hud led data with received data
                LEDFrame newFrame = new LEDFrame(this, frame.Leds, LightZone.All, frame.Priority);
                frame = newFrame;
            }

            InvokeNewFrameReady(frame);
            lastExternalFrameReceivedTimer += 30; // add 30ms
        }

        /// <summary>
        /// Processes game events such as kills
        /// </summary>
        private void ProcessGameEvents(bool firstTime = false)
        {
            if (firstTime)
            {
                if (GameState.GameEvents.Count > 0)
                    currentGameTimestamp = GameState.GameEvents.Last().EventTime;

                return;
            }
            foreach (Event ev in GameState.GameEvents)
            {
                if (ev.EventTime <= currentGameTimestamp)
                    continue;
                currentGameTimestamp = ev.EventTime;
                switch (ev.EventName)
                {
                    case "ChampionKill":
                        OnChampionKill(ev);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// If the player is dead, color red, and returns false so any other HUD Modules don't output data (dead = only red lights)
        /// </summary>
        /// <returns></returns>
        private bool CheckIfDead()
        {
            // TODO: Transition from dead to alive a bit slow
            if (GameState.PlayerChampion.IsDead)
            {
                Animator.HoldColor(DeadColor, LightZone.All, 1f, true);
                wasDeadLastFrame = true;
                return true;
            }
            else
            {
                /*if (wasDeadLastFrame) // Not needed, because hold color is only 1 second
                {
                    InvokeNewFrameReady(LEDFrame.CreateEmpty(this));
                    wasDeadLastFrame = false;
                }*/
                return false;
            }
        }

        private void OnChampionKill(Event ev)
        {
            if (ev.KillerName == GameState.ActivePlayer.SummonerName)
            {
                CurrentLEDSource = Animator;
                if (customKillAnimation != null)
                {
                    Animator.RunAnimationOnce(customKillAnimation, LightZone.All, 0.1f);
                    CurrentLEDSource = championModule;
                    customKillAnimation = null;
                }
                else
                {
                    Animator.ColorBurst(KillColor, LightZone.All, 0.01f);
                    CurrentLEDSource = championModule;
                }
            }
        }

        private static List<Type> GetControllers<T>()
            where T : Attribute
        {
            // TODO: This is a generally useful function that uses reflection, must be abstracted elsewhere
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttributes(typeof(T), true).Length > 0).ToList();
        }

        private static List<Type> GetChampionControllers() => GetControllers<ChampionAttribute>();
        private static List<Type> GetItemControllers() => GetControllers<ItemAttribute>();

        private void OnItemActivated(object s, EventArgs e)
        {
            CurrentLEDSource = (LEDModule)s;
        }

        /// <summary>
        /// Called when the player tried to cast an ability but was out of mana.
        /// </summary>
        private void OnAbilityCastNoMana()
        {
            CurrentLEDSource = Animator;
            Animator.ColorBurst(NoManaColor, LightZone.Desk, 0.3f);
            CurrentLEDSource = championModule;
        }

        public override void Dispose()
        {
            masterCancelToken.Cancel();
            Animator.StopCurrentAnimation();
            championModule?.Dispose();
            for (int i = 0; i < ItemModules.Length; i++)
            {
                ItemModules[i]?.Dispose();
                ItemModules[i] = null;
            }
            ItemUtils.Dispose();
            ItemCooldownController.Dispose();
        }
    }
}
