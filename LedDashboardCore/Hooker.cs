using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LedDashboardCore
{
    enum HookMessageType { MouseDown, KeyDown, KeyUp }
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

        public HookMessageType messageType;
        public Keys key;
        public MouseButtons mouseButton;
        public int mouseX;
        public int mouseY;
    }

    class Hooker : IDisposable
    {
        int MouseHookHandle;
        int KeyboardHookHandle;
        HookProc MouseHookGCRootedDelegate;
        HookProc KeyboardHookGCRootedDelegate;
        BlockingCollection<HookMessage> messageQueue;

        public event OnMouseWheelDelegate OnMouseWheel;
        public event OnMouseMoveDelegate OnMouseMove;
        public event OnMouseButtonDelegate OnMouseButton;
        public event OnKeyDownDelegate OnKeyDown;
        public event OnKeyUpDelegate OnKeyUp;

        public bool IsMouseHooked
        {
            get
            {
                return MouseHookHandle != 0;
            }

            set
            {
                HookMouse(value);
            }
        }

        public bool IsKeyboardHooked
        {
            get
            {
                return KeyboardHookHandle != 0;
            }

            set
            {
                HookKeyboard(value);
            }
        }

       /* public bool HasMessageAvailable
        {
            get
            {
                return messageQueue.Count > 0;
            }
        }

        public HookMessage NextMessage
        {
            get
            {
                return messageQueue.taDequeue();
            }
        }*/

        public BlockingCollection<HookMessage> MessageQueue
        {
            get
            {
                return messageQueue;
            }
        }

        public Hooker()
        {
            // HookEvents.RegisterItself();
            MouseHookGCRootedDelegate = MouseHook;
            KeyboardHookGCRootedDelegate = KeyboardHook;

            messageQueue = new BlockingCollection<HookMessage>();
        }

        void HookKeyboard(bool bHook)
        {
            if (KeyboardHookHandle == 0 && bHook)
            {
                using (var mainMod = Process.GetCurrentProcess().MainModule)
                    KeyboardHookHandle = HookNativeDefinitions.SetWindowsHookEx(HookNativeDefinitions.WH_KEYBOARD_LL, KeyboardHookGCRootedDelegate, HookNativeDefinitions.GetModuleHandle(mainMod.ModuleName), 0);

                //If the SetWindowsHookEx function fails.
                if (KeyboardHookHandle == 0)
                {
                    MessageBox.Show("SetWindowsHookEx Failed " + new Win32Exception(Marshal.GetLastWin32Error()));
                    return;
                }
            }
            else if (bHook == false)
            {
                Debug.Print("Unhook keyboard");
                HookNativeDefinitions.UnhookWindowsHookEx(KeyboardHookHandle);
                KeyboardHookHandle = 0;
            }

        }

        public void DisableHooks()
        {
            IsKeyboardHooked = false;
            IsMouseHooked = false;
            OnMouseButton = null;
            OnKeyDown = null;
            OnMouseWheel = null;
            OnMouseMove = null;
        }

        public void EnableHooks()
        {
            IsKeyboardHooked = true;
            IsMouseHooked = true;
        }

        private void HookMouse(bool bHook)
        {
            if (MouseHookHandle == 0 && bHook)
            {
                using (var mainMod = Process.GetCurrentProcess().MainModule)
                    MouseHookHandle = HookNativeDefinitions.SetWindowsHookEx(HookNativeDefinitions.WH_MOUSE_LL, MouseHookGCRootedDelegate, HookNativeDefinitions.GetModuleHandle(mainMod.ModuleName), 0);
                //If the SetWindowsHookEx function fails.
                if (MouseHookHandle == 0)
                {
                    MessageBox.Show("SetWindowsHookEx Failed " + new Win32Exception(Marshal.GetLastWin32Error()));
                    return;
                }
            }
            else if (bHook == false)
            {
                Debug.Print("Unhook mouse");
                HookNativeDefinitions.UnhookWindowsHookEx(MouseHookHandle);
                MouseHookHandle = 0;
            }
        }

        public int KeyboardHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //     Debug.Print("KeyboardHook called");
                var keyboardData = (HookNativeDefinitions.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(HookNativeDefinitions.KeyboardHookStruct));
                unchecked
                {
                    // wParam is WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP
                    int wInt = wParam.ToInt32();

                    var key = KeyInterop.KeyFromVirtualKey((int)keyboardData.vkCode);
                    if (wInt == WM.KEYDOWN || wInt == WM.SYSKEYDOWN && OnKeyDown != null)
                    {
                        // OnKeyDown?.Invoke(key);
                        messageQueue.Add(HookMessage.KeyDown(key));
                    }
                    else if (wInt == WM.KEYUP || wInt == WM.SYSKEYUP && OnKeyUp != null)
                    {
                        // OnKeyUp?.Invoke(key);
                        messageQueue.Add(HookMessage.KeyUp(key));
                    }
                }

            }

            return HookNativeDefinitions.CallNextHookEx(MouseHookHandle, nCode, wParam, lParam);
        }


        public int MouseHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //    Debug.Print("MouseHook called");
                unchecked
                {
                    int wParami = wParam.ToInt32();

                    //Marshal the data from the callback.
                    var mouseData = (HookNativeDefinitions.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(HookNativeDefinitions.MouseHookStruct));
                    var mouseButton = WM.GetMouseButton(wParam);
                    int wheelDelta = 0;
                    if (mouseButton.button == MouseButtons.None && wParami == WM.MOUSEWHEEL)
                    {
                        wheelDelta = WM.GetWheelDDelta(mouseData.mouseData);
                    }

                    if (wParami == WM.MOUSEMOVE)
                    {
                        if (OnMouseMove != null)
                        {
                           // OnMouseMove(mouseData.pt.x, mouseData.pt.y);
                        }
                        else
                        {
                            // mouse move is disabled ignore event
                        }
                    }
                    else if (wParami == WM.MOUSEWHEEL && OnMouseWheel != null)
                    {
                        //OnMouseWheel?.Invoke(wheelDelta, mouseData.pt.x, mouseData.pt.y);
                    }
                    else if (OnMouseButton != null)
                    {
                        //OnMouseButton?.Invoke(mouseButton, mouseData.pt.x, mouseData.pt.y);
                        messageQueue.Add(HookMessage.MouseDown(mouseButton.button, mouseData.pt.x, mouseData.pt.y));
                    }
                }
            }

            return HookNativeDefinitions.CallNextHookEx(MouseHookHandle, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            if (MouseHookHandle != 0)
            {
                Debug.Print("Unhook mouse");
                HookNativeDefinitions.UnhookWindowsHookEx(MouseHookHandle);
                MouseHookHandle = 0;
            }

            if (KeyboardHookHandle != 0)
            {
                Debug.Print("Unhook Keyboard");
                HookNativeDefinitions.UnhookWindowsHookEx(KeyboardHookHandle);
                KeyboardHookHandle = 0;
            }
        }
    }

    delegate void OnMouseWheelDelegate(int wheelDetla, int x, int y);
    delegate void OnMouseMoveDelegate(int x, int y);
    delegate void OnMouseButtonDelegate(MouseButton button, int x, int y);
    delegate void OnKeyDownDelegate(Keys key);
    delegate void OnKeyUpDelegate(Keys key);
}