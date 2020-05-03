using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using LedDashboardCore.Modules.BasicAnimation;

namespace Games.LeagueOfLegends
{
    public abstract class BaseGameModule : LEDModule
    {
        // Variables

        //protected readonly Led[] Leds;

        protected GameState GameState;

        protected AnimationModule Animator;

        // Events

        public event LEDModule.FrameReadyHandler NewFrameReady;

        /// <summary>
        /// The preferred lighting mode (when possible, use this one) For example, if keyboard is preferred, 
        /// use animations optimized for keyboards rather than for LED strips.
        /// </summary>
        protected AbilityCastPreference PreferredCastMode;

        /// <summary>
        /// The current module that is sending information to the LED strip.
        /// </summary>
        protected LEDModule CurrentLEDSource;

        protected BaseGameModule(GameState gameState, AbilityCastPreference castMode)
        {
            PreferredCastMode = castMode;

            GameState = gameState;
            // Load animation module
            Animator = AnimationModule.Create();
            CurrentLEDSource = Animator;
        }

        protected void AddAnimatorEvent()
        {
            Animator.NewFrameReady += NewFrameReadyHandler;
        }
        protected abstract void NewFrameReadyHandler(LEDFrame frame);

        protected void InvokeNewFrameReady(LEDFrame frame)
        {
            frame.SenderChain.Add(this);
            NewFrameReady?.Invoke(frame);
        }

        public abstract void Dispose();
    }
}
