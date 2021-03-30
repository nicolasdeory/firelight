using FirelightCore;
using FirelightCore.Modules.BasicAnimation;

namespace FirelightCore
{
    public abstract class BaseGameModule : LEDModule
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

        protected ModuleAttributes ModuleAttributes;

        protected BaseGameModule(string gameId)
        {
            // Load animation module
            Animator = AnimationModule.Create();
            CurrentLEDSource = Animator;
            if (ModuleManager.AttributeDict.ContainsKey(gameId))
                ModuleAttributes = ModuleManager.AttributeDict[gameId];
            else 
                ModuleAttributes = null;
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
