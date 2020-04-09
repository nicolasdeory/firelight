using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChromaSDK;
using static ChromaSDK.Keyboard;

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

        private int baseKeyboardAnim;
        private static int MAX_KEYBOARD_COLS = ChromaAnimationAPI.GetMaxColumn((int)ChromaAnimationAPI.Device2D.Keyboard);
        private static int MAX_KEYBOARD_ROWS = ChromaAnimationAPI.GetMaxRow((int)ChromaAnimationAPI.Device2D.Keyboard);
        private static int KEYBOARD_LED_COUNT = MAX_KEYBOARD_COLS * MAX_KEYBOARD_ROWS;

        public static RazerChromaController Create()
        {
            return new RazerChromaController();
        }


        private RazerChromaController() // TODO: Dispose afterr a while if no data is received
        {
            Init();
        }

        private void Init()
        {
            // TODO: Check synapse version. https://chromasdk.io:54236/razer/chromasdk Version must be >= 3.X
            try
            {
                int status = ChromaAnimationAPI.Init();
                if (status != 0)
                {
                    throw new InvalidOperationException("Chroma SDK exception. Status " + status);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error initializing Razer Chroma controller. Is Razer Synapse installed?");
                throw new InvalidOperationException("Error initializing Razer Chroma controller. Is Razer Synapse installed?", e);
            }
            baseKeyboardAnim = ChromaAnimationAPI.CreateAnimationInMemory((int)ChromaAnimationAPI.DeviceType.DE_2D, (int)ChromaAnimationAPI.Device2D.Keyboard);
            if (baseKeyboardAnim == -1)
            { // TODO: Returns -1 if the keyboard is not connected? Check how it works
                MessageBox.Show("Error initializing Razer Chroma controller. Check that your keyboard is connected and Razer Synapse is installed");
                throw new InvalidOperationException("CreateAnimationInMemory ChromaSDK returned -1");
            }
            // is AddFrame needed?
            ChromaAnimationAPI.AddFrame(baseKeyboardAnim, 1, new int[MAX_KEYBOARD_COLS * MAX_KEYBOARD_ROWS], MAX_KEYBOARD_COLS * MAX_KEYBOARD_ROWS);
            
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
                    int x = (int)Utils.Scale(i, 0, ledCount, 0, 22);// TODO: Handle keyboards without numpads
                    points.Clear();
                    for (int j = 0; j < 6; j++)
                        points.Add(new Point(x, j));
                    try
                    {
                        foreach (var point in points)
                        {
                            ChromaAnimationAPI.Set2DColor(baseKeyboardAnim, 0, point.X, point.Y, ChromaAnimationAPI.GetRGB(c.R, c.G, c.B));
                        }
                       // keyboardFrame.SetKeys(points, c);
                    }
                    catch
                    {
                        Console.WriteLine("Error sending data to Chroma keyboard. Perhaps it doesn't have a keypad");
                    }
                    
                }
            } else if (mode == LightingMode.Point)
            {
                for (int i = 0; i < KEYBOARD_LED_COUNT; i++)
                {
                    int col = ChromaAnimationAPI.GetRGB(colorArray[0], colorArray[1], colorArray[2]);
                    ChromaAnimationAPI.Set1DColor(baseKeyboardAnim, 0, i, col);
                }
                /*points.Clear();
                for (int i = 0; i < 22; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        points.Add(new Point(i, j));
                    }
                }
                try
                {
                    keyboardFrame.SetKeys(points, Color.FromArgb(colorArray[0], colorArray[1], colorArray[2]));
                } catch (Exception) // TODO: Handle keyboards without numpads
                {
                    Console.WriteLine("Error sending data to Chroma keyboard. Perhaps it doesn't have a keypad");
                }*/

            }
            else if (mode == LightingMode.Keyboard)
            {
                int black = ChromaAnimationAPI.GetRGB(0, 0, 0);
                foreach (Keyboard.RZKEY key in numPadKeys)
                {
                    ChromaAnimationAPI.SetKeyColor(baseKeyboardAnim, 0, (int)key, black);
                    //keyboardFrame.SetKey(key, Color.Black); // set numpad keys to black
                }
                for (int i = 0; i < 88; i++)
                {
                    Color c = Color.FromArgb(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                    //keyboardFrame.SetKey(indexKeyMap[i], c);
                }

            }
            else
            {
                Console.Error.WriteLine("RazerChroma: Invalid lighting mode");
                throw new ArgumentException("Invalid lighting mode");
            }

            // keyboardFrame.Update();
            ChromaAnimationAPI.PreviewFrame(baseKeyboardAnim, 0);
        }

        bool enabled = true;

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value) return;
                if (!value)
                {
                    Dispose();
                }
                else
                {
                    Init();
                }
                enabled = value;
            }
        }

        public void Dispose()
        {
         /*   if (api != null) api.Dispose();
            api = null;
            keyboardFrame = null;*/
            int res = ChromaAnimationAPI.Uninit();
            if (res != 0)
            {
                Debug.WriteLine("WARNING: Error disposing chroma api");
            }
        }

        public bool IsEnabled()
        {
            return enabled;
        }

        static RZKEY[] indexKeyMap = new RZKEY[] // TODO work with numpads!!
        {
            RZKEY.RZKEY_ESC,
            RZKEY.RZKEY_F1,RZKEY.RZKEY_F2,RZKEY.RZKEY_F3,RZKEY.RZKEY_F4,RZKEY.RZKEY_F5,RZKEY.RZKEY_F6,RZKEY.RZKEY_F7,RZKEY.RZKEY_F8,RZKEY.RZKEY_F9,RZKEY.RZKEY_F10,RZKEY.RZKEY_F11,RZKEY.RZKEY_F12,
            RZKEY.RZKEY_PRINTSCREEN,
            RZKEY.RZKEY_SCROLL,
            RZKEY.RZKEY_PAUSE,
            RZKEY.RZKEY_OEM_1, // º SPAIN
            RZKEY.RZKEY_NUMPAD1,RZKEY.RZKEY_NUMPAD2,RZKEY.RZKEY_NUMPAD3,RZKEY.RZKEY_NUMPAD4,RZKEY.RZKEY_NUMPAD5,RZKEY.RZKEY_NUMPAD6,RZKEY.RZKEY_NUMPAD7,RZKEY.RZKEY_NUMPAD8,RZKEY.RZKEY_NUMPAD9,RZKEY.RZKEY_NUMPAD0,
            RZKEY.RZKEY_OEM_2,
            RZKEY.RZKEY_OEM_3,
            RZKEY.RZKEY_BACKSPACE,
            RZKEY.RZKEY_INSERT,
            RZKEY.RZKEY_HOME,
            RZKEY.RZKEY_PAGEUP,
            RZKEY.RZKEY_TAB,
            RZKEY.RZKEY_Q,RZKEY.RZKEY_W,RZKEY.RZKEY_E,RZKEY.RZKEY_R,RZKEY.RZKEY_T,RZKEY.RZKEY_Y,RZKEY.RZKEY_U,RZKEY.RZKEY_I,RZKEY.RZKEY_O,RZKEY.RZKEY_P,
            RZKEY.RZKEY_OEM_4,
            RZKEY.RZKEY_OEM_5,
            RZKEY.RZKEY_ENTER,
            RZKEY.RZKEY_DELETE,
            RZKEY.RZKEY_END,
            RZKEY.RZKEY_PAGEDOWN,
            RZKEY.RZKEY_CAPSLOCK,
            RZKEY.RZKEY_A,RZKEY.RZKEY_S,RZKEY.RZKEY_D,RZKEY.RZKEY_F,RZKEY.RZKEY_G,RZKEY.RZKEY_H,RZKEY.RZKEY_J,RZKEY.RZKEY_K,RZKEY.RZKEY_L,
            RZKEY.RZKEY_OEM_7,
            RZKEY.RZKEY_OEM_8,
            RZKEY.RZKEY_EUR_1,
            RZKEY.RZKEY_LSHIFT,
            RZKEY.RZKEY_EUR_2, // << >> IN SPANISH LAYOUT?
            RZKEY.RZKEY_Z,RZKEY.RZKEY_X,RZKEY.RZKEY_C,RZKEY.RZKEY_V,RZKEY.RZKEY_B,RZKEY.RZKEY_N,RZKEY.RZKEY_M,
            RZKEY.RZKEY_OEM_9,
            RZKEY.RZKEY_OEM_10,
            RZKEY.RZKEY_OEM_11,
            RZKEY.RZKEY_RSHIFT,
            RZKEY.RZKEY_UP,
            RZKEY.RZKEY_LCTRL,
            RZKEY.RZKEY_LWIN,
            RZKEY.RZKEY_LALT,
            RZKEY.RZKEY_SPACE,
            RZKEY.RZKEY_RALT,
            RZKEY.RZKEY_FN,
            RZKEY.RZKEY_RMENU,
            RZKEY.RZKEY_RCTRL,
            RZKEY.RZKEY_LEFT,
            RZKEY.RZKEY_DOWN,
            RZKEY.RZKEY_RIGHT
        };

        static RZKEY[] numPadKeys = new RZKEY[]
        {
            RZKEY.RZKEY_NUMLOCK,
            RZKEY.RZKEY_NUMPAD_DIVIDE,
            RZKEY.RZKEY_NUMPAD_MULTIPLY,
            RZKEY.RZKEY_NUMPAD_SUBTRACT,
            RZKEY.RZKEY_NUMPAD7,
            RZKEY.RZKEY_NUMPAD8,
            RZKEY.RZKEY_NUMPAD9,
            RZKEY.RZKEY_NUMPAD_ADD,
            RZKEY.RZKEY_NUMPAD4,
            RZKEY.RZKEY_NUMPAD5,
            RZKEY.RZKEY_NUMPAD6,
            RZKEY.RZKEY_NUMPAD1,
            RZKEY.RZKEY_NUMPAD2,
            RZKEY.RZKEY_NUMPAD3,
            RZKEY.RZKEY_NUMPAD_ENTER,
            RZKEY.RZKEY_NUMPAD0,
            RZKEY.RZKEY_NUMPAD_DECIMAL
        };
    }
}
