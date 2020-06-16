using LedDashboardCore;
using LedDashboardCore.Modules.BasicAnimation;

namespace Games.RocketLeague
{
    public abstract class BaseRLModule : LEDModule
    {
        // Variables

        //protected readonly Led[] Leds;

        protected AnimationModule Animator;

        // Events

        public event LEDModule.FrameReadyHandler NewFrameReady;

        /// <summary>
        /// The current module that is sending information to the LED strip.
        /// </summary>
        protected LEDModule CurrentLEDSource;

        protected BaseRLModule()
        {
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
