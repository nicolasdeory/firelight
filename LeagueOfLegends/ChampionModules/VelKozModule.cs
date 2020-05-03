using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class VelKozModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Velkoz";

        // Variables

        // Champion-specific Variables

        bool qCastInProgress = false;
        bool rCastInProgress = false; // this is used to make the animation for Vel'Koz's R to take preference over other animations

        public VelKozModule(GameState gameState, AbilityCastPreference preferredCastMode)
            : base(CHAMPION_NAME, gameState, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Normal(1150);
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant(2300);

        protected override async Task OnCastQ()
        {
            // Here you should write code to trigger the appropiate animations to play when the user casts Q.
            // The code will slightly change between each champion, because you might want to implement custom animation logic.

            // Trigger the start animation.
            await Task.Delay(100);
            if (!rCastInProgress)
            {
                RunAnimationOnce("q_start", LightZone.Desk, timeScale: 0.9f);
            }

            // The Q cast is in progress.
            qCastInProgress = true;

            // After 1.15s, if user didn't press Q again already, the Q split animation plays.
            // TODO: Q runs a bit slow.

            Animator.HoldLastFrame(LightZone.Desk, 1.15f);
            if (!rCastInProgress && qCastInProgress)
            {
                RunAnimationOnce("q_recast", LightZone.Desk, priority: false);
            }
            qCastInProgress = false;
        }
        protected override async Task OnCastW()
        {
            RunAnimationOnce("w_cast", LightZone.Desk);
            Animator.HoldLastFrame(LightZone.Desk, 1.8f);
            if (!rCastInProgress)
            {
                Animator.ColorBurst(HSVColor.FromRGB(229, 115, 255), LightZone.Desk, 0.8f, false);
            }
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(1000);
            if (!rCastInProgress)
            {
                Animator.ColorBurst(HSVColor.FromRGB(229, 115, 255), LightZone.Desk, 0.8f);
            }
        }
        protected override async Task OnCastR()
        {
            Animator.StopCurrentAnimation();
            RunAnimationInLoop("r_loop", LightZone.Desk, 2.3f, 2f);
            rCastInProgress = true;

            await Task.Delay(2300);
            rCastInProgress = false;
        }

        protected override async Task OnRecastQ()
        {
            if (qCastInProgress)
            {
                // Would there ever be a Q recast while R is being cast?
                if (!rCastInProgress)
                {
                    RunAnimationOnce("q_recast", LightZone.Desk);
                }
            }
            qCastInProgress = false;
        }
        protected override async Task OnRecastR()
        {
            Animator.StopCurrentAnimation();
            rCastInProgress = false;
        }
    }
}
