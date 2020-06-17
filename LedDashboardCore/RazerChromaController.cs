using ChromaSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ChromaSDK.Keyboard;
using static ChromaSDK.Mouse;

namespace LedDashboardCore
{


    public class RazerChromaController : LightController
    {

        // Error codes
        //! Invalid
        const long RZRESULT_INVALID = -1;
        //! Success
        const long RZRESULT_SUCCESS = 0;
        //! Access denied
        const long RZRESULT_ACCESS_DENIED = 5;
        //! Invalid handle
        const long RZRESULT_INVALID_HANDLE = 6;
        //! Not supported
        const long RZRESULT_NOT_SUPPORTED = 50;
        //! Invalid parameter.
        const long RZRESULT_INVALID_PARAMETER = 87;
        //! The service has not been started
        const long RZRESULT_SERVICE_NOT_ACTIVE = 1062;
        //! Cannot start more than one instance of the specified program.
        const long RZRESULT_SINGLE_INSTANCE_APP = 1152;
        //! Device not connected
        const long RZRESULT_DEVICE_NOT_CONNECTED = 1167;
        //! Element not found.
        const long RZRESULT_NOT_FOUND = 1168;
        //! Request aborted.
        const long RZRESULT_REQUEST_ABORTED = 1235;
        //! An attempt was made to perform an initialization operation when initialization has already been completed.
        const long RZRESULT_ALREADY_INITIALIZED = 1247;
        //! Resource not available or disabled
        const long RZRESULT_RESOURCE_DISABLED = 4309;
        //! Device not available or supported
        const long RZRESULT_DEVICE_NOT_AVAILABLE = 4319;
        //! The group or resource is not in the correct state to perform the requested operation.
        const long RZRESULT_NOT_VALID_STATE = 5023;
        //! No more items
        const long RZRESULT_NO_MORE_ITEMS = 259;
        //! DLL not found
        const long RZRESULT_DLL_NOT_FOUND = 6023;
        //! Invalid signature
        const long RZRESULT_DLL_INVALID_SIGNATURE = 6033;
        //! General failure.
        const long RZRESULT_FAILED = 2147500037;

        static RZKEY[] indexKeyMap = new RZKEY[] // TODO work with numpads!!
        {
            RZKEY.RZKEY_ESC,
            RZKEY.RZKEY_F1,RZKEY.RZKEY_F2,RZKEY.RZKEY_F3,RZKEY.RZKEY_F4,RZKEY.RZKEY_F5,RZKEY.RZKEY_F6,RZKEY.RZKEY_F7,RZKEY.RZKEY_F8,RZKEY.RZKEY_F9,RZKEY.RZKEY_F10,RZKEY.RZKEY_F11,RZKEY.RZKEY_F12,
            RZKEY.RZKEY_PRINTSCREEN,
            RZKEY.RZKEY_SCROLL,
            RZKEY.RZKEY_PAUSE,
            RZKEY.RZKEY_OEM_1, // º SPAIN
            RZKEY.RZKEY_1,RZKEY.RZKEY_2,RZKEY.RZKEY_3,RZKEY.RZKEY_4,RZKEY.RZKEY_5,RZKEY.RZKEY_6,RZKEY.RZKEY_7,RZKEY.RZKEY_8,RZKEY.RZKEY_9,RZKEY.RZKEY_0,
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

        // Mouse LED mapping
        static RZLED2[] MOUSE_LEDS = new RZLED2[]
        {
            RZLED2.RZLED2_LOGO, RZLED2.RZLED2_SCROLLWHEEL, // special handling needed to cover bottom and backlight
            RZLED2.RZLED2_LEFT_SIDE1,RZLED2.RZLED2_LEFT_SIDE2,RZLED2.RZLED2_LEFT_SIDE3,RZLED2.RZLED2_LEFT_SIDE4,RZLED2.RZLED2_LEFT_SIDE5,RZLED2.RZLED2_LEFT_SIDE6,RZLED2.RZLED2_LEFT_SIDE7,
            RZLED2.RZLED2_RIGHT_SIDE1,RZLED2.RZLED2_RIGHT_SIDE2,RZLED2.RZLED2_RIGHT_SIDE3,RZLED2.RZLED2_LEFT_SIDE4,RZLED2.RZLED2_RIGHT_SIDE5,RZLED2.RZLED2_RIGHT_SIDE6,RZLED2.RZLED2_RIGHT_SIDE7
        };

        // Bottom mouse part
        static RZLED2[] MOUSE_BOTTOM_LEDS = new RZLED2[]
        {
            RZLED2.RZLED2_SCROLLWHEEL, RZLED2.RZLED2_BOTTOM1,RZLED2.RZLED2_BOTTOM2,RZLED2.RZLED2_BOTTOM3,RZLED2.RZLED2_BOTTOM4,RZLED2.RZLED2_BOTTOM5
        };

        private int baseKeyboardAnim;
        private int baseMouseAnim;
        private int baseMousepadAnim;
        private int baseKeypadAnim;
        private int baseHeadsetAnim;
        private int baseGeneralAnim;


        private static int MAX_KEYBOARD_COLS = ChromaAnimationAPI.GetMaxColumn((int)ChromaAnimationAPI.Device2D.Keyboard);
        private static int MAX_KEYBOARD_ROWS = ChromaAnimationAPI.GetMaxRow((int)ChromaAnimationAPI.Device2D.Keyboard);
        private static int KEYBOARD_LED_COUNT = MAX_KEYBOARD_COLS * MAX_KEYBOARD_ROWS;
        private static int MOUSE_LED_COUNT = 16;

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
            int status = 0;
            try
            {
                status = ChromaAnimationAPI.Init();
                if (status != 0)
                {
                    throw new InvalidOperationException("Chroma SDK exception. Status " + status);
                }
            }
            catch (Exception e)
            {
                string errorString = "Please make sure that Razer Synapse 3 is installed and updated to the latest version.";
                switch ((long)status)
                {
                    case RZRESULT_DLL_NOT_FOUND:
                        errorString = "Couldn't find Chroma SDK. This is usually caused because Razer Synapse 3 is not installed or updated to the latest version.";
                        break;
                    case RZRESULT_SERVICE_NOT_ACTIVE:
                        errorString = "The Chroma SDK service is not active. This is usually caused because Razer Synapse VERSION 3 is not installed or updated to the latest version.";
                        break;
                }
                MessageBox.Show("Error initializing Razer Chroma controller. Status=" + status + "\n" + errorString);
                throw new InvalidOperationException("Error initializing Razer Chroma controller. Is Razer Synapse installed?", e);
            }

            baseKeyboardAnim = CreateAnimation(ChromaAnimationAPI.DeviceType.DE_2D, (int)ChromaAnimationAPI.Device2D.Keyboard);
            //ChromaAnimationAPI.AddFrame(baseKeyboardAnim, 1, new int[MAX_KEYBOARD_COLS * MAX_KEYBOARD_ROWS], MAX_KEYBOARD_COLS * MAX_KEYBOARD_ROWS);

            baseMouseAnim = CreateAnimation(ChromaAnimationAPI.DeviceType.DE_2D, (int)ChromaAnimationAPI.Device2D.Mouse);

            baseMousepadAnim = CreateAnimation(ChromaAnimationAPI.DeviceType.DE_1D, (int)ChromaAnimationAPI.Device1D.Mousepad);

            baseHeadsetAnim = CreateAnimation(ChromaAnimationAPI.DeviceType.DE_1D, (int)ChromaAnimationAPI.Device1D.Headset);

            baseKeypadAnim = CreateAnimation(ChromaAnimationAPI.DeviceType.DE_2D, (int)ChromaAnimationAPI.Device2D.Keypad);

            baseGeneralAnim = CreateAnimation(ChromaAnimationAPI.DeviceType.DE_1D, (int)ChromaAnimationAPI.Device1D.ChromaLink);


            disposed = false;
        }

        private int CreateAnimation(ChromaAnimationAPI.DeviceType deviceType, int device)
        {
            int anim = ChromaAnimationAPI.CreateAnimationInMemory((int)deviceType, device);
            if (anim == -1)
            {
                MessageBox.Show("Error initializing Razer Chroma controller. CreateAnimationInMemory returned -1. " +
                                "Check that your mouse is connected and Razer Synapse is installed");
                throw new InvalidOperationException("CreateAnimationInMemory ChromaSDK returned -1");
            }
            return anim;
        }

        public void SendData(LEDFrame frame) // TODO: Abstract away these details, make parent class
        {
            if (!enabled || disposed) return;
            LEDData data = frame.Leds;

            // KEYBOARD
            if (frame.Zones.HasFlag(LightZone.Keyboard))
                SendKeyboardData(data);

            // MOUSE
            if (frame.Zones.HasFlag(LightZone.Mouse))
                SendMouseData(data);

            // MOUSEPAD
            if (frame.Zones.HasFlag(LightZone.Mouse))
                SendMousepadData(data);

            // HEADSET
            if (frame.Zones.HasFlag(LightZone.Headset))
                SendHeadsetData(data);

            // 2D KEYPAD
            if (frame.Zones.HasFlag(LightZone.Keypad))
                SendKeypadData(data);

            // GENERAL (CHROMA LINK)
            if (frame.Zones.HasFlag(LightZone.General))
                SendGeneralData(data);

        }

        private void SendKeyboardData(LEDData data)
        {
            byte[] colorArray = data.Keyboard.ToByteArray();

            int black = ChromaAnimationAPI.GetRGB(0, 0, 0);
            foreach (Keyboard.RZKEY key in numPadKeys)
            {
                ChromaAnimationAPI.SetKeyColor(baseKeyboardAnim, 0, (int)key, black);
            }
            for (int i = 0; i < 88; i++) // TODO: NUMPAD
            {
                int color = ChromaAnimationAPI.GetRGB(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                ChromaAnimationAPI.SetKeyColor(baseKeyboardAnim, 0, (int)indexKeyMap[i], color);
            }
            ChromaAnimationAPI.PreviewFrame(baseKeyboardAnim, 0);
        }

        private void SendMouseData(LEDData data)
        {
            byte[] colorArray = data.Mouse.ToByteArray();

            for (int i = 0; i < 16; i++)
            {
                int color = ChromaAnimationAPI.GetRGB(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);

                ushort number;
                byte upper;
                byte lower;
                if (i == 0) // logo and bottom leds
                {
                    foreach (RZLED2 led in MOUSE_BOTTOM_LEDS)
                    {
                        number = Convert.ToUInt16(RZLED2.RZLED2_LOGO);
                        upper = (byte)(number >> 8);
                        lower = (byte)(number & 0xff);
                        ChromaAnimationAPI.Set2DColor(baseMouseAnim, 0, upper, lower, color);

                    }
                }
                else if (i == 1) // scrollwheel and backlight
                {
                    number = Convert.ToUInt16(RZLED2.RZLED2_SCROLLWHEEL);
                    upper = (byte)(number >> 8);
                    lower = (byte)(number & 0xff);
                    ChromaAnimationAPI.Set2DColor(baseMouseAnim, 0, upper, lower, color);

                    number = Convert.ToUInt16(RZLED2.RZLED2_BACKLIGHT);
                    upper = (byte)(number >> 8);
                    lower = (byte)(number & 0xff);
                    ChromaAnimationAPI.Set2DColor(baseMouseAnim, 0, upper, lower, color);
                }
                else
                {
                    number = Convert.ToUInt16(MOUSE_LEDS[i]);
                    upper = (byte)(number >> 8);
                    lower = (byte)(number & 0xff);
                    ChromaAnimationAPI.Set2DColor(baseMouseAnim, 0, upper, lower, color);
                }
            }
            ChromaAnimationAPI.PreviewFrame(baseMouseAnim, 0);
        }

        private void SendMousepadData(LEDData data)
        {
            byte[] colorArray = data.Mousepad.ToByteArray();

            for (int i = 0; i < 16; i++)
            {
                int color = ChromaAnimationAPI.GetRGB(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                ChromaAnimationAPI.Set1DColor(baseMousepadAnim, 0, 16-i, color); // inverted order
            }
            ChromaAnimationAPI.PreviewFrame(baseMousepadAnim, 0);
        }

        private void SendHeadsetData(LEDData data)
        {
            byte[] colorArray = data.Mousepad.ToByteArray();

            for (int i = 0; i < 2; i++)
            {
                int color = ChromaAnimationAPI.GetRGB(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                ChromaAnimationAPI.Set1DColor(baseHeadsetAnim, 0, i, color);
            }
            ChromaAnimationAPI.PreviewFrame(baseHeadsetAnim, 0);
        }

        private void SendKeypadData(LEDData data)
        {
            byte[] colorArray = data.Keypad.ToByteArray();

            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    int index = col * 5 + row;
                    int color = ChromaAnimationAPI.GetRGB(colorArray[index * 3], colorArray[index * 3 + 1], colorArray[index * 3 + 2]);
                    ChromaAnimationAPI.Set2DColor(baseKeypadAnim, 0, col, row, color);
                }
            }
            ChromaAnimationAPI.PreviewFrame(baseKeypadAnim, 0);
        }

        private void SendGeneralData(LEDData data)
        {
            byte[] colorArray = data.General.ToByteArray();

            for (int i = 0; i < 5; i++)
            {
                int color = ChromaAnimationAPI.GetRGB(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                ChromaAnimationAPI.Set1DColor(baseGeneralAnim, 0, i, color);
            }
            ChromaAnimationAPI.PreviewFrame(baseGeneralAnim, 0);
        }


        bool enabled = true;
        private bool disposed;

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
            disposed = true;
            ChromaAnimationAPI.StopAll();
            ChromaAnimationAPI.CloseAll();
            int res = ChromaAnimationAPI.Uninit();
            if (res != 0)
            {
                Debug.WriteLine("WARNING: Error disposing chroma api. Code " + res);
            }
        }

        public bool IsEnabled()
        {
            return enabled;
        }

    }
}
