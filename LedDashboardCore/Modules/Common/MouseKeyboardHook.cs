using Gma.System.MouseKeyHook;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winook;

namespace FirelightCore
{
    enum HookMessageType { MouseDown, MouseUp, KeyDown, KeyUp }
    class HookMessage
    {
        public static HookMessage KeyUp(Keys key)
        {
            return new HookMessage()
            {
                key = key,
                messageType = HookMessageType.KeyUp
            };
        }
        public static HookMessage KeyDown(Keys key)
        {
            return new HookMessage()
            {
                key = key,
                messageType = HookMessageType.KeyDown
            };
        }

        public static HookMessage MouseDown(MouseButtons button, int x, int y)
        {
            return new HookMessage()
            {
                mouseX = x,
                mouseY = y,
                mouseButton = button,
                messageType = HookMessageType.MouseDown
            };
        }

        public static HookMessage MouseUp(MouseButtons button, int x, int y)
        {
            return new HookMessage()
            {
                mouseX = x,
                mouseY = y,
                mouseButton = button,
                messageType = HookMessageType.MouseUp
            };
        }

        public HookMessageType messageType;
        public Keys key;
        public MouseButtons mouseButton;
        public int mouseX;
        public int mouseY;
    }

    public class MouseKeyboardHook
    {

        private static Dictionary<int, MouseKeyboardHook> InstanceDictionary;

        /// <summary>
        /// Creates a <see cref="MouseKeyboardHook"/> instance.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static MouseKeyboardHook CreateInstance(int processId)
        {
            if (InstanceDictionary == null)
                InstanceDictionary = new Dictionary<int, MouseKeyboardHook>();

            if (InstanceDictionary.ContainsKey(processId))
                throw new InvalidOperationException("Instance already exists for the given process id. You must dispose of the existing one first.");

            MouseKeyboardHook instance = new MouseKeyboardHook(processId);
            InstanceDictionary.Add(processId, instance);

            return instance;
        }

        /// <summary>
        /// Disposes of the <see cref="MouseKeyboardHook"/> instance associated to the given processId and unhooks events.
        /// </summary>
        /// <param name="processId"></param>
        public static void DisposeInstance(int processId)
        {
            if (InstanceDictionary == null || !InstanceDictionary.ContainsKey(processId))
                throw new InvalidOperationException("Instance doesn't exist.");

            InstanceDictionary[processId].Unhook();
            InstanceDictionary.Remove(processId);
        }

        /// <summary>
        /// Gets the <see cref="MouseKeyboardHook"/> instance associated to the given processId or creates it if it doesn't exist.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static MouseKeyboardHook GetInstance(int processId)
        {
            if (InstanceDictionary == null || !InstanceDictionary.ContainsKey(processId))
            {
                return CreateInstance(processId);
            }

            return InstanceDictionary[processId];
        }

        /// <summary>
        /// Raised when the mouse is down.
        /// </summary>
        public event MouseEventHandler OnMouseDown;

        /// <summary>
        /// Raised when the mouse is up.
        /// </summary>
        public event MouseEventHandler OnMouseUp;

        /// <summary>
        /// Raised when a key is pressed (down)
        /// </summary>
        public event KeyEventHandler OnKeyPressed;

        /// <summary>
        /// Raised when a key is released
        /// </summary>
        public event KeyEventHandler OnKeyReleased;

        //public event KeyEventHandler OnKeyDown; // this shouldn't be needed

        //Hooker hooker;

        private MouseHook mouseHook;
        private KeyboardHook keyboardHook;

        private BlockingCollection<HookMessage> messageQueue;

        private MouseKeyboardHook(int processId)
        {
            messageQueue = new BlockingCollection<HookMessage>();

            mouseHook = new MouseHook(processId);
            mouseHook.MessageReceived += OnMouseHookMessageReceived;
            mouseHook.InstallAsync();

            keyboardHook = new KeyboardHook(processId);
            keyboardHook.MessageReceived += OnKeyboardHookMessageReceived;
            keyboardHook.InstallAsync();

            Task.Run(MessageLoop).CatchExceptions();

        }

        private void OnKeyboardHookMessageReceived(object sender, KeyboardMessageEventArgs e)
        {
            if (e.Control || e.Alt || e.Shift)
                return; // Modifier keys disabled for now

            if (e.Direction == KeyDirection.Down)
            {
                messageQueue.Add(HookMessage.KeyDown((Keys)e.KeyValue));
            }
            else if (e.Direction == KeyDirection.Up)
            {
                messageQueue.Add(HookMessage.KeyUp((Keys)e.KeyValue));
            }
        }

        private void OnMouseHookMessageReceived(object sender, MouseMessageEventArgs e)
        {
            if (e.MessageCode == (int)MouseMessageCode.LeftButtonDown)
            {
                messageQueue.Add(HookMessage.MouseDown(MouseButtons.Left, e.X, e.Y));
            }
            else if (e.MessageCode == (int)MouseMessageCode.LeftButtonUp)
            {
                messageQueue.Add(HookMessage.MouseUp(MouseButtons.Left, e.X, e.Y));
            }
            else if (e.MessageCode == (int)MouseMessageCode.RightButtonDown)
            {
                messageQueue.Add(HookMessage.MouseDown(MouseButtons.Right, e.X, e.Y));
            }
            else if (e.MessageCode == (int)MouseMessageCode.RightButtonUp)
            {
                messageQueue.Add(HookMessage.MouseUp(MouseButtons.Right, e.X, e.Y));
            }
            else if (e.MessageCode == (int)MouseMessageCode.MiddleButtonDown)
            {
                messageQueue.Add(HookMessage.MouseDown(MouseButtons.Middle, e.X, e.Y));
            }
            else if (e.MessageCode == (int)MouseMessageCode.MiddleButtonUp)
            {
                messageQueue.Add(HookMessage.MouseUp(MouseButtons.Middle, e.X, e.Y));
            }
            else if (e.MessageCode == (int)MouseMessageCode.XButtonDown)
            {
                messageQueue.Add(HookMessage.MouseDown(MouseButtons.XButton1, e.X, e.Y));
                // TODO XButton1 and XButton2 difference
            }
            else if (e.MessageCode == (int)MouseMessageCode.XButtonUp)
            {
                messageQueue.Add(HookMessage.MouseUp(MouseButtons.XButton1, e.X, e.Y));
                // TODO XButton1 and XButton2 difference
            }
        }



        private void MessageLoop()
        {
            foreach (HookMessage message in messageQueue.GetConsumingEnumerable())
            {
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
                    OnMouseDown?.Invoke(this, new MouseEventArgs(message.mouseButton, 1, message.mouseX, message.mouseY, 0));
                }
                else if (message.messageType == HookMessageType.MouseUp)
                {
                    OnMouseUp?.Invoke(this, new MouseEventArgs(message.mouseButton, 1, message.mouseX, message.mouseY, 0));
                }
            }
        }


        private void OnMouseButtonPressed(MouseButton button, int x, int y)
        {
            if (button.button == MouseButtons.Left)
            {
                MouseEventArgs args = new MouseEventArgs(button.button, 1, x, y, 0);

                // Invoke on another thread to prevent blocking the hook chain
                Task.Run(() =>
                {
                    OnMouseDown?.Invoke(this, args);
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
            messageQueue.CompleteAdding();
            mouseHook.Dispose();
        }
    }
}