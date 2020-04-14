using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using LedDashboardCore.Modules.BasicAnimation;

namespace Games.LeagueOfLegends
{
    public abstract class BaseGameModule : LEDModule
    {
        // Variables

        protected readonly Led[] Leds;

        protected GameState GameState;

        protected AnimationModule Animator;

        // Events

        public event LEDModule.FrameReadyHandler NewFrameReady;

        /// <summary>
        /// The preferred lighting mode (when possible, use this one) For example, if keyboard is preferred, 
        /// use animations optimized for keyboards rather than for LED strips.
        /// </summary>
        protected LightingMode LightingMode;
        protected AbilityCastPreference PreferredCastMode;

        /// <summary>
        /// The current module that is sending information to the LED strip.
        /// </summary>
        protected LEDModule CurrentLEDSource;

        protected BaseGameModule(int ledCount, GameState gameState, LightingMode mode, AbilityCastPreference castMode)
        {
            PreferredCastMode = castMode;

            GameState = gameState;

            // LED Initialization
            LightingMode = mode;
            Leds = new Led[ledCount];
            for (int i = 0; i < ledCount; i++)
                Leds[i] = new Led();

            // Load animation module
            Animator = AnimationModule.Create(ledCount);
            CurrentLEDSource = Animator;
        }

        protected void AddAnimatorEvent()
        {
            Animator.NewFrameReady += NewFrameReadyHandler;
        }
        protected abstract void NewFrameReadyHandler(object s, Led[] ls, LightingMode mode);

        protected void InvokeNewFrameReady(object s, Led[] ls, LightingMode mode)
        {
            NewFrameReady?.Invoke(s, ls, mode);
        }

        public abstract void Dispose();
    }
}
