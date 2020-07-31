using Gma.System.MouseKeyHook;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelightCore
{
    public class KeyboardHook
    {

        public static KeyboardHook Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new KeyboardHook();
                return _instance;
            }
        }

        public static void Init()
        {
            _instance = new KeyboardHook();
        }

        private static KeyboardHook _instance;

        private static IKeyboardMouseEvents m_GlobalHook;

        /// <summary>
        /// Raised when the mouse is clicked.
        /// </summary>
        public event MouseEventHandler OnMouseClicked; // TODO: Check window in focus. i.e for league of legends make sure it's when the client window is in focus.

        /// <summary>
        /// Raised when a key is pressed (down)
        /// </summary>
        public event KeyEventHandler OnKeyPressed;

        /// <summary>
        /// Raised when a key is released
        /// </summary>
        public event KeyEventHandler OnKeyReleased;

        private CancellationTokenSource cancelToken;

        //public event KeyEventHandler OnKeyDown; // this shouldn't be needed

        Hooker hooker;
        private KeyboardHook()
        {
            cancelToken = new CancellationTokenSource();
            hooker = new Hooker();
            hooker.OnKeyDown += OnKeyPress;
            hooker.OnKeyUp += OnKeyRelease;
            hooker.OnMouseButton += OnMouseButtonPressed;
            hooker.EnableHooks();

            Task.Run(MessageLoop).CatchExceptions();

        }

        private void MessageLoop()
        {
            foreach (HookMessage message in hooker.MessageQueue.GetConsumingEnumerable())
            {
                /*  ProcessNewTagReceivedLogic(tag);
                  Console.WriteLine("Process Buffer #Tag {0}. Buffer Count #{1}", tag.NewLoopId, LogicBuffer.Count);*/
                if (message.messageType == HookMessageType.KeyDown)
                {
                    OnKeyReleased?.Invoke(this, new KeyEventArgs(message.key));
                }
                else if (message.messageType == HookMessageType.KeyUp)
                {
                    OnKeyPressed?.Invoke(this, new KeyEventArgs(message.key));
                }
                else if (message.messageType == HookMessageType.MouseDown)
                {
                    OnMouseClicked?.Invoke(this, new MouseEventArgs(message.mouseButton, 1, message.mouseX, message.mouseY, 0));
                }
            }

           /* while (true)
            {
                if (cancelToken.IsCancellationRequested)
                    return;

                

                /*if (hooker.HasMessageAvailable)
                {
                    HookMessage message = hooker.NextMessage;
                    if (message.messageType == HookMessageType.KeyDown)
                    {
                        OnKeyReleased?.Invoke(this, new KeyEventArgs(message.key));
                    }
                    else if (message.messageType == HookMessageType.KeyUp)
                    {
                        OnKeyPressed?.Invoke(this, new KeyEventArgs(message.key));
                    }
                    else if (message.messageType == HookMessageType.MouseDown)
                    {
                        OnMouseClicked?.Invoke(this, new MouseEventArgs(message.mouseButton, 1, message.mouseX, message.mouseY, 0));
                    }
                }
                await Task.Delay(10);
            }*/
        }


        private void OnMouseButtonPressed(MouseButton button, int x, int y)
        {
            if (button.button == MouseButtons.Left)
            {
                MouseEventArgs args = new MouseEventArgs(button.button, 1, x, y, 0);

                // Invoke on another thread to prevent blocking the hook chain
                Task.Run(() =>
                {
                    OnMouseClicked?.Invoke(this, args);
                });
                
            }
        }

        private void OnKeyRelease(Keys key)
        {
            KeyEventArgs args = new KeyEventArgs(key);

            Task.Run(() =>
            {
                OnKeyReleased?.Invoke(this, args);
            });
        }

        private void OnKeyPress(Keys key)
        {
            KeyEventArgs args = new KeyEventArgs(key);

            Task.Run(() =>
            {
                OnKeyPressed?.Invoke(this, args);
            });
        }

        public void Unhook()
        {
            cancelToken.Cancel();
            hooker.DisableHooks();
            hooker.Dispose();
        }
    }
}