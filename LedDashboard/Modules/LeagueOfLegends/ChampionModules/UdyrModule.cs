using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.Common;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using SharpDX.RawInput;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard.Modules.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    class UdyrModule : ChampionModule
    {

        public const string CHAMPION_NAME = "Udyr";
        // Variables

        // Champion-specific Variables

        static HSVColor QColor = new HSVColor(0.09f, 1, 1);
        static HSVColor WColor = new HSVColor(0.24f, 1, 0.74f);
        static HSVColor EColor = new HSVColor(0.08f, 1, 0.64f);
        static HSVColor RColor = new HSVColor(0.54f, 1, 1);


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static UdyrModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new UdyrModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private UdyrModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, championName, gameState, preferredLightMode, preferredCastMode)
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
                [AbilityKey.Q] = AbilityCastMode.Instant(),
                [AbilityKey.W] = AbilityCastMode.Instant(),
                [AbilityKey.E] = AbilityCastMode.Instant(),
                [AbilityKey.R] = AbilityCastMode.Instant(),
            };
            AbilityCastModes = abilityCastModes;

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

        }

        protected override async Task OnCastQ()
        {
            animator.ColorBurst(QColor);
        }
        protected override async Task OnCastW()
        {
            animator.ColorBurst(WColor);
        }
        protected override async Task OnCastE()
        {
            animator.ColorBurst(EColor);
        }
        protected override async Task OnCastR()
        {
            animator.ColorBurst(RColor);
        }
    }
}
