using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore.Modules.BasicAnimation;
using LedDashboardCore.Modules.Common;
using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games.LeagueOfLegends.ChampionModules
{

    [Champion(CHAMPION_NAME)]
    class VelKozModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Velkoz";

        // Variables

        // Champion-specific Variables

        bool qCastInProgress = false;
        bool rCastInProgress = false; // this is used to make the animation for Vel'Koz's R to take preference over other animations


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static VelKozModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new VelKozModule(ledCount, gameState, preferredLightMode, preferredCastMode);
        }


        private VelKozModule(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode) 
            : base(ledCount, CHAMPION_NAME, gameState, preferredLightMode, preferredCastMode, true)
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
                RunAnimationOnce("q_start", true, 0.9f);
            }

            // The Q cast is in progress.
            qCastInProgress = true;

            // After 1.15s, if user didn't press Q again already, the Q split animation plays.
            // TODO: Q runs a bit slow.
            
            await Task.Delay(1150);
            if (!rCastInProgress && qCastInProgress)
            {
                RunAnimationOnce("q_recast", true, 0);
            }
            qCastInProgress = false;
        }
        protected override async Task OnCastW()
        {
            RunAnimationOnce("w_cast", true, 0.85f);
            await Task.Delay(1800);
            if (!rCastInProgress)
            {
                RunAnimationOnce("w_close", false, 0.15f);
            }
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(1000);
            if (!rCastInProgress)
            {
                Animator.ColorBurst(HSVColor.FromRGB(229, 115, 255), 0.15f);
            }
        }
        protected override async Task OnCastR()
        {
            Animator.StopCurrentAnimation();
            RunAnimationInLoop("r_loop", 2300, 0.15f);
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
                    RunAnimationOnce("q_recast");
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
