#define X64

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ChromaSDK
{

    public class Mouse
    {
        // Mouse definitions
        public enum RZLED2
        {
            RZLED2_SCROLLWHEEL = 0x0203,  //!< Scroll Wheel LED.
            RZLED2_LOGO = 0x0703,  //!< Logo LED.
            RZLED2_BACKLIGHT = 0x0403,  //!< Backlight LED.
            RZLED2_LEFT_SIDE1 = 0x0100,  //!< Left LED 1.
            RZLED2_LEFT_SIDE2 = 0x0200,  //!< Left LED 2.
            RZLED2_LEFT_SIDE3 = 0x0300,  //!< Left LED 3.
            RZLED2_LEFT_SIDE4 = 0x0400,  //!< Left LED 4.
            RZLED2_LEFT_SIDE5 = 0x0500,  //!< Left LED 5.
            RZLED2_LEFT_SIDE6 = 0x0600,  //!< Left LED 6.
            RZLED2_LEFT_SIDE7 = 0x0700,  //!< Left LED 7.
            RZLED2_BOTTOM1 = 0x0801,  //!< Bottom LED 1.
            RZLED2_BOTTOM2 = 0x0802,  //!< Bottom LED 2.
            RZLED2_BOTTOM3 = 0x0803,  //!< Bottom LED 3.
            RZLED2_BOTTOM4 = 0x0804,  //!< Bottom LED 4.
            RZLED2_BOTTOM5 = 0x0805,  //!< Bottom LED 5.
            RZLED2_RIGHT_SIDE1 = 0x0106,  //!< Right LED 1.
            RZLED2_RIGHT_SIDE2 = 0x0206,  //!< Right LED 2.
            RZLED2_RIGHT_SIDE3 = 0x0306,  //!< Right LED 3.
            RZLED2_RIGHT_SIDE4 = 0x0406,  //!< Right LED 4.
            RZLED2_RIGHT_SIDE5 = 0x0506,  //!< Right LED 5.
            RZLED2_RIGHT_SIDE6 = 0x0606,  //!< Right LED 6.
            RZLED2_RIGHT_SIDE7 = 0x0706   //!< Right LED 7.
        }
    }

    public class Keyboard
    {

        //! Definitions of keys.
        public enum RZKEY
        {
            RZKEY_ESC = 0x0001,                 /*!< Esc (VK_ESCAPE) */
            RZKEY_F1 = 0x0003,                  /*!< F1 (VK_F1) */
            RZKEY_F2 = 0x0004,                  /*!< F2 (VK_F2) */
            RZKEY_F3 = 0x0005,                  /*!< F3 (VK_F3) */
            RZKEY_F4 = 0x0006,                  /*!< F4 (VK_F4) */
            RZKEY_F5 = 0x0007,                  /*!< F5 (VK_F5) */
            RZKEY_F6 = 0x0008,                  /*!< F6 (VK_F6) */
            RZKEY_F7 = 0x0009,                  /*!< F7 (VK_F7) */
            RZKEY_F8 = 0x000A,                  /*!< F8 (VK_F8) */
            RZKEY_F9 = 0x000B,                  /*!< F9 (VK_F9) */
            RZKEY_F10 = 0x000C,                 /*!< F10 (VK_F10) */
            RZKEY_F11 = 0x000D,                 /*!< F11 (VK_F11) */
            RZKEY_F12 = 0x000E,                 /*!< F12 (VK_F12) */
            RZKEY_1 = 0x0102,                   /*!< 1 (VK_1) */
            RZKEY_2 = 0x0103,                   /*!< 2 (VK_2) */
            RZKEY_3 = 0x0104,                   /*!< 3 (VK_3) */
            RZKEY_4 = 0x0105,                   /*!< 4 (VK_4) */
            RZKEY_5 = 0x0106,                   /*!< 5 (VK_5) */
            RZKEY_6 = 0x0107,                   /*!< 6 (VK_6) */
            RZKEY_7 = 0x0108,                   /*!< 7 (VK_7) */
            RZKEY_8 = 0x0109,                   /*!< 8 (VK_8) */
            RZKEY_9 = 0x010A,                   /*!< 9 (VK_9) */
            RZKEY_0 = 0x010B,                   /*!< 0 (VK_0) */
            RZKEY_A = 0x0302,                   /*!< A (VK_A) */
            RZKEY_B = 0x0407,                   /*!< B (VK_B) */
            RZKEY_C = 0x0405,                   /*!< C (VK_C) */
            RZKEY_D = 0x0304,                   /*!< D (VK_D) */
            RZKEY_E = 0x0204,                   /*!< E (VK_E) */
            RZKEY_F = 0x0305,                   /*!< F (VK_F) */
            RZKEY_G = 0x0306,                   /*!< G (VK_G) */
            RZKEY_H = 0x0307,                   /*!< H (VK_H) */
            RZKEY_I = 0x0209,                   /*!< I (VK_I) */
            RZKEY_J = 0x0308,                   /*!< J (VK_J) */
            RZKEY_K = 0x0309,                   /*!< K (VK_K) */
            RZKEY_L = 0x030A,                   /*!< L (VK_L) */
            RZKEY_M = 0x0409,                   /*!< M (VK_M) */
            RZKEY_N = 0x0408,                   /*!< N (VK_N) */
            RZKEY_O = 0x020A,                   /*!< O (VK_O) */
            RZKEY_P = 0x020B,                   /*!< P (VK_P) */
            RZKEY_Q = 0x0202,                   /*!< Q (VK_Q) */
            RZKEY_R = 0x0205,                   /*!< R (VK_R) */
            RZKEY_S = 0x0303,                   /*!< S (VK_S) */
            RZKEY_T = 0x0206,                   /*!< T (VK_T) */
            RZKEY_U = 0x0208,                   /*!< U (VK_U) */
            RZKEY_V = 0x0406,                   /*!< V (VK_V) */
            RZKEY_W = 0x0203,                   /*!< W (VK_W) */
            RZKEY_X = 0x0404,                   /*!< X (VK_X) */
            RZKEY_Y = 0x0207,                   /*!< Y (VK_Y) */
            RZKEY_Z = 0x0403,                   /*!< Z (VK_Z) */
            RZKEY_NUMLOCK = 0x0112,             /*!< Numlock (VK_NUMLOCK) */
            RZKEY_NUMPAD0 = 0x0513,             /*!< Numpad 0 (VK_NUMPAD0) */
            RZKEY_NUMPAD1 = 0x0412,             /*!< Numpad 1 (VK_NUMPAD1) */
            RZKEY_NUMPAD2 = 0x0413,             /*!< Numpad 2 (VK_NUMPAD2) */
            RZKEY_NUMPAD3 = 0x0414,             /*!< Numpad 3 (VK_NUMPAD3) */
            RZKEY_NUMPAD4 = 0x0312,             /*!< Numpad 4 (VK_NUMPAD4) */
            RZKEY_NUMPAD5 = 0x0313,             /*!< Numpad 5 (VK_NUMPAD5) */
            RZKEY_NUMPAD6 = 0x0314,             /*!< Numpad 6 (VK_NUMPAD6) */
            RZKEY_NUMPAD7 = 0x0212,             /*!< Numpad 7 (VK_NUMPAD7) */
            RZKEY_NUMPAD8 = 0x0213,             /*!< Numpad 8 (VK_NUMPAD8) */
            RZKEY_NUMPAD9 = 0x0214,             /*!< Numpad 9 (VK_ NUMPAD9*/
            RZKEY_NUMPAD_DIVIDE = 0x0113,       /*!< Divide (VK_DIVIDE) */
            RZKEY_NUMPAD_MULTIPLY = 0x0114,     /*!< Multiply (VK_MULTIPLY) */
            RZKEY_NUMPAD_SUBTRACT = 0x0115,     /*!< Subtract (VK_SUBTRACT) */
            RZKEY_NUMPAD_ADD = 0x0215,          /*!< Add (VK_ADD) */
            RZKEY_NUMPAD_ENTER = 0x0415,        /*!< Enter (VK_RETURN - Extended) */
            RZKEY_NUMPAD_DECIMAL = 0x0514,      /*!< Decimal (VK_DECIMAL) */
            RZKEY_PRINTSCREEN = 0x000F,         /*!< Print Screen (VK_PRINT) */
            RZKEY_SCROLL = 0x0010,              /*!< Scroll Lock (VK_SCROLL) */
            RZKEY_PAUSE = 0x0011,               /*!< Pause (VK_PAUSE) */
            RZKEY_INSERT = 0x010F,              /*!< Insert (VK_INSERT) */
            RZKEY_HOME = 0x0110,                /*!< Home (VK_HOME) */
            RZKEY_PAGEUP = 0x0111,              /*!< Page Up (VK_PRIOR) */
            RZKEY_DELETE = 0x020f,              /*!< Delete (VK_DELETE) */
            RZKEY_END = 0x0210,                 /*!< End (VK_END) */
            RZKEY_PAGEDOWN = 0x0211,            /*!< Page Down (VK_NEXT) */
            RZKEY_UP = 0x0410,                  /*!< Up (VK_UP) */
            RZKEY_LEFT = 0x050F,                /*!< Left (VK_LEFT) */
            RZKEY_DOWN = 0x0510,                /*!< Down (VK_DOWN) */
            RZKEY_RIGHT = 0x0511,               /*!< Right (VK_RIGHT) */
            RZKEY_TAB = 0x0201,                 /*!< Tab (VK_TAB) */
            RZKEY_CAPSLOCK = 0x0301,            /*!< Caps Lock(VK_CAPITAL) */
            RZKEY_BACKSPACE = 0x010E,           /*!< Backspace (VK_BACK) */
            RZKEY_ENTER = 0x030E,               /*!< Enter (VK_RETURN) */
            RZKEY_LCTRL = 0x0501,               /*!< Left Control(VK_LCONTROL) */
            RZKEY_LWIN = 0x0502,                /*!< Left Window (VK_LWIN) */
            RZKEY_LALT = 0x0503,                /*!< Left Alt (VK_LMENU) */
            RZKEY_SPACE = 0x0507,               /*!< Spacebar (VK_SPACE) */
            RZKEY_RALT = 0x050B,                /*!< Right Alt (VK_RMENU) */
            RZKEY_FN = 0x050C,                  /*!< Function key. */
            RZKEY_RMENU = 0x050D,               /*!< Right Menu (VK_APPS) */
            RZKEY_RCTRL = 0x050E,               /*!< Right Control (VK_RCONTROL) */
            RZKEY_LSHIFT = 0x0401,              /*!< Left Shift (VK_LSHIFT) */
            RZKEY_RSHIFT = 0x040E,              /*!< Right Shift (VK_RSHIFT) */
            RZKEY_MACRO1 = 0x0100,              /*!< Macro Key 1 */
            RZKEY_MACRO2 = 0x0200,              /*!< Macro Key 2 */
            RZKEY_MACRO3 = 0x0300,              /*!< Macro Key 3 */
            RZKEY_MACRO4 = 0x0400,              /*!< Macro Key 4 */
            RZKEY_MACRO5 = 0x0500,              /*!< Macro Key 5 */
            RZKEY_OEM_1 = 0x0101,               /*!< ~ (tilde/半角/全角) (VK_OEM_3) */
            RZKEY_OEM_2 = 0x010C,               /*!< -- (minus) (VK_OEM_MINUS) */
            RZKEY_OEM_3 = 0x010D,               /*!< = (equal) (VK_OEM_PLUS) */
            RZKEY_OEM_4 = 0x020C,               /*!< [ (left sqaure bracket) (VK_OEM_4) */
            RZKEY_OEM_5 = 0x020D,               /*!< ] (right square bracket) (VK_OEM_6) */
            RZKEY_OEM_6 = 0x020E,               /*!< \ (backslash) (VK_OEM_5) */
            RZKEY_OEM_7 = 0x030B,               /*!< ; (semi-colon) (VK_OEM_1) */
            RZKEY_OEM_8 = 0x030C,               /*!< ' (apostrophe) (VK_OEM_7) */
            RZKEY_OEM_9 = 0x040A,               /*!< , (comma) (VK_OEM_COMMA) */
            RZKEY_OEM_10 = 0x040B,              /*!< . (period) (VK_OEM_PERIOD) */
            RZKEY_OEM_11 = 0x040C,              /*!< / (forward slash) (VK_OEM_2) */
            RZKEY_EUR_1 = 0x030D,               /*!< "#" (VK_OEM_5) */
            RZKEY_EUR_2 = 0x0402,               /*!< \ (VK_OEM_102) */
            RZKEY_JPN_1 = 0x0015,               /*!< ¥ (0xFF) */
            RZKEY_JPN_2 = 0x040D,               /*!< \ (0xC1) */
            RZKEY_JPN_3 = 0x0504,               /*!< 無変換 (VK_OEM_PA1) */
            RZKEY_JPN_4 = 0x0509,               /*!< 変換 (0xFF) */
            RZKEY_JPN_5 = 0x050A,               /*!< ひらがな/カタカナ (0xFF) */
            RZKEY_KOR_1 = 0x0015,               /*!< | (0xFF) */
            RZKEY_KOR_2 = 0x030D,               /*!< (VK_OEM_5) */
            RZKEY_KOR_3 = 0x0402,               /*!< (VK_OEM_102) */
            RZKEY_KOR_4 = 0x040D,               /*!< (0xC1) */
            RZKEY_KOR_5 = 0x0504,               /*!< (VK_OEM_PA1) */
            RZKEY_KOR_6 = 0x0509,               /*!< 한/영 (0xFF) */
            RZKEY_KOR_7 = 0x050A,               /*!< (0xFF) */
            RZKEY_INVALID = 0xFFFF              /*!< Invalid keys. */
        }

        //! Definition of LEDs.
        public enum RZLED
        {
            RZLED_LOGO = 0x0014                 /*!< Razer logo */
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FChromaSDKGuid
    {
        Guid Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEVICE_INFO_TYPE
    {
        int DeviceType;
        uint Connected;
    }

    public enum EFFECT_TYPE
    {
        CHROMA_NONE = 0,            //!< No effect.
        CHROMA_WAVE,                //!< Wave effect (This effect type has deprecated and should not be used).
        CHROMA_SPECTRUMCYCLING,     //!< Spectrum cycling effect (This effect type has deprecated and should not be used).
        CHROMA_BREATHING,           //!< Breathing effect (This effect type has deprecated and should not be used).
        CHROMA_BLINKING,            //!< Blinking effect (This effect type has deprecated and should not be used).
        CHROMA_REACTIVE,            //!< Reactive effect (This effect type has deprecated and should not be used).
        CHROMA_STATIC,              //!< Static effect.
        CHROMA_CUSTOM,              //!< Custom effect. For mice, please see Mouse::CHROMA_CUSTOM2.
        CHROMA_RESERVED,            //!< Reserved
        CHROMA_INVALID              //!< Invalid effect.
    }

    public class ChromaAnimationAPI
    {

#if X64
        const string DLL_NAME = "CChromaEditorLibrary64";
#else
        const string DLL_NAME = "CChromaEditorLibrary";
#endif

        #region Data Structures

        public enum DeviceType
        {
            Invalid = -1,
            DE_1D = 0,
            DE_2D = 1,
            MAX = 2,
        }

        public enum Device
        {
            Invalid = -1,
            ChromaLink = 0,
            Headset = 1,
            Keyboard = 2,
            Keypad = 3,
            Mouse = 4,
            Mousepad = 5,
            MAX = 6,
        }

        public enum Device1D
        {
            Invalid = -1,
            ChromaLink = 0,
            Headset = 1,
            Mousepad = 2,
            MAX = 3,
        }

        public enum Device2D
        {
            Invalid = -1,
            Keyboard = 0,
            Keypad = 1,
            Mouse = 2,
            MAX = 3,
        }


        #endregion

        #region Helpers (handle path conversions)

        /// <summary>
        /// Helper to convert string to IntPtr
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static IntPtr GetIntPtr(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return IntPtr.Zero;
            }
            FileInfo fi = new FileInfo(path);
            byte[] array = ASCIIEncoding.ASCII.GetBytes(fi.FullName + "\0");
            IntPtr lpData = Marshal.AllocHGlobal(array.Length);
            Marshal.Copy(array, 0, lpData, array.Length);
            return lpData;
        }

        /// <summary>
        /// Helper to recycle the IntPtr
        /// </summary>
        /// <param name="lpData"></param>
        private static void FreeIntPtr(IntPtr lpData)
        {
            if (lpData != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(lpData);
            }
        }
        #endregion

        #region Public API Methods
        /// <summary>
        /// Adds a frame to the `Chroma` animation and sets the `duration` (in seconds). 
        /// The `color` is expected to be an array of the dimensions for the `deviceType/device`. 
        /// The `length` parameter is the size of the `color` array. For `EChromaSDKDevice1DEnum` 
        /// the array size should be `MAX LEDS`. For `EChromaSDKDevice2DEnum` the array 
        /// size should be `MAX ROW` * `MAX COLUMN`. Returns the animation id upon 
        /// success. Returns -1 upon failure.
        /// </summary>
        public static int AddFrame(int animationId, float duration, int[] colors, int length)
        {
            int result = PluginAddFrame(animationId, duration, colors, length);
            return result;
        }
        /// <summary>
        /// Add source color to target where color is not black for all frames, reference 
        /// source and target by id.
        /// </summary>
        public static void AddNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginAddNonZeroAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Add source color to target where color is not black for all frames, reference 
        /// source and target by name.
        /// </summary>
        public static void AddNonZeroAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginAddNonZeroAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double AddNonZeroAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginAddNonZeroAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Add source color to target where color is not black for all frames starting 
        /// at offset for the length of the source, reference source and target by 
        /// id.
        /// </summary>
        public static void AddNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset)
        {
            PluginAddNonZeroAllKeysAllFramesOffset(sourceAnimationId, targetAnimationId, offset);
        }
        /// <summary>
        /// Add source color to target where color is not black for all frames starting 
        /// at offset for the length of the source, reference source and target by 
        /// name.
        /// </summary>
        public static void AddNonZeroAllKeysAllFramesOffsetName(string sourceAnimation, string targetAnimation, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginAddNonZeroAllKeysAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double AddNonZeroAllKeysAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginAddNonZeroAllKeysAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Add source color to target where color is not black for the source frame 
        /// and target offset frame, reference source and target by id.
        /// </summary>
        public static void AddNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset)
        {
            PluginAddNonZeroAllKeysOffset(sourceAnimationId, targetAnimationId, frameId, offset);
        }
        /// <summary>
        /// Add source color to target where color is not black for the source frame 
        /// and target offset frame, reference source and target by name.
        /// </summary>
        public static void AddNonZeroAllKeysOffsetName(string sourceAnimation, string targetAnimation, int frameId, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginAddNonZeroAllKeysOffsetName(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double AddNonZeroAllKeysOffsetNameD(string sourceAnimation, string targetAnimation, double frameId, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginAddNonZeroAllKeysOffsetNameD(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Add source color to target where the target color is not black for all frames, 
        /// reference source and target by id.
        /// </summary>
        public static void AddNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginAddNonZeroTargetAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Add source color to target where the target color is not black for all frames, 
        /// reference source and target by name.
        /// </summary>
        public static void AddNonZeroTargetAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginAddNonZeroTargetAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double AddNonZeroTargetAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginAddNonZeroTargetAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Add source color to target where the target color is not black for all frames 
        /// starting at offset for the length of the source, reference source and target 
        /// by id.
        /// </summary>
        public static void AddNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset)
        {
            PluginAddNonZeroTargetAllKeysAllFramesOffset(sourceAnimationId, targetAnimationId, offset);
        }
        /// <summary>
        /// Add source color to target where the target color is not black for all frames 
        /// starting at offset for the length of the source, reference source and target 
        /// by name.
        /// </summary>
        public static void AddNonZeroTargetAllKeysAllFramesOffsetName(string sourceAnimation, string targetAnimation, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginAddNonZeroTargetAllKeysAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double AddNonZeroTargetAllKeysAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginAddNonZeroTargetAllKeysAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Add source color to target where target color is not blank from the source 
        /// frame to the target offset frame, reference source and target by id.
        /// </summary>
        public static void AddNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset)
        {
            PluginAddNonZeroTargetAllKeysOffset(sourceAnimationId, targetAnimationId, frameId, offset);
        }
        /// <summary>
        /// Add source color to target where target color is not blank from the source 
        /// frame to the target offset frame, reference source and target by name. 
        ///
        /// </summary>
        public static void AddNonZeroTargetAllKeysOffsetName(string sourceAnimation, string targetAnimation, int frameId, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginAddNonZeroTargetAllKeysOffsetName(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double AddNonZeroTargetAllKeysOffsetNameD(string sourceAnimation, string targetAnimation, double frameId, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginAddNonZeroTargetAllKeysOffsetNameD(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Append all source frames to the target animation, reference source and target 
        /// by id.
        /// </summary>
        public static void AppendAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginAppendAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Append all source frames to the target animation, reference source and target 
        /// by name.
        /// </summary>
        public static void AppendAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginAppendAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double AppendAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginAppendAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// `PluginClearAll` will issue a `CLEAR` effect for all devices.
        /// </summary>
        public static void ClearAll()
        {
            PluginClearAll();
        }
        /// <summary>
        /// `PluginClearAnimationType` will issue a `CLEAR` effect for the given device. 
        ///
        /// </summary>
        public static void ClearAnimationType(int deviceType, int device)
        {
            PluginClearAnimationType(deviceType, device);
        }
        /// <summary>
        /// `PluginCloseAll` closes all open animations so they can be reloaded from 
        /// disk. The set of animations will be stopped if playing.
        /// </summary>
        public static void CloseAll()
        {
            PluginCloseAll();
        }
        /// <summary>
        /// Closes the `Chroma` animation to free up resources referenced by id. Returns 
        /// the animation id upon success. Returns -1 upon failure. This might be used 
        /// while authoring effects if there was a change necessitating re-opening 
        /// the animation. The animation id can no longer be used once closed.
        /// </summary>
        public static int CloseAnimation(int animationId)
        {
            int result = PluginCloseAnimation(animationId);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CloseAnimationD(double animationId)
        {
            double result = PluginCloseAnimationD(animationId);
            return result;
        }
        /// <summary>
        /// Closes the `Chroma` animation referenced by name so that the animation can 
        /// be reloaded from disk.
        /// </summary>
        public static void CloseAnimationName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginCloseAnimationName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CloseAnimationNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginCloseAnimationNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// `PluginCloseComposite` closes a set of animations so they can be reloaded 
        /// from disk. The set of animations will be stopped if playing.
        /// </summary>
        public static void CloseComposite(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            PluginCloseComposite(lpName);
            FreeIntPtr(lpName);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CloseCompositeD(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            double result = PluginCloseCompositeD(lpName);
            FreeIntPtr(lpName);
            return result;
        }
        /// <summary>
        /// Copy animation to named target animation in memory. If target animation 
        /// exists, close first. Source is referenced by id.
        /// </summary>
        public static int CopyAnimation(int sourceAnimationId, string targetAnimation)
        {
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            int result = PluginCopyAnimation(sourceAnimationId, lpTargetAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy animation to named target animation in memory. If target animation 
        /// exists, close first. Source is referenced by name.
        /// </summary>
        public static void CopyAnimationName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyAnimationName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyAnimationNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyAnimationNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy blue channel to other channels for all frames. Intensity range is 0.0 
        /// to 1.0. Reference the animation by id.
        /// </summary>
        public static void CopyBlueChannelAllFrames(int animationId, float redIntensity, float greenIntensity)
        {
            PluginCopyBlueChannelAllFrames(animationId, redIntensity, greenIntensity);
        }
        /// <summary>
        /// Copy blue channel to other channels for all frames. Intensity range is 0.0 
        /// to 1.0. Reference the animation by name.
        /// </summary>
        public static void CopyBlueChannelAllFramesName(string path, float redIntensity, float greenIntensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginCopyBlueChannelAllFramesName(lpPath, redIntensity, greenIntensity);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyBlueChannelAllFramesNameD(string path, double redIntensity, double greenIntensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginCopyBlueChannelAllFramesNameD(lpPath, redIntensity, greenIntensity);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Copy green channel to other channels for all frames. Intensity range is 
        /// 0.0 to 1.0. Reference the animation by id.
        /// </summary>
        public static void CopyGreenChannelAllFrames(int animationId, float redIntensity, float blueIntensity)
        {
            PluginCopyGreenChannelAllFrames(animationId, redIntensity, blueIntensity);
        }
        /// <summary>
        /// Copy green channel to other channels for all frames. Intensity range is 
        /// 0.0 to 1.0. Reference the animation by name.
        /// </summary>
        public static void CopyGreenChannelAllFramesName(string path, float redIntensity, float blueIntensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginCopyGreenChannelAllFramesName(lpPath, redIntensity, blueIntensity);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyGreenChannelAllFramesNameD(string path, double redIntensity, double blueIntensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginCopyGreenChannelAllFramesNameD(lpPath, redIntensity, blueIntensity);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame. Reference the source and target by id.
        /// </summary>
        public static void CopyKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey)
        {
            PluginCopyKeyColor(sourceAnimationId, targetAnimationId, frameId, rzkey);
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames. Reference the source and target by id.
        /// </summary>
        public static void CopyKeyColorAllFrames(int sourceAnimationId, int targetAnimationId, int rzkey)
        {
            PluginCopyKeyColorAllFrames(sourceAnimationId, targetAnimationId, rzkey);
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames. Reference the source and target by name.
        /// </summary>
        public static void CopyKeyColorAllFramesName(string sourceAnimation, string targetAnimation, int rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyKeyColorAllFramesName(lpSourceAnimation, lpTargetAnimation, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyKeyColorAllFramesNameD(string sourceAnimation, string targetAnimation, double rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyKeyColorAllFramesNameD(lpSourceAnimation, lpTargetAnimation, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames, starting at the offset for the length of the source animation. 
        /// Source and target are referenced by id.
        /// </summary>
        public static void CopyKeyColorAllFramesOffset(int sourceAnimationId, int targetAnimationId, int rzkey, int offset)
        {
            PluginCopyKeyColorAllFramesOffset(sourceAnimationId, targetAnimationId, rzkey, offset);
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames, starting at the offset for the length of the source animation. 
        /// Source and target are referenced by name.
        /// </summary>
        public static void CopyKeyColorAllFramesOffsetName(string sourceAnimation, string targetAnimation, int rzkey, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyKeyColorAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, rzkey, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyKeyColorAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double rzkey, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyKeyColorAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, rzkey, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame.
        /// </summary>
        public static void CopyKeyColorName(string sourceAnimation, string targetAnimation, int frameId, int rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyKeyColorName(lpSourceAnimation, lpTargetAnimation, frameId, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyKeyColorNameD(string sourceAnimation, string targetAnimation, double frameId, double rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyKeyColorNameD(lpSourceAnimation, lpTargetAnimation, frameId, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for the given frame. Reference the source and target by 
        /// id.
        /// </summary>
        public static void CopyKeysColor(int sourceAnimationId, int targetAnimationId, int frameId, int[] keys, int size)
        {
            PluginCopyKeysColor(sourceAnimationId, targetAnimationId, frameId, keys, size);
        }
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for all frames. Reference the source and target by id. 
        ///
        /// </summary>
        public static void CopyKeysColorAllFrames(int sourceAnimationId, int targetAnimationId, int[] keys, int size)
        {
            PluginCopyKeysColorAllFrames(sourceAnimationId, targetAnimationId, keys, size);
        }
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for all frames. Reference the source and target by name. 
        ///
        /// </summary>
        public static void CopyKeysColorAllFramesName(string sourceAnimation, string targetAnimation, int[] keys, int size)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyKeysColorAllFramesName(lpSourceAnimation, lpTargetAnimation, keys, size);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for the given frame. Reference the source and target by 
        /// name.
        /// </summary>
        public static void CopyKeysColorName(string sourceAnimation, string targetAnimation, int frameId, int[] keys, int size)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyKeysColorName(lpSourceAnimation, lpTargetAnimation, frameId, keys, size);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation from the source frame to the target frame. Reference the 
        /// source and target by id.
        /// </summary>
        public static void CopyKeysColorOffset(int sourceAnimationId, int targetAnimationId, int sourceFrameId, int targetFrameId, int[] keys, int size)
        {
            PluginCopyKeysColorOffset(sourceAnimationId, targetAnimationId, sourceFrameId, targetFrameId, keys, size);
        }
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation from the source frame to the target frame. Reference the 
        /// source and target by name.
        /// </summary>
        public static void CopyKeysColorOffsetName(string sourceAnimation, string targetAnimation, int sourceFrameId, int targetFrameId, int[] keys, int size)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyKeysColorOffsetName(lpSourceAnimation, lpTargetAnimation, sourceFrameId, targetFrameId, keys, size);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// Copy source animation to target animation for the given frame. Source and 
        /// target are referenced by id.
        /// </summary>
        public static void CopyNonZeroAllKeys(int sourceAnimationId, int targetAnimationId, int frameId)
        {
            PluginCopyNonZeroAllKeys(sourceAnimationId, targetAnimationId, frameId);
        }
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames. Reference source and target by id.
        /// </summary>
        public static void CopyNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginCopyNonZeroAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames. Reference source and target by name.
        /// </summary>
        public static void CopyNonZeroAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames starting at the offset for the length of the source animation. The 
        /// source and target are referenced by id.
        /// </summary>
        public static void CopyNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset)
        {
            PluginCopyNonZeroAllKeysAllFramesOffset(sourceAnimationId, targetAnimationId, offset);
        }
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames starting at the offset for the length of the source animation. The 
        /// source and target are referenced by name.
        /// </summary>
        public static void CopyNonZeroAllKeysAllFramesOffsetName(string sourceAnimation, string targetAnimation, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroAllKeysAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroAllKeysAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroAllKeysAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from source animation to target animation for the specified 
        /// frame. Source and target are referenced by id.
        /// </summary>
        public static void CopyNonZeroAllKeysName(string sourceAnimation, string targetAnimation, int frameId)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroAllKeysName(lpSourceAnimation, lpTargetAnimation, frameId);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroAllKeysNameD(string sourceAnimation, string targetAnimation, double frameId)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroAllKeysNameD(lpSourceAnimation, lpTargetAnimation, frameId);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation from 
        /// the source frame to the target offset frame. Source and target are referenced 
        /// by id.
        /// </summary>
        public static void CopyNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset)
        {
            PluginCopyNonZeroAllKeysOffset(sourceAnimationId, targetAnimationId, frameId, offset);
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation from 
        /// the source frame to the target offset frame. Source and target are referenced 
        /// by name.
        /// </summary>
        public static void CopyNonZeroAllKeysOffsetName(string sourceAnimation, string targetAnimation, int frameId, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroAllKeysOffsetName(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroAllKeysOffsetNameD(string sourceAnimation, string targetAnimation, double frameId, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroAllKeysOffsetNameD(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame where color is not zero.
        /// </summary>
        public static void CopyNonZeroKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey)
        {
            PluginCopyNonZeroKeyColor(sourceAnimationId, targetAnimationId, frameId, rzkey);
        }
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame where color is not zero.
        /// </summary>
        public static void CopyNonZeroKeyColorName(string sourceAnimation, string targetAnimation, int frameId, int rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroKeyColorName(lpSourceAnimation, lpTargetAnimation, frameId, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroKeyColorNameD(string sourceAnimation, string targetAnimation, double frameId, double rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroKeyColorNameD(lpSourceAnimation, lpTargetAnimation, frameId, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified frame. Source and target 
        /// are referenced by id.
        /// </summary>
        public static void CopyNonZeroTargetAllKeys(int sourceAnimationId, int targetAnimationId, int frameId)
        {
            PluginCopyNonZeroTargetAllKeys(sourceAnimationId, targetAnimationId, frameId);
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames. Source and target are referenced 
        /// by id.
        /// </summary>
        public static void CopyNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginCopyNonZeroTargetAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames. Source and target are referenced 
        /// by name.
        /// </summary>
        public static void CopyNonZeroTargetAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroTargetAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroTargetAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroTargetAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames. Source and target are referenced 
        /// by name.
        /// </summary>
        public static void CopyNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset)
        {
            PluginCopyNonZeroTargetAllKeysAllFramesOffset(sourceAnimationId, targetAnimationId, offset);
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames starting at the target offset 
        /// for the length of the source animation. Source and target animations are 
        /// referenced by name.
        /// </summary>
        public static void CopyNonZeroTargetAllKeysAllFramesOffsetName(string sourceAnimation, string targetAnimation, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroTargetAllKeysAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroTargetAllKeysAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroTargetAllKeysAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified frame. The source and target 
        /// are referenced by name.
        /// </summary>
        public static void CopyNonZeroTargetAllKeysName(string sourceAnimation, string targetAnimation, int frameId)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroTargetAllKeysName(lpSourceAnimation, lpTargetAnimation, frameId);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroTargetAllKeysNameD(string sourceAnimation, string targetAnimation, double frameId)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroTargetAllKeysNameD(lpSourceAnimation, lpTargetAnimation, frameId);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified source frame and target offset 
        /// frame. The source and target are referenced by id.
        /// </summary>
        public static void CopyNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset)
        {
            PluginCopyNonZeroTargetAllKeysOffset(sourceAnimationId, targetAnimationId, frameId, offset);
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified source frame and target offset 
        /// frame. The source and target are referenced by name.
        /// </summary>
        public static void CopyNonZeroTargetAllKeysOffsetName(string sourceAnimation, string targetAnimation, int frameId, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroTargetAllKeysOffsetName(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroTargetAllKeysOffsetNameD(string sourceAnimation, string targetAnimation, double frameId, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroTargetAllKeysOffsetNameD(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is zero for all frames. Source and target are referenced 
        /// by id.
        /// </summary>
        public static void CopyNonZeroTargetZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginCopyNonZeroTargetZeroAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is zero for all frames. Source and target are referenced 
        /// by name.
        /// </summary>
        public static void CopyNonZeroTargetZeroAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyNonZeroTargetZeroAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyNonZeroTargetZeroAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyNonZeroTargetZeroAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy red channel to other channels for all frames. Intensity range is 0.0 
        /// to 1.0. Reference the animation by id.
        /// </summary>
        public static void CopyRedChannelAllFrames(int animationId, float greenIntensity, float blueIntensity)
        {
            PluginCopyRedChannelAllFrames(animationId, greenIntensity, blueIntensity);
        }
        /// <summary>
        /// Copy green channel to other channels for all frames. Intensity range is 
        /// 0.0 to 1.0. Reference the animation by name.
        /// </summary>
        public static void CopyRedChannelAllFramesName(string path, float greenIntensity, float blueIntensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginCopyRedChannelAllFramesName(lpPath, greenIntensity, blueIntensity);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyRedChannelAllFramesNameD(string path, double greenIntensity, double blueIntensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginCopyRedChannelAllFramesNameD(lpPath, greenIntensity, blueIntensity);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames. 
        /// Source and target are referenced by id.
        /// </summary>
        public static void CopyZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginCopyZeroAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames. 
        /// Source and target are referenced by name.
        /// </summary>
        public static void CopyZeroAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyZeroAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyZeroAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyZeroAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames 
        /// starting at the target offset for the length of the source animation. Source 
        /// and target are referenced by id.
        /// </summary>
        public static void CopyZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset)
        {
            PluginCopyZeroAllKeysAllFramesOffset(sourceAnimationId, targetAnimationId, offset);
        }
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames 
        /// starting at the target offset for the length of the source animation. Source 
        /// and target are referenced by name.
        /// </summary>
        public static void CopyZeroAllKeysAllFramesOffsetName(string sourceAnimation, string targetAnimation, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyZeroAllKeysAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyZeroAllKeysAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyZeroAllKeysAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy zero key color from source animation to target animation for the specified 
        /// frame. Source and target are referenced by id.
        /// </summary>
        public static void CopyZeroKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey)
        {
            PluginCopyZeroKeyColor(sourceAnimationId, targetAnimationId, frameId, rzkey);
        }
        /// <summary>
        /// Copy zero key color from source animation to target animation for the specified 
        /// frame. Source and target are referenced by name.
        /// </summary>
        public static void CopyZeroKeyColorName(string sourceAnimation, string targetAnimation, int frameId, int rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyZeroKeyColorName(lpSourceAnimation, lpTargetAnimation, frameId, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyZeroKeyColorNameD(string sourceAnimation, string targetAnimation, double frameId, double rzkey)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyZeroKeyColorNameD(lpSourceAnimation, lpTargetAnimation, frameId, rzkey);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Copy nonzero color from source animation to target animation where target 
        /// is zero for all frames. Source and target are referenced by id.
        /// </summary>
        public static void CopyZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginCopyZeroTargetAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Copy nonzero color from source animation to target animation where target 
        /// is zero for all frames. Source and target are referenced by name.
        /// </summary>
        public static void CopyZeroTargetAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginCopyZeroTargetAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double CopyZeroTargetAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginCopyZeroTargetAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreCreateChromaLinkEffect(int Effect, IntPtr pParam, out Guid pEffectId)
        {
            int result = PluginCoreCreateChromaLinkEffect(Effect, pParam, out pEffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreCreateEffect(Guid DeviceId, EFFECT_TYPE Effect, IntPtr pParam, out Guid pEffectId)
        {
            int result = PluginCoreCreateEffect(DeviceId, (int)Effect, pParam, out pEffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreCreateHeadsetEffect(int Effect, IntPtr pParam, out Guid pEffectId)
        {
            int result = PluginCoreCreateHeadsetEffect(Effect, pParam, out pEffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreCreateKeyboardEffect(int Effect, IntPtr pParam, out Guid pEffectId)
        {
            int result = PluginCoreCreateKeyboardEffect(Effect, pParam, out pEffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreCreateKeypadEffect(int Effect, IntPtr pParam, out Guid pEffectId)
        {
            int result = PluginCoreCreateKeypadEffect(Effect, pParam, out pEffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreCreateMouseEffect(int Effect, IntPtr pParam, out Guid pEffectId)
        {
            int result = PluginCoreCreateMouseEffect(Effect, pParam, out pEffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreCreateMousepadEffect(int Effect, IntPtr pParam, out Guid pEffectId)
        {
            int result = PluginCoreCreateMousepadEffect(Effect, pParam, out pEffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreDeleteEffect(Guid EffectId)
        {
            int result = PluginCoreDeleteEffect(EffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreInit()
        {
            int result = PluginCoreInit();
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreQueryDevice(Guid DeviceId, out DEVICE_INFO_TYPE DeviceInfo)
        {
            int result = PluginCoreQueryDevice(DeviceId, out DeviceInfo);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreSetEffect(Guid EffectId)
        {
            int result = PluginCoreSetEffect(EffectId);
            return result;
        }
        /// <summary>
        /// Direct access to low level API.
        /// </summary>
        public static int CoreUnInit()
        {
            int result = PluginCoreUnInit();
            return result;
        }
        /// <summary>
        /// Creates a `Chroma` animation at the given path. The `deviceType` parameter 
        /// uses `EChromaSDKDeviceTypeEnum` as an integer. The `device` parameter uses 
        /// `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` as an integer, respective 
        /// to the `deviceType`. Returns the animation id upon success. Returns -1 
        /// upon failure. Saves a `Chroma` animation file with the `.chroma` extension 
        /// at the given path. Returns the animation id upon success. Returns -1 upon 
        /// failure.
        /// </summary>
        public static int CreateAnimation(string path, int deviceType, int device)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginCreateAnimation(lpPath, deviceType, device);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Creates a `Chroma` animation in memory without creating a file. The `deviceType` 
        /// parameter uses `EChromaSDKDeviceTypeEnum` as an integer. The `device` parameter 
        /// uses `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` as an integer, 
        /// respective to the `deviceType`. Returns the animation id upon success. 
        /// Returns -1 upon failure. Returns the animation id upon success. Returns 
        /// -1 upon failure.
        /// </summary>
        public static int CreateAnimationInMemory(int deviceType, int device)
        {
            int result = PluginCreateAnimationInMemory(deviceType, device);
            return result;
        }
        /// <summary>
        /// Create a device specific effect.
        /// </summary>
        public static int CreateEffect(Guid deviceId, EFFECT_TYPE effect, int[] colors, int size, out FChromaSDKGuid effectId)
        {
            int result = PluginCreateEffect(deviceId, (int)effect, colors, size, out effectId);
            return result;
        }
        /// <summary>
        /// Delete an effect given the effect id.
        /// </summary>
        public static int DeleteEffect(Guid effectId)
        {
            int result = PluginDeleteEffect(effectId);
            return result;
        }
        /// <summary>
        /// Duplicate the first animation frame so that the animation length matches 
        /// the frame count. Animation is referenced by id.
        /// </summary>
        public static void DuplicateFirstFrame(int animationId, int frameCount)
        {
            PluginDuplicateFirstFrame(animationId, frameCount);
        }
        /// <summary>
        /// Duplicate the first animation frame so that the animation length matches 
        /// the frame count. Animation is referenced by name.
        /// </summary>
        public static void DuplicateFirstFrameName(string path, int frameCount)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginDuplicateFirstFrameName(lpPath, frameCount);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double DuplicateFirstFrameNameD(string path, double frameCount)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginDuplicateFirstFrameNameD(lpPath, frameCount);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Duplicate all the frames of the animation to double the animation length. 
        /// Frame 1 becomes frame 1 and 2. Frame 2 becomes frame 3 and 4. And so on. 
        /// The animation is referenced by id.
        /// </summary>
        public static void DuplicateFrames(int animationId)
        {
            PluginDuplicateFrames(animationId);
        }
        /// <summary>
        /// Duplicate all the frames of the animation to double the animation length. 
        /// Frame 1 becomes frame 1 and 2. Frame 2 becomes frame 3 and 4. And so on. 
        /// The animation is referenced by name.
        /// </summary>
        public static void DuplicateFramesName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginDuplicateFramesName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double DuplicateFramesNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginDuplicateFramesNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Duplicate all the animation frames in reverse so that the animation plays 
        /// forwards and backwards. Animation is referenced by id.
        /// </summary>
        public static void DuplicateMirrorFrames(int animationId)
        {
            PluginDuplicateMirrorFrames(animationId);
        }
        /// <summary>
        /// Duplicate all the animation frames in reverse so that the animation plays 
        /// forwards and backwards. Animation is referenced by name.
        /// </summary>
        public static void DuplicateMirrorFramesName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginDuplicateMirrorFramesName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double DuplicateMirrorFramesNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginDuplicateMirrorFramesNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fade the animation to black starting at the fade frame index to the end 
        /// of the animation. Animation is referenced by id.
        /// </summary>
        public static void FadeEndFrames(int animationId, int fade)
        {
            PluginFadeEndFrames(animationId, fade);
        }
        /// <summary>
        /// Fade the animation to black starting at the fade frame index to the end 
        /// of the animation. Animation is referenced by name.
        /// </summary>
        public static void FadeEndFramesName(string path, int fade)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFadeEndFramesName(lpPath, fade);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FadeEndFramesNameD(string path, double fade)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFadeEndFramesNameD(lpPath, fade);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fade the animation from black to full color starting at 0 to the fade frame 
        /// index. Animation is referenced by id.
        /// </summary>
        public static void FadeStartFrames(int animationId, int fade)
        {
            PluginFadeStartFrames(animationId, fade);
        }
        /// <summary>
        /// Fade the animation from black to full color starting at 0 to the fade frame 
        /// index. Animation is referenced by name.
        /// </summary>
        public static void FadeStartFramesName(string path, int fade)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFadeStartFramesName(lpPath, fade);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FadeStartFramesNameD(string path, double fade)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFadeStartFramesNameD(lpPath, fade);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by id.
        /// </summary>
        public static void FillColor(int animationId, int frameId, int color)
        {
            PluginFillColor(animationId, frameId, color);
        }
        /// <summary>
        /// Set the RGB value for all colors for all frames. Animation is referenced 
        /// by id.
        /// </summary>
        public static void FillColorAllFrames(int animationId, int color)
        {
            PluginFillColorAllFrames(animationId, color);
        }
        /// <summary>
        /// Set the RGB value for all colors for all frames. Animation is referenced 
        /// by name.
        /// </summary>
        public static void FillColorAllFramesName(string path, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillColorAllFramesName(lpPath, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillColorAllFramesNameD(string path, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillColorAllFramesNameD(lpPath, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the RGB value for all colors for all frames. Use the range of 0 to 255 
        /// for red, green, and blue parameters. Animation is referenced by id.
        /// </summary>
        public static void FillColorAllFramesRGB(int animationId, int red, int green, int blue)
        {
            PluginFillColorAllFramesRGB(animationId, red, green, blue);
        }
        /// <summary>
        /// Set the RGB value for all colors for all frames. Use the range of 0 to 255 
        /// for red, green, and blue parameters. Animation is referenced by name.
        /// </summary>
        public static void FillColorAllFramesRGBName(string path, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillColorAllFramesRGBName(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillColorAllFramesRGBNameD(string path, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillColorAllFramesRGBNameD(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by name.
        /// </summary>
        public static void FillColorName(string path, int frameId, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillColorName(lpPath, frameId, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillColorNameD(string path, double frameId, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillColorNameD(lpPath, frameId, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by id.
        /// </summary>
        public static void FillColorRGB(int animationId, int frameId, int red, int green, int blue)
        {
            PluginFillColorRGB(animationId, frameId, red, green, blue);
        }
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by name.
        /// </summary>
        public static void FillColorRGBName(string path, int frameId, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillColorRGBName(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillColorRGBNameD(string path, double frameId, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillColorRGBNameD(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Animation is referenced by id.
        /// </summary>
        public static void FillNonZeroColor(int animationId, int frameId, int color)
        {
            PluginFillNonZeroColor(animationId, frameId, color);
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Animation is referenced by id.
        /// </summary>
        public static void FillNonZeroColorAllFrames(int animationId, int color)
        {
            PluginFillNonZeroColorAllFrames(animationId, color);
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Animation is referenced by name.
        /// </summary>
        public static void FillNonZeroColorAllFramesName(string path, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillNonZeroColorAllFramesName(lpPath, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillNonZeroColorAllFramesNameD(string path, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillNonZeroColorAllFramesNameD(lpPath, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by id.
        /// </summary>
        public static void FillNonZeroColorAllFramesRGB(int animationId, int red, int green, int blue)
        {
            PluginFillNonZeroColorAllFramesRGB(animationId, red, green, blue);
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by name.
        /// </summary>
        public static void FillNonZeroColorAllFramesRGBName(string path, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillNonZeroColorAllFramesRGBName(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillNonZeroColorAllFramesRGBNameD(string path, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillNonZeroColorAllFramesRGBNameD(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Animation is referenced by name.
        /// </summary>
        public static void FillNonZeroColorName(string path, int frameId, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillNonZeroColorName(lpPath, frameId, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillNonZeroColorNameD(string path, double frameId, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillNonZeroColorNameD(lpPath, frameId, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by id.
        /// </summary>
        public static void FillNonZeroColorRGB(int animationId, int frameId, int red, int green, int blue)
        {
            PluginFillNonZeroColorRGB(animationId, frameId, red, green, blue);
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by name.
        /// </summary>
        public static void FillNonZeroColorRGBName(string path, int frameId, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillNonZeroColorRGBName(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillNonZeroColorRGBNameD(string path, double frameId, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillNonZeroColorRGBNameD(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the frame with random RGB values for the given frame. Animation is 
        /// referenced by id.
        /// </summary>
        public static void FillRandomColors(int animationId, int frameId)
        {
            PluginFillRandomColors(animationId, frameId);
        }
        /// <summary>
        /// Fill the frame with random RGB values for all frames. Animation is referenced 
        /// by id.
        /// </summary>
        public static void FillRandomColorsAllFrames(int animationId)
        {
            PluginFillRandomColorsAllFrames(animationId);
        }
        /// <summary>
        /// Fill the frame with random RGB values for all frames. Animation is referenced 
        /// by name.
        /// </summary>
        public static void FillRandomColorsAllFramesName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillRandomColorsAllFramesName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillRandomColorsAllFramesNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillRandomColorsAllFramesNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the frame with random black and white values for the specified frame. 
        /// Animation is referenced by id.
        /// </summary>
        public static void FillRandomColorsBlackAndWhite(int animationId, int frameId)
        {
            PluginFillRandomColorsBlackAndWhite(animationId, frameId);
        }
        /// <summary>
        /// Fill the frame with random black and white values for all frames. Animation 
        /// is referenced by id.
        /// </summary>
        public static void FillRandomColorsBlackAndWhiteAllFrames(int animationId)
        {
            PluginFillRandomColorsBlackAndWhiteAllFrames(animationId);
        }
        /// <summary>
        /// Fill the frame with random black and white values for all frames. Animation 
        /// is referenced by name.
        /// </summary>
        public static void FillRandomColorsBlackAndWhiteAllFramesName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillRandomColorsBlackAndWhiteAllFramesName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillRandomColorsBlackAndWhiteAllFramesNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillRandomColorsBlackAndWhiteAllFramesNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the frame with random black and white values for the specified frame. 
        /// Animation is referenced by name.
        /// </summary>
        public static void FillRandomColorsBlackAndWhiteName(string path, int frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillRandomColorsBlackAndWhiteName(lpPath, frameId);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillRandomColorsBlackAndWhiteNameD(string path, double frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillRandomColorsBlackAndWhiteNameD(lpPath, frameId);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the frame with random RGB values for the given frame. Animation is 
        /// referenced by name.
        /// </summary>
        public static void FillRandomColorsName(string path, int frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillRandomColorsName(lpPath, frameId);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillRandomColorsNameD(string path, double frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillRandomColorsNameD(lpPath, frameId);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdColors(int animationId, int frameId, int threshold, int color)
        {
            PluginFillThresholdColors(animationId, frameId, threshold, color);
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdColorsAllFrames(int animationId, int threshold, int color)
        {
            PluginFillThresholdColorsAllFrames(animationId, threshold, color);
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdColorsAllFramesName(string path, int threshold, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdColorsAllFramesName(lpPath, threshold, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdColorsAllFramesNameD(string path, double threshold, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdColorsAllFramesNameD(lpPath, threshold, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdColorsAllFramesRGB(int animationId, int threshold, int red, int green, int blue)
        {
            PluginFillThresholdColorsAllFramesRGB(animationId, threshold, red, green, blue);
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdColorsAllFramesRGBName(string path, int threshold, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdColorsAllFramesRGBName(lpPath, threshold, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdColorsAllFramesRGBNameD(string path, double threshold, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdColorsAllFramesRGBNameD(lpPath, threshold, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill all frames with the min RGB color where the animation color is less 
        /// than the min threshold AND with the max RGB color where the animation is 
        /// more than the max threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdColorsMinMaxAllFramesRGB(int animationId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue)
        {
            PluginFillThresholdColorsMinMaxAllFramesRGB(animationId, minThreshold, minRed, minGreen, minBlue, maxThreshold, maxRed, maxGreen, maxBlue);
        }
        /// <summary>
        /// Fill all frames with the min RGB color where the animation color is less 
        /// than the min threshold AND with the max RGB color where the animation is 
        /// more than the max threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdColorsMinMaxAllFramesRGBName(string path, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdColorsMinMaxAllFramesRGBName(lpPath, minThreshold, minRed, minGreen, minBlue, maxThreshold, maxRed, maxGreen, maxBlue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdColorsMinMaxAllFramesRGBNameD(string path, double minThreshold, double minRed, double minGreen, double minBlue, double maxThreshold, double maxRed, double maxGreen, double maxBlue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdColorsMinMaxAllFramesRGBNameD(lpPath, minThreshold, minRed, minGreen, minBlue, maxThreshold, maxRed, maxGreen, maxBlue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with the min RGB color where the animation color 
        /// is less than the min threshold AND with the max RGB color where the animation 
        /// is more than the max threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdColorsMinMaxRGB(int animationId, int frameId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue)
        {
            PluginFillThresholdColorsMinMaxRGB(animationId, frameId, minThreshold, minRed, minGreen, minBlue, maxThreshold, maxRed, maxGreen, maxBlue);
        }
        /// <summary>
        /// Fill the specified frame with the min RGB color where the animation color 
        /// is less than the min threshold AND with the max RGB color where the animation 
        /// is more than the max threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdColorsMinMaxRGBName(string path, int frameId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdColorsMinMaxRGBName(lpPath, frameId, minThreshold, minRed, minGreen, minBlue, maxThreshold, maxRed, maxGreen, maxBlue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdColorsMinMaxRGBNameD(string path, double frameId, double minThreshold, double minRed, double minGreen, double minBlue, double maxThreshold, double maxRed, double maxGreen, double maxBlue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdColorsMinMaxRGBNameD(lpPath, frameId, minThreshold, minRed, minGreen, minBlue, maxThreshold, maxRed, maxGreen, maxBlue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdColorsName(string path, int frameId, int threshold, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdColorsName(lpPath, frameId, threshold, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdColorsNameD(string path, double frameId, double threshold, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdColorsNameD(lpPath, frameId, threshold, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdColorsRGB(int animationId, int frameId, int threshold, int red, int green, int blue)
        {
            PluginFillThresholdColorsRGB(animationId, frameId, threshold, red, green, blue);
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdColorsRGBName(string path, int frameId, int threshold, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdColorsRGBName(lpPath, frameId, threshold, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdColorsRGBNameD(string path, double frameId, double threshold, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdColorsRGBNameD(lpPath, frameId, threshold, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdRGBColorsAllFramesRGB(int animationId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue)
        {
            PluginFillThresholdRGBColorsAllFramesRGB(animationId, redThreshold, greenThreshold, blueThreshold, red, green, blue);
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdRGBColorsAllFramesRGBName(string path, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdRGBColorsAllFramesRGBName(lpPath, redThreshold, greenThreshold, blueThreshold, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdRGBColorsAllFramesRGBNameD(string path, double redThreshold, double greenThreshold, double blueThreshold, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdRGBColorsAllFramesRGBNameD(lpPath, redThreshold, greenThreshold, blueThreshold, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by id.
        /// </summary>
        public static void FillThresholdRGBColorsRGB(int animationId, int frameId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue)
        {
            PluginFillThresholdRGBColorsRGB(animationId, frameId, redThreshold, greenThreshold, blueThreshold, red, green, blue);
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by name.
        /// </summary>
        public static void FillThresholdRGBColorsRGBName(string path, int frameId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillThresholdRGBColorsRGBName(lpPath, frameId, redThreshold, greenThreshold, blueThreshold, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillThresholdRGBColorsRGBNameD(string path, double frameId, double redThreshold, double greenThreshold, double blueThreshold, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillThresholdRGBColorsRGBNameD(lpPath, frameId, redThreshold, greenThreshold, blueThreshold, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by id.
        /// </summary>
        public static void FillZeroColor(int animationId, int frameId, int color)
        {
            PluginFillZeroColor(animationId, frameId, color);
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by id.
        /// </summary>
        public static void FillZeroColorAllFrames(int animationId, int color)
        {
            PluginFillZeroColorAllFrames(animationId, color);
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by name.
        /// </summary>
        public static void FillZeroColorAllFramesName(string path, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillZeroColorAllFramesName(lpPath, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillZeroColorAllFramesNameD(string path, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillZeroColorAllFramesNameD(lpPath, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by id.
        /// </summary>
        public static void FillZeroColorAllFramesRGB(int animationId, int red, int green, int blue)
        {
            PluginFillZeroColorAllFramesRGB(animationId, red, green, blue);
        }
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by name.
        /// </summary>
        public static void FillZeroColorAllFramesRGBName(string path, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillZeroColorAllFramesRGBName(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillZeroColorAllFramesRGBNameD(string path, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillZeroColorAllFramesRGBNameD(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by name.
        /// </summary>
        public static void FillZeroColorName(string path, int frameId, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillZeroColorName(lpPath, frameId, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillZeroColorNameD(string path, double frameId, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillZeroColorNameD(lpPath, frameId, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by id.
        /// </summary>
        public static void FillZeroColorRGB(int animationId, int frameId, int red, int green, int blue)
        {
            PluginFillZeroColorRGB(animationId, frameId, red, green, blue);
        }
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by name.
        /// </summary>
        public static void FillZeroColorRGBName(string path, int frameId, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginFillZeroColorRGBName(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double FillZeroColorRGBNameD(string path, double frameId, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginFillZeroColorRGBNameD(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Get the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. Animation is 
        /// referenced by id.
        /// </summary>
        public static int Get1DColor(int animationId, int frameId, int led)
        {
            int result = PluginGet1DColor(animationId, frameId, led);
            return result;
        }
        /// <summary>
        /// Get the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. Animation is 
        /// referenced by name.
        /// </summary>
        public static int Get1DColorName(string path, int frameId, int led)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginGet1DColorName(lpPath, frameId, led);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double Get1DColorNameD(string path, double frameId, double led)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginGet1DColorNameD(lpPath, frameId, led);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Get the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// Animation is referenced by id.
        /// </summary>
        public static int Get2DColor(int animationId, int frameId, int row, int column)
        {
            int result = PluginGet2DColor(animationId, frameId, row, column);
            return result;
        }
        /// <summary>
        /// Get the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// Animation is referenced by name.
        /// </summary>
        public static int Get2DColorName(string path, int frameId, int row, int column)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginGet2DColorName(lpPath, frameId, row, column);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double Get2DColorNameD(string path, double frameId, double row, double column)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginGet2DColorNameD(lpPath, frameId, row, column);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Get the animation id for the named animation.
        /// </summary>
        public static int GetAnimation(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            int result = PluginGetAnimation(lpName);
            FreeIntPtr(lpName);
            return result;
        }
        /// <summary>
        /// `PluginGetAnimationCount` will return the number of loaded animations.
        /// </summary>
        public static int GetAnimationCount()
        {
            int result = PluginGetAnimationCount();
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetAnimationD(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            double result = PluginGetAnimationD(lpName);
            FreeIntPtr(lpName);
            return result;
        }
        /// <summary>
        /// `PluginGetAnimationId` will return the `animationId` given the `index` of 
        /// the loaded animation. The `index` is zero-based and less than the number 
        /// returned by `PluginGetAnimationCount`. Use `PluginGetAnimationName` to 
        /// get the name of the animation.
        /// </summary>
        public static int GetAnimationId(int index)
        {
            int result = PluginGetAnimationId(index);
            return result;
        }
        /// <summary>
        /// `PluginGetAnimationName` takes an `animationId` and returns the name of 
        /// the animation of the `.chroma` animation file. If a name is not available 
        /// then an empty string will be returned.
        /// </summary>
        public static string GetAnimationName(int animationId)
        {
            string result = Marshal.PtrToStringAnsi(PluginGetAnimationName(animationId));
            return result;
        }
        /// <summary>
        /// Get the current frame of the animation referenced by id.
        /// </summary>
        public static int GetCurrentFrame(int animationId)
        {
            int result = PluginGetCurrentFrame(animationId);
            return result;
        }
        /// <summary>
        /// Get the current frame of the animation referenced by name.
        /// </summary>
        public static int GetCurrentFrameName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginGetCurrentFrameName(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetCurrentFrameNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginGetCurrentFrameNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Returns the `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` of a `Chroma` 
        /// animation respective to the `deviceType`, as an integer upon success. Returns 
        /// -1 upon failure.
        /// </summary>
        public static int GetDevice(int animationId)
        {
            int result = PluginGetDevice(animationId);
            return result;
        }
        /// <summary>
        /// Returns the `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` of a `Chroma` 
        /// animation respective to the `deviceType`, as an integer upon success. Returns 
        /// -1 upon failure.
        /// </summary>
        public static int GetDeviceName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginGetDeviceName(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetDeviceNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginGetDeviceNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Returns the `EChromaSDKDeviceTypeEnum` of a `Chroma` animation as an integer 
        /// upon success. Returns -1 upon failure.
        /// </summary>
        public static int GetDeviceType(int animationId)
        {
            int result = PluginGetDeviceType(animationId);
            return result;
        }
        /// <summary>
        /// Returns the `EChromaSDKDeviceTypeEnum` of a `Chroma` animation as an integer 
        /// upon success. Returns -1 upon failure.
        /// </summary>
        public static int GetDeviceTypeName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginGetDeviceTypeName(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetDeviceTypeNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginGetDeviceTypeNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Gets the frame colors and duration (in seconds) for a `Chroma` animation. 
        /// The `color` is expected to be an array of the expected dimensions for the 
        /// `deviceType/device`. The `length` parameter is the size of the `color` 
        /// array. For `EChromaSDKDevice1DEnum` the array size should be `MAX LEDS`. 
        /// For `EChromaSDKDevice2DEnum` the array size should be `MAX ROW` * `MAX 
        /// COLUMN`. Returns the animation id upon success. Returns -1 upon failure. 
        ///
        /// </summary>
        public static int GetFrame(int animationId, int frameIndex, out float duration, int[] colors, int length)
        {
            int result = PluginGetFrame(animationId, frameIndex, out duration, colors, length);
            return result;
        }
        /// <summary>
        /// Returns the frame count of a `Chroma` animation upon success. Returns -1 
        /// upon failure.
        /// </summary>
        public static int GetFrameCount(int animationId)
        {
            int result = PluginGetFrameCount(animationId);
            return result;
        }
        /// <summary>
        /// Returns the frame count of a `Chroma` animation upon success. Returns -1 
        /// upon failure.
        /// </summary>
        public static int GetFrameCountName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginGetFrameCountName(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetFrameCountNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginGetFrameCountNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Get the color of an animation key for the given frame referenced by id. 
        ///
        /// </summary>
        public static int GetKeyColor(int animationId, int frameId, int rzkey)
        {
            int result = PluginGetKeyColor(animationId, frameId, rzkey);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetKeyColorD(string path, double frameId, double rzkey)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginGetKeyColorD(lpPath, frameId, rzkey);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Get the color of an animation key for the given frame referenced by name. 
        ///
        /// </summary>
        public static int GetKeyColorName(string path, int frameId, int rzkey)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginGetKeyColorName(lpPath, frameId, rzkey);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Returns `RZRESULT_SUCCESS` if the plugin has been initialized successfully. 
        /// Returns `RZRESULT_DLL_NOT_FOUND` if core Chroma library is not found. Returns 
        /// `RZRESULT_DLL_INVALID_SIGNATURE` if core Chroma library has an invalid 
        /// signature.
        /// </summary>
        public static int GetLibraryLoadedState()
        {
            int result = PluginGetLibraryLoadedState();
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetLibraryLoadedStateD()
        {
            double result = PluginGetLibraryLoadedStateD();
            return result;
        }
        /// <summary>
        /// Returns the `MAX COLUMN` given the `EChromaSDKDevice2DEnum` device as an 
        /// integer upon success. Returns -1 upon failure.
        /// </summary>
        public static int GetMaxColumn(Device2D device)
        {
            int result = PluginGetMaxColumn((int)device);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetMaxColumnD(double device)
        {
            double result = PluginGetMaxColumnD(device);
            return result;
        }
        /// <summary>
        /// Returns the MAX LEDS given the `EChromaSDKDevice1DEnum` device as an integer 
        /// upon success. Returns -1 upon failure.
        /// </summary>
        public static int GetMaxLeds(Device1D device)
        {
            int result = PluginGetMaxLeds((int)device);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetMaxLedsD(double device)
        {
            double result = PluginGetMaxLedsD(device);
            return result;
        }
        /// <summary>
        /// Returns the `MAX ROW` given the `EChromaSDKDevice2DEnum` device as an integer 
        /// upon success. Returns -1 upon failure.
        /// </summary>
        public static int GetMaxRow(Device2D device)
        {
            int result = PluginGetMaxRow((int)device);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetMaxRowD(double device)
        {
            double result = PluginGetMaxRowD(device);
            return result;
        }
        /// <summary>
        /// `PluginGetPlayingAnimationCount` will return the number of playing animations. 
        ///
        /// </summary>
        public static int GetPlayingAnimationCount()
        {
            int result = PluginGetPlayingAnimationCount();
            return result;
        }
        /// <summary>
        /// `PluginGetPlayingAnimationId` will return the `animationId` given the `index` 
        /// of the playing animation. The `index` is zero-based and less than the number 
        /// returned by `PluginGetPlayingAnimationCount`. Use `PluginGetAnimationName` 
        /// to get the name of the animation.
        /// </summary>
        public static int GetPlayingAnimationId(int index)
        {
            int result = PluginGetPlayingAnimationId(index);
            return result;
        }
        /// <summary>
        /// Get the RGB color given red, green, and blue.
        /// </summary>
        public static int GetRGB(int red, int green, int blue)
        {
            int result = PluginGetRGB(red, green, blue);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double GetRGBD(double red, double green, double blue)
        {
            double result = PluginGetRGBD(red, green, blue);
            return result;
        }
        /// <summary>
        /// Check if the animation has loop enabled referenced by id.
        /// </summary>
        public static bool HasAnimationLoop(int animationId)
        {
            bool result = PluginHasAnimationLoop(animationId);
            return result;
        }
        /// <summary>
        /// Check if the animation has loop enabled referenced by name.
        /// </summary>
        public static bool HasAnimationLoopName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            bool result = PluginHasAnimationLoopName(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double HasAnimationLoopNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginHasAnimationLoopNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Initialize the ChromaSDK. Zero indicates  success, otherwise failure. Many 
        /// API methods auto initialize the ChromaSDK if not already initialized.
        /// </summary>
        public static int Init()
        {
            int result = PluginInit();
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double InitD()
        {
            double result = PluginInitD();
            return result;
        }
        /// <summary>
        /// Insert an animation delay by duplicating the frame by the delay number of 
        /// times. Animation is referenced by id.
        /// </summary>
        public static void InsertDelay(int animationId, int frameId, int delay)
        {
            PluginInsertDelay(animationId, frameId, delay);
        }
        /// <summary>
        /// Insert an animation delay by duplicating the frame by the delay number of 
        /// times. Animation is referenced by name.
        /// </summary>
        public static void InsertDelayName(string path, int frameId, int delay)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginInsertDelayName(lpPath, frameId, delay);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double InsertDelayNameD(string path, double frameId, double delay)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginInsertDelayNameD(lpPath, frameId, delay);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Duplicate the source frame index at the target frame index. Animation is 
        /// referenced by id.
        /// </summary>
        public static void InsertFrame(int animationId, int sourceFrame, int targetFrame)
        {
            PluginInsertFrame(animationId, sourceFrame, targetFrame);
        }
        /// <summary>
        /// Duplicate the source frame index at the target frame index. Animation is 
        /// referenced by name.
        /// </summary>
        public static void InsertFrameName(string path, int sourceFrame, int targetFrame)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginInsertFrameName(lpPath, sourceFrame, targetFrame);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double InsertFrameNameD(string path, double sourceFrame, double targetFrame)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginInsertFrameNameD(lpPath, sourceFrame, targetFrame);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Invert all the colors at the specified frame. Animation is referenced by 
        /// id.
        /// </summary>
        public static void InvertColors(int animationId, int frameId)
        {
            PluginInvertColors(animationId, frameId);
        }
        /// <summary>
        /// Invert all the colors for all frames. Animation is referenced by id.
        /// </summary>
        public static void InvertColorsAllFrames(int animationId)
        {
            PluginInvertColorsAllFrames(animationId);
        }
        /// <summary>
        /// Invert all the colors for all frames. Animation is referenced by name.
        /// </summary>
        public static void InvertColorsAllFramesName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginInvertColorsAllFramesName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double InvertColorsAllFramesNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginInvertColorsAllFramesNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Invert all the colors at the specified frame. Animation is referenced by 
        /// name.
        /// </summary>
        public static void InvertColorsName(string path, int frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginInvertColorsName(lpPath, frameId);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double InvertColorsNameD(string path, double frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginInvertColorsNameD(lpPath, frameId);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Check if the animation is paused referenced by id.
        /// </summary>
        public static bool IsAnimationPaused(int animationId)
        {
            bool result = PluginIsAnimationPaused(animationId);
            return result;
        }
        /// <summary>
        /// Check if the animation is paused referenced by name.
        /// </summary>
        public static bool IsAnimationPausedName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            bool result = PluginIsAnimationPausedName(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double IsAnimationPausedNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginIsAnimationPausedNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// The editor dialog is a non-blocking modal window, this method returns true 
        /// if the modal window is open, otherwise false.
        /// </summary>
        public static bool IsDialogOpen()
        {
            bool result = PluginIsDialogOpen();
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double IsDialogOpenD()
        {
            double result = PluginIsDialogOpenD();
            return result;
        }
        /// <summary>
        /// Returns true if the plugin has been initialized. Returns false if the plugin 
        /// is uninitialized.
        /// </summary>
        public static bool IsInitialized()
        {
            bool result = PluginIsInitialized();
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double IsInitializedD()
        {
            double result = PluginIsInitializedD();
            return result;
        }
        /// <summary>
        /// If the method can be invoked the method returns true.
        /// </summary>
        public static bool IsPlatformSupported()
        {
            bool result = PluginIsPlatformSupported();
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double IsPlatformSupportedD()
        {
            double result = PluginIsPlatformSupportedD();
            return result;
        }
        /// <summary>
        /// `PluginIsPlayingName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The method 
        /// will return whether the animation is playing or not. Animation is referenced 
        /// by id.
        /// </summary>
        public static bool IsPlaying(int animationId)
        {
            bool result = PluginIsPlaying(animationId);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double IsPlayingD(double animationId)
        {
            double result = PluginIsPlayingD(animationId);
            return result;
        }
        /// <summary>
        /// `PluginIsPlayingName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The method 
        /// will return whether the animation is playing or not. Animation is referenced 
        /// by name.
        /// </summary>
        public static bool IsPlayingName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            bool result = PluginIsPlayingName(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double IsPlayingNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginIsPlayingNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// `PluginIsPlayingType` automatically handles initializing the `ChromaSDK`. 
        /// If any animation is playing for the `deviceType` and `device` combination, 
        /// the method will return true, otherwise false.
        /// </summary>
        public static bool IsPlayingType(int deviceType, int device)
        {
            bool result = PluginIsPlayingType(deviceType, device);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double IsPlayingTypeD(double deviceType, double device)
        {
            double result = PluginIsPlayingTypeD(deviceType, device);
            return result;
        }
        /// <summary>
        /// Do a lerp math operation on a float.
        /// </summary>
        public static float Lerp(float start, float end, float amt)
        {
            float result = PluginLerp(start, end, amt);
            return result;
        }
        /// <summary>
        /// Lerp from one color to another given t in the range 0.0 to 1.0.
        /// </summary>
        public static int LerpColor(int from, int to, float t)
        {
            int result = PluginLerpColor(from, to, t);
            return result;
        }
        /// <summary>
        /// Loads `Chroma` effects so that the animation can be played immediately. 
        /// Returns the animation id upon success. Returns -1 upon failure.
        /// </summary>
        public static int LoadAnimation(int animationId)
        {
            int result = PluginLoadAnimation(animationId);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double LoadAnimationD(double animationId)
        {
            double result = PluginLoadAnimationD(animationId);
            return result;
        }
        /// <summary>
        /// Load the named animation.
        /// </summary>
        public static void LoadAnimationName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginLoadAnimationName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Load a composite set of animations.
        /// </summary>
        public static void LoadComposite(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            PluginLoadComposite(lpName);
            FreeIntPtr(lpName);
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by id.
        /// </summary>
        public static void MakeBlankFrames(int animationId, int frameCount, float duration, int color)
        {
            PluginMakeBlankFrames(animationId, frameCount, duration, color);
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by name.
        /// </summary>
        public static void MakeBlankFramesName(string path, int frameCount, float duration, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMakeBlankFramesName(lpPath, frameCount, duration, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MakeBlankFramesNameD(string path, double frameCount, double duration, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMakeBlankFramesNameD(lpPath, frameCount, duration, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random. Animation is referenced 
        /// by id.
        /// </summary>
        public static void MakeBlankFramesRandom(int animationId, int frameCount, float duration)
        {
            PluginMakeBlankFramesRandom(animationId, frameCount, duration);
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random black and white. Animation 
        /// is referenced by id.
        /// </summary>
        public static void MakeBlankFramesRandomBlackAndWhite(int animationId, int frameCount, float duration)
        {
            PluginMakeBlankFramesRandomBlackAndWhite(animationId, frameCount, duration);
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random black and white. Animation 
        /// is referenced by name.
        /// </summary>
        public static void MakeBlankFramesRandomBlackAndWhiteName(string path, int frameCount, float duration)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMakeBlankFramesRandomBlackAndWhiteName(lpPath, frameCount, duration);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MakeBlankFramesRandomBlackAndWhiteNameD(string path, double frameCount, double duration)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMakeBlankFramesRandomBlackAndWhiteNameD(lpPath, frameCount, duration);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random. Animation is referenced 
        /// by name.
        /// </summary>
        public static void MakeBlankFramesRandomName(string path, int frameCount, float duration)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMakeBlankFramesRandomName(lpPath, frameCount, duration);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MakeBlankFramesRandomNameD(string path, double frameCount, double duration)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMakeBlankFramesRandomNameD(lpPath, frameCount, duration);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by id.
        /// </summary>
        public static void MakeBlankFramesRGB(int animationId, int frameCount, float duration, int red, int green, int blue)
        {
            PluginMakeBlankFramesRGB(animationId, frameCount, duration, red, green, blue);
        }
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by name.
        /// </summary>
        public static void MakeBlankFramesRGBName(string path, int frameCount, float duration, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMakeBlankFramesRGBName(lpPath, frameCount, duration, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MakeBlankFramesRGBNameD(string path, double frameCount, double duration, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMakeBlankFramesRGBNameD(lpPath, frameCount, duration, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Flips the color grid horizontally for all `Chroma` animation frames. Returns 
        /// the animation id upon success. Returns -1 upon failure.
        /// </summary>
        public static int MirrorHorizontally(int animationId)
        {
            int result = PluginMirrorHorizontally(animationId);
            return result;
        }
        /// <summary>
        /// Flips the color grid vertically for all `Chroma` animation frames. This 
        /// method has no effect for `EChromaSDKDevice1DEnum` devices. Returns the 
        /// animation id upon success. Returns -1 upon failure.
        /// </summary>
        public static int MirrorVertically(int animationId)
        {
            int result = PluginMirrorVertically(animationId);
            return result;
        }
        /// <summary>
        /// Multiply the color intensity with the lerp result from color 1 to color 
        /// 2 using the frame index divided by the frame count for the `t` parameter. 
        /// Animation is referenced in id.
        /// </summary>
        public static void MultiplyColorLerpAllFrames(int animationId, int color1, int color2)
        {
            PluginMultiplyColorLerpAllFrames(animationId, color1, color2);
        }
        /// <summary>
        /// Multiply the color intensity with the lerp result from color 1 to color 
        /// 2 using the frame index divided by the frame count for the `t` parameter. 
        /// Animation is referenced in name.
        /// </summary>
        public static void MultiplyColorLerpAllFramesName(string path, int color1, int color2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyColorLerpAllFramesName(lpPath, color1, color2);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyColorLerpAllFramesNameD(string path, double color1, double color2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyColorLerpAllFramesNameD(lpPath, color1, color2);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply all the colors in the frame by the intensity value. The valid the 
        /// intensity range is from 0.0 to 255.0. RGB components are multiplied equally. 
        /// An intensity of 0.5 would half the color value. Black colors in the frame 
        /// will not be affected by this method.
        /// </summary>
        public static void MultiplyIntensity(int animationId, int frameId, float intensity)
        {
            PluginMultiplyIntensity(animationId, frameId, intensity);
        }
        /// <summary>
        /// Multiply all the colors for all frames by the intensity value. The valid 
        /// the intensity range is from 0.0 to 255.0. RGB components are multiplied 
        /// equally. An intensity of 0.5 would half the color value. Black colors in 
        /// the frame will not be affected by this method.
        /// </summary>
        public static void MultiplyIntensityAllFrames(int animationId, float intensity)
        {
            PluginMultiplyIntensityAllFrames(animationId, intensity);
        }
        /// <summary>
        /// Multiply all the colors for all frames by the intensity value. The valid 
        /// the intensity range is from 0.0 to 255.0. RGB components are multiplied 
        /// equally. An intensity of 0.5 would half the color value. Black colors in 
        /// the frame will not be affected by this method.
        /// </summary>
        public static void MultiplyIntensityAllFramesName(string path, float intensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyIntensityAllFramesName(lpPath, intensity);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyIntensityAllFramesNameD(string path, double intensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyIntensityAllFramesNameD(lpPath, intensity);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by id.
        /// </summary>
        public static void MultiplyIntensityAllFramesRGB(int animationId, int red, int green, int blue)
        {
            PluginMultiplyIntensityAllFramesRGB(animationId, red, green, blue);
        }
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by name.
        /// </summary>
        public static void MultiplyIntensityAllFramesRGBName(string path, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyIntensityAllFramesRGBName(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyIntensityAllFramesRGBNameD(string path, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyIntensityAllFramesRGBNameD(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by id.
        /// </summary>
        public static void MultiplyIntensityColor(int animationId, int frameId, int color)
        {
            PluginMultiplyIntensityColor(animationId, frameId, color);
        }
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by id.
        /// </summary>
        public static void MultiplyIntensityColorAllFrames(int animationId, int color)
        {
            PluginMultiplyIntensityColorAllFrames(animationId, color);
        }
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by name.
        /// </summary>
        public static void MultiplyIntensityColorAllFramesName(string path, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyIntensityColorAllFramesName(lpPath, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyIntensityColorAllFramesNameD(string path, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyIntensityColorAllFramesNameD(lpPath, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by name.
        /// </summary>
        public static void MultiplyIntensityColorName(string path, int frameId, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyIntensityColorName(lpPath, frameId, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyIntensityColorNameD(string path, double frameId, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyIntensityColorNameD(lpPath, frameId, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply all the colors in the frame by the intensity value. The valid the 
        /// intensity range is from 0.0 to 255.0. RGB components are multiplied equally. 
        /// An intensity of 0.5 would half the color value. Black colors in the frame 
        /// will not be affected by this method.
        /// </summary>
        public static void MultiplyIntensityName(string path, int frameId, float intensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyIntensityName(lpPath, frameId, intensity);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyIntensityNameD(string path, double frameId, double intensity)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyIntensityNameD(lpPath, frameId, intensity);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by id.
        /// </summary>
        public static void MultiplyIntensityRGB(int animationId, int frameId, int red, int green, int blue)
        {
            PluginMultiplyIntensityRGB(animationId, frameId, red, green, blue);
        }
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by name.
        /// </summary>
        public static void MultiplyIntensityRGBName(string path, int frameId, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyIntensityRGBName(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyIntensityRGBNameD(string path, double frameId, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyIntensityRGBNameD(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply the specific frame by the color lerp result between color 1 and 
        /// 2 using the frame color value as the `t` value. Animation is referenced 
        /// by id.
        /// </summary>
        public static void MultiplyNonZeroTargetColorLerp(int animationId, int frameId, int color1, int color2)
        {
            PluginMultiplyNonZeroTargetColorLerp(animationId, frameId, color1, color2);
        }
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by id. 
        ///
        /// </summary>
        public static void MultiplyNonZeroTargetColorLerpAllFrames(int animationId, int color1, int color2)
        {
            PluginMultiplyNonZeroTargetColorLerpAllFrames(animationId, color1, color2);
        }
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by name. 
        ///
        /// </summary>
        public static void MultiplyNonZeroTargetColorLerpAllFramesName(string path, int color1, int color2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyNonZeroTargetColorLerpAllFramesName(lpPath, color1, color2);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyNonZeroTargetColorLerpAllFramesNameD(string path, double color1, double color2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyNonZeroTargetColorLerpAllFramesNameD(lpPath, color1, color2);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply the specific frame by the color lerp result between RGB 1 and 2 
        /// using the frame color value as the `t` value. Animation is referenced by 
        /// id.
        /// </summary>
        public static void MultiplyNonZeroTargetColorLerpAllFramesRGB(int animationId, int red1, int green1, int blue1, int red2, int green2, int blue2)
        {
            PluginMultiplyNonZeroTargetColorLerpAllFramesRGB(animationId, red1, green1, blue1, red2, green2, blue2);
        }
        /// <summary>
        /// Multiply the specific frame by the color lerp result between RGB 1 and 2 
        /// using the frame color value as the `t` value. Animation is referenced by 
        /// name.
        /// </summary>
        public static void MultiplyNonZeroTargetColorLerpAllFramesRGBName(string path, int red1, int green1, int blue1, int red2, int green2, int blue2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyNonZeroTargetColorLerpAllFramesRGBName(lpPath, red1, green1, blue1, red2, green2, blue2);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyNonZeroTargetColorLerpAllFramesRGBNameD(string path, double red1, double green1, double blue1, double red2, double green2, double blue2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyNonZeroTargetColorLerpAllFramesRGBNameD(lpPath, red1, green1, blue1, red2, green2, blue2);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply the specific frame by the color lerp result between color 1 and 
        /// 2 using the frame color value as the `t` value. Animation is referenced 
        /// by id.
        /// </summary>
        public static void MultiplyTargetColorLerp(int animationId, int frameId, int color1, int color2)
        {
            PluginMultiplyTargetColorLerp(animationId, frameId, color1, color2);
        }
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by id. 
        ///
        /// </summary>
        public static void MultiplyTargetColorLerpAllFrames(int animationId, int color1, int color2)
        {
            PluginMultiplyTargetColorLerpAllFrames(animationId, color1, color2);
        }
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by name. 
        ///
        /// </summary>
        public static void MultiplyTargetColorLerpAllFramesName(string path, int color1, int color2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyTargetColorLerpAllFramesName(lpPath, color1, color2);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyTargetColorLerpAllFramesNameD(string path, double color1, double color2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyTargetColorLerpAllFramesNameD(lpPath, color1, color2);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Multiply all frames by the color lerp result between RGB 1 and 2 using the 
        /// frame color value as the `t` value. Animation is referenced by id.
        /// </summary>
        public static void MultiplyTargetColorLerpAllFramesRGB(int animationId, int red1, int green1, int blue1, int red2, int green2, int blue2)
        {
            PluginMultiplyTargetColorLerpAllFramesRGB(animationId, red1, green1, blue1, red2, green2, blue2);
        }
        /// <summary>
        /// Multiply all frames by the color lerp result between RGB 1 and 2 using the 
        /// frame color value as the `t` value. Animation is referenced by name.
        /// </summary>
        public static void MultiplyTargetColorLerpAllFramesRGBName(string path, int red1, int green1, int blue1, int red2, int green2, int blue2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginMultiplyTargetColorLerpAllFramesRGBName(lpPath, red1, green1, blue1, red2, green2, blue2);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double MultiplyTargetColorLerpAllFramesRGBNameD(string path, double red1, double green1, double blue1, double red2, double green2, double blue2)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginMultiplyTargetColorLerpAllFramesRGBNameD(lpPath, red1, green1, blue1, red2, green2, blue2);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Offset all colors in the frame using the RGB offset. Use the range of -255 
        /// to 255 for red, green, and blue parameters. Negative values remove color. 
        /// Positive values add color.
        /// </summary>
        public static void OffsetColors(int animationId, int frameId, int red, int green, int blue)
        {
            PluginOffsetColors(animationId, frameId, red, green, blue);
        }
        /// <summary>
        /// Offset all colors for all frames using the RGB offset. Use the range of 
        /// -255 to 255 for red, green, and blue parameters. Negative values remove 
        /// color. Positive values add color.
        /// </summary>
        public static void OffsetColorsAllFrames(int animationId, int red, int green, int blue)
        {
            PluginOffsetColorsAllFrames(animationId, red, green, blue);
        }
        /// <summary>
        /// Offset all colors for all frames using the RGB offset. Use the range of 
        /// -255 to 255 for red, green, and blue parameters. Negative values remove 
        /// color. Positive values add color.
        /// </summary>
        public static void OffsetColorsAllFramesName(string path, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginOffsetColorsAllFramesName(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OffsetColorsAllFramesNameD(string path, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginOffsetColorsAllFramesNameD(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Offset all colors in the frame using the RGB offset. Use the range of -255 
        /// to 255 for red, green, and blue parameters. Negative values remove color. 
        /// Positive values add color.
        /// </summary>
        public static void OffsetColorsName(string path, int frameId, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginOffsetColorsName(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OffsetColorsNameD(string path, double frameId, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginOffsetColorsNameD(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors in the frame using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// </summary>
        public static void OffsetNonZeroColors(int animationId, int frameId, int red, int green, int blue)
        {
            PluginOffsetNonZeroColors(animationId, frameId, red, green, blue);
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors for all frames using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// </summary>
        public static void OffsetNonZeroColorsAllFrames(int animationId, int red, int green, int blue)
        {
            PluginOffsetNonZeroColorsAllFrames(animationId, red, green, blue);
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors for all frames using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// </summary>
        public static void OffsetNonZeroColorsAllFramesName(string path, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginOffsetNonZeroColorsAllFramesName(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OffsetNonZeroColorsAllFramesNameD(string path, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginOffsetNonZeroColorsAllFramesNameD(lpPath, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors in the frame using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// </summary>
        public static void OffsetNonZeroColorsName(string path, int frameId, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginOffsetNonZeroColorsName(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OffsetNonZeroColorsNameD(string path, double frameId, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginOffsetNonZeroColorsNameD(lpPath, frameId, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Opens a `Chroma` animation file so that it can be played. Returns an animation 
        /// id >= 0 upon success. Returns -1 if there was a failure. The animation 
        /// id is used in most of the API methods.
        /// </summary>
        public static int OpenAnimation(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginOpenAnimation(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OpenAnimationD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginOpenAnimationD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Opens a `Chroma` animation data from memory so that it can be played. `Data` 
        /// is a pointer to byte array of the loaded animation in memory. `Name` will 
        /// be assigned to the animation when loaded. Returns an animation id >= 0 
        /// upon success. Returns -1 if there was a failure. The animation id is used 
        /// in most of the API methods.
        /// </summary>
        public static int OpenAnimationFromMemory(byte[] data, string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            int result = PluginOpenAnimationFromMemory(data, lpName);
            FreeIntPtr(lpName);
            return result;
        }
        /// <summary>
        /// Opens a `Chroma` animation file with the `.chroma` extension. Returns zero 
        /// upon success. Returns -1 if there was a failure.
        /// </summary>
        public static int OpenEditorDialog(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginOpenEditorDialog(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Open the named animation in the editor dialog and play the animation at 
        /// start.
        /// </summary>
        public static int OpenEditorDialogAndPlay(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginOpenEditorDialogAndPlay(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OpenEditorDialogAndPlayD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginOpenEditorDialogAndPlayD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OpenEditorDialogD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginOpenEditorDialogD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Sets the `duration` for all grames in the `Chroma` animation to the `duration` 
        /// parameter. Returns the animation id upon success. Returns -1 upon failure. 
        ///
        /// </summary>
        public static int OverrideFrameDuration(int animationId, float duration)
        {
            int result = PluginOverrideFrameDuration(animationId, duration);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double OverrideFrameDurationD(double animationId, double duration)
        {
            double result = PluginOverrideFrameDurationD(animationId, duration);
            return result;
        }
        /// <summary>
        /// Override the duration of all frames with the `duration` value. Animation 
        /// is referenced by name.
        /// </summary>
        public static void OverrideFrameDurationName(string path, float duration)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginOverrideFrameDurationName(lpPath, duration);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Pause the current animation referenced by id.
        /// </summary>
        public static void PauseAnimation(int animationId)
        {
            PluginPauseAnimation(animationId);
        }
        /// <summary>
        /// Pause the current animation referenced by name.
        /// </summary>
        public static void PauseAnimationName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginPauseAnimationName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double PauseAnimationNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginPauseAnimationNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Plays the `Chroma` animation. This will load the animation, if not loaded 
        /// previously. Returns the animation id upon success. Returns -1 upon failure. 
        ///
        /// </summary>
        public static int PlayAnimation(int animationId)
        {
            int result = PluginPlayAnimation(animationId);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double PlayAnimationD(double animationId)
        {
            double result = PluginPlayAnimationD(animationId);
            return result;
        }
        /// <summary>
        /// `PluginPlayAnimationFrame` automatically handles initializing the `ChromaSDK`. 
        /// The method will play the animation given the `animationId` with looping 
        /// `on` or `off` starting at the `frameId`.
        /// </summary>
        public static void PlayAnimationFrame(int animationId, int frameId, bool loop)
        {
            PluginPlayAnimationFrame(animationId, frameId, loop);
        }
        /// <summary>
        /// `PluginPlayAnimationFrameName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The animation 
        /// will play with looping `on` or `off` starting at the `frameId`.
        /// </summary>
        public static void PlayAnimationFrameName(string path, int frameId, bool loop)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginPlayAnimationFrameName(lpPath, frameId, loop);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double PlayAnimationFrameNameD(string path, double frameId, double loop)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginPlayAnimationFrameNameD(lpPath, frameId, loop);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// `PluginPlayAnimationLoop` automatically handles initializing the `ChromaSDK`. 
        /// The method will play the animation given the `animationId` with looping 
        /// `on` or `off`.
        /// </summary>
        public static void PlayAnimationLoop(int animationId, bool loop)
        {
            PluginPlayAnimationLoop(animationId, loop);
        }
        /// <summary>
        /// `PluginPlayAnimationName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The animation 
        /// will play with looping `on` or `off`.
        /// </summary>
        public static void PlayAnimationName(string path, bool loop)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginPlayAnimationName(lpPath, loop);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double PlayAnimationNameD(string path, double loop)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginPlayAnimationNameD(lpPath, loop);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// `PluginPlayComposite` automatically handles initializing the `ChromaSDK`. 
        /// The named animation files for the `.chroma` set will be automatically opened. 
        /// The set of animations will play with looping `on` or `off`.
        /// </summary>
        public static void PlayComposite(string name, bool loop)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            PluginPlayComposite(lpName, loop);
            FreeIntPtr(lpName);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double PlayCompositeD(string name, double loop)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            double result = PluginPlayCompositeD(lpName, loop);
            FreeIntPtr(lpName);
            return result;
        }
        /// <summary>
        /// Displays the `Chroma` animation frame on `Chroma` hardware given the `frameIndex`. 
        /// Returns the animation id upon success. Returns -1 upon failure.
        /// </summary>
        public static int PreviewFrame(int animationId, int frameIndex)
        {
            int result = PluginPreviewFrame(animationId, frameIndex);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double PreviewFrameD(double animationId, double frameIndex)
        {
            double result = PluginPreviewFrameD(animationId, frameIndex);
            return result;
        }
        /// <summary>
        /// Displays the `Chroma` animation frame on `Chroma` hardware given the `frameIndex`. 
        /// Animaton is referenced by name.
        /// </summary>
        public static void PreviewFrameName(string path, int frameIndex)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginPreviewFrameName(lpPath, frameIndex);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Reduce the frames of the animation by removing every nth element. Animation 
        /// is referenced by id.
        /// </summary>
        public static void ReduceFrames(int animationId, int n)
        {
            PluginReduceFrames(animationId, n);
        }
        /// <summary>
        /// Reduce the frames of the animation by removing every nth element. Animation 
        /// is referenced by name.
        /// </summary>
        public static void ReduceFramesName(string path, int n)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginReduceFramesName(lpPath, n);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double ReduceFramesNameD(string path, double n)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginReduceFramesNameD(lpPath, n);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Resets the `Chroma` animation to 1 blank frame. Returns the animation id 
        /// upon success. Returns -1 upon failure.
        /// </summary>
        public static int ResetAnimation(int animationId)
        {
            int result = PluginResetAnimation(animationId);
            return result;
        }
        /// <summary>
        /// Resume the animation with loop `ON` or `OFF` referenced by id.
        /// </summary>
        public static void ResumeAnimation(int animationId, bool loop)
        {
            PluginResumeAnimation(animationId, loop);
        }
        /// <summary>
        /// Resume the animation with loop `ON` or `OFF` referenced by name.
        /// </summary>
        public static void ResumeAnimationName(string path, bool loop)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginResumeAnimationName(lpPath, loop);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double ResumeAnimationNameD(string path, double loop)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginResumeAnimationNameD(lpPath, loop);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Reverse the animation frame order of the `Chroma` animation. Returns the 
        /// animation id upon success. Returns -1 upon failure. Animation is referenced 
        /// by id.
        /// </summary>
        public static int Reverse(int animationId)
        {
            int result = PluginReverse(animationId);
            return result;
        }
        /// <summary>
        /// Reverse the animation frame order of the `Chroma` animation. Animation is 
        /// referenced by id.
        /// </summary>
        public static void ReverseAllFrames(int animationId)
        {
            PluginReverseAllFrames(animationId);
        }
        /// <summary>
        /// Reverse the animation frame order of the `Chroma` animation. Animation is 
        /// referenced by name.
        /// </summary>
        public static void ReverseAllFramesName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginReverseAllFramesName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double ReverseAllFramesNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginReverseAllFramesNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Save the animation referenced by id to the path specified.
        /// </summary>
        public static int SaveAnimation(int animationId, string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            int result = PluginSaveAnimation(animationId, lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Save the named animation to the target path specified.
        /// </summary>
        public static int SaveAnimationName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            int result = PluginSaveAnimationName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Set the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. The animation 
        /// is referenced by id.
        /// </summary>
        public static void Set1DColor(int animationId, int frameId, int led, int color)
        {
            PluginSet1DColor(animationId, frameId, led, color);
        }
        /// <summary>
        /// Set the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. The animation 
        /// is referenced by name.
        /// </summary>
        public static void Set1DColorName(string path, int frameId, int led, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSet1DColorName(lpPath, frameId, led, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double Set1DColorNameD(string path, double frameId, double led, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSet1DColorNameD(lpPath, frameId, led, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// The animation is referenced by id.
        /// </summary>
        public static void Set2DColor(int animationId, int frameId, int row, int column, int color)
        {
            PluginSet2DColor(animationId, frameId, row, column, color);
        }
        /// <summary>
        /// Set the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// The animation is referenced by name.
        /// </summary>
        public static void Set2DColorName(string path, int frameId, int row, int column, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSet2DColorName(lpPath, frameId, row, column, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double Set2DColorNameD(string path, double frameId, double rowColumnIndex, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSet2DColorNameD(lpPath, frameId, rowColumnIndex, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// When custom color is set, the custom key mode will be used. The animation 
        /// is referenced by id.
        /// </summary>
        public static void SetChromaCustomColorAllFrames(int animationId)
        {
            PluginSetChromaCustomColorAllFrames(animationId);
        }
        /// <summary>
        /// When custom color is set, the custom key mode will be used. The animation 
        /// is referenced by name.
        /// </summary>
        public static void SetChromaCustomColorAllFramesName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetChromaCustomColorAllFramesName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetChromaCustomColorAllFramesNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetChromaCustomColorAllFramesNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the Chroma custom key color flag on all frames. `True` changes the layout 
        /// from grid to key. `True` changes the layout from key to grid. Animation 
        /// is referenced by id.
        /// </summary>
        public static void SetChromaCustomFlag(int animationId, bool flag)
        {
            PluginSetChromaCustomFlag(animationId, flag);
        }
        /// <summary>
        /// Set the Chroma custom key color flag on all frames. `True` changes the layout 
        /// from grid to key. `True` changes the layout from key to grid. Animation 
        /// is referenced by name.
        /// </summary>
        public static void SetChromaCustomFlagName(string path, bool flag)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetChromaCustomFlagName(lpPath, flag);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetChromaCustomFlagNameD(string path, double flag)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetChromaCustomFlagNameD(lpPath, flag);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the current frame of the animation referenced by id.
        /// </summary>
        public static void SetCurrentFrame(int animationId, int frameId)
        {
            PluginSetCurrentFrame(animationId, frameId);
        }
        /// <summary>
        /// Set the current frame of the animation referenced by name.
        /// </summary>
        public static void SetCurrentFrameName(string path, int frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetCurrentFrameName(lpPath, frameId);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetCurrentFrameNameD(string path, double frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetCurrentFrameNameD(lpPath, frameId);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Changes the `deviceType` and `device` of a `Chroma` animation. If the device 
        /// is changed, the `Chroma` animation will be reset with 1 blank frame. Returns 
        /// the animation id upon success. Returns -1 upon failure.
        /// </summary>
        public static int SetDevice(int animationId, int deviceType, int device)
        {
            int result = PluginSetDevice(animationId, deviceType, device);
            return result;
        }
        /// <summary>
        /// SetEffect will display the referenced effect id.
        /// </summary>
        public static int SetEffect(Guid effectId)
        {
            int result = PluginSetEffect(effectId);
            return result;
        }
        /// <summary>
        /// When the idle animation is used, the named animation will play when no other 
        /// animations are playing. Reference the animation by id.
        /// </summary>
        public static void SetIdleAnimation(int animationId)
        {
            PluginSetIdleAnimation(animationId);
        }
        /// <summary>
        /// When the idle animation is used, the named animation will play when no other 
        /// animations are playing. Reference the animation by name.
        /// </summary>
        public static void SetIdleAnimationName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetIdleAnimationName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set animation key to a static color for the given frame.
        /// </summary>
        public static void SetKeyColor(int animationId, int frameId, int rzkey, int color)
        {
            PluginSetKeyColor(animationId, frameId, rzkey, color);
        }
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by id.
        /// </summary>
        public static void SetKeyColorAllFrames(int animationId, int rzkey, int color)
        {
            PluginSetKeyColorAllFrames(animationId, rzkey, color);
        }
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by name.
        /// </summary>
        public static void SetKeyColorAllFramesName(string path, int rzkey, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyColorAllFramesName(lpPath, rzkey, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyColorAllFramesNameD(string path, double rzkey, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyColorAllFramesNameD(lpPath, rzkey, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by id.
        /// </summary>
        public static void SetKeyColorAllFramesRGB(int animationId, int rzkey, int red, int green, int blue)
        {
            PluginSetKeyColorAllFramesRGB(animationId, rzkey, red, green, blue);
        }
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by name.
        /// </summary>
        public static void SetKeyColorAllFramesRGBName(string path, int rzkey, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyColorAllFramesRGBName(lpPath, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyColorAllFramesRGBNameD(string path, double rzkey, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyColorAllFramesRGBNameD(lpPath, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set animation key to a static color for the given frame.
        /// </summary>
        public static void SetKeyColorName(string path, int frameId, int rzkey, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyColorName(lpPath, frameId, rzkey, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyColorNameD(string path, double frameId, double rzkey, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyColorNameD(lpPath, frameId, rzkey, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the key to the specified key color for the specified frame. Animation 
        /// is referenced by id.
        /// </summary>
        public static void SetKeyColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue)
        {
            PluginSetKeyColorRGB(animationId, frameId, rzkey, red, green, blue);
        }
        /// <summary>
        /// Set the key to the specified key color for the specified frame. Animation 
        /// is referenced by name.
        /// </summary>
        public static void SetKeyColorRGBName(string path, int frameId, int rzkey, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyColorRGBName(lpPath, frameId, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyColorRGBNameD(string path, double frameId, double rzkey, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyColorRGBNameD(lpPath, frameId, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set animation key to a static color for the given frame if the existing 
        /// color is not already black.
        /// </summary>
        public static void SetKeyNonZeroColor(int animationId, int frameId, int rzkey, int color)
        {
            PluginSetKeyNonZeroColor(animationId, frameId, rzkey, color);
        }
        /// <summary>
        /// Set animation key to a static color for the given frame if the existing 
        /// color is not already black.
        /// </summary>
        public static void SetKeyNonZeroColorName(string path, int frameId, int rzkey, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyNonZeroColorName(lpPath, frameId, rzkey, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyNonZeroColorNameD(string path, double frameId, double rzkey, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyNonZeroColorNameD(lpPath, frameId, rzkey, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set the key to the specified key color for the specified frame where color 
        /// is not black. Animation is referenced by id.
        /// </summary>
        public static void SetKeyNonZeroColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue)
        {
            PluginSetKeyNonZeroColorRGB(animationId, frameId, rzkey, red, green, blue);
        }
        /// <summary>
        /// Set the key to the specified key color for the specified frame where color 
        /// is not black. Animation is referenced by name.
        /// </summary>
        public static void SetKeyNonZeroColorRGBName(string path, int frameId, int rzkey, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyNonZeroColorRGBName(lpPath, frameId, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyNonZeroColorRGBNameD(string path, double frameId, double rzkey, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyNonZeroColorRGBNameD(lpPath, frameId, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame. Animation 
        /// is referenced by id.
        /// </summary>
        public static void SetKeysColor(int animationId, int frameId, int[] rzkeys, int keyCount, int color)
        {
            PluginSetKeysColor(animationId, frameId, rzkeys, keyCount, color);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by id.
        /// </summary>
        public static void SetKeysColorAllFrames(int animationId, int[] rzkeys, int keyCount, int color)
        {
            PluginSetKeysColorAllFrames(animationId, rzkeys, keyCount, color);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by name.
        /// </summary>
        public static void SetKeysColorAllFramesName(string path, int[] rzkeys, int keyCount, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysColorAllFramesName(lpPath, rzkeys, keyCount, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by id.
        /// </summary>
        public static void SetKeysColorAllFramesRGB(int animationId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            PluginSetKeysColorAllFramesRGB(animationId, rzkeys, keyCount, red, green, blue);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by name.
        /// </summary>
        public static void SetKeysColorAllFramesRGBName(string path, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysColorAllFramesRGBName(lpPath, rzkeys, keyCount, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame.
        /// </summary>
        public static void SetKeysColorName(string path, int frameId, int[] rzkeys, int keyCount, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysColorName(lpPath, frameId, rzkeys, keyCount, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame. Animation 
        /// is referenced by id.
        /// </summary>
        public static void SetKeysColorRGB(int animationId, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            PluginSetKeysColorRGB(animationId, frameId, rzkeys, keyCount, red, green, blue);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame. Animation 
        /// is referenced by name.
        /// </summary>
        public static void SetKeysColorRGBName(string path, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysColorRGBName(lpPath, frameId, rzkeys, keyCount, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame if 
        /// the existing color is not already black.
        /// </summary>
        public static void SetKeysNonZeroColor(int animationId, int frameId, int[] rzkeys, int keyCount, int color)
        {
            PluginSetKeysNonZeroColor(animationId, frameId, rzkeys, keyCount, color);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is not black. Animation is referenced by id.
        /// </summary>
        public static void SetKeysNonZeroColorAllFrames(int animationId, int[] rzkeys, int keyCount, int color)
        {
            PluginSetKeysNonZeroColorAllFrames(animationId, rzkeys, keyCount, color);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames if the existing 
        /// color is not already black. Reference animation by name.
        /// </summary>
        public static void SetKeysNonZeroColorAllFramesName(string path, int[] rzkeys, int keyCount, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysNonZeroColorAllFramesName(lpPath, rzkeys, keyCount, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame if 
        /// the existing color is not already black. Reference animation by name.
        /// </summary>
        public static void SetKeysNonZeroColorName(string path, int frameId, int[] rzkeys, int keyCount, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysNonZeroColorName(lpPath, frameId, rzkeys, keyCount, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is not black. Animation is referenced by id.
        /// </summary>
        public static void SetKeysNonZeroColorRGB(int animationId, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            PluginSetKeysNonZeroColorRGB(animationId, frameId, rzkeys, keyCount, red, green, blue);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is not black. Animation is referenced by name.
        /// </summary>
        public static void SetKeysNonZeroColorRGBName(string path, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysNonZeroColorRGBName(lpPath, frameId, rzkeys, keyCount, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by id.
        /// </summary>
        public static void SetKeysZeroColor(int animationId, int frameId, int[] rzkeys, int keyCount, int color)
        {
            PluginSetKeysZeroColor(animationId, frameId, rzkeys, keyCount, color);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by id.
        /// </summary>
        public static void SetKeysZeroColorAllFrames(int animationId, int[] rzkeys, int keyCount, int color)
        {
            PluginSetKeysZeroColorAllFrames(animationId, rzkeys, keyCount, color);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by name.
        /// </summary>
        public static void SetKeysZeroColorAllFramesName(string path, int[] rzkeys, int keyCount, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysZeroColorAllFramesName(lpPath, rzkeys, keyCount, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by id.
        /// </summary>
        public static void SetKeysZeroColorAllFramesRGB(int animationId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            PluginSetKeysZeroColorAllFramesRGB(animationId, rzkeys, keyCount, red, green, blue);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by name.
        /// </summary>
        public static void SetKeysZeroColorAllFramesRGBName(string path, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysZeroColorAllFramesRGBName(lpPath, rzkeys, keyCount, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by name.
        /// </summary>
        public static void SetKeysZeroColorName(string path, int frameId, int[] rzkeys, int keyCount, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysZeroColorName(lpPath, frameId, rzkeys, keyCount, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by id.
        /// </summary>
        public static void SetKeysZeroColorRGB(int animationId, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            PluginSetKeysZeroColorRGB(animationId, frameId, rzkeys, keyCount, red, green, blue);
        }
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by name.
        /// </summary>
        public static void SetKeysZeroColorRGBName(string path, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeysZeroColorRGBName(lpPath, frameId, rzkeys, keyCount, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by id.
        /// </summary>
        public static void SetKeyZeroColor(int animationId, int frameId, int rzkey, int color)
        {
            PluginSetKeyZeroColor(animationId, frameId, rzkey, color);
        }
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by name.
        /// </summary>
        public static void SetKeyZeroColorName(string path, int frameId, int rzkey, int color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyZeroColorName(lpPath, frameId, rzkey, color);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyZeroColorNameD(string path, double frameId, double rzkey, double color)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyZeroColorNameD(lpPath, frameId, rzkey, color);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by id.
        /// </summary>
        public static void SetKeyZeroColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue)
        {
            PluginSetKeyZeroColorRGB(animationId, frameId, rzkey, red, green, blue);
        }
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by name.
        /// </summary>
        public static void SetKeyZeroColorRGBName(string path, int frameId, int rzkey, int red, int green, int blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginSetKeyZeroColorRGBName(lpPath, frameId, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SetKeyZeroColorRGBNameD(string path, double frameId, double rzkey, double red, double green, double blue)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginSetKeyZeroColorRGBNameD(lpPath, frameId, rzkey, red, green, blue);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Invokes the setup for a debug logging callback so that `stdout` is redirected 
        /// to the callback. This is used by `Unity` so that debug messages can appear 
        /// in the console window.
        /// </summary>
        public static void SetLogDelegate(IntPtr fp)
        {
            PluginSetLogDelegate(fp);
        }
        /// <summary>
        /// `PluginStaticColor` sets the target device to the static color.
        /// </summary>
        public static void StaticColor(int deviceType, int device, int color)
        {
            PluginStaticColor(deviceType, device, color);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double StaticColorD(double deviceType, double device, double color)
        {
            double result = PluginStaticColorD(deviceType, device, color);
            return result;
        }
        /// <summary>
        /// `PluginStopAll` will automatically stop all animations that are playing. 
        ///
        /// </summary>
        public static void StopAll()
        {
            PluginStopAll();
        }
        /// <summary>
        /// Stops animation playback if in progress. Returns the animation id upon success. 
        /// Returns -1 upon failure.
        /// </summary>
        public static int StopAnimation(int animationId)
        {
            int result = PluginStopAnimation(animationId);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double StopAnimationD(double animationId)
        {
            double result = PluginStopAnimationD(animationId);
            return result;
        }
        /// <summary>
        /// `PluginStopAnimationName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The animation 
        /// will stop if playing.
        /// </summary>
        public static void StopAnimationName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginStopAnimationName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double StopAnimationNameD(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginStopAnimationNameD(lpPath);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// `PluginStopAnimationType` automatically handles initializing the `ChromaSDK`. 
        /// If any animation is playing for the `deviceType` and `device` combination, 
        /// it will be stopped.
        /// </summary>
        public static void StopAnimationType(int deviceType, int device)
        {
            PluginStopAnimationType(deviceType, device);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double StopAnimationTypeD(double deviceType, double device)
        {
            double result = PluginStopAnimationTypeD(deviceType, device);
            return result;
        }
        /// <summary>
        /// `PluginStopComposite` automatically handles initializing the `ChromaSDK`. 
        /// The named animation files for the `.chroma` set will be automatically opened. 
        /// The set of animations will be stopped if playing.
        /// </summary>
        public static void StopComposite(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            PluginStopComposite(lpName);
            FreeIntPtr(lpName);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double StopCompositeD(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            double result = PluginStopCompositeD(lpName);
            FreeIntPtr(lpName);
            return result;
        }
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black. Source and target are referenced by id.
        /// </summary>
        public static void SubtractNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginSubtractNonZeroAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black. Source and target are referenced by name.
        /// </summary>
        public static void SubtractNonZeroAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginSubtractNonZeroAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SubtractNonZeroAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginSubtractNonZeroAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black starting at offset for the length of the source. 
        /// Source and target are referenced by id.
        /// </summary>
        public static void SubtractNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset)
        {
            PluginSubtractNonZeroAllKeysAllFramesOffset(sourceAnimationId, targetAnimationId, offset);
        }
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black starting at offset for the length of the source. 
        /// Source and target are referenced by name.
        /// </summary>
        public static void SubtractNonZeroAllKeysAllFramesOffsetName(string sourceAnimation, string targetAnimation, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginSubtractNonZeroAllKeysAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SubtractNonZeroAllKeysAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginSubtractNonZeroAllKeysAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Subtract the source color from the target where color is not black for the 
        /// source frame and target offset frame, reference source and target by id. 
        ///
        /// </summary>
        public static void SubtractNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset)
        {
            PluginSubtractNonZeroAllKeysOffset(sourceAnimationId, targetAnimationId, frameId, offset);
        }
        /// <summary>
        /// Subtract the source color from the target where color is not black for the 
        /// source frame and target offset frame, reference source and target by name. 
        ///
        /// </summary>
        public static void SubtractNonZeroAllKeysOffsetName(string sourceAnimation, string targetAnimation, int frameId, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginSubtractNonZeroAllKeysOffsetName(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SubtractNonZeroAllKeysOffsetNameD(string sourceAnimation, string targetAnimation, double frameId, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginSubtractNonZeroAllKeysOffsetNameD(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames. Reference source and target by id.
        /// </summary>
        public static void SubtractNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId)
        {
            PluginSubtractNonZeroTargetAllKeysAllFrames(sourceAnimationId, targetAnimationId);
        }
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames. Reference source and target by name.
        /// </summary>
        public static void SubtractNonZeroTargetAllKeysAllFramesName(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginSubtractNonZeroTargetAllKeysAllFramesName(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SubtractNonZeroTargetAllKeysAllFramesNameD(string sourceAnimation, string targetAnimation)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginSubtractNonZeroTargetAllKeysAllFramesNameD(lpSourceAnimation, lpTargetAnimation);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames starting at the target offset for the length of 
        /// the source. Reference source and target by id.
        /// </summary>
        public static void SubtractNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset)
        {
            PluginSubtractNonZeroTargetAllKeysAllFramesOffset(sourceAnimationId, targetAnimationId, offset);
        }
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames starting at the target offset for the length of 
        /// the source. Reference source and target by name.
        /// </summary>
        public static void SubtractNonZeroTargetAllKeysAllFramesOffsetName(string sourceAnimation, string targetAnimation, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginSubtractNonZeroTargetAllKeysAllFramesOffsetName(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SubtractNonZeroTargetAllKeysAllFramesOffsetNameD(string sourceAnimation, string targetAnimation, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginSubtractNonZeroTargetAllKeysAllFramesOffsetNameD(lpSourceAnimation, lpTargetAnimation, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black from the source frame to the target offset frame. Reference source 
        /// and target by id.
        /// </summary>
        public static void SubtractNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset)
        {
            PluginSubtractNonZeroTargetAllKeysOffset(sourceAnimationId, targetAnimationId, frameId, offset);
        }
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black from the source frame to the target offset frame. Reference source 
        /// and target by name.
        /// </summary>
        public static void SubtractNonZeroTargetAllKeysOffsetName(string sourceAnimation, string targetAnimation, int frameId, int offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            PluginSubtractNonZeroTargetAllKeysOffsetName(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double SubtractNonZeroTargetAllKeysOffsetNameD(string sourceAnimation, string targetAnimation, double frameId, double offset)
        {
            string pathSourceAnimation = sourceAnimation;
            IntPtr lpSourceAnimation = GetIntPtr(pathSourceAnimation);
            string pathTargetAnimation = targetAnimation;
            IntPtr lpTargetAnimation = GetIntPtr(pathTargetAnimation);
            double result = PluginSubtractNonZeroTargetAllKeysOffsetNameD(lpSourceAnimation, lpTargetAnimation, frameId, offset);
            FreeIntPtr(lpSourceAnimation);
            FreeIntPtr(lpTargetAnimation);
            return result;
        }
        /// <summary>
        /// Trim the end of the animation. The length of the animation will be the lastFrameId 
        /// + 1. Reference the animation by id.
        /// </summary>
        public static void TrimEndFrames(int animationId, int lastFrameId)
        {
            PluginTrimEndFrames(animationId, lastFrameId);
        }
        /// <summary>
        /// Trim the end of the animation. The length of the animation will be the lastFrameId 
        /// + 1. Reference the animation by name.
        /// </summary>
        public static void TrimEndFramesName(string path, int lastFrameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginTrimEndFramesName(lpPath, lastFrameId);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double TrimEndFramesNameD(string path, double lastFrameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginTrimEndFramesNameD(lpPath, lastFrameId);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Remove the frame from the animation. Reference animation by id.
        /// </summary>
        public static void TrimFrame(int animationId, int frameId)
        {
            PluginTrimFrame(animationId, frameId);
        }
        /// <summary>
        /// Remove the frame from the animation. Reference animation by name.
        /// </summary>
        public static void TrimFrameName(string path, int frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginTrimFrameName(lpPath, frameId);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double TrimFrameNameD(string path, double frameId)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginTrimFrameNameD(lpPath, frameId);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Trim the start of the animation starting at frame 0 for the number of frames. 
        /// Reference the animation by id.
        /// </summary>
        public static void TrimStartFrames(int animationId, int numberOfFrames)
        {
            PluginTrimStartFrames(animationId, numberOfFrames);
        }
        /// <summary>
        /// Trim the start of the animation starting at frame 0 for the number of frames. 
        /// Reference the animation by name.
        /// </summary>
        public static void TrimStartFramesName(string path, int numberOfFrames)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginTrimStartFramesName(lpPath, numberOfFrames);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double TrimStartFramesNameD(string path, double numberOfFrames)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            double result = PluginTrimStartFramesNameD(lpPath, numberOfFrames);
            FreeIntPtr(lpPath);
            return result;
        }
        /// <summary>
        /// Uninitializes the `ChromaSDK`. Returns 0 upon success. Returns -1 upon failure. 
        ///
        /// </summary>
        public static int Uninit()
        {
            int result = PluginUninit();
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double UninitD()
        {
            double result = PluginUninitD();
            return result;
        }
        /// <summary>
        /// Unloads `Chroma` effects to free up resources. Returns the animation id 
        /// upon success. Returns -1 upon failure. Reference the animation by id.
        /// </summary>
        public static int UnloadAnimation(int animationId)
        {
            int result = PluginUnloadAnimation(animationId);
            return result;
        }
        /// <summary>
        /// D suffix for limited data types.
        /// </summary>
        public static double UnloadAnimationD(double animationId)
        {
            double result = PluginUnloadAnimationD(animationId);
            return result;
        }
        /// <summary>
        /// Unload the animation effects. Reference the animation by name.
        /// </summary>
        public static void UnloadAnimationName(string path)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginUnloadAnimationName(lpPath);
            FreeIntPtr(lpPath);
        }
        /// <summary>
        /// Unload the the composite set of animation effects. Reference the animation 
        /// by name.
        /// </summary>
        public static void UnloadComposite(string name)
        {
            string pathName = name;
            IntPtr lpName = GetIntPtr(pathName);
            PluginUnloadComposite(lpName);
            FreeIntPtr(lpName);
        }
        /// <summary>
        /// Updates the `frameIndex` of the `Chroma` animation and sets the `duration` 
        /// (in seconds). The `color` is expected to be an array of the dimensions 
        /// for the `deviceType/device`. The `length` parameter is the size of the 
        /// `color` array. For `EChromaSDKDevice1DEnum` the array size should be `MAX 
        /// LEDS`. For `EChromaSDKDevice2DEnum` the array size should be `MAX ROW` 
        /// * `MAX COLUMN`. Returns the animation id upon success. Returns -1 upon 
        /// failure.
        /// </summary>
        public static int UpdateFrame(int animationId, int frameIndex, float duration, int[] colors, int length)
        {
            int result = PluginUpdateFrame(animationId, frameIndex, duration, colors, length);
            return result;
        }
        /// <summary>
        /// When the idle animation flag is true, when no other animations are playing, 
        /// the idle animation will be used. The idle animation will not be affected 
        /// by the API calls to PluginIsPlaying, PluginStopAnimationType, PluginGetPlayingAnimationId, 
        /// and PluginGetPlayingAnimationCount. Then the idle animation flag is false, 
        /// the idle animation is disabled. `Device` uses `EChromaSDKDeviceEnum` enums. 
        ///
        /// </summary>
        public static void UseIdleAnimation(int device, bool flag)
        {
            PluginUseIdleAnimation(device, flag);
        }
        /// <summary>
        /// Set idle animation flag for all devices.
        /// </summary>
        public static void UseIdleAnimations(bool flag)
        {
            PluginUseIdleAnimations(flag);
        }
        /// <summary>
        /// Set preloading animation flag, which is set to true by default. Reference 
        /// animation by id.
        /// </summary>
        public static void UsePreloading(int animationId, bool flag)
        {
            PluginUsePreloading(animationId, flag);
        }
        /// <summary>
        /// Set preloading animation flag, which is set to true by default. Reference 
        /// animation by name.
        /// </summary>
        public static void UsePreloadingName(string path, bool flag)
        {
            string pathPath = path;
            IntPtr lpPath = GetIntPtr(pathPath);
            PluginUsePreloadingName(lpPath, flag);
            FreeIntPtr(lpPath);
        }
        #endregion

        #region Private DLL Hooks
        /// <summary>
        /// Adds a frame to the `Chroma` animation and sets the `duration` (in seconds). 
        /// The `color` is expected to be an array of the dimensions for the `deviceType/device`. 
        /// The `length` parameter is the size of the `color` array. For `EChromaSDKDevice1DEnum` 
        /// the array size should be `MAX LEDS`. For `EChromaSDKDevice2DEnum` the array 
        /// size should be `MAX ROW` * `MAX COLUMN`. Returns the animation id upon 
        /// success. Returns -1 upon failure.
        /// EXPORT_API int PluginAddFrame(int animationId, float duration, int* colors, int length);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginAddFrame(int animationId, float duration, int[] colors, int length);
        /// <summary>
        /// Add source color to target where color is not black for all frames, reference 
        /// source and target by id.
        /// EXPORT_API void PluginAddNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Add source color to target where color is not black for all frames, reference 
        /// source and target by name.
        /// EXPORT_API void PluginAddNonZeroAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginAddNonZeroAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginAddNonZeroAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Add source color to target where color is not black for all frames starting 
        /// at offset for the length of the source, reference source and target by 
        /// id.
        /// EXPORT_API void PluginAddNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// <summary>
        /// Add source color to target where color is not black for all frames starting 
        /// at offset for the length of the source, reference source and target by 
        /// name.
        /// EXPORT_API void PluginAddNonZeroAllKeysAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroAllKeysAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginAddNonZeroAllKeysAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginAddNonZeroAllKeysAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double offset);
        /// <summary>
        /// Add source color to target where color is not black for the source frame 
        /// and target offset frame, reference source and target by id.
        /// EXPORT_API void PluginAddNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// <summary>
        /// Add source color to target where color is not black for the source frame 
        /// and target offset frame, reference source and target by name.
        /// EXPORT_API void PluginAddNonZeroAllKeysOffsetName(const char* sourceAnimation, const char* targetAnimation, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroAllKeysOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginAddNonZeroAllKeysOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginAddNonZeroAllKeysOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double offset);
        /// <summary>
        /// Add source color to target where the target color is not black for all frames, 
        /// reference source and target by id.
        /// EXPORT_API void PluginAddNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Add source color to target where the target color is not black for all frames, 
        /// reference source and target by name.
        /// EXPORT_API void PluginAddNonZeroTargetAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroTargetAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginAddNonZeroTargetAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginAddNonZeroTargetAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Add source color to target where the target color is not black for all frames 
        /// starting at offset for the length of the source, reference source and target 
        /// by id.
        /// EXPORT_API void PluginAddNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// <summary>
        /// Add source color to target where the target color is not black for all frames 
        /// starting at offset for the length of the source, reference source and target 
        /// by name.
        /// EXPORT_API void PluginAddNonZeroTargetAllKeysAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroTargetAllKeysAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginAddNonZeroTargetAllKeysAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginAddNonZeroTargetAllKeysAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double offset);
        /// <summary>
        /// Add source color to target where target color is not blank from the source 
        /// frame to the target offset frame, reference source and target by id.
        /// EXPORT_API void PluginAddNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// <summary>
        /// Add source color to target where target color is not blank from the source 
        /// frame to the target offset frame, reference source and target by name. 
        ///
        /// EXPORT_API void PluginAddNonZeroTargetAllKeysOffsetName(const char* sourceAnimation, const char* targetAnimation, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAddNonZeroTargetAllKeysOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginAddNonZeroTargetAllKeysOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginAddNonZeroTargetAllKeysOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double offset);
        /// <summary>
        /// Append all source frames to the target animation, reference source and target 
        /// by id.
        /// EXPORT_API void PluginAppendAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAppendAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Append all source frames to the target animation, reference source and target 
        /// by name.
        /// EXPORT_API void PluginAppendAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginAppendAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginAppendAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginAppendAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// `PluginClearAll` will issue a `CLEAR` effect for all devices.
        /// EXPORT_API void PluginClearAll();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginClearAll();
        /// <summary>
        /// `PluginClearAnimationType` will issue a `CLEAR` effect for the given device. 
        ///
        /// EXPORT_API void PluginClearAnimationType(int deviceType, int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginClearAnimationType(int deviceType, int device);
        /// <summary>
        /// `PluginCloseAll` closes all open animations so they can be reloaded from 
        /// disk. The set of animations will be stopped if playing.
        /// EXPORT_API void PluginCloseAll();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCloseAll();
        /// <summary>
        /// Closes the `Chroma` animation to free up resources referenced by id. Returns 
        /// the animation id upon success. Returns -1 upon failure. This might be used 
        /// while authoring effects if there was a change necessitating re-opening 
        /// the animation. The animation id can no longer be used once closed.
        /// EXPORT_API int PluginCloseAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCloseAnimation(int animationId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCloseAnimationD(double animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCloseAnimationD(double animationId);
        /// <summary>
        /// Closes the `Chroma` animation referenced by name so that the animation can 
        /// be reloaded from disk.
        /// EXPORT_API void PluginCloseAnimationName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCloseAnimationName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCloseAnimationNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCloseAnimationNameD(IntPtr path);
        /// <summary>
        /// `PluginCloseComposite` closes a set of animations so they can be reloaded 
        /// from disk. The set of animations will be stopped if playing.
        /// EXPORT_API void PluginCloseComposite(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCloseComposite(IntPtr name);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCloseCompositeD(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCloseCompositeD(IntPtr name);
        /// <summary>
        /// Copy animation to named target animation in memory. If target animation 
        /// exists, close first. Source is referenced by id.
        /// EXPORT_API int PluginCopyAnimation(int sourceAnimationId, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCopyAnimation(int sourceAnimationId, IntPtr targetAnimation);
        /// <summary>
        /// Copy animation to named target animation in memory. If target animation 
        /// exists, close first. Source is referenced by name.
        /// EXPORT_API void PluginCopyAnimationName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyAnimationName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyAnimationNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyAnimationNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Copy blue channel to other channels for all frames. Intensity range is 0.0 
        /// to 1.0. Reference the animation by id.
        /// EXPORT_API void PluginCopyBlueChannelAllFrames(int animationId, float redIntensity, float greenIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyBlueChannelAllFrames(int animationId, float redIntensity, float greenIntensity);
        /// <summary>
        /// Copy blue channel to other channels for all frames. Intensity range is 0.0 
        /// to 1.0. Reference the animation by name.
        /// EXPORT_API void PluginCopyBlueChannelAllFramesName(const char* path, float redIntensity, float greenIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyBlueChannelAllFramesName(IntPtr path, float redIntensity, float greenIntensity);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyBlueChannelAllFramesNameD(const char* path, double redIntensity, double greenIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyBlueChannelAllFramesNameD(IntPtr path, double redIntensity, double greenIntensity);
        /// <summary>
        /// Copy green channel to other channels for all frames. Intensity range is 
        /// 0.0 to 1.0. Reference the animation by id.
        /// EXPORT_API void PluginCopyGreenChannelAllFrames(int animationId, float redIntensity, float blueIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyGreenChannelAllFrames(int animationId, float redIntensity, float blueIntensity);
        /// <summary>
        /// Copy green channel to other channels for all frames. Intensity range is 
        /// 0.0 to 1.0. Reference the animation by name.
        /// EXPORT_API void PluginCopyGreenChannelAllFramesName(const char* path, float redIntensity, float blueIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyGreenChannelAllFramesName(IntPtr path, float redIntensity, float blueIntensity);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyGreenChannelAllFramesNameD(const char* path, double redIntensity, double blueIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyGreenChannelAllFramesNameD(IntPtr path, double redIntensity, double blueIntensity);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame. Reference the source and target by id.
        /// EXPORT_API void PluginCopyKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames. Reference the source and target by id.
        /// EXPORT_API void PluginCopyKeyColorAllFrames(int sourceAnimationId, int targetAnimationId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeyColorAllFrames(int sourceAnimationId, int targetAnimationId, int rzkey);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames. Reference the source and target by name.
        /// EXPORT_API void PluginCopyKeyColorAllFramesName(const char* sourceAnimation, const char* targetAnimation, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeyColorAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation, int rzkey);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyKeyColorAllFramesNameD(const char* sourceAnimation, const char* targetAnimation, double rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyKeyColorAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double rzkey);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames, starting at the offset for the length of the source animation. 
        /// Source and target are referenced by id.
        /// EXPORT_API void PluginCopyKeyColorAllFramesOffset(int sourceAnimationId, int targetAnimationId, int rzkey, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeyColorAllFramesOffset(int sourceAnimationId, int targetAnimationId, int rzkey, int offset);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for all frames, starting at the offset for the length of the source animation. 
        /// Source and target are referenced by name.
        /// EXPORT_API void PluginCopyKeyColorAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int rzkey, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeyColorAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int rzkey, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyKeyColorAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double rzkey, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyKeyColorAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double rzkey, double offset);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame.
        /// EXPORT_API void PluginCopyKeyColorName(const char* sourceAnimation, const char* targetAnimation, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeyColorName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int rzkey);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyKeyColorNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyKeyColorNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double rzkey);
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for the given frame. Reference the source and target by 
        /// id.
        /// EXPORT_API void PluginCopyKeysColor(int sourceAnimationId, int targetAnimationId, int frameId, int* keys, int size);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeysColor(int sourceAnimationId, int targetAnimationId, int frameId, int[] keys, int size);
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for all frames. Reference the source and target by id. 
        ///
        /// EXPORT_API void PluginCopyKeysColorAllFrames(int sourceAnimationId, int targetAnimationId, int* keys, int size);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeysColorAllFrames(int sourceAnimationId, int targetAnimationId, int[] keys, int size);
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for all frames. Reference the source and target by name. 
        ///
        /// EXPORT_API void PluginCopyKeysColorAllFramesName(const char* sourceAnimation, const char* targetAnimation, int* keys, int size);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeysColorAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation, int[] keys, int size);
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation for the given frame. Reference the source and target by 
        /// name.
        /// EXPORT_API void PluginCopyKeysColorName(const char* sourceAnimation, const char* targetAnimation, int frameId, int* keys, int size);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeysColorName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int[] keys, int size);
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation from the source frame to the target frame. Reference the 
        /// source and target by id.
        /// EXPORT_API void PluginCopyKeysColorOffset(int sourceAnimationId, int targetAnimationId, int sourceFrameId, int targetFrameId, int* keys, int size);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeysColorOffset(int sourceAnimationId, int targetAnimationId, int sourceFrameId, int targetFrameId, int[] keys, int size);
        /// <summary>
        /// Copy animation color for a set of keys from the source animation to the 
        /// target animation from the source frame to the target frame. Reference the 
        /// source and target by name.
        /// EXPORT_API void PluginCopyKeysColorOffsetName(const char* sourceAnimation, const char* targetAnimation, int sourceFrameId, int targetFrameId, int* keys, int size);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyKeysColorOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int sourceFrameId, int targetFrameId, int[] keys, int size);
        /// <summary>
        /// Copy source animation to target animation for the given frame. Source and 
        /// target are referenced by id.
        /// EXPORT_API void PluginCopyNonZeroAllKeys(int sourceAnimationId, int targetAnimationId, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeys(int sourceAnimationId, int targetAnimationId, int frameId);
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames. Reference source and target by id.
        /// EXPORT_API void PluginCopyNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames. Reference source and target by name.
        /// EXPORT_API void PluginCopyNonZeroAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames starting at the offset for the length of the source animation. The 
        /// source and target are referenced by id.
        /// EXPORT_API void PluginCopyNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// <summary>
        /// Copy nonzero colors from a source animation to a target animation for all 
        /// frames starting at the offset for the length of the source animation. The 
        /// source and target are referenced by name.
        /// EXPORT_API void PluginCopyNonZeroAllKeysAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeysAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroAllKeysAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroAllKeysAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double offset);
        /// <summary>
        /// Copy nonzero colors from source animation to target animation for the specified 
        /// frame. Source and target are referenced by id.
        /// EXPORT_API void PluginCopyNonZeroAllKeysName(const char* sourceAnimation, const char* targetAnimation, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeysName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroAllKeysNameD(const char* sourceAnimation, const char* targetAnimation, double frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroAllKeysNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation from 
        /// the source frame to the target offset frame. Source and target are referenced 
        /// by id.
        /// EXPORT_API void PluginCopyNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation from 
        /// the source frame to the target offset frame. Source and target are referenced 
        /// by name.
        /// EXPORT_API void PluginCopyNonZeroAllKeysOffsetName(const char* sourceAnimation, const char* targetAnimation, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroAllKeysOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroAllKeysOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroAllKeysOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double offset);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame where color is not zero.
        /// EXPORT_API void PluginCopyNonZeroKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey);
        /// <summary>
        /// Copy animation key color from the source animation to the target animation 
        /// for the given frame where color is not zero.
        /// EXPORT_API void PluginCopyNonZeroKeyColorName(const char* sourceAnimation, const char* targetAnimation, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroKeyColorName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int rzkey);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroKeyColorNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroKeyColorNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double rzkey);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified frame. Source and target 
        /// are referenced by id.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeys(int sourceAnimationId, int targetAnimationId, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeys(int sourceAnimationId, int targetAnimationId, int frameId);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames. Source and target are referenced 
        /// by id.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames. Source and target are referenced 
        /// by name.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroTargetAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroTargetAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames. Source and target are referenced 
        /// by name.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for all frames starting at the target offset 
        /// for the length of the source animation. Source and target animations are 
        /// referenced by name.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeysAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeysAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroTargetAllKeysAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroTargetAllKeysAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double offset);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified frame. The source and target 
        /// are referenced by name.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeysName(const char* sourceAnimation, const char* targetAnimation, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeysName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroTargetAllKeysNameD(const char* sourceAnimation, const char* targetAnimation, double frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroTargetAllKeysNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified source frame and target offset 
        /// frame. The source and target are referenced by id.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is nonzero for the specified source frame and target offset 
        /// frame. The source and target are referenced by name.
        /// EXPORT_API void PluginCopyNonZeroTargetAllKeysOffsetName(const char* sourceAnimation, const char* targetAnimation, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetAllKeysOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroTargetAllKeysOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroTargetAllKeysOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double offset);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is zero for all frames. Source and target are referenced 
        /// by id.
        /// EXPORT_API void PluginCopyNonZeroTargetZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Copy nonzero colors from the source animation to the target animation where 
        /// the target color is zero for all frames. Source and target are referenced 
        /// by name.
        /// EXPORT_API void PluginCopyNonZeroTargetZeroAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyNonZeroTargetZeroAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyNonZeroTargetZeroAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyNonZeroTargetZeroAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Copy red channel to other channels for all frames. Intensity range is 0.0 
        /// to 1.0. Reference the animation by id.
        /// EXPORT_API void PluginCopyRedChannelAllFrames(int animationId, float greenIntensity, float blueIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyRedChannelAllFrames(int animationId, float greenIntensity, float blueIntensity);
        /// <summary>
        /// Copy green channel to other channels for all frames. Intensity range is 
        /// 0.0 to 1.0. Reference the animation by name.
        /// EXPORT_API void PluginCopyRedChannelAllFramesName(const char* path, float greenIntensity, float blueIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyRedChannelAllFramesName(IntPtr path, float greenIntensity, float blueIntensity);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyRedChannelAllFramesNameD(const char* path, double greenIntensity, double blueIntensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyRedChannelAllFramesNameD(IntPtr path, double greenIntensity, double blueIntensity);
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames. 
        /// Source and target are referenced by id.
        /// EXPORT_API void PluginCopyZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames. 
        /// Source and target are referenced by name.
        /// EXPORT_API void PluginCopyZeroAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyZeroAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyZeroAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames 
        /// starting at the target offset for the length of the source animation. Source 
        /// and target are referenced by id.
        /// EXPORT_API void PluginCopyZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// <summary>
        /// Copy zero colors from source animation to target animation for all frames 
        /// starting at the target offset for the length of the source animation. Source 
        /// and target are referenced by name.
        /// EXPORT_API void PluginCopyZeroAllKeysAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroAllKeysAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyZeroAllKeysAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyZeroAllKeysAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double offset);
        /// <summary>
        /// Copy zero key color from source animation to target animation for the specified 
        /// frame. Source and target are referenced by id.
        /// EXPORT_API void PluginCopyZeroKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroKeyColor(int sourceAnimationId, int targetAnimationId, int frameId, int rzkey);
        /// <summary>
        /// Copy zero key color from source animation to target animation for the specified 
        /// frame. Source and target are referenced by name.
        /// EXPORT_API void PluginCopyZeroKeyColorName(const char* sourceAnimation, const char* targetAnimation, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroKeyColorName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int rzkey);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyZeroKeyColorNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyZeroKeyColorNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double rzkey);
        /// <summary>
        /// Copy nonzero color from source animation to target animation where target 
        /// is zero for all frames. Source and target are referenced by id.
        /// EXPORT_API void PluginCopyZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Copy nonzero color from source animation to target animation where target 
        /// is zero for all frames. Source and target are referenced by name.
        /// EXPORT_API void PluginCopyZeroTargetAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginCopyZeroTargetAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginCopyZeroTargetAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginCopyZeroTargetAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreCreateChromaLinkEffect(ChromaSDK::ChromaLink::EFFECT_TYPE Effect, PRZPARAM pParam, RZEFFECTID* pEffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreCreateChromaLinkEffect(int Effect, IntPtr pParam, out Guid pEffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreCreateEffect(RZDEVICEID DeviceId, ChromaSDK::EFFECT_TYPE Effect, PRZPARAM pParam, RZEFFECTID* pEffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreCreateEffect(Guid DeviceId, int Effect, IntPtr pParam, out Guid pEffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreCreateHeadsetEffect(ChromaSDK::Headset::EFFECT_TYPE Effect, PRZPARAM pParam, RZEFFECTID* pEffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreCreateHeadsetEffect(int Effect, IntPtr pParam, out Guid pEffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreCreateKeyboardEffect(ChromaSDK::Keyboard::EFFECT_TYPE Effect, PRZPARAM pParam, RZEFFECTID* pEffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreCreateKeyboardEffect(int Effect, IntPtr pParam, out Guid pEffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreCreateKeypadEffect(ChromaSDK::Keypad::EFFECT_TYPE Effect, PRZPARAM pParam, RZEFFECTID* pEffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreCreateKeypadEffect(int Effect, IntPtr pParam, out Guid pEffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreCreateMouseEffect(ChromaSDK::Mouse::EFFECT_TYPE Effect, PRZPARAM pParam, RZEFFECTID* pEffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreCreateMouseEffect(int Effect, IntPtr pParam, out Guid pEffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreCreateMousepadEffect(ChromaSDK::Mousepad::EFFECT_TYPE Effect, PRZPARAM pParam, RZEFFECTID* pEffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreCreateMousepadEffect(int Effect, IntPtr pParam, out Guid pEffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreDeleteEffect(RZEFFECTID EffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreDeleteEffect(Guid EffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreInit();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreInit();
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreQueryDevice(RZDEVICEID DeviceId, ChromaSDK::DEVICE_INFO_TYPE& DeviceInfo);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreQueryDevice(Guid DeviceId, out DEVICE_INFO_TYPE DeviceInfo);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreSetEffect(RZEFFECTID EffectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreSetEffect(Guid EffectId);
        /// <summary>
        /// Direct access to low level API.
        /// EXPORT_API RZRESULT PluginCoreUnInit();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCoreUnInit();
        /// <summary>
        /// Creates a `Chroma` animation at the given path. The `deviceType` parameter 
        /// uses `EChromaSDKDeviceTypeEnum` as an integer. The `device` parameter uses 
        /// `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` as an integer, respective 
        /// to the `deviceType`. Returns the animation id upon success. Returns -1 
        /// upon failure. Saves a `Chroma` animation file with the `.chroma` extension 
        /// at the given path. Returns the animation id upon success. Returns -1 upon 
        /// failure.
        /// EXPORT_API int PluginCreateAnimation(const char* path, int deviceType, int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCreateAnimation(IntPtr path, int deviceType, int device);
        /// <summary>
        /// Creates a `Chroma` animation in memory without creating a file. The `deviceType` 
        /// parameter uses `EChromaSDKDeviceTypeEnum` as an integer. The `device` parameter 
        /// uses `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` as an integer, 
        /// respective to the `deviceType`. Returns the animation id upon success. 
        /// Returns -1 upon failure. Returns the animation id upon success. Returns 
        /// -1 upon failure.
        /// EXPORT_API int PluginCreateAnimationInMemory(int deviceType, int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCreateAnimationInMemory(int deviceType, int device);
        /// <summary>
        /// Create a device specific effect.
        /// EXPORT_API RZRESULT PluginCreateEffect(RZDEVICEID deviceId, ChromaSDK::EFFECT_TYPE effect, int* colors, int size, ChromaSDK::FChromaSDKGuid* effectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginCreateEffect(Guid deviceId, int effect, int[] colors, int size, out FChromaSDKGuid effectId);
        /// <summary>
        /// Delete an effect given the effect id.
        /// EXPORT_API RZRESULT PluginDeleteEffect(const ChromaSDK::FChromaSDKGuid& effectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginDeleteEffect(Guid effectId);
        /// <summary>
        /// Duplicate the first animation frame so that the animation length matches 
        /// the frame count. Animation is referenced by id.
        /// EXPORT_API void PluginDuplicateFirstFrame(int animationId, int frameCount);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginDuplicateFirstFrame(int animationId, int frameCount);
        /// <summary>
        /// Duplicate the first animation frame so that the animation length matches 
        /// the frame count. Animation is referenced by name.
        /// EXPORT_API void PluginDuplicateFirstFrameName(const char* path, int frameCount);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginDuplicateFirstFrameName(IntPtr path, int frameCount);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginDuplicateFirstFrameNameD(const char* path, double frameCount);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginDuplicateFirstFrameNameD(IntPtr path, double frameCount);
        /// <summary>
        /// Duplicate all the frames of the animation to double the animation length. 
        /// Frame 1 becomes frame 1 and 2. Frame 2 becomes frame 3 and 4. And so on. 
        /// The animation is referenced by id.
        /// EXPORT_API void PluginDuplicateFrames(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginDuplicateFrames(int animationId);
        /// <summary>
        /// Duplicate all the frames of the animation to double the animation length. 
        /// Frame 1 becomes frame 1 and 2. Frame 2 becomes frame 3 and 4. And so on. 
        /// The animation is referenced by name.
        /// EXPORT_API void PluginDuplicateFramesName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginDuplicateFramesName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginDuplicateFramesNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginDuplicateFramesNameD(IntPtr path);
        /// <summary>
        /// Duplicate all the animation frames in reverse so that the animation plays 
        /// forwards and backwards. Animation is referenced by id.
        /// EXPORT_API void PluginDuplicateMirrorFrames(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginDuplicateMirrorFrames(int animationId);
        /// <summary>
        /// Duplicate all the animation frames in reverse so that the animation plays 
        /// forwards and backwards. Animation is referenced by name.
        /// EXPORT_API void PluginDuplicateMirrorFramesName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginDuplicateMirrorFramesName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginDuplicateMirrorFramesNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginDuplicateMirrorFramesNameD(IntPtr path);
        /// <summary>
        /// Fade the animation to black starting at the fade frame index to the end 
        /// of the animation. Animation is referenced by id.
        /// EXPORT_API void PluginFadeEndFrames(int animationId, int fade);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFadeEndFrames(int animationId, int fade);
        /// <summary>
        /// Fade the animation to black starting at the fade frame index to the end 
        /// of the animation. Animation is referenced by name.
        /// EXPORT_API void PluginFadeEndFramesName(const char* path, int fade);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFadeEndFramesName(IntPtr path, int fade);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFadeEndFramesNameD(const char* path, double fade);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFadeEndFramesNameD(IntPtr path, double fade);
        /// <summary>
        /// Fade the animation from black to full color starting at 0 to the fade frame 
        /// index. Animation is referenced by id.
        /// EXPORT_API void PluginFadeStartFrames(int animationId, int fade);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFadeStartFrames(int animationId, int fade);
        /// <summary>
        /// Fade the animation from black to full color starting at 0 to the fade frame 
        /// index. Animation is referenced by name.
        /// EXPORT_API void PluginFadeStartFramesName(const char* path, int fade);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFadeStartFramesName(IntPtr path, int fade);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFadeStartFramesNameD(const char* path, double fade);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFadeStartFramesNameD(IntPtr path, double fade);
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginFillColor(int animationId, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColor(int animationId, int frameId, int color);
        /// <summary>
        /// Set the RGB value for all colors for all frames. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginFillColorAllFrames(int animationId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColorAllFrames(int animationId, int color);
        /// <summary>
        /// Set the RGB value for all colors for all frames. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginFillColorAllFramesName(const char* path, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColorAllFramesName(IntPtr path, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillColorAllFramesNameD(const char* path, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillColorAllFramesNameD(IntPtr path, double color);
        /// <summary>
        /// Set the RGB value for all colors for all frames. Use the range of 0 to 255 
        /// for red, green, and blue parameters. Animation is referenced by id.
        /// EXPORT_API void PluginFillColorAllFramesRGB(int animationId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColorAllFramesRGB(int animationId, int red, int green, int blue);
        /// <summary>
        /// Set the RGB value for all colors for all frames. Use the range of 0 to 255 
        /// for red, green, and blue parameters. Animation is referenced by name.
        /// EXPORT_API void PluginFillColorAllFramesRGBName(const char* path, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColorAllFramesRGBName(IntPtr path, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillColorAllFramesRGBNameD(const char* path, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillColorAllFramesRGBNameD(IntPtr path, double red, double green, double blue);
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginFillColorName(const char* path, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColorName(IntPtr path, int frameId, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillColorNameD(const char* path, double frameId, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillColorNameD(IntPtr path, double frameId, double color);
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginFillColorRGB(int animationId, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColorRGB(int animationId, int frameId, int red, int green, int blue);
        /// <summary>
        /// Set the RGB value for all colors in the specified frame. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginFillColorRGBName(const char* path, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillColorRGBName(IntPtr path, int frameId, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillColorRGBNameD(const char* path, double frameId, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillColorRGBNameD(IntPtr path, double frameId, double red, double green, double blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Animation is referenced by id.
        /// EXPORT_API void PluginFillNonZeroColor(int animationId, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColor(int animationId, int frameId, int color);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Animation is referenced by id.
        /// EXPORT_API void PluginFillNonZeroColorAllFrames(int animationId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColorAllFrames(int animationId, int color);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Animation is referenced by name.
        /// EXPORT_API void PluginFillNonZeroColorAllFramesName(const char* path, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColorAllFramesName(IntPtr path, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillNonZeroColorAllFramesNameD(const char* path, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillNonZeroColorAllFramesNameD(IntPtr path, double color);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginFillNonZeroColorAllFramesRGB(int animationId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColorAllFramesRGB(int animationId, int red, int green, int blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors for all frames. 
        /// Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginFillNonZeroColorAllFramesRGBName(const char* path, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColorAllFramesRGBName(IntPtr path, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillNonZeroColorAllFramesRGBNameD(const char* path, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillNonZeroColorAllFramesRGBNameD(IntPtr path, double red, double green, double blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Animation is referenced by name.
        /// EXPORT_API void PluginFillNonZeroColorName(const char* path, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColorName(IntPtr path, int frameId, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillNonZeroColorNameD(const char* path, double frameId, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillNonZeroColorNameD(IntPtr path, double frameId, double color);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginFillNonZeroColorRGB(int animationId, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColorRGB(int animationId, int frameId, int red, int green, int blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Set the RGB value for a subset of colors in the specified 
        /// frame. Use the range of 0 to 255 for red, green, and blue parameters. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginFillNonZeroColorRGBName(const char* path, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillNonZeroColorRGBName(IntPtr path, int frameId, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillNonZeroColorRGBNameD(const char* path, double frameId, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillNonZeroColorRGBNameD(IntPtr path, double frameId, double red, double green, double blue);
        /// <summary>
        /// Fill the frame with random RGB values for the given frame. Animation is 
        /// referenced by id.
        /// EXPORT_API void PluginFillRandomColors(int animationId, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColors(int animationId, int frameId);
        /// <summary>
        /// Fill the frame with random RGB values for all frames. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginFillRandomColorsAllFrames(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColorsAllFrames(int animationId);
        /// <summary>
        /// Fill the frame with random RGB values for all frames. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginFillRandomColorsAllFramesName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColorsAllFramesName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillRandomColorsAllFramesNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillRandomColorsAllFramesNameD(IntPtr path);
        /// <summary>
        /// Fill the frame with random black and white values for the specified frame. 
        /// Animation is referenced by id.
        /// EXPORT_API void PluginFillRandomColorsBlackAndWhite(int animationId, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColorsBlackAndWhite(int animationId, int frameId);
        /// <summary>
        /// Fill the frame with random black and white values for all frames. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginFillRandomColorsBlackAndWhiteAllFrames(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColorsBlackAndWhiteAllFrames(int animationId);
        /// <summary>
        /// Fill the frame with random black and white values for all frames. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginFillRandomColorsBlackAndWhiteAllFramesName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColorsBlackAndWhiteAllFramesName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillRandomColorsBlackAndWhiteAllFramesNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillRandomColorsBlackAndWhiteAllFramesNameD(IntPtr path);
        /// <summary>
        /// Fill the frame with random black and white values for the specified frame. 
        /// Animation is referenced by name.
        /// EXPORT_API void PluginFillRandomColorsBlackAndWhiteName(const char* path, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColorsBlackAndWhiteName(IntPtr path, int frameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillRandomColorsBlackAndWhiteNameD(const char* path, double frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillRandomColorsBlackAndWhiteNameD(IntPtr path, double frameId);
        /// <summary>
        /// Fill the frame with random RGB values for the given frame. Animation is 
        /// referenced by name.
        /// EXPORT_API void PluginFillRandomColorsName(const char* path, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillRandomColorsName(IntPtr path, int frameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillRandomColorsNameD(const char* path, double frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillRandomColorsNameD(IntPtr path, double frameId);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdColors(int animationId, int frameId, int threshold, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColors(int animationId, int frameId, int threshold, int color);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdColorsAllFrames(int animationId, int threshold, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsAllFrames(int animationId, int threshold, int color);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdColorsAllFramesName(const char* path, int threshold, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsAllFramesName(IntPtr path, int threshold, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdColorsAllFramesNameD(const char* path, double threshold, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdColorsAllFramesNameD(IntPtr path, double threshold, double color);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdColorsAllFramesRGB(int animationId, int threshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsAllFramesRGB(int animationId, int threshold, int red, int green, int blue);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdColorsAllFramesRGBName(const char* path, int threshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsAllFramesRGBName(IntPtr path, int threshold, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdColorsAllFramesRGBNameD(const char* path, double threshold, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdColorsAllFramesRGBNameD(IntPtr path, double threshold, double red, double green, double blue);
        /// <summary>
        /// Fill all frames with the min RGB color where the animation color is less 
        /// than the min threshold AND with the max RGB color where the animation is 
        /// more than the max threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdColorsMinMaxAllFramesRGB(int animationId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsMinMaxAllFramesRGB(int animationId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// <summary>
        /// Fill all frames with the min RGB color where the animation color is less 
        /// than the min threshold AND with the max RGB color where the animation is 
        /// more than the max threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdColorsMinMaxAllFramesRGBName(const char* path, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsMinMaxAllFramesRGBName(IntPtr path, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdColorsMinMaxAllFramesRGBNameD(const char* path, double minThreshold, double minRed, double minGreen, double minBlue, double maxThreshold, double maxRed, double maxGreen, double maxBlue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdColorsMinMaxAllFramesRGBNameD(IntPtr path, double minThreshold, double minRed, double minGreen, double minBlue, double maxThreshold, double maxRed, double maxGreen, double maxBlue);
        /// <summary>
        /// Fill the specified frame with the min RGB color where the animation color 
        /// is less than the min threshold AND with the max RGB color where the animation 
        /// is more than the max threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdColorsMinMaxRGB(int animationId, int frameId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsMinMaxRGB(int animationId, int frameId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// <summary>
        /// Fill the specified frame with the min RGB color where the animation color 
        /// is less than the min threshold AND with the max RGB color where the animation 
        /// is more than the max threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdColorsMinMaxRGBName(const char* path, int frameId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsMinMaxRGBName(IntPtr path, int frameId, int minThreshold, int minRed, int minGreen, int minBlue, int maxThreshold, int maxRed, int maxGreen, int maxBlue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdColorsMinMaxRGBNameD(const char* path, double frameId, double minThreshold, double minRed, double minGreen, double minBlue, double maxThreshold, double maxRed, double maxGreen, double maxBlue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdColorsMinMaxRGBNameD(IntPtr path, double frameId, double minThreshold, double minRed, double minGreen, double minBlue, double maxThreshold, double maxRed, double maxGreen, double maxBlue);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdColorsName(const char* path, int frameId, int threshold, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsName(IntPtr path, int frameId, int threshold, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdColorsNameD(const char* path, double frameId, double threshold, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdColorsNameD(IntPtr path, double frameId, double threshold, double color);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdColorsRGB(int animationId, int frameId, int threshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsRGB(int animationId, int frameId, int threshold, int red, int green, int blue);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdColorsRGBName(const char* path, int frameId, int threshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdColorsRGBName(IntPtr path, int frameId, int threshold, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdColorsRGBNameD(const char* path, double frameId, double threshold, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdColorsRGBNameD(IntPtr path, double frameId, double threshold, double red, double green, double blue);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdRGBColorsAllFramesRGB(int animationId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdRGBColorsAllFramesRGB(int animationId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is less than the 
        /// RGB threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdRGBColorsAllFramesRGBName(const char* path, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdRGBColorsAllFramesRGBName(IntPtr path, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdRGBColorsAllFramesRGBNameD(const char* path, double redThreshold, double greenThreshold, double blueThreshold, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdRGBColorsAllFramesRGBNameD(IntPtr path, double redThreshold, double greenThreshold, double blueThreshold, double red, double green, double blue);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by id.
        /// EXPORT_API void PluginFillThresholdRGBColorsRGB(int animationId, int frameId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdRGBColorsRGB(int animationId, int frameId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is less 
        /// than the RGB threshold. Animation is referenced by name.
        /// EXPORT_API void PluginFillThresholdRGBColorsRGBName(const char* path, int frameId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillThresholdRGBColorsRGBName(IntPtr path, int frameId, int redThreshold, int greenThreshold, int blueThreshold, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillThresholdRGBColorsRGBNameD(const char* path, double frameId, double redThreshold, double greenThreshold, double blueThreshold, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillThresholdRGBColorsRGBNameD(IntPtr path, double frameId, double redThreshold, double greenThreshold, double blueThreshold, double red, double green, double blue);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by id.
        /// EXPORT_API void PluginFillZeroColor(int animationId, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColor(int animationId, int frameId, int color);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginFillZeroColorAllFrames(int animationId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColorAllFrames(int animationId, int color);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginFillZeroColorAllFramesName(const char* path, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColorAllFramesName(IntPtr path, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillZeroColorAllFramesNameD(const char* path, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillZeroColorAllFramesNameD(IntPtr path, double color);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginFillZeroColorAllFramesRGB(int animationId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColorAllFramesRGB(int animationId, int red, int green, int blue);
        /// <summary>
        /// Fill all frames with RGB color where the animation color is zero. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginFillZeroColorAllFramesRGBName(const char* path, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColorAllFramesRGBName(IntPtr path, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillZeroColorAllFramesRGBNameD(const char* path, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillZeroColorAllFramesRGBNameD(IntPtr path, double red, double green, double blue);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by name.
        /// EXPORT_API void PluginFillZeroColorName(const char* path, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColorName(IntPtr path, int frameId, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillZeroColorNameD(const char* path, double frameId, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillZeroColorNameD(IntPtr path, double frameId, double color);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by id.
        /// EXPORT_API void PluginFillZeroColorRGB(int animationId, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColorRGB(int animationId, int frameId, int red, int green, int blue);
        /// <summary>
        /// Fill the specified frame with RGB color where the animation color is zero. 
        /// Animation is referenced by name.
        /// EXPORT_API void PluginFillZeroColorRGBName(const char* path, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginFillZeroColorRGBName(IntPtr path, int frameId, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginFillZeroColorRGBNameD(const char* path, double frameId, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginFillZeroColorRGBNameD(IntPtr path, double frameId, double red, double green, double blue);
        /// <summary>
        /// Get the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. Animation is 
        /// referenced by id.
        /// EXPORT_API int PluginGet1DColor(int animationId, int frameId, int led);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGet1DColor(int animationId, int frameId, int led);
        /// <summary>
        /// Get the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. Animation is 
        /// referenced by name.
        /// EXPORT_API int PluginGet1DColorName(const char* path, int frameId, int led);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGet1DColorName(IntPtr path, int frameId, int led);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGet1DColorNameD(const char* path, double frameId, double led);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGet1DColorNameD(IntPtr path, double frameId, double led);
        /// <summary>
        /// Get the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// Animation is referenced by id.
        /// EXPORT_API int PluginGet2DColor(int animationId, int frameId, int row, int column);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGet2DColor(int animationId, int frameId, int row, int column);
        /// <summary>
        /// Get the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// Animation is referenced by name.
        /// EXPORT_API int PluginGet2DColorName(const char* path, int frameId, int row, int column);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGet2DColorName(IntPtr path, int frameId, int row, int column);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGet2DColorNameD(const char* path, double frameId, double row, double column);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGet2DColorNameD(IntPtr path, double frameId, double row, double column);
        /// <summary>
        /// Get the animation id for the named animation.
        /// EXPORT_API int PluginGetAnimation(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetAnimation(IntPtr name);
        /// <summary>
        /// `PluginGetAnimationCount` will return the number of loaded animations.
        /// EXPORT_API int PluginGetAnimationCount();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetAnimationCount();
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetAnimationD(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetAnimationD(IntPtr name);
        /// <summary>
        /// `PluginGetAnimationId` will return the `animationId` given the `index` of 
        /// the loaded animation. The `index` is zero-based and less than the number 
        /// returned by `PluginGetAnimationCount`. Use `PluginGetAnimationName` to 
        /// get the name of the animation.
        /// EXPORT_API int PluginGetAnimationId(int index);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetAnimationId(int index);
        /// <summary>
        /// `PluginGetAnimationName` takes an `animationId` and returns the name of 
        /// the animation of the `.chroma` animation file. If a name is not available 
        /// then an empty string will be returned.
        /// EXPORT_API const char* PluginGetAnimationName(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr PluginGetAnimationName(int animationId);
        /// <summary>
        /// Get the current frame of the animation referenced by id.
        /// EXPORT_API int PluginGetCurrentFrame(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetCurrentFrame(int animationId);
        /// <summary>
        /// Get the current frame of the animation referenced by name.
        /// EXPORT_API int PluginGetCurrentFrameName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetCurrentFrameName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetCurrentFrameNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetCurrentFrameNameD(IntPtr path);
        /// <summary>
        /// Returns the `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` of a `Chroma` 
        /// animation respective to the `deviceType`, as an integer upon success. Returns 
        /// -1 upon failure.
        /// EXPORT_API int PluginGetDevice(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetDevice(int animationId);
        /// <summary>
        /// Returns the `EChromaSDKDevice1DEnum` or `EChromaSDKDevice2DEnum` of a `Chroma` 
        /// animation respective to the `deviceType`, as an integer upon success. Returns 
        /// -1 upon failure.
        /// EXPORT_API int PluginGetDeviceName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetDeviceName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetDeviceNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetDeviceNameD(IntPtr path);
        /// <summary>
        /// Returns the `EChromaSDKDeviceTypeEnum` of a `Chroma` animation as an integer 
        /// upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginGetDeviceType(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetDeviceType(int animationId);
        /// <summary>
        /// Returns the `EChromaSDKDeviceTypeEnum` of a `Chroma` animation as an integer 
        /// upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginGetDeviceTypeName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetDeviceTypeName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetDeviceTypeNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetDeviceTypeNameD(IntPtr path);
        /// <summary>
        /// Gets the frame colors and duration (in seconds) for a `Chroma` animation. 
        /// The `color` is expected to be an array of the expected dimensions for the 
        /// `deviceType/device`. The `length` parameter is the size of the `color` 
        /// array. For `EChromaSDKDevice1DEnum` the array size should be `MAX LEDS`. 
        /// For `EChromaSDKDevice2DEnum` the array size should be `MAX ROW` * `MAX 
        /// COLUMN`. Returns the animation id upon success. Returns -1 upon failure. 
        ///
        /// EXPORT_API int PluginGetFrame(int animationId, int frameIndex, float* duration, int* colors, int length);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetFrame(int animationId, int frameIndex, out float duration, int[] colors, int length);
        /// <summary>
        /// Returns the frame count of a `Chroma` animation upon success. Returns -1 
        /// upon failure.
        /// EXPORT_API int PluginGetFrameCount(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetFrameCount(int animationId);
        /// <summary>
        /// Returns the frame count of a `Chroma` animation upon success. Returns -1 
        /// upon failure.
        /// EXPORT_API int PluginGetFrameCountName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetFrameCountName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetFrameCountNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetFrameCountNameD(IntPtr path);
        /// <summary>
        /// Get the color of an animation key for the given frame referenced by id. 
        ///
        /// EXPORT_API int PluginGetKeyColor(int animationId, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetKeyColor(int animationId, int frameId, int rzkey);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetKeyColorD(const char* path, double frameId, double rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetKeyColorD(IntPtr path, double frameId, double rzkey);
        /// <summary>
        /// Get the color of an animation key for the given frame referenced by name. 
        ///
        /// EXPORT_API int PluginGetKeyColorName(const char* path, int frameId, int rzkey);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetKeyColorName(IntPtr path, int frameId, int rzkey);
        /// <summary>
        /// Returns `RZRESULT_SUCCESS` if the plugin has been initialized successfully. 
        /// Returns `RZRESULT_DLL_NOT_FOUND` if core Chroma library is not found. Returns 
        /// `RZRESULT_DLL_INVALID_SIGNATURE` if core Chroma library has an invalid 
        /// signature.
        /// EXPORT_API RZRESULT PluginGetLibraryLoadedState();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetLibraryLoadedState();
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetLibraryLoadedStateD();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetLibraryLoadedStateD();
        /// <summary>
        /// Returns the `MAX COLUMN` given the `EChromaSDKDevice2DEnum` device as an 
        /// integer upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginGetMaxColumn(int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetMaxColumn(int device);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetMaxColumnD(double device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetMaxColumnD(double device);
        /// <summary>
        /// Returns the MAX LEDS given the `EChromaSDKDevice1DEnum` device as an integer 
        /// upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginGetMaxLeds(int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetMaxLeds(int device);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetMaxLedsD(double device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetMaxLedsD(double device);
        /// <summary>
        /// Returns the `MAX ROW` given the `EChromaSDKDevice2DEnum` device as an integer 
        /// upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginGetMaxRow(int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetMaxRow(int device);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetMaxRowD(double device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetMaxRowD(double device);
        /// <summary>
        /// `PluginGetPlayingAnimationCount` will return the number of playing animations. 
        ///
        /// EXPORT_API int PluginGetPlayingAnimationCount();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetPlayingAnimationCount();
        /// <summary>
        /// `PluginGetPlayingAnimationId` will return the `animationId` given the `index` 
        /// of the playing animation. The `index` is zero-based and less than the number 
        /// returned by `PluginGetPlayingAnimationCount`. Use `PluginGetAnimationName` 
        /// to get the name of the animation.
        /// EXPORT_API int PluginGetPlayingAnimationId(int index);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetPlayingAnimationId(int index);
        /// <summary>
        /// Get the RGB color given red, green, and blue.
        /// EXPORT_API int PluginGetRGB(int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginGetRGB(int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginGetRGBD(double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginGetRGBD(double red, double green, double blue);
        /// <summary>
        /// Check if the animation has loop enabled referenced by id.
        /// EXPORT_API bool PluginHasAnimationLoop(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginHasAnimationLoop(int animationId);
        /// <summary>
        /// Check if the animation has loop enabled referenced by name.
        /// EXPORT_API bool PluginHasAnimationLoopName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginHasAnimationLoopName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginHasAnimationLoopNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginHasAnimationLoopNameD(IntPtr path);
        /// <summary>
        /// Initialize the ChromaSDK. Zero indicates  success, otherwise failure. Many 
        /// API methods auto initialize the ChromaSDK if not already initialized.
        /// EXPORT_API RZRESULT PluginInit();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginInit();
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginInitD();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginInitD();
        /// <summary>
        /// Insert an animation delay by duplicating the frame by the delay number of 
        /// times. Animation is referenced by id.
        /// EXPORT_API void PluginInsertDelay(int animationId, int frameId, int delay);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInsertDelay(int animationId, int frameId, int delay);
        /// <summary>
        /// Insert an animation delay by duplicating the frame by the delay number of 
        /// times. Animation is referenced by name.
        /// EXPORT_API void PluginInsertDelayName(const char* path, int frameId, int delay);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInsertDelayName(IntPtr path, int frameId, int delay);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginInsertDelayNameD(const char* path, double frameId, double delay);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginInsertDelayNameD(IntPtr path, double frameId, double delay);
        /// <summary>
        /// Duplicate the source frame index at the target frame index. Animation is 
        /// referenced by id.
        /// EXPORT_API void PluginInsertFrame(int animationId, int sourceFrame, int targetFrame);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInsertFrame(int animationId, int sourceFrame, int targetFrame);
        /// <summary>
        /// Duplicate the source frame index at the target frame index. Animation is 
        /// referenced by name.
        /// EXPORT_API void PluginInsertFrameName(const char* path, int sourceFrame, int targetFrame);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInsertFrameName(IntPtr path, int sourceFrame, int targetFrame);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginInsertFrameNameD(const char* path, double sourceFrame, double targetFrame);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginInsertFrameNameD(IntPtr path, double sourceFrame, double targetFrame);
        /// <summary>
        /// Invert all the colors at the specified frame. Animation is referenced by 
        /// id.
        /// EXPORT_API void PluginInvertColors(int animationId, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInvertColors(int animationId, int frameId);
        /// <summary>
        /// Invert all the colors for all frames. Animation is referenced by id.
        /// EXPORT_API void PluginInvertColorsAllFrames(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInvertColorsAllFrames(int animationId);
        /// <summary>
        /// Invert all the colors for all frames. Animation is referenced by name.
        /// EXPORT_API void PluginInvertColorsAllFramesName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInvertColorsAllFramesName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginInvertColorsAllFramesNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginInvertColorsAllFramesNameD(IntPtr path);
        /// <summary>
        /// Invert all the colors at the specified frame. Animation is referenced by 
        /// name.
        /// EXPORT_API void PluginInvertColorsName(const char* path, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginInvertColorsName(IntPtr path, int frameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginInvertColorsNameD(const char* path, double frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginInvertColorsNameD(IntPtr path, double frameId);
        /// <summary>
        /// Check if the animation is paused referenced by id.
        /// EXPORT_API bool PluginIsAnimationPaused(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsAnimationPaused(int animationId);
        /// <summary>
        /// Check if the animation is paused referenced by name.
        /// EXPORT_API bool PluginIsAnimationPausedName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsAnimationPausedName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginIsAnimationPausedNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginIsAnimationPausedNameD(IntPtr path);
        /// <summary>
        /// The editor dialog is a non-blocking modal window, this method returns true 
        /// if the modal window is open, otherwise false.
        /// EXPORT_API bool PluginIsDialogOpen();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsDialogOpen();
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginIsDialogOpenD();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginIsDialogOpenD();
        /// <summary>
        /// Returns true if the plugin has been initialized. Returns false if the plugin 
        /// is uninitialized.
        /// EXPORT_API bool PluginIsInitialized();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsInitialized();
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginIsInitializedD();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginIsInitializedD();
        /// <summary>
        /// If the method can be invoked the method returns true.
        /// EXPORT_API bool PluginIsPlatformSupported();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsPlatformSupported();
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginIsPlatformSupportedD();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginIsPlatformSupportedD();
        /// <summary>
        /// `PluginIsPlayingName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The method 
        /// will return whether the animation is playing or not. Animation is referenced 
        /// by id.
        /// EXPORT_API bool PluginIsPlaying(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsPlaying(int animationId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginIsPlayingD(double animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginIsPlayingD(double animationId);
        /// <summary>
        /// `PluginIsPlayingName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The method 
        /// will return whether the animation is playing or not. Animation is referenced 
        /// by name.
        /// EXPORT_API bool PluginIsPlayingName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsPlayingName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginIsPlayingNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginIsPlayingNameD(IntPtr path);
        /// <summary>
        /// `PluginIsPlayingType` automatically handles initializing the `ChromaSDK`. 
        /// If any animation is playing for the `deviceType` and `device` combination, 
        /// the method will return true, otherwise false.
        /// EXPORT_API bool PluginIsPlayingType(int deviceType, int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool PluginIsPlayingType(int deviceType, int device);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginIsPlayingTypeD(double deviceType, double device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginIsPlayingTypeD(double deviceType, double device);
        /// <summary>
        /// Do a lerp math operation on a float.
        /// EXPORT_API float PluginLerp(float start, float end, float amt);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern float PluginLerp(float start, float end, float amt);
        /// <summary>
        /// Lerp from one color to another given t in the range 0.0 to 1.0.
        /// EXPORT_API int PluginLerpColor(int from, int to, float t);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginLerpColor(int from, int to, float t);
        /// <summary>
        /// Loads `Chroma` effects so that the animation can be played immediately. 
        /// Returns the animation id upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginLoadAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginLoadAnimation(int animationId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginLoadAnimationD(double animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginLoadAnimationD(double animationId);
        /// <summary>
        /// Load the named animation.
        /// EXPORT_API void PluginLoadAnimationName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginLoadAnimationName(IntPtr path);
        /// <summary>
        /// Load a composite set of animations.
        /// EXPORT_API void PluginLoadComposite(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginLoadComposite(IntPtr name);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginMakeBlankFrames(int animationId, int frameCount, float duration, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFrames(int animationId, int frameCount, float duration, int color);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginMakeBlankFramesName(const char* path, int frameCount, float duration, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFramesName(IntPtr path, int frameCount, float duration, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMakeBlankFramesNameD(const char* path, double frameCount, double duration, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMakeBlankFramesNameD(IntPtr path, double frameCount, double duration, double color);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginMakeBlankFramesRandom(int animationId, int frameCount, float duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFramesRandom(int animationId, int frameCount, float duration);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random black and white. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginMakeBlankFramesRandomBlackAndWhite(int animationId, int frameCount, float duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFramesRandomBlackAndWhite(int animationId, int frameCount, float duration);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random black and white. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginMakeBlankFramesRandomBlackAndWhiteName(const char* path, int frameCount, float duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFramesRandomBlackAndWhiteName(IntPtr path, int frameCount, float duration);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMakeBlankFramesRandomBlackAndWhiteNameD(const char* path, double frameCount, double duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMakeBlankFramesRandomBlackAndWhiteNameD(IntPtr path, double frameCount, double duration);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color is random. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginMakeBlankFramesRandomName(const char* path, int frameCount, float duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFramesRandomName(IntPtr path, int frameCount, float duration);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMakeBlankFramesRandomNameD(const char* path, double frameCount, double duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMakeBlankFramesRandomNameD(IntPtr path, double frameCount, double duration);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginMakeBlankFramesRGB(int animationId, int frameCount, float duration, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFramesRGB(int animationId, int frameCount, float duration, int red, int green, int blue);
        /// <summary>
        /// Make a blank animation for the length of the frame count. Frame duration 
        /// defaults to the duration. The frame color defaults to color. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginMakeBlankFramesRGBName(const char* path, int frameCount, float duration, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMakeBlankFramesRGBName(IntPtr path, int frameCount, float duration, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMakeBlankFramesRGBNameD(const char* path, double frameCount, double duration, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMakeBlankFramesRGBNameD(IntPtr path, double frameCount, double duration, double red, double green, double blue);
        /// <summary>
        /// Flips the color grid horizontally for all `Chroma` animation frames. Returns 
        /// the animation id upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginMirrorHorizontally(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginMirrorHorizontally(int animationId);
        /// <summary>
        /// Flips the color grid vertically for all `Chroma` animation frames. This 
        /// method has no effect for `EChromaSDKDevice1DEnum` devices. Returns the 
        /// animation id upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginMirrorVertically(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginMirrorVertically(int animationId);
        /// <summary>
        /// Multiply the color intensity with the lerp result from color 1 to color 
        /// 2 using the frame index divided by the frame count for the `t` parameter. 
        /// Animation is referenced in id.
        /// EXPORT_API void PluginMultiplyColorLerpAllFrames(int animationId, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyColorLerpAllFrames(int animationId, int color1, int color2);
        /// <summary>
        /// Multiply the color intensity with the lerp result from color 1 to color 
        /// 2 using the frame index divided by the frame count for the `t` parameter. 
        /// Animation is referenced in name.
        /// EXPORT_API void PluginMultiplyColorLerpAllFramesName(const char* path, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyColorLerpAllFramesName(IntPtr path, int color1, int color2);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyColorLerpAllFramesNameD(const char* path, double color1, double color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyColorLerpAllFramesNameD(IntPtr path, double color1, double color2);
        /// <summary>
        /// Multiply all the colors in the frame by the intensity value. The valid the 
        /// intensity range is from 0.0 to 255.0. RGB components are multiplied equally. 
        /// An intensity of 0.5 would half the color value. Black colors in the frame 
        /// will not be affected by this method.
        /// EXPORT_API void PluginMultiplyIntensity(int animationId, int frameId, float intensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensity(int animationId, int frameId, float intensity);
        /// <summary>
        /// Multiply all the colors for all frames by the intensity value. The valid 
        /// the intensity range is from 0.0 to 255.0. RGB components are multiplied 
        /// equally. An intensity of 0.5 would half the color value. Black colors in 
        /// the frame will not be affected by this method.
        /// EXPORT_API void PluginMultiplyIntensityAllFrames(int animationId, float intensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityAllFrames(int animationId, float intensity);
        /// <summary>
        /// Multiply all the colors for all frames by the intensity value. The valid 
        /// the intensity range is from 0.0 to 255.0. RGB components are multiplied 
        /// equally. An intensity of 0.5 would half the color value. Black colors in 
        /// the frame will not be affected by this method.
        /// EXPORT_API void PluginMultiplyIntensityAllFramesName(const char* path, float intensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityAllFramesName(IntPtr path, float intensity);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyIntensityAllFramesNameD(const char* path, double intensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyIntensityAllFramesNameD(IntPtr path, double intensity);
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginMultiplyIntensityAllFramesRGB(int animationId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityAllFramesRGB(int animationId, int red, int green, int blue);
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginMultiplyIntensityAllFramesRGBName(const char* path, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityAllFramesRGBName(IntPtr path, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyIntensityAllFramesRGBNameD(const char* path, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyIntensityAllFramesRGBNameD(IntPtr path, double red, double green, double blue);
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginMultiplyIntensityColor(int animationId, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityColor(int animationId, int frameId, int color);
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginMultiplyIntensityColorAllFrames(int animationId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityColorAllFrames(int animationId, int color);
        /// <summary>
        /// Multiply all frames by the RBG color intensity. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginMultiplyIntensityColorAllFramesName(const char* path, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityColorAllFramesName(IntPtr path, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyIntensityColorAllFramesNameD(const char* path, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyIntensityColorAllFramesNameD(IntPtr path, double color);
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginMultiplyIntensityColorName(const char* path, int frameId, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityColorName(IntPtr path, int frameId, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyIntensityColorNameD(const char* path, double frameId, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyIntensityColorNameD(IntPtr path, double frameId, double color);
        /// <summary>
        /// Multiply all the colors in the frame by the intensity value. The valid the 
        /// intensity range is from 0.0 to 255.0. RGB components are multiplied equally. 
        /// An intensity of 0.5 would half the color value. Black colors in the frame 
        /// will not be affected by this method.
        /// EXPORT_API void PluginMultiplyIntensityName(const char* path, int frameId, float intensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityName(IntPtr path, int frameId, float intensity);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyIntensityNameD(const char* path, double frameId, double intensity);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyIntensityNameD(IntPtr path, double frameId, double intensity);
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginMultiplyIntensityRGB(int animationId, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityRGB(int animationId, int frameId, int red, int green, int blue);
        /// <summary>
        /// Multiply the specific frame by the RBG color intensity. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginMultiplyIntensityRGBName(const char* path, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyIntensityRGBName(IntPtr path, int frameId, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyIntensityRGBNameD(const char* path, double frameId, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyIntensityRGBNameD(IntPtr path, double frameId, double red, double green, double blue);
        /// <summary>
        /// Multiply the specific frame by the color lerp result between color 1 and 
        /// 2 using the frame color value as the `t` value. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginMultiplyNonZeroTargetColorLerp(int animationId, int frameId, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyNonZeroTargetColorLerp(int animationId, int frameId, int color1, int color2);
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by id. 
        ///
        /// EXPORT_API void PluginMultiplyNonZeroTargetColorLerpAllFrames(int animationId, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyNonZeroTargetColorLerpAllFrames(int animationId, int color1, int color2);
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by name. 
        ///
        /// EXPORT_API void PluginMultiplyNonZeroTargetColorLerpAllFramesName(const char* path, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyNonZeroTargetColorLerpAllFramesName(IntPtr path, int color1, int color2);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyNonZeroTargetColorLerpAllFramesNameD(const char* path, double color1, double color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyNonZeroTargetColorLerpAllFramesNameD(IntPtr path, double color1, double color2);
        /// <summary>
        /// Multiply the specific frame by the color lerp result between RGB 1 and 2 
        /// using the frame color value as the `t` value. Animation is referenced by 
        /// id.
        /// EXPORT_API void PluginMultiplyNonZeroTargetColorLerpAllFramesRGB(int animationId, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyNonZeroTargetColorLerpAllFramesRGB(int animationId, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// <summary>
        /// Multiply the specific frame by the color lerp result between RGB 1 and 2 
        /// using the frame color value as the `t` value. Animation is referenced by 
        /// name.
        /// EXPORT_API void PluginMultiplyNonZeroTargetColorLerpAllFramesRGBName(const char* path, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyNonZeroTargetColorLerpAllFramesRGBName(IntPtr path, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyNonZeroTargetColorLerpAllFramesRGBNameD(const char* path, double red1, double green1, double blue1, double red2, double green2, double blue2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyNonZeroTargetColorLerpAllFramesRGBNameD(IntPtr path, double red1, double green1, double blue1, double red2, double green2, double blue2);
        /// <summary>
        /// Multiply the specific frame by the color lerp result between color 1 and 
        /// 2 using the frame color value as the `t` value. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginMultiplyTargetColorLerp(int animationId, int frameId, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyTargetColorLerp(int animationId, int frameId, int color1, int color2);
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by id. 
        ///
        /// EXPORT_API void PluginMultiplyTargetColorLerpAllFrames(int animationId, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyTargetColorLerpAllFrames(int animationId, int color1, int color2);
        /// <summary>
        /// Multiply all frames by the color lerp result between color 1 and 2 using 
        /// the frame color value as the `t` value. Animation is referenced by name. 
        ///
        /// EXPORT_API void PluginMultiplyTargetColorLerpAllFramesName(const char* path, int color1, int color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyTargetColorLerpAllFramesName(IntPtr path, int color1, int color2);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyTargetColorLerpAllFramesNameD(const char* path, double color1, double color2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyTargetColorLerpAllFramesNameD(IntPtr path, double color1, double color2);
        /// <summary>
        /// Multiply all frames by the color lerp result between RGB 1 and 2 using the 
        /// frame color value as the `t` value. Animation is referenced by id.
        /// EXPORT_API void PluginMultiplyTargetColorLerpAllFramesRGB(int animationId, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyTargetColorLerpAllFramesRGB(int animationId, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// <summary>
        /// Multiply all frames by the color lerp result between RGB 1 and 2 using the 
        /// frame color value as the `t` value. Animation is referenced by name.
        /// EXPORT_API void PluginMultiplyTargetColorLerpAllFramesRGBName(const char* path, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginMultiplyTargetColorLerpAllFramesRGBName(IntPtr path, int red1, int green1, int blue1, int red2, int green2, int blue2);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginMultiplyTargetColorLerpAllFramesRGBNameD(const char* path, double red1, double green1, double blue1, double red2, double green2, double blue2);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginMultiplyTargetColorLerpAllFramesRGBNameD(IntPtr path, double red1, double green1, double blue1, double red2, double green2, double blue2);
        /// <summary>
        /// Offset all colors in the frame using the RGB offset. Use the range of -255 
        /// to 255 for red, green, and blue parameters. Negative values remove color. 
        /// Positive values add color.
        /// EXPORT_API void PluginOffsetColors(int animationId, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetColors(int animationId, int frameId, int red, int green, int blue);
        /// <summary>
        /// Offset all colors for all frames using the RGB offset. Use the range of 
        /// -255 to 255 for red, green, and blue parameters. Negative values remove 
        /// color. Positive values add color.
        /// EXPORT_API void PluginOffsetColorsAllFrames(int animationId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetColorsAllFrames(int animationId, int red, int green, int blue);
        /// <summary>
        /// Offset all colors for all frames using the RGB offset. Use the range of 
        /// -255 to 255 for red, green, and blue parameters. Negative values remove 
        /// color. Positive values add color.
        /// EXPORT_API void PluginOffsetColorsAllFramesName(const char* path, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetColorsAllFramesName(IntPtr path, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOffsetColorsAllFramesNameD(const char* path, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOffsetColorsAllFramesNameD(IntPtr path, double red, double green, double blue);
        /// <summary>
        /// Offset all colors in the frame using the RGB offset. Use the range of -255 
        /// to 255 for red, green, and blue parameters. Negative values remove color. 
        /// Positive values add color.
        /// EXPORT_API void PluginOffsetColorsName(const char* path, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetColorsName(IntPtr path, int frameId, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOffsetColorsNameD(const char* path, double frameId, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOffsetColorsNameD(IntPtr path, double frameId, double red, double green, double blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors in the frame using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// EXPORT_API void PluginOffsetNonZeroColors(int animationId, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetNonZeroColors(int animationId, int frameId, int red, int green, int blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors for all frames using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// EXPORT_API void PluginOffsetNonZeroColorsAllFrames(int animationId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetNonZeroColorsAllFrames(int animationId, int red, int green, int blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors for all frames using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// EXPORT_API void PluginOffsetNonZeroColorsAllFramesName(const char* path, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetNonZeroColorsAllFramesName(IntPtr path, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOffsetNonZeroColorsAllFramesNameD(const char* path, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOffsetNonZeroColorsAllFramesNameD(IntPtr path, double red, double green, double blue);
        /// <summary>
        /// This method will only update colors in the animation that are not already 
        /// set to black. Offset a subset of colors in the frame using the RGB offset. 
        /// Use the range of -255 to 255 for red, green, and blue parameters. Negative 
        /// values remove color. Positive values add color.
        /// EXPORT_API void PluginOffsetNonZeroColorsName(const char* path, int frameId, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOffsetNonZeroColorsName(IntPtr path, int frameId, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOffsetNonZeroColorsNameD(const char* path, double frameId, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOffsetNonZeroColorsNameD(IntPtr path, double frameId, double red, double green, double blue);
        /// <summary>
        /// Opens a `Chroma` animation file so that it can be played. Returns an animation 
        /// id >= 0 upon success. Returns -1 if there was a failure. The animation 
        /// id is used in most of the API methods.
        /// EXPORT_API int PluginOpenAnimation(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginOpenAnimation(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOpenAnimationD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOpenAnimationD(IntPtr path);
        /// <summary>
        /// Opens a `Chroma` animation data from memory so that it can be played. `Data` 
        /// is a pointer to byte array of the loaded animation in memory. `Name` will 
        /// be assigned to the animation when loaded. Returns an animation id >= 0 
        /// upon success. Returns -1 if there was a failure. The animation id is used 
        /// in most of the API methods.
        /// EXPORT_API int PluginOpenAnimationFromMemory(const byte* data, const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginOpenAnimationFromMemory(byte[] data, IntPtr name);
        /// <summary>
        /// Opens a `Chroma` animation file with the `.chroma` extension. Returns zero 
        /// upon success. Returns -1 if there was a failure.
        /// EXPORT_API int PluginOpenEditorDialog(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginOpenEditorDialog(IntPtr path);
        /// <summary>
        /// Open the named animation in the editor dialog and play the animation at 
        /// start.
        /// EXPORT_API int PluginOpenEditorDialogAndPlay(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginOpenEditorDialogAndPlay(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOpenEditorDialogAndPlayD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOpenEditorDialogAndPlayD(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOpenEditorDialogD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOpenEditorDialogD(IntPtr path);
        /// <summary>
        /// Sets the `duration` for all grames in the `Chroma` animation to the `duration` 
        /// parameter. Returns the animation id upon success. Returns -1 upon failure. 
        ///
        /// EXPORT_API int PluginOverrideFrameDuration(int animationId, float duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginOverrideFrameDuration(int animationId, float duration);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginOverrideFrameDurationD(double animationId, double duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginOverrideFrameDurationD(double animationId, double duration);
        /// <summary>
        /// Override the duration of all frames with the `duration` value. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginOverrideFrameDurationName(const char* path, float duration);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginOverrideFrameDurationName(IntPtr path, float duration);
        /// <summary>
        /// Pause the current animation referenced by id.
        /// EXPORT_API void PluginPauseAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPauseAnimation(int animationId);
        /// <summary>
        /// Pause the current animation referenced by name.
        /// EXPORT_API void PluginPauseAnimationName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPauseAnimationName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginPauseAnimationNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginPauseAnimationNameD(IntPtr path);
        /// <summary>
        /// Plays the `Chroma` animation. This will load the animation, if not loaded 
        /// previously. Returns the animation id upon success. Returns -1 upon failure. 
        ///
        /// EXPORT_API int PluginPlayAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginPlayAnimation(int animationId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginPlayAnimationD(double animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginPlayAnimationD(double animationId);
        /// <summary>
        /// `PluginPlayAnimationFrame` automatically handles initializing the `ChromaSDK`. 
        /// The method will play the animation given the `animationId` with looping 
        /// `on` or `off` starting at the `frameId`.
        /// EXPORT_API void PluginPlayAnimationFrame(int animationId, int frameId, bool loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPlayAnimationFrame(int animationId, int frameId, bool loop);
        /// <summary>
        /// `PluginPlayAnimationFrameName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The animation 
        /// will play with looping `on` or `off` starting at the `frameId`.
        /// EXPORT_API void PluginPlayAnimationFrameName(const char* path, int frameId, bool loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPlayAnimationFrameName(IntPtr path, int frameId, bool loop);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginPlayAnimationFrameNameD(const char* path, double frameId, double loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginPlayAnimationFrameNameD(IntPtr path, double frameId, double loop);
        /// <summary>
        /// `PluginPlayAnimationLoop` automatically handles initializing the `ChromaSDK`. 
        /// The method will play the animation given the `animationId` with looping 
        /// `on` or `off`.
        /// EXPORT_API void PluginPlayAnimationLoop(int animationId, bool loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPlayAnimationLoop(int animationId, bool loop);
        /// <summary>
        /// `PluginPlayAnimationName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The animation 
        /// will play with looping `on` or `off`.
        /// EXPORT_API void PluginPlayAnimationName(const char* path, bool loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPlayAnimationName(IntPtr path, bool loop);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginPlayAnimationNameD(const char* path, double loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginPlayAnimationNameD(IntPtr path, double loop);
        /// <summary>
        /// `PluginPlayComposite` automatically handles initializing the `ChromaSDK`. 
        /// The named animation files for the `.chroma` set will be automatically opened. 
        /// The set of animations will play with looping `on` or `off`.
        /// EXPORT_API void PluginPlayComposite(const char* name, bool loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPlayComposite(IntPtr name, bool loop);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginPlayCompositeD(const char* name, double loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginPlayCompositeD(IntPtr name, double loop);
        /// <summary>
        /// Displays the `Chroma` animation frame on `Chroma` hardware given the `frameIndex`. 
        /// Returns the animation id upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginPreviewFrame(int animationId, int frameIndex);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginPreviewFrame(int animationId, int frameIndex);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginPreviewFrameD(double animationId, double frameIndex);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginPreviewFrameD(double animationId, double frameIndex);
        /// <summary>
        /// Displays the `Chroma` animation frame on `Chroma` hardware given the `frameIndex`. 
        /// Animaton is referenced by name.
        /// EXPORT_API void PluginPreviewFrameName(const char* path, int frameIndex);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginPreviewFrameName(IntPtr path, int frameIndex);
        /// <summary>
        /// Reduce the frames of the animation by removing every nth element. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginReduceFrames(int animationId, int n);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginReduceFrames(int animationId, int n);
        /// <summary>
        /// Reduce the frames of the animation by removing every nth element. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginReduceFramesName(const char* path, int n);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginReduceFramesName(IntPtr path, int n);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginReduceFramesNameD(const char* path, double n);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginReduceFramesNameD(IntPtr path, double n);
        /// <summary>
        /// Resets the `Chroma` animation to 1 blank frame. Returns the animation id 
        /// upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginResetAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginResetAnimation(int animationId);
        /// <summary>
        /// Resume the animation with loop `ON` or `OFF` referenced by id.
        /// EXPORT_API void PluginResumeAnimation(int animationId, bool loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginResumeAnimation(int animationId, bool loop);
        /// <summary>
        /// Resume the animation with loop `ON` or `OFF` referenced by name.
        /// EXPORT_API void PluginResumeAnimationName(const char* path, bool loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginResumeAnimationName(IntPtr path, bool loop);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginResumeAnimationNameD(const char* path, double loop);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginResumeAnimationNameD(IntPtr path, double loop);
        /// <summary>
        /// Reverse the animation frame order of the `Chroma` animation. Returns the 
        /// animation id upon success. Returns -1 upon failure. Animation is referenced 
        /// by id.
        /// EXPORT_API int PluginReverse(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginReverse(int animationId);
        /// <summary>
        /// Reverse the animation frame order of the `Chroma` animation. Animation is 
        /// referenced by id.
        /// EXPORT_API void PluginReverseAllFrames(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginReverseAllFrames(int animationId);
        /// <summary>
        /// Reverse the animation frame order of the `Chroma` animation. Animation is 
        /// referenced by name.
        /// EXPORT_API void PluginReverseAllFramesName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginReverseAllFramesName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginReverseAllFramesNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginReverseAllFramesNameD(IntPtr path);
        /// <summary>
        /// Save the animation referenced by id to the path specified.
        /// EXPORT_API int PluginSaveAnimation(int animationId, const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginSaveAnimation(int animationId, IntPtr path);
        /// <summary>
        /// Save the named animation to the target path specified.
        /// EXPORT_API int PluginSaveAnimationName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginSaveAnimationName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Set the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. The animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSet1DColor(int animationId, int frameId, int led, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSet1DColor(int animationId, int frameId, int led, int color);
        /// <summary>
        /// Set the animation color for a frame given the `1D` `led`. The `led` should 
        /// be greater than or equal to 0 and less than the `MaxLeds`. The animation 
        /// is referenced by name.
        /// EXPORT_API void PluginSet1DColorName(const char* path, int frameId, int led, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSet1DColorName(IntPtr path, int frameId, int led, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSet1DColorNameD(const char* path, double frameId, double led, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSet1DColorNameD(IntPtr path, double frameId, double led, double color);
        /// <summary>
        /// Set the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// The animation is referenced by id.
        /// EXPORT_API void PluginSet2DColor(int animationId, int frameId, int row, int column, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSet2DColor(int animationId, int frameId, int row, int column, int color);
        /// <summary>
        /// Set the animation color for a frame given the `2D` `row` and `column`. The 
        /// `row` should be greater than or equal to 0 and less than the `MaxRow`. 
        /// The `column` should be greater than or equal to 0 and less than the `MaxColumn`. 
        /// The animation is referenced by name.
        /// EXPORT_API void PluginSet2DColorName(const char* path, int frameId, int row, int column, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSet2DColorName(IntPtr path, int frameId, int row, int column, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSet2DColorNameD(const char* path, double frameId, double rowColumnIndex, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSet2DColorNameD(IntPtr path, double frameId, double rowColumnIndex, double color);
        /// <summary>
        /// When custom color is set, the custom key mode will be used. The animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSetChromaCustomColorAllFrames(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetChromaCustomColorAllFrames(int animationId);
        /// <summary>
        /// When custom color is set, the custom key mode will be used. The animation 
        /// is referenced by name.
        /// EXPORT_API void PluginSetChromaCustomColorAllFramesName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetChromaCustomColorAllFramesName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetChromaCustomColorAllFramesNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetChromaCustomColorAllFramesNameD(IntPtr path);
        /// <summary>
        /// Set the Chroma custom key color flag on all frames. `True` changes the layout 
        /// from grid to key. `True` changes the layout from key to grid. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSetChromaCustomFlag(int animationId, bool flag);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetChromaCustomFlag(int animationId, bool flag);
        /// <summary>
        /// Set the Chroma custom key color flag on all frames. `True` changes the layout 
        /// from grid to key. `True` changes the layout from key to grid. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginSetChromaCustomFlagName(const char* path, bool flag);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetChromaCustomFlagName(IntPtr path, bool flag);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetChromaCustomFlagNameD(const char* path, double flag);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetChromaCustomFlagNameD(IntPtr path, double flag);
        /// <summary>
        /// Set the current frame of the animation referenced by id.
        /// EXPORT_API void PluginSetCurrentFrame(int animationId, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetCurrentFrame(int animationId, int frameId);
        /// <summary>
        /// Set the current frame of the animation referenced by name.
        /// EXPORT_API void PluginSetCurrentFrameName(const char* path, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetCurrentFrameName(IntPtr path, int frameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetCurrentFrameNameD(const char* path, double frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetCurrentFrameNameD(IntPtr path, double frameId);
        /// <summary>
        /// Changes the `deviceType` and `device` of a `Chroma` animation. If the device 
        /// is changed, the `Chroma` animation will be reset with 1 blank frame. Returns 
        /// the animation id upon success. Returns -1 upon failure.
        /// EXPORT_API int PluginSetDevice(int animationId, int deviceType, int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginSetDevice(int animationId, int deviceType, int device);
        /// <summary>
        /// SetEffect will display the referenced effect id.
        /// EXPORT_API RZRESULT PluginSetEffect(const ChromaSDK::FChromaSDKGuid& effectId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginSetEffect(Guid effectId);
        /// <summary>
        /// When the idle animation is used, the named animation will play when no other 
        /// animations are playing. Reference the animation by id.
        /// EXPORT_API void PluginSetIdleAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetIdleAnimation(int animationId);
        /// <summary>
        /// When the idle animation is used, the named animation will play when no other 
        /// animations are playing. Reference the animation by name.
        /// EXPORT_API void PluginSetIdleAnimationName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetIdleAnimationName(IntPtr path);
        /// <summary>
        /// Set animation key to a static color for the given frame.
        /// EXPORT_API void PluginSetKeyColor(int animationId, int frameId, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColor(int animationId, int frameId, int rzkey, int color);
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginSetKeyColorAllFrames(int animationId, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColorAllFrames(int animationId, int rzkey, int color);
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginSetKeyColorAllFramesName(const char* path, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColorAllFramesName(IntPtr path, int rzkey, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyColorAllFramesNameD(const char* path, double rzkey, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyColorAllFramesNameD(IntPtr path, double rzkey, double color);
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by id.
        /// EXPORT_API void PluginSetKeyColorAllFramesRGB(int animationId, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColorAllFramesRGB(int animationId, int rzkey, int red, int green, int blue);
        /// <summary>
        /// Set the key to the specified key color for all frames. Animation is referenced 
        /// by name.
        /// EXPORT_API void PluginSetKeyColorAllFramesRGBName(const char* path, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColorAllFramesRGBName(IntPtr path, int rzkey, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyColorAllFramesRGBNameD(const char* path, double rzkey, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyColorAllFramesRGBNameD(IntPtr path, double rzkey, double red, double green, double blue);
        /// <summary>
        /// Set animation key to a static color for the given frame.
        /// EXPORT_API void PluginSetKeyColorName(const char* path, int frameId, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColorName(IntPtr path, int frameId, int rzkey, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyColorNameD(const char* path, double frameId, double rzkey, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyColorNameD(IntPtr path, double frameId, double rzkey, double color);
        /// <summary>
        /// Set the key to the specified key color for the specified frame. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSetKeyColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue);
        /// <summary>
        /// Set the key to the specified key color for the specified frame. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginSetKeyColorRGBName(const char* path, int frameId, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyColorRGBName(IntPtr path, int frameId, int rzkey, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyColorRGBNameD(const char* path, double frameId, double rzkey, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyColorRGBNameD(IntPtr path, double frameId, double rzkey, double red, double green, double blue);
        /// <summary>
        /// Set animation key to a static color for the given frame if the existing 
        /// color is not already black.
        /// EXPORT_API void PluginSetKeyNonZeroColor(int animationId, int frameId, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyNonZeroColor(int animationId, int frameId, int rzkey, int color);
        /// <summary>
        /// Set animation key to a static color for the given frame if the existing 
        /// color is not already black.
        /// EXPORT_API void PluginSetKeyNonZeroColorName(const char* path, int frameId, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyNonZeroColorName(IntPtr path, int frameId, int rzkey, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyNonZeroColorNameD(const char* path, double frameId, double rzkey, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyNonZeroColorNameD(IntPtr path, double frameId, double rzkey, double color);
        /// <summary>
        /// Set the key to the specified key color for the specified frame where color 
        /// is not black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeyNonZeroColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyNonZeroColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue);
        /// <summary>
        /// Set the key to the specified key color for the specified frame where color 
        /// is not black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeyNonZeroColorRGBName(const char* path, int frameId, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyNonZeroColorRGBName(IntPtr path, int frameId, int rzkey, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyNonZeroColorRGBNameD(const char* path, double frameId, double rzkey, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyNonZeroColorRGBNameD(IntPtr path, double frameId, double rzkey, double red, double green, double blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSetKeysColor(int animationId, int frameId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColor(int animationId, int frameId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSetKeysColorAllFrames(int animationId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColorAllFrames(int animationId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginSetKeysColorAllFramesName(const char* path, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColorAllFramesName(IntPtr path, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSetKeysColorAllFramesRGB(int animationId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColorAllFramesRGB(int animationId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginSetKeysColorAllFramesRGBName(const char* path, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColorAllFramesRGBName(IntPtr path, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame.
        /// EXPORT_API void PluginSetKeysColorName(const char* path, int frameId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColorName(IntPtr path, int frameId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame. Animation 
        /// is referenced by id.
        /// EXPORT_API void PluginSetKeysColorRGB(int animationId, int frameId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColorRGB(int animationId, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame. Animation 
        /// is referenced by name.
        /// EXPORT_API void PluginSetKeysColorRGBName(const char* path, int frameId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysColorRGBName(IntPtr path, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame if 
        /// the existing color is not already black.
        /// EXPORT_API void PluginSetKeysNonZeroColor(int animationId, int frameId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysNonZeroColor(int animationId, int frameId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is not black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeysNonZeroColorAllFrames(int animationId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysNonZeroColorAllFrames(int animationId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames if the existing 
        /// color is not already black. Reference animation by name.
        /// EXPORT_API void PluginSetKeysNonZeroColorAllFramesName(const char* path, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysNonZeroColorAllFramesName(IntPtr path, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame if 
        /// the existing color is not already black. Reference animation by name.
        /// EXPORT_API void PluginSetKeysNonZeroColorName(const char* path, int frameId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysNonZeroColorName(IntPtr path, int frameId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is not black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeysNonZeroColorRGB(int animationId, int frameId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysNonZeroColorRGB(int animationId, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is not black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeysNonZeroColorRGBName(const char* path, int frameId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysNonZeroColorRGBName(IntPtr path, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeysZeroColor(int animationId, int frameId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColor(int animationId, int frameId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeysZeroColorAllFrames(int animationId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColorAllFrames(int animationId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeysZeroColorAllFramesName(const char* path, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColorAllFramesName(IntPtr path, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeysZeroColorAllFramesRGB(int animationId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColorAllFramesRGB(int animationId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for all frames where the 
        /// color is black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeysZeroColorAllFramesRGBName(const char* path, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColorAllFramesRGBName(IntPtr path, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeysZeroColorName(const char* path, int frameId, const int* rzkeys, int keyCount, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColorName(IntPtr path, int frameId, int[] rzkeys, int keyCount, int color);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeysZeroColorRGB(int animationId, int frameId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColorRGB(int animationId, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set an array of animation keys to a static color for the given frame where 
        /// the color is black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeysZeroColorRGBName(const char* path, int frameId, const int* rzkeys, int keyCount, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeysZeroColorRGBName(IntPtr path, int frameId, int[] rzkeys, int keyCount, int red, int green, int blue);
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeyZeroColor(int animationId, int frameId, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyZeroColor(int animationId, int frameId, int rzkey, int color);
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeyZeroColorName(const char* path, int frameId, int rzkey, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyZeroColorName(IntPtr path, int frameId, int rzkey, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyZeroColorNameD(const char* path, double frameId, double rzkey, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyZeroColorNameD(IntPtr path, double frameId, double rzkey, double color);
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by id.
        /// EXPORT_API void PluginSetKeyZeroColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyZeroColorRGB(int animationId, int frameId, int rzkey, int red, int green, int blue);
        /// <summary>
        /// Set animation key to a static color for the given frame where the color 
        /// is black. Animation is referenced by name.
        /// EXPORT_API void PluginSetKeyZeroColorRGBName(const char* path, int frameId, int rzkey, int red, int green, int blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetKeyZeroColorRGBName(IntPtr path, int frameId, int rzkey, int red, int green, int blue);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSetKeyZeroColorRGBNameD(const char* path, double frameId, double rzkey, double red, double green, double blue);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSetKeyZeroColorRGBNameD(IntPtr path, double frameId, double rzkey, double red, double green, double blue);
        /// <summary>
        /// Invokes the setup for a debug logging callback so that `stdout` is redirected 
        /// to the callback. This is used by `Unity` so that debug messages can appear 
        /// in the console window.
        /// EXPORT_API void PluginSetLogDelegate(DebugLogPtr fp);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSetLogDelegate(IntPtr fp);
        /// <summary>
        /// `PluginStaticColor` sets the target device to the static color.
        /// EXPORT_API void PluginStaticColor(int deviceType, int device, int color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginStaticColor(int deviceType, int device, int color);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginStaticColorD(double deviceType, double device, double color);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginStaticColorD(double deviceType, double device, double color);
        /// <summary>
        /// `PluginStopAll` will automatically stop all animations that are playing. 
        ///
        /// EXPORT_API void PluginStopAll();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginStopAll();
        /// <summary>
        /// Stops animation playback if in progress. Returns the animation id upon success. 
        /// Returns -1 upon failure.
        /// EXPORT_API int PluginStopAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginStopAnimation(int animationId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginStopAnimationD(double animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginStopAnimationD(double animationId);
        /// <summary>
        /// `PluginStopAnimationName` automatically handles initializing the `ChromaSDK`. 
        /// The named `.chroma` animation file will be automatically opened. The animation 
        /// will stop if playing.
        /// EXPORT_API void PluginStopAnimationName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginStopAnimationName(IntPtr path);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginStopAnimationNameD(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginStopAnimationNameD(IntPtr path);
        /// <summary>
        /// `PluginStopAnimationType` automatically handles initializing the `ChromaSDK`. 
        /// If any animation is playing for the `deviceType` and `device` combination, 
        /// it will be stopped.
        /// EXPORT_API void PluginStopAnimationType(int deviceType, int device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginStopAnimationType(int deviceType, int device);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginStopAnimationTypeD(double deviceType, double device);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginStopAnimationTypeD(double deviceType, double device);
        /// <summary>
        /// `PluginStopComposite` automatically handles initializing the `ChromaSDK`. 
        /// The named animation files for the `.chroma` set will be automatically opened. 
        /// The set of animations will be stopped if playing.
        /// EXPORT_API void PluginStopComposite(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginStopComposite(IntPtr name);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginStopCompositeD(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginStopCompositeD(IntPtr name);
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black. Source and target are referenced by id.
        /// EXPORT_API void PluginSubtractNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black. Source and target are referenced by name.
        /// EXPORT_API void PluginSubtractNonZeroAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSubtractNonZeroAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSubtractNonZeroAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black starting at offset for the length of the source. 
        /// Source and target are referenced by id.
        /// EXPORT_API void PluginSubtractNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// <summary>
        /// Subtract the source color from the target color for all frames where the 
        /// target color is not black starting at offset for the length of the source. 
        /// Source and target are referenced by name.
        /// EXPORT_API void PluginSubtractNonZeroAllKeysAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroAllKeysAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSubtractNonZeroAllKeysAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSubtractNonZeroAllKeysAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double offset);
        /// <summary>
        /// Subtract the source color from the target where color is not black for the 
        /// source frame and target offset frame, reference source and target by id. 
        ///
        /// EXPORT_API void PluginSubtractNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// <summary>
        /// Subtract the source color from the target where color is not black for the 
        /// source frame and target offset frame, reference source and target by name. 
        ///
        /// EXPORT_API void PluginSubtractNonZeroAllKeysOffsetName(const char* sourceAnimation, const char* targetAnimation, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroAllKeysOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSubtractNonZeroAllKeysOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSubtractNonZeroAllKeysOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double offset);
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames. Reference source and target by id.
        /// EXPORT_API void PluginSubtractNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroTargetAllKeysAllFrames(int sourceAnimationId, int targetAnimationId);
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames. Reference source and target by name.
        /// EXPORT_API void PluginSubtractNonZeroTargetAllKeysAllFramesName(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroTargetAllKeysAllFramesName(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSubtractNonZeroTargetAllKeysAllFramesNameD(const char* sourceAnimation, const char* targetAnimation);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSubtractNonZeroTargetAllKeysAllFramesNameD(IntPtr sourceAnimation, IntPtr targetAnimation);
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames starting at the target offset for the length of 
        /// the source. Reference source and target by id.
        /// EXPORT_API void PluginSubtractNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroTargetAllKeysAllFramesOffset(int sourceAnimationId, int targetAnimationId, int offset);
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black for all frames starting at the target offset for the length of 
        /// the source. Reference source and target by name.
        /// EXPORT_API void PluginSubtractNonZeroTargetAllKeysAllFramesOffsetName(const char* sourceAnimation, const char* targetAnimation, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroTargetAllKeysAllFramesOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSubtractNonZeroTargetAllKeysAllFramesOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSubtractNonZeroTargetAllKeysAllFramesOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double offset);
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black from the source frame to the target offset frame. Reference source 
        /// and target by id.
        /// EXPORT_API void PluginSubtractNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroTargetAllKeysOffset(int sourceAnimationId, int targetAnimationId, int frameId, int offset);
        /// <summary>
        /// Subtract the source color from the target color where the target color is 
        /// not black from the source frame to the target offset frame. Reference source 
        /// and target by name.
        /// EXPORT_API void PluginSubtractNonZeroTargetAllKeysOffsetName(const char* sourceAnimation, const char* targetAnimation, int frameId, int offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginSubtractNonZeroTargetAllKeysOffsetName(IntPtr sourceAnimation, IntPtr targetAnimation, int frameId, int offset);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginSubtractNonZeroTargetAllKeysOffsetNameD(const char* sourceAnimation, const char* targetAnimation, double frameId, double offset);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginSubtractNonZeroTargetAllKeysOffsetNameD(IntPtr sourceAnimation, IntPtr targetAnimation, double frameId, double offset);
        /// <summary>
        /// Trim the end of the animation. The length of the animation will be the lastFrameId 
        /// + 1. Reference the animation by id.
        /// EXPORT_API void PluginTrimEndFrames(int animationId, int lastFrameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginTrimEndFrames(int animationId, int lastFrameId);
        /// <summary>
        /// Trim the end of the animation. The length of the animation will be the lastFrameId 
        /// + 1. Reference the animation by name.
        /// EXPORT_API void PluginTrimEndFramesName(const char* path, int lastFrameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginTrimEndFramesName(IntPtr path, int lastFrameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginTrimEndFramesNameD(const char* path, double lastFrameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginTrimEndFramesNameD(IntPtr path, double lastFrameId);
        /// <summary>
        /// Remove the frame from the animation. Reference animation by id.
        /// EXPORT_API void PluginTrimFrame(int animationId, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginTrimFrame(int animationId, int frameId);
        /// <summary>
        /// Remove the frame from the animation. Reference animation by name.
        /// EXPORT_API void PluginTrimFrameName(const char* path, int frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginTrimFrameName(IntPtr path, int frameId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginTrimFrameNameD(const char* path, double frameId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginTrimFrameNameD(IntPtr path, double frameId);
        /// <summary>
        /// Trim the start of the animation starting at frame 0 for the number of frames. 
        /// Reference the animation by id.
        /// EXPORT_API void PluginTrimStartFrames(int animationId, int numberOfFrames);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginTrimStartFrames(int animationId, int numberOfFrames);
        /// <summary>
        /// Trim the start of the animation starting at frame 0 for the number of frames. 
        /// Reference the animation by name.
        /// EXPORT_API void PluginTrimStartFramesName(const char* path, int numberOfFrames);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginTrimStartFramesName(IntPtr path, int numberOfFrames);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginTrimStartFramesNameD(const char* path, double numberOfFrames);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginTrimStartFramesNameD(IntPtr path, double numberOfFrames);
        /// <summary>
        /// Uninitializes the `ChromaSDK`. Returns 0 upon success. Returns -1 upon failure. 
        ///
        /// EXPORT_API RZRESULT PluginUninit();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginUninit();
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginUninitD();
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginUninitD();
        /// <summary>
        /// Unloads `Chroma` effects to free up resources. Returns the animation id 
        /// upon success. Returns -1 upon failure. Reference the animation by id.
        /// EXPORT_API int PluginUnloadAnimation(int animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginUnloadAnimation(int animationId);
        /// <summary>
        /// D suffix for limited data types.
        /// EXPORT_API double PluginUnloadAnimationD(double animationId);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern double PluginUnloadAnimationD(double animationId);
        /// <summary>
        /// Unload the animation effects. Reference the animation by name.
        /// EXPORT_API void PluginUnloadAnimationName(const char* path);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginUnloadAnimationName(IntPtr path);
        /// <summary>
        /// Unload the the composite set of animation effects. Reference the animation 
        /// by name.
        /// EXPORT_API void PluginUnloadComposite(const char* name);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginUnloadComposite(IntPtr name);
        /// <summary>
        /// Updates the `frameIndex` of the `Chroma` animation and sets the `duration` 
        /// (in seconds). The `color` is expected to be an array of the dimensions 
        /// for the `deviceType/device`. The `length` parameter is the size of the 
        /// `color` array. For `EChromaSDKDevice1DEnum` the array size should be `MAX 
        /// LEDS`. For `EChromaSDKDevice2DEnum` the array size should be `MAX ROW` 
        /// * `MAX COLUMN`. Returns the animation id upon success. Returns -1 upon 
        /// failure.
        /// EXPORT_API int PluginUpdateFrame(int animationId, int frameIndex, float duration, int* colors, int length);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int PluginUpdateFrame(int animationId, int frameIndex, float duration, int[] colors, int length);
        /// <summary>
        /// When the idle animation flag is true, when no other animations are playing, 
        /// the idle animation will be used. The idle animation will not be affected 
        /// by the API calls to PluginIsPlaying, PluginStopAnimationType, PluginGetPlayingAnimationId, 
        /// and PluginGetPlayingAnimationCount. Then the idle animation flag is false, 
        /// the idle animation is disabled. `Device` uses `EChromaSDKDeviceEnum` enums. 
        ///
        /// EXPORT_API void PluginUseIdleAnimation(int device, bool flag);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginUseIdleAnimation(int device, bool flag);
        /// <summary>
        /// Set idle animation flag for all devices.
        /// EXPORT_API void PluginUseIdleAnimations(bool flag);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginUseIdleAnimations(bool flag);
        /// <summary>
        /// Set preloading animation flag, which is set to true by default. Reference 
        /// animation by id.
        /// EXPORT_API void PluginUsePreloading(int animationId, bool flag);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginUsePreloading(int animationId, bool flag);
        /// <summary>
        /// Set preloading animation flag, which is set to true by default. Reference 
        /// animation by name.
        /// EXPORT_API void PluginUsePreloadingName(const char* path, bool flag);
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void PluginUsePreloadingName(IntPtr path, bool flag);
        #endregion
    }
}