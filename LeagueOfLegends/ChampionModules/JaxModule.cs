using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class JaxModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Jax";
        // Variables

        // Champion-specific Variables

        static HSVColor RColor = new HSVColor(0.17f, 0.83f, 0.93f);
        bool castingE;
        bool canRecastE;

        public JaxModule(GameState gameState, AbilityCastPreference preferredCastMode)
            : base(CHAMPION_NAME, gameState, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.PointAndClick();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Instant(3000);
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant();

        protected override async Task OnCastQ()
        {
            RunAnimationOnce("q_cast", LightZone.Desk, timeScale: 1.7f);
        }
        protected override async Task OnCastW()
        {
            RunAnimationInLoop("w_cast_loop", LightZone.Desk, 0.5f);
            //await Task.Delay(500);
            Animator.HoldLastFrame(LightZone.Desk, 0.5f);
            RunAnimationOnce("w_cast_end", LightZone.Desk);
        }
        protected override async Task OnCastE()
        {
            castingE = true;
            RunAnimationInLoop("e_cast_loop", LightZone.MouseKey, 2f, 1f);
            Animator.HoldLastFrame(LightZone.MouseKey, 1f);
            canRecastE = true;
            Animator.HoldLastFrame(LightZone.MouseKey, 1f);
            if (castingE)
            {
                RunAnimationOnce("e_recast_end", LightZone.MouseKey);
                castingE = false;
                canRecastE = false;
            }
        }
        protected override async Task OnCastR()
        {
            Animator.ColorBurst(RColor, LightZone.Desk, 2f);
        }

        protected override async Task OnRecastE()
        {
            if (canRecastE)
            {
                castingE = false;
                await Task.Delay(200);
                RunAnimationOnce("e_recast_end", LightZone.MouseKey);
            }
        }
    }
}
