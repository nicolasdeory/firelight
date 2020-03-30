using RazerChroma.Net;
using RazerChromaFrameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RazerChroma.Net.Keyboard.Definitions;

namespace LedDashboard
{
    public enum LightingMode
    {
        Point,
        Line,
        Keyboard
    }

    class RazerChromaController : LightController
    {
        public static RazerChromaController Create()
        {
            return new RazerChromaController();
        }

        private KeyboradFrame keyboardFrame;
        private NativeRazerApi api;

        private RazerChromaController() // TODO: Dispose afterr a while if no data is received
        {
            Init();
        }

        private void Init()
        {
            try
            {
                if (api == null) api = new NativeRazerApi();
                if (keyboardFrame == null) keyboardFrame = new KeyboradFrame(api);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error initializing Razer Chroma controller. Is Razer Synapse installed?");
                throw new InvalidOperationException("Error initializing Razer Chroma controller. Is Razer Synapse installed?", e);
            }
        }

        public void SendData(int ledCount, byte[] colorArray, LightingMode mode)
        {
            if (!enabled) return;
            List<Point> points = new List<Point>();
            if (mode == LightingMode.Line)
            {
                for (int i = 0; i < ledCount; i++)
                {
                    Color c = Color.FromArgb(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                    int x = (int)Utils.Scale(i, 0, ledCount, 0, 22);
                    points.Clear();
                    points.Add(new Point(x, 0));
                    points.Add(new Point(x, 1));
                    points.Add(new Point(x, 2));
                    points.Add(new Point(x, 3));
                    points.Add(new Point(x, 4));
                    points.Add(new Point(x, 5));
                    keyboardFrame.SetKeys(points, c);
                }
            } else if (mode == LightingMode.Point)
            {
                points.Clear();
                for (int i = 0; i < 22; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        points.Add(new Point(i, j));
                        
                    }
                }
                keyboardFrame.SetKeys(points, Color.FromArgb(colorArray[0], colorArray[1], colorArray[2]));
            } else if (mode == LightingMode.Keyboard)
            {
                foreach(RzKey key in numPadKeys)
                {
                    keyboardFrame.SetKey(key, Color.Black); // set numpad keys to black
                }
                for(int i = 0; i < 88;i++)
                {
                    Color c = Color.FromArgb(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                    keyboardFrame.SetKey(indexKeyMap[i], c);
                }
            } else
            {
                throw new ArgumentException("Invalid lighting mode");
            }
            
            keyboardFrame.Update();
        }

        bool enabled = true;
        public void SetEnabled(bool enabled)
        {
            if (this.enabled == enabled) return;
            if(!enabled)
            {
                if (api != null) api.Dispose();
                api = null;
                keyboardFrame = null;
            } else
            {
                Init();
            }
            this.enabled = enabled;
        }

        public void Dispose()
        {
            if (api != null) api.Dispose();
            api = null;
            keyboardFrame = null;
        }

        public bool IsEnabled()
        {
            return enabled;
        }

        static RzKey[] indexKeyMap = new RzKey[] // TODO work with numpads!!
        {
            RzKey.Esc,
            RzKey.F1,RzKey.F2,RzKey.F3,RzKey.F4,RzKey.F5,RzKey.F6,RzKey.F7,RzKey.F8,RzKey.F9,RzKey.F10,RzKey.F11,RzKey.F12,
            RzKey.Printscreen,
            RzKey.Scroll,
            RzKey.Pause,
            RzKey.Oem_1, // º spain
            RzKey.Num1,RzKey.Num2,RzKey.Num3,RzKey.Num4,RzKey.Num5,RzKey.Num6,RzKey.Num7,RzKey.Num8,RzKey.Num9,RzKey.Num0,
            RzKey.Oem_2,
            RzKey.Oem_3,
            RzKey.Backspace,
            RzKey.Insert,
            RzKey.Home,
            RzKey.Pageup,
            RzKey.Tab,
            RzKey.Q,RzKey.W,RzKey.E,RzKey.R,RzKey.T,RzKey.Y,RzKey.U,RzKey.I,RzKey.O,RzKey.P,
            RzKey.Oem_4,
            RzKey.Oem_5,
            RzKey.Enter,
            RzKey.Delete,
            RzKey.End,
            RzKey.Pagedown,
            RzKey.Capslock,
            RzKey.A,RzKey.S,RzKey.D,RzKey.F,RzKey.G,RzKey.H,RzKey.J,RzKey.K,RzKey.L,
            RzKey.Oem_7,
            RzKey.Oem_8,
            RzKey.Eur_1,
            RzKey.Lshift,
            RzKey.Eur_2, // << >> in spanish layout?
            RzKey.Z,RzKey.X,RzKey.C,RzKey.V,RzKey.B,RzKey.N,RzKey.M,
            RzKey.Oem_9,
            RzKey.Oem_10,
            RzKey.Oem_11,
            RzKey.Rshift,
            RzKey.Up,
            RzKey.Lctrl,
            RzKey.Lwin,
            RzKey.Lalt,
            RzKey.Space,
            RzKey.Ralt,
            RzKey.Fn,
            RzKey.Rmenu,
            RzKey.Rctrl,
            RzKey.Left,
            RzKey.Down,
            RzKey.Right
        };

        static RzKey[] numPadKeys = new RzKey[]
        {
            RzKey.Numlock,
            RzKey.Numpad_divide,
            RzKey.Numpad_multiply,
            RzKey.Numpad_subtract,
            RzKey.Numpad7,
            RzKey.Numpad8,
            RzKey.Numpad9,
            RzKey.Numpad_add,
            RzKey.Numpad4,
            RzKey.Numpad5,
            RzKey.Numpad6,
            RzKey.Numpad1,
            RzKey.Numpad2,
            RzKey.Numpad3,
            RzKey.Numpad_enter,
            RzKey.Numpad0,
            RzKey.Numpad_decimal
        };
    }
}
