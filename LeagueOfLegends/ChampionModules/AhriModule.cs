using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class AhriModule : ChampionModule
    {
        // Change to whatever champion you want to implement
        public const string CHAMPION_NAME = "Ahri";

        // Variables

        // Champion-specific Variables

        int rCastInProgress = 0;

        public AhriModule(GameState gameState, AbilityCastPreference preferredCastMode)
            : base(CHAMPION_NAME, gameState, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant(10000, 2);

        protected override async Task OnCastQ()
        {
            RunAnimationOnce("q_start", LightZone.Keyboard);
            Animator.HoldLastFrame(LightZone.Keyboard, 1f);
            RunAnimationOnce("q_end", LightZone.Keyboard);
        }
        protected override async Task OnCastW()
        {
            RunAnimationOnce("w_cast", LightZone.Keyboard, 3f);
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(100);
            RunAnimationOnce("e_cast", LightZone.Keyboard);
        }
        protected override async Task OnCastR()
        {
            // Trigger the start animation.

            RunAnimationOnce("r_right", LightZone.Keyboard);

            // The R cast is in progress.
            rCastInProgress = 1;

            // TODO: Validate that this does not break
            await Task.Delay(7000); // if after 7s no recast, effect disappears
            rCastInProgress = 0;
        }

        protected override async Task OnRecastR()
        {
            ProcessRCasts();
            rCastInProgress++;
            rCastInProgress %= 3;
        }

        private void ProcessRCasts()
        {
            switch (rCastInProgress)
            {
                case 1:
                    RunAnimationOnce("r_left", LightZone.Keyboard);
                    break;
                case 2:
                    RunAnimationOnce("r_right", LightZone.Keyboard);
                    break;
            }
        }
    }
}
