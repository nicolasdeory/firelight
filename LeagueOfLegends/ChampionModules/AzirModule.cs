using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class AzirModule : ChampionModule
    {
        // Change to whatever champion you want to implement
        public const string CHAMPION_NAME = "Azir";

        // Variables

        // Champion-specific Variables

        public AzirModule(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, CHAMPION_NAME, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.PointAndClick();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Normal();

        protected override async Task OnCastQ()
        {
            RunAnimationOnce("q_cast", timeScale: 1.5f);
        }
        protected override async Task OnCastW()
        {
            RunAnimationOnce("w_cast", timeScale: 0.5f);
        }
        protected override async Task OnCastE()
        {
            RunAnimationOnce("e_cast", timeScale: 1.6f);
        }
        protected override async Task OnCastR()
        {
            RunAnimationOnce("r_cast", timeScale: 0.3f);
        }
    }
}