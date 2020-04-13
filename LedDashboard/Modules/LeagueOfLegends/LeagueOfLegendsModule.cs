using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.ItemModules;
using LedDashboard.Modules.LeagueOfLegends.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
{
    // TODO: Refactor this into a separate project
    public class LeagueOfLegendsModule : LEDModule
    {
        private static List<Type> ChampionControllers = GetChampionControllers();
        private static List<Type> ItemControllers = GetItemControllers();

        // Static members

        public static GameState CurrentGameState { get; set; }

        // Constants

        HSVColor LoadingColor = new HSVColor(0.09f, 0.8f, 1f);

        HSVColor DeadColor = new HSVColor(0f, 0.8f, 0.77f);
        HSVColor NoManaColor = new HSVColor(0.52f, 0.66f, 1f);

        HSVColor KillColor = new HSVColor(0.06f, 0.96f, 1f);

        // Variables

        Led[] leds;

        AbilityCastPreference preferredCastMode;

        GameState gameState = new GameState();

        ChampionModule championModule;
        AnimationModule animationModule;

        ItemModule[] ItemModules = new ItemModule[7];

        ulong msSinceLastExternalFrameReceived = 30000;
        ulong msAnimationTimerThreshold = 1500; // how long to wait for animation data until health bar kicks back in.
        double currentGameTimestamp = 0;

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        string customKillAnimation = null; // can be changed by champion module
        bool wasDeadLastFrame = false;

        // Events

        public event LEDModule.FrameReadyHandler NewFrameReady;

        /// <summary>
        /// The preferred lighting mode (when possible, use this one) For example, if keyboard is preferred, 
        /// use animations optimized for keyboards rather than for LED strips.
        /// </summary>
        LightingMode lightingMode;

        /// <summary>
        /// Creates a new <see cref="LeagueOfLegendsModule"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        public static LeagueOfLegendsModule Create(LightingMode preferLightMode, int ledCount, Dictionary<string, string> options)
        {
            AbilityCastPreference castMode = AbilityCastPreference.Normal;
            if (options.ContainsKey("castMode"))
            {
                castMode = GetCastPreference(options["castMode"]);
            }
            if (preferLightMode == LightingMode.Keyboard)
            {
                ledCount = 88;
            }
            return new LeagueOfLegendsModule(ledCount, preferLightMode, castMode);
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

        /// <summary>
        /// The current module that is sending information to the LED strip.
        /// </summary>
        LEDModule CurrentLEDSource;

        private LeagueOfLegendsModule(int ledCount, LightingMode mode, AbilityCastPreference castMode)
        {
            // League of Legends integration Initialization

            // Init Item Attributes
            ItemUtils.Init();
            ItemCooldownController.Init();

            this.preferredCastMode = castMode;

            // LED Initialization
            lightingMode = mode;
            this.leds = new Led[ledCount];
            for (int i = 0; i < ledCount; i++)
                leds[i] = new Led();

            // Load animation module
            animationModule = AnimationModule.Create(this.leds.Length);
            animationModule.NewFrameReady += OnNewFrameReceived;
            CurrentLEDSource = animationModule;

            PlayLoadingAnimation();
            WaitForGameInitialization();
        }

        // plays it indefinitely
        private void PlayLoadingAnimation()
        {
            animationModule.AlternateBetweenTwoColors(HSVColor.Black, LoadingColor, -1, 1.5f);
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

                await Task.Delay(1000);
            }
            await OnGameInitialized();
        }

        private async Task OnGameInitialized() // TODO: Handle summoner spells
        {
            animationModule.StopCurrentAnimation(); // stops the current anim

            // Queries the game information
            await QueryPlayerInfo(true);

            // Set trinket initial cooldowns
            double avgChampLevel = gameState.AverageChampionLevel;
            // note: this assumes the program is run BEFORE the game starts, or else it won't be very accurate.
            ItemCooldownController.SetCooldown(OracleLensModule.ITEM_ID, OracleLensModule.GetCooldownDuration(avgChampLevel)); 
            ItemCooldownController.SetCooldown(FarsightAlterationModule.ITEM_ID, FarsightAlterationModule.GetCooldownDuration(avgChampLevel));

            // Load champion module. Different modules will be loaded depending on the champion.
            // If there is no suitable module for the selected champion, just the health bar will be displayed.

            string champName = gameState.PlayerChampion.RawChampionName.ToLower();

            Type champType = ChampionControllers.FirstOrDefault(
                                x => champName.ToLower()
                                .Contains((x.GetCustomAttribute(typeof(ChampionAttribute)) as ChampionAttribute)
                                             .ChampionName.ToLower())); // find champion module
            if (champType != null)
            {
                championModule = champType.GetMethod("Create")
                                    .Invoke(null, new object[] { this.leds.Length, this.gameState, this.lightingMode, this.preferredCastMode }) 
                                    as ChampionModule;
                championModule.NewFrameReady += OnNewFrameReceived;
                championModule.TriedToCastOutOfMana += OnAbilityCastNoMana;
            }
            CurrentLEDSource = championModule;

            // Initialize endless player info checking check
            UpdatePlayerInfo();

            // start frame timer
            FrameTimer();
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
            catch (WebException e)
            {
                Console.WriteLine("InvalidOperationException: Game client disconnected");
                throw new InvalidOperationException("Couldn't connect with the game client", e);
            }
            
            var gameData = JsonConvert.DeserializeObject<dynamic>(json);
            gameState.GameEvents = (gameData.events.Events as JArray).ToObject<List<Event>>();
            // Get active player info
            gameState.ActivePlayer = ActivePlayer.FromData(gameData.activePlayer);
            // Get player champion info (IsDead, Items, etc)
            gameState.Champions = (gameData.allPlayers as JArray).ToObject<List<Champion>>();
            gameState.PlayerChampion = gameState.Champions.Find(x => x.SummonerName == gameState.ActivePlayer.SummonerName);
            // Update active player based on player champion data
            gameState.ActivePlayer.IsDead = gameState.PlayerChampion.IsDead;
            // Update champion LED module information
            championModule?.UpdateGameState(gameState);
            // Update player ability cooldowns
            gameState.PlayerAbilityCooldowns = championModule?.AbilitiesOnCooldown;
            // Set current game state
            CurrentGameState = gameState; // This call is possibly not needed because the reference is always the same
            // Get player items

            foreach (Item item in gameState.PlayerChampion.Items)
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
            Type itemType = ItemControllers.FirstOrDefault(
                            x => item.ItemID == (x.GetCustomAttribute(typeof(ItemAttribute)) as ItemAttribute).ItemID);
            if (itemType != null)
            {
                if (ItemModules[item.Slot] == null || !(ItemModules[item.Slot].GetType().IsAssignableFrom(itemType)))
                {
                    ItemModules[item.Slot]?.Dispose();
                    ItemModules[item.Slot] = itemType.GetMethod("Create")
                                        .Invoke(null, new object[] { this.leds.Length, this.gameState, item.Slot, this.lightingMode, this.preferredCastMode })
                                        as ItemModule;
                    ItemModules[item.Slot].RequestActivation += OnItemActivated;
                    ItemModules[item.Slot].NewFrameReady += OnNewFrameReceived;
                    ItemCooldownController.AssignItemIdToSlot(item.Slot, item.ItemID);
                    if (item.ItemID == WardingTotemModule.ITEM_ID)
                    {
                        WardingTotemModule.Current = ItemModules[item.Slot] as WardingTotemModule; // HACK to make it accessible to HUDModule
                    }
                    // TODO: Show an item buy animation here?
                }
                ItemModules[item.Slot].UpdateGameState(gameState);
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
                if (msSinceLastExternalFrameReceived >= msAnimationTimerThreshold)
                {
                    if (!CheckIfDead())
                    {
                        HUDModule.DoFrame(this.leds, this.lightingMode, this.gameState);
                        NewFrameReady?.Invoke(this, this.leds, this.lightingMode);
                    }
                }
                await Task.Delay(30);
                msSinceLastExternalFrameReceived += 30;
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
        private void OnNewFrameReceived(object s, Led[] data, LightingMode mode)
        {
            if ((s is ChampionModule && CurrentLEDSource is ItemModule item && !item.IsPriorityItem)) // Champion modules take priority over item casts... for the moment
            {
                CurrentLEDSource = (LEDModule)s;
            }
            if (s != CurrentLEDSource)
                return; // If it's from a different source that what we're listening to, ignore it
            NewFrameReady?.Invoke(this, data, mode);
            msSinceLastExternalFrameReceived = 0;
        }

        /// <summary>
        /// Processes game events such as kills
        /// </summary>
        private void ProcessGameEvents(bool firstTime = false)
        {
            if (firstTime)
            {
                if (gameState.GameEvents.Count > 0)
                    currentGameTimestamp = gameState.GameEvents.Last().EventTime;

                return;
            }
            foreach (Event ev in gameState.GameEvents)
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
            if (gameState.PlayerChampion.IsDead)
            {
                for (int i = 0; i < leds.Length; i++)
                {
                    leds[i].Color(DeadColor);
                }
                wasDeadLastFrame = true;
                NewFrameReady?.Invoke(this, this.leds, LightingMode.Line);
                return true;
            }
            else
            {
                if (wasDeadLastFrame)
                {
                    leds.SetAllToBlack();
                    wasDeadLastFrame = false;
                }
                return false;
            }
        }

        private void OnChampionKill(Event ev)
        {
            if (ev.KillerName == gameState.ActivePlayer.SummonerName)
            {
                CurrentLEDSource = animationModule;
                if (customKillAnimation != null)
                {
                    animationModule.RunAnimationOnce(customKillAnimation, false, 0.1f).ContinueWith((t) =>
                    {
                        CurrentLEDSource = championModule;
                        customKillAnimation = null;
                    });
                }
                else
                {
                    animationModule.ColorBurst(KillColor, 0.01f).ContinueWith((t) =>
                    {
                        CurrentLEDSource = championModule;
                    });
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
            CurrentLEDSource = animationModule;
            animationModule.ColorBurst(NoManaColor, 0.3f).ContinueWith((t) =>
            {
                CurrentLEDSource = championModule;
            });
        }

        public void Dispose()
        {
            masterCancelToken.Cancel();
            animationModule.StopCurrentAnimation();
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
