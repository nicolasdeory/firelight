using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.Common;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using SharpDX.RawInput;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard.Modules.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    class XerathModule : ChampionModule
    {

        public const string CHAMPION_NAME = "Xerath";
        // Variables

        // Champion-specific Variables
        int rTimeRemaining = 0;
        int chargesRemaining = 0;
        bool castingQ;

        HSVColor BlueExplodeColor = new HSVColor(0.59f, 1, 1);

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static XerathModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new XerathModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private XerathModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(ledCount, championName, gameState, preferredLightMode, true)
        {
            // Initialization for the champion module occurs here.

            // Set preferred cast mode. It's a player choice (Quick cast, Quick cast with indicator, or Normal cast)
            PreferredCastMode = preferredCastMode;

            // Set cast modes for abilities.
            // For Vel'Koz, for example:
            // Q -> Normal ability, but it can be recast within 1.15s
            // W -> Normal ability
            // E -> Normal ability
            // R -> Instant ability, it is cast the moment the key is pressed, but it can be recast within 2.3s
            Dictionary<AbilityKey, AbilityCastMode> abilityCastModes = new Dictionary<AbilityKey, AbilityCastMode>()
            {
                [AbilityKey.Q] = AbilityCastMode.Instant(3500, 1, AbilityCastMode.KeyUpRecast()),
                [AbilityKey.W] = AbilityCastMode.Normal(),
                [AbilityKey.E] = AbilityCastMode.Normal(),
                [AbilityKey.R] = AbilityCastMode.Instant(10000,3,AbilityCastMode.Instant()), // TODO: Handle levels! Recast number changes
            };
            AbilityCastModes = abilityCastModes;

            ChampionInfoLoaded += OnChampionInfoLoaded;
            GameStateUpdated += OnGameStateUpdated;
        }

        /// <summary>
        /// Called when the champion info has been retrieved.
        /// </summary>
        private void OnChampionInfoLoaded(ChampionAttributes champInfo)
        {
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls, mode);
            AbilityCast += OnAbilityCast;
            AbilityRecast += OnAbilityRecast;
            KeyboardHookService.Instance.OnMouseClicked += MouseClicked;
        }

        private void MouseClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chargesRemaining > 0)
            {
                if (chargesRemaining == GameState.ActivePlayer.Abilities.R_Level + 2)
                {
                    StartCooldownTimer(AbilityKey.R, GetCooldownForAbility(AbilityKey.R) / 2);
                } else
                {
                    StartCooldownTimer(AbilityKey.R, GetCooldownForAbility(AbilityKey.R));
                }
                CancelRecast(AbilityKey.R);
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "Xerath/q_retract.txt", timeScale: 0.6f);
                chargesRemaining = 0;
            }
        }

        /// <summary>
        /// Called when the game state is updated
        /// </summary>
        private void OnGameStateUpdated(GameState state)
        {
            int casts = 3;
            switch (state.ActivePlayer.Abilities.R_Level)
            {
                case 1:
                    casts = 3;
                    break;
                case 2:
                    casts = 4;
                    break;
                case 3:
                    casts = 5;
                    break;
            }
            AbilityCastModes[AbilityKey.R].MaxRecasts = casts;
        }

        /// <summary>
        /// Called when an ability is cast.
        /// </summary>
        private void OnAbilityCast(object s, AbilityKey key)
        {
            if (key == AbilityKey.Q)
            {
                OnCastQ();
            }
            if (key == AbilityKey.W)
            {
                OnCastW();
            }
            if (key == AbilityKey.E)
            {
                OnCastE();
            }
            if (key == AbilityKey.R)
            {
                OnCastR();
            }
        }

        private void OnCastQ()
        {
            Task.Run(async () =>
            {
                castingQ = true;
                await animator.RunAnimationOnce(ANIMATION_PATH + "Xerath/q_charge.txt", true, timeScale: 0.3f);
                if (castingQ) // we need to check again after playing last anim
                    await animator.HoldColor(BlueExplodeColor, 1500);
                if (castingQ)
                {
                    _ = animator.RunAnimationOnce(ANIMATION_PATH + "Xerath/q_retract.txt", timeScale: 0.6f);
                    castingQ = false;
                }
            });
        }

        private void OnCastW()
        {
            Task.Run(async () =>
            {
                await Task.Delay(100);
                animator.RunAnimationInLoop(ANIMATION_PATH + "Xerath/w_cast.txt", 1000, timeScale: 1f);
                await Task.Delay(1000);
                _ = animator.ColorBurst(HSVColor.White);
            });
        }

        private void OnCastE()
        {
            Task.Run(async () =>
            {
                await Task.Delay(100);
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "Xerath/e_cast.txt", timeScale: 0.5f);
            });
        }

        private void OnCastR()
        {
            Task.Run(async () =>
            {
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "Xerath/r_open.txt", true, timeScale: 0.23f);
                StartRTimer();
                await Task.Delay(500);
                if (chargesRemaining == 3) _ = animator.HoldColor(HSVColor.White, 10000);
                
            });
        }

        private void StartRTimer()
        {
            chargesRemaining = GameState.ActivePlayer.Abilities.R_Level + 2;
            Task.Run(async () =>
            {
                rTimeRemaining = 10000;
                while (rTimeRemaining >= 0)
                {
                    await Task.Delay(100);
                    rTimeRemaining -= 100;
                }
                
                if (chargesRemaining > 0)
                {
                    chargesRemaining = 0;
                    animator.StopCurrentAnimation();
                }
            });
        }


        /// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        private void OnAbilityRecast(object s, AbilityKey key)
        {
            if (key == AbilityKey.Q)
            {
                Task.Run(async () =>
                {
                    castingQ = false;
                    await animator.RunAnimationOnce(ANIMATION_PATH + "Xerath/q_retract.txt", timeScale: 0.6f);
                    await Task.Delay(300);
                    _ = animator.ColorBurst(BlueExplodeColor);
                });

            }
            else if (key == AbilityKey.R)
            {
                if (chargesRemaining == 0) return;
                chargesRemaining--;
                Task.Run(async () =>
                {
                    _ = animator.RunAnimationOnce(ANIMATION_PATH + "Xerath/r_launch.txt", true, timeScale: 0.4f);
                    await Task.Delay(700);
                    if (chargesRemaining > 0)
                    {
                        int previousCharges = chargesRemaining;
                        await animator.ColorBurst(BlueExplodeColor, 0.15f, HSVColor.White);
                        if (previousCharges == chargesRemaining) _ = animator.HoldColor(HSVColor.White, rTimeRemaining);
                    } else if (chargesRemaining == 0)
                    {
                        await animator.ColorBurst(BlueExplodeColor, 0.10f);
                    }
                    
                });
                
            }
        }

        // udy

    }
}
