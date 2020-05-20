using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class YasuoModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Yasuo";
        // Variables

        // Champion-specific Variables

        static HSVColor RColor = new HSVColor(0.17f, 0.83f, 0.93f);

        public YasuoModule(GameState gameState, AbilityCastPreference preferredCastMode)
            : base(CHAMPION_NAME, gameState, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.PointAndClick();

        protected override async Task OnCastQ()
        {
            RunAnimationOnce("q_cast", LightZone.Keyboard, timeScale: 0.09f);
        }
        protected override async Task OnCastW()
        {
            RunAnimationOnce("w_cast", LightZone.Keyboard, 0.5f);
        }
        protected override async Task OnCastE()
        {
            RunAnimationOnce("e_cast", LightZone.Keyboard, 0.02f);
        }
        protected override async Task OnCastR()
        {
            RunAnimationOnce("r_cast", LightZone.Keyboard, 0.5f);
        }

    }
}
