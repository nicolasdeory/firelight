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
    class EzrealModule : ChampionModule
    {

        public const string CHAMPION_NAME = "Ezreal";
        // Variables

        // Champion-specific Variables


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static EzrealModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new EzrealModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private EzrealModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, championName, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Normal();

        protected override async Task OnCastQ()
        {
            await Task.Delay(150);
            await RunAnimationOnce("q_cast", timeScale: 0.8f);
        }
        protected override async Task OnCastW()
        {
            await Task.Delay(150);
            await RunAnimationOnce("w_cast");
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(250);
            await RunAnimationOnce("e_cast", false, 0.15f);
        }
        protected override async Task OnCastR()
        {
            await RunAnimationOnce("r_channel", true);
            await Task.Delay(700);
            await RunAnimationOnce("r_launch", timeScale: 0.7f);
        }
    }
}
