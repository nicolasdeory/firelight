using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using FirelightCore;
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

        public AzirModule(GameState gameState, AbilityCastPreference preferredCastMode)
            : base(CHAMPION_NAME, gameState, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.PointAndClick();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Normal();

        protected override async Task OnCastQ()
        {
            RunAnimationOnce("q_cast", LightZone.Keyboard, timeScale: 1.5f);
        }
        protected override async Task OnCastW()
        {
            RunAnimationOnce("w_cast", LightZone.Keyboard, timeScale: 0.5f);
        }
        protected override async Task OnCastE()
        {
            RunAnimationOnce("e_cast", LightZone.Keyboard, timeScale: 1.6f);
        }
        protected override async Task OnCastR()
        {
            RunAnimationOnce("r_cast", LightZone.Keyboard, timeScale: 0.3f);
        }
    }
}