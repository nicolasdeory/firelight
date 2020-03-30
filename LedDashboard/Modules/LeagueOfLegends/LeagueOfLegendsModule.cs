using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules;
using LedDashboard.Modules.LeagueOfLegends.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
{
    class LeagueOfLegendsModule : LEDModule
    {

        // Constants

        HSVColor LoadingColor = new HSVColor(0.09f, 0.8f, 1f);

        HSVColor HealthColor = new HSVColor(0.29f, 0.79f, 1f);
        HSVColor HurtColor = new HSVColor(0.09f, 0.8f, 1f);
        HSVColor DeadColor = new HSVColor(0f, 0.8f, 0.77f);
        HSVColor NoManaColor = new HSVColor(0.52f, 0.66f, 1f);

        HSVColor KillColor = new HSVColor(0.06f, 0.96f, 1f);

        // Variables

        Led[] leds;

        ActivePlayer activePlayer;
        List<Champion> champions;
        Champion playerChampion;
        List<Event> gameEvents;

        ChampionModule championModule;
        AnimationModule animationModule;

        ulong msSinceLastExternalFrameReceived = 30000;
        ulong msAnimationTimerThreshold = 1500; // how long to wait for animation data until health bar kicks back in.
        double currentGameTimestamp = 0;

        //CancellationTokenSource loadingAnimToken = new CancellationTokenSource();
        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

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
        public static LeagueOfLegendsModule Create(LightingMode preferredMode, int ledCount = 0)
        {
            if(preferredMode == LightingMode.Keyboard)
            {
                return new LeagueOfLegendsModule(88, preferredMode);
            } else
            {
                return new LeagueOfLegendsModule(ledCount, preferredMode);
            }
        }

        /// <summary>
        /// The current module that is sending information to the LED strip.
        /// </summary>
        LEDModule CurrentLEDSource;

        private LeagueOfLegendsModule(int ledCount, LightingMode mode)
        {

            // League of Legends integration Initialization

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

        private void WaitForGameInitialization()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (masterCancelToken.IsCancellationRequested) return;
                    try
                    {
                        if (await WebRequestUtil.IsLive("https://127.0.0.1:2999/liveclientdata/allgamedata"))
                        {
                            break;
                        } else
                        {
                            await Task.Delay(1000);
                            continue;
                        }
                    }
                    catch (WebException e)
                    {
                        // TODO: Account for League client disconnects, game ended, etc. without crashing the whole program
                        //throw new InvalidOperationException("Couldn't connect with the game client", e);
                        await Task.Delay(1000);
                        continue;
                    }

                }
                await OnGameInitialized();
            });

        }

        private async Task OnGameInitialized()
        {
            animationModule.StopCurrentAnimation(); // stops the current anim

            // Queries the game information
            await QueryPlayerInfo(true);

            // Load champion module. Different modules will be loaded depending on the champion.
            // If there is no suitable module for the selected champion, just the health bar will be displayed.

            // TODO: Make this easily extendable when there are many champion modules
            if (playerChampion.RawChampionName.ToLower().Contains("velkoz"))
            {
                championModule = VelKozModule.Create(this.leds.Length, activePlayer, this.lightingMode);
                championModule.NewFrameReady += OnNewFrameReceived;
                championModule.TriedToCastOutOfMana += OnAbilityCastNoMana;
            }
            CurrentLEDSource = championModule;

            // Sets up a task to always check for updated player info
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    if (masterCancelToken.IsCancellationRequested) return;
                    await QueryPlayerInfo();
                    await Task.Delay(150);
                }
            });

            // start frame timer
            _ = Task.Run(FrameTimer);

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
                // TODO: Account for League client disconnects, game ended, etc. without crashing the whole program
                throw new InvalidOperationException("Couldn't connect with the game client", e); 
            }

            var gameData = JsonConvert.DeserializeObject<dynamic>(json);
            gameEvents = (gameData.events.Events as JArray).ToObject<List<Event>>();
            // Get active player info
            activePlayer = ActivePlayer.FromData(gameData.activePlayer);
            // Get player champion info (IsDead, Items, etc)
            champions = (gameData.allPlayers as JArray).ToObject<List<Champion>>();
            playerChampion = champions.Find(x => x.SummonerName == activePlayer.SummonerName);
            // Update active player based on player champion data
            activePlayer.IsDead = playerChampion.IsDead;
            // Update champion LED module information
            if (championModule != null) championModule.UpdatePlayerInfo(activePlayer);
            // Process game events
            ProcessGameEvents(firstTime);
            
        }

        /// <summary>
        /// Task that periodically updates the health bar.
        /// </summary>
        private async Task FrameTimer()
        {
            while(true)
            {
                if (masterCancelToken.IsCancellationRequested) return;
                if (msSinceLastExternalFrameReceived >= msAnimationTimerThreshold)
                {
                    UpdateHealthBar();
                }
                await Task.Delay(30);
                msSinceLastExternalFrameReceived += 30;
            }
        }

        /// <summary>
        /// Called by a <see cref="LEDModule"/> when a new frame is available to be processed.
        /// </summary>
        /// <param name="s">Module that sent the message</param>
        /// <param name="data">LED data</param>
        private void OnNewFrameReceived(object s, Led[] data, LightingMode mode)
        {
            if (s != CurrentLEDSource) return; // If it's from a different source that what we're listening too, ignore it
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
                if (gameEvents.Count > 0)
                    currentGameTimestamp = gameEvents[gameEvents.Count - 1].EventTime;
                else
                    currentGameTimestamp = 0;

                return;
            }
            foreach(Event ev in gameEvents)
            {
                if (ev.EventTime <= currentGameTimestamp) continue;
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

        private void OnChampionKill(Event ev)
        {
            if(ev.KillerName == activePlayer.SummonerName)
            {
                CurrentLEDSource = animationModule;
                animationModule.ColorBurst(KillColor, 0.01f, HealthColor).ContinueWith((t) =>
                {
                    CurrentLEDSource = championModule;
                });
            }
        }

        bool wasDeadLastFrame = false;
        /// <summary>
        /// Updates the health bar.
        /// </summary>
        private void UpdateHealthBar()
        {
            if (playerChampion.IsDead)
            {
                for (int i = 0; i < leds.Length; i++)
                {
                    this.leds[i].Color(DeadColor);
                }
                wasDeadLastFrame = true;
            } else
            {
                if (wasDeadLastFrame)
                {
                    this.leds.SetAllToBlack();
                    wasDeadLastFrame = false;
                }
                float maxHealth = activePlayer.Stats.MaxHealth;
                float currentHealth = activePlayer.Stats.CurrentHealth;
                float healthPercentage = currentHealth / maxHealth;
                if (lightingMode == LightingMode.Keyboard)
                {
                    // row 1
                    int ledsToTurnOn = (int)Utils.Scale(healthPercentage, 0, 1, 0, 13);
                    for (int i = 0; i < 13; i++)
                    {
                        if (i < ledsToTurnOn)
                            this.leds[i].MixNewColor(HealthColor, true, 0.2f);
                        else
                        {
                            if (this.leds[i].color.AlmostEqual(HealthColor))
                            {
                                this.leds[i].Color(HurtColor);
                            }
                            else
                            {
                                this.leds[i].FadeToBlackBy(0.05f);
                            }
                        }

                    }
                    // row2
                    ledsToTurnOn = (int)Utils.Scale(healthPercentage, 0, 1, 0, 14);
                    for (int i = 16; i < 30; i++)
                    {
                        if (i-16 < ledsToTurnOn)
                            this.leds[i].MixNewColor(HealthColor, true, 0.2f);
                        else
                        {
                            if (this.leds[i].color.AlmostEqual(HealthColor))
                            {
                                this.leds[i].Color(HurtColor);
                            }
                            else
                            {
                                this.leds[i].FadeToBlackBy(0.05f);
                            }
                        }
                    }
                    NewFrameReady?.Invoke(this, this.leds, LightingMode.Keyboard);
                } else
                {
                    int ledsToTurnOn = (int)(healthPercentage * leds.Length);
                    for (int i = 0; i < leds.Length; i++)
                    {
                        if (i < ledsToTurnOn)
                            this.leds[i].MixNewColor(HealthColor, true, 0.2f);
                        else
                        {
                            if (this.leds[i].color.AlmostEqual(HealthColor))
                            {
                                this.leds[i].Color(HurtColor);
                            }
                            else
                            {
                                this.leds[i].FadeToBlackBy(0.05f);
                            }
                        }

                    }
                    NewFrameReady?.Invoke(this, this.leds, LightingMode.Line);
                }
                
            }
            
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
        }
    }
}
