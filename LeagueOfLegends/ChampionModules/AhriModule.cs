using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    class AhriModule : ChampionModule
    {

        // Change to whatever champion you want to implement
        public const string CHAMPION_NAME = "Ahri";

        // Variables

        // Champion-specific Variables

        int rCastInProgress = 0;

        public AhriModule(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, CHAMPION_NAME, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant(10000, 2);

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
