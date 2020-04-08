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

        public int MaxRCasts => GameState.ActivePlayer.AbilityLoadout.R_Level + 2;

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
            : base(ledCount, championName, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.

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

            GameStateUpdated += OnGameStateUpdated;
        }

        /// <summary>
        /// Called when the champion info has been retrieved.
        /// </summary>
        protected override void OnChampionInfoLoaded(ChampionAttributes champInfo)
        {
            base.OnChampionInfoLoaded(champInfo);
            KeyboardHookService.Instance.OnMouseClicked += MouseClicked;
        }

        private void MouseClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chargesRemaining > 0)
            {
                int finalCooldown = GetCooldownForAbility(AbilityKey.R);
                if (chargesRemaining == MaxRCasts)
                {
                    finalCooldown /= 2;
                }
                StartCooldownTimer(AbilityKey.R, finalCooldown);
                CancelRecast(AbilityKey.R);
                RunAnimationOnce("q_retract", timeScale: 0.6f);
                chargesRemaining = 0;
            }
        }

        /// <summary>
        /// Called when the game state is updated
        /// </summary>
        private void OnGameStateUpdated(GameState state)
        {
            AbilityCastModes[AbilityKey.R].MaxRecasts = MaxRCasts;
        }

        protected override async Task OnCastQ()
        {
            castingQ = true;
            await RunAnimationOnce("q_charge", true, timeScale: 0.3f);
            if (castingQ) // we need to check again after playing last anim
                await animator.HoldColor(BlueExplodeColor, 1500);
            if (castingQ)
            {
                RunAnimationOnce("q_retract", timeScale: 0.6f);
                castingQ = false;
            }
        }
        protected override async Task OnCastW()
        {
            await Task.Delay(100);
            RunAnimationInLoop("w_cast", 1000, timeScale: 1f);
            await Task.Delay(1000);
            animator.ColorBurst(HSVColor.White);
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(100);
            RunAnimationOnce("e_cast", timeScale: 0.5f);
        }
        protected override async Task OnCastR()
        {
            RunAnimationOnce("r_open", true, timeScale: 0.23f);
            StartRTimer();
            await Task.Delay(500);
            if (chargesRemaining == 3)
            {
                animator.HoldColor(HSVColor.White, 10000);
            }
        }

        private async Task StartRTimer()
        {
            chargesRemaining = MaxRCasts;

            await Task.Delay(10000);
                
            if (chargesRemaining > 0)
            {
                animator.StopCurrentAnimation();
            }
            chargesRemaining = 0;
        }

        protected override async Task OnRecastQ()
        {
            castingQ = false;
            await RunAnimationOnce("q_retract", timeScale: 0.6f);
            await Task.Delay(300);
            animator.ColorBurst(BlueExplodeColor);
        }
        protected override async Task OnRecastR()
        {
            if (chargesRemaining == 0)
            {
                return;
            }

            chargesRemaining--;
            RunAnimationOnce("r_launch", true, timeScale: 0.4f);
            await Task.Delay(700);

            if (chargesRemaining > 0)
            {
                int previousCharges = chargesRemaining;
                await animator.ColorBurst(BlueExplodeColor, 0.15f, HSVColor.White);
                if (previousCharges == chargesRemaining)
                {
                    animator.HoldColor(HSVColor.White, rTimeRemaining);
                }
            }
            else if (chargesRemaining == 0)
            {
                await animator.ColorBurst(BlueExplodeColor, 0.10f);
            }
        }
    }
}
