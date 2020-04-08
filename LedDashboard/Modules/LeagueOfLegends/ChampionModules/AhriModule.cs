using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.Common;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using SharpDX.RawInput;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard.Modules.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    class AhriModule : ChampionModule
    {

        // Change to whatever champion you want to implement
        public const string CHAMPION_NAME = "Ahri";

        // Variables

        // Champion-specific Variables

        int rCastInProgress = 0;


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static AhriModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new AhriModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }

        private AhriModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
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
                [AbilityKey.Q] = AbilityCastMode.Normal(),
                [AbilityKey.W] = AbilityCastMode.Instant(),
                [AbilityKey.E] = AbilityCastMode.Normal(),
                [AbilityKey.R] = AbilityCastMode.Instant(10000,2),
            };
            AbilityCastModes = abilityCastModes;
        }

        protected override async Task OnCastQ()
        {
            RunAnimationOnce("q_start", true);
            await Task.Delay(1000);
            RunAnimationOnce("q_end", true);
        }
        protected override async Task OnCastW()
        {
            RunAnimationOnce("w_cast", false, 0.08f);
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(100);
            RunAnimationOnce("e_cast");
        }
        protected override async Task OnCastR()
        {
            // Trigger the start animation.

            RunAnimationOnce("r_right");

            // The R cast is in progress.
            rCastInProgress = 1;

            // TODO: Validate that this does not break
            await Task.Delay(7000); // if after 7s no recast, effect disappears
            rCastInProgress = 0;
        }

        protected override async Task OnRecastR()
        {
            await ProcessRCasts();
            rCastInProgress++;
            rCastInProgress %= 3;
        }

        private Task ProcessRCasts()
        {
            return rCastInProgress switch
            {
                1 => RunAnimationOnce("r_left"),
                2 => RunAnimationOnce("r_right"),
                _ => Task.FromResult(false),
            };
        }
    }
}
