using FirelightCore;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Games.Fortnite
{
    public class DanceModule : BaseGameModule
    {
        private Keys lastPressedKey = Keys.None;

        private int processId;

        const string ModuleAnimationPath = "Animations/Fortnite";

        //protected new LeagueOfLegendsModuleAttributes ModuleAttributes;

        public DanceModule(bool preloadAllAnimations = false) : base(FortniteModule.GAME_ID)
        {
            //ModuleAttributes = ModuleManager.AttributeDict["leagueoflegends"] as LeagueOfLegendsModuleAttributes;

            //processId = ProcessListenerService.GetProcessId("FortniteClient-Win64-Shipping");
            processId = -1;

            AddInputHandlers();

            Animator.NewFrameReady += NewFrameReadyHandler;

            if (preloadAllAnimations)
                PreloadAllAnimations();
        }

        protected string GetAnimationPath(string animationName) => $"{ModuleAnimationPath}/{animationName}.txt";

        protected void PreloadAnimation(string animationName)
        {
            Animator.PreloadAnimation(GetAnimationPath(animationName));
        }
        protected void PreloadAllAnimations()
        {
            try
            {
                foreach (var file in Directory.GetFiles($"{ModuleAnimationPath}/"))
                    Animator.PreloadAnimation(file);
            }
            catch (DirectoryNotFoundException)
            {
                Debug.WriteLine("No animations found for Dance Fortnite");
            }
        }

        /// <summary>
        /// Dispatches a frame with the given LED data, raising the NewFrameReady event.
        /// </summary>
        protected override void NewFrameReadyHandler(LEDFrame frame)
        {
            InvokeNewFrameReady(frame);
        }

        protected void AddInputHandlers()
        {
            //keyboardHook = new KeyboardHook();
            //MouseKeyboardHook.GetInstance(processId).OnMouseDown += OnMouseDown;
            //MouseKeyboardHook.GetInstance(processId).OnMouseUp += OnMouseUp;
            MouseKeyboardHook.GetInstance(processId).OnKeyPressed += OnKeyPress;
            MouseKeyboardHook.GetInstance(processId).OnKeyReleased += OnKeyRelease;
        }

        //protected abstract void OnMouseDown(object s, MouseEventArgs e);
        //protected abstract void OnMouseUp(object s, MouseEventArgs e);
        protected void OnKeyRelease(object s, KeyEventArgs e)
        {
            // TODO: when user presses WASD stop dancing, play forever
            if (e.KeyCode == Keys.B) // TODO: Configurable binding
            {
                Animator.RunAnimationInLoop(GetAnimationPath("dance"), LightZone.Keyboard, 5f, timeScale: 2, fadeoutDuration: 1f);
            }
        }

        protected void OnKeyPress(object s, KeyEventArgs e)
        {
            //Debug.WriteLine("a");
        }

        /// <summary>
        /// Processes a key press. When overriden, the base versions of this virtual function must be called first.
        /// </summary>
        protected virtual void ProcessKeyPress(object s, Keys key, bool keyUp = false)
        {
            if (key == lastPressedKey && !keyUp)
                return; // prevent duplicate calls. Without this, this gets called every frame a key is pressed.
            lastPressedKey = keyUp ? Keys.None : key;
        }

        public override void Dispose()
        {
            Animator?.Dispose();
            if (MouseKeyboardHook.GetInstance(processId) != null)
            {
                //MouseKeyboardHook.GetInstance(processId).OnMouseDown -= OnMouseDown;
                //MouseKeyboardHook.GetInstance(processId).OnKeyPressed -= OnKeyPress;
                MouseKeyboardHook.GetInstance(processId).OnKeyReleased -= OnKeyRelease;
            }
        }

        public void StopAnimations()
        {
            Animator.StopCurrentAnimation();
        }
    }
}
