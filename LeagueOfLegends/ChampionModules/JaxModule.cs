using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    class JaxModule : ChampionModule
    {

        public const string CHAMPION_NAME = "Jax";
        // Variables

        // Champion-specific Variables

        static HSVColor RColor = new HSVColor(0.17f, 0.83f, 0.93f);
        bool castingE;
        bool canRecastE;


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static JaxModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new JaxModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private JaxModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(ledCount, championName, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.PointAndClick();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Instant(3000);
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant();

        protected override async Task OnCastQ()
        {
            await RunAnimationOnce("q_cast", timeScale: 1.7f);
        }
        protected override async Task OnCastW()
        {
            RunAnimationInLoop("w_cast_loop", 500);
            await Task.Delay(500);
            RunAnimationOnce("w_cast_end");
        }
        protected override async Task OnCastE()
        {
            castingE = true;
            RunAnimationInLoop("e_cast_loop", 2000, 0.15f);
            await Task.Delay(1000);
            canRecastE = true;
            await Task.Delay(1000);
            if (castingE)
            {
                RunAnimationOnce("e_recast_end");
                castingE = false;
                canRecastE = false;
            }
        }
        protected override async Task OnCastR()
        {
            Animator.ColorBurst(RColor, 0.1f);
        }

        protected override async Task OnRecastE()
        {
            if (canRecastE)
            {
                castingE = false;
                await Task.Delay(200);
                RunAnimationOnce("e_recast_end");
            }
        }
    }
}
