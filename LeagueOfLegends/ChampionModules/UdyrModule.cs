using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class UdyrModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Udyr";
        // Variables

        // Champion-specific Variables

        static HSVColor QColor = new HSVColor(0.09f, 1, 1);
        static HSVColor WColor = new HSVColor(0.24f, 1, 0.74f);
        static HSVColor EColor = new HSVColor(0.08f, 1, 0.64f);
        static HSVColor RColor = new HSVColor(0.54f, 1, 1);

        public UdyrModule(GameState gameState, AbilityCastPreference preferredCastMode)
            : base(CHAMPION_NAME, gameState, preferredCastMode)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant();

        protected override async Task OnCastQ()
        {
            Animator.ColorBurst(QColor, LightZone.Desk);
        }
        protected override async Task OnCastW()
        {
            Animator.ColorBurst(WColor, LightZone.Desk);
        }
        protected override async Task OnCastE()
        {
            Animator.ColorBurst(EColor, LightZone.Desk);
        }
        protected override async Task OnCastR()
        {
            Animator.ColorBurst(RColor, LightZone.Desk);
        }
    }
}
