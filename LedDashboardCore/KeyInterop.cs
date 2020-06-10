// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows.Forms;

namespace LedDashboardCore
{
    /// <summary>
    ///     Provides static methods to convert between Win32 VirtualKeyss
    ///     and our Keys enum.
    /// </summary>
    public static class KeyInterop
    {
        /// <summary>
        ///     Convert a Win32 VirtualKeys into our Keys enum.
        /// </summary>
        public static Keys KeyFromVirtualKey(int virtualKey)
        {
            Keys Keys = Keys.None;

            switch (virtualKey)
            {
                case NativeMethods.VK_CANCEL:
                    Keys = Keys.Cancel;
                    break;

                case NativeMethods.VK_BACK:
                    Keys = Keys.Back;
                    break;

                case NativeMethods.VK_TAB:
                    Keys = Keys.Tab;
                    break;

                case NativeMethods.VK_CLEAR:
                    Keys = Keys.Clear;
                    break;

                case NativeMethods.VK_RETURN:
                    Keys = Keys.Return;
                    break;

                case NativeMethods.VK_PAUSE:
                    Keys = Keys.Pause;
                    break;

                case NativeMethods.VK_CAPITAL:
                    Keys = Keys.Capital;
                    break;

                case NativeMethods.VK_KANA:
                    Keys = Keys.KanaMode;
                    break;

                case NativeMethods.VK_JUNJA:
                    Keys = Keys.JunjaMode;
                    break;

                case NativeMethods.VK_FINAL:
                    Keys = Keys.FinalMode;
                    break;

                case NativeMethods.VK_KANJI:
                    Keys = Keys.KanjiMode;
                    break;

                case NativeMethods.VK_ESCAPE:
                    Keys = Keys.Escape;
                    break;

                case NativeMethods.VK_CONVERT:
                    Keys = Keys.IMEConvert;
                    break;

                case NativeMethods.VK_NONCONVERT:
                    Keys = Keys.IMENonconvert;
                    break;

                case NativeMethods.VK_ACCEPT:
                    Keys = Keys.IMEAccept;
                    break;

                case NativeMethods.VK_MODECHANGE:
                    Keys = Keys.IMEModeChange;
                    break;

                case NativeMethods.VK_SPACE:
                    Keys = Keys.Space;
                    break;

                case NativeMethods.VK_PRIOR:
                    Keys = Keys.Prior;
                    break;

                case NativeMethods.VK_NEXT:
                    Keys = Keys.Next;
                    break;

                case NativeMethods.VK_END:
                    Keys = Keys.End;
                    break;

                case NativeMethods.VK_HOME:
                    Keys = Keys.Home;
                    break;

                case NativeMethods.VK_LEFT:
                    Keys = Keys.Left;
                    break;

                case NativeMethods.VK_UP:
                    Keys = Keys.Up;
                    break;

                case NativeMethods.VK_RIGHT:
                    Keys = Keys.Right;
                    break;

                case NativeMethods.VK_DOWN:
                    Keys = Keys.Down;
                    break;

                case NativeMethods.VK_SELECT:
                    Keys = Keys.Select;
                    break;

                case NativeMethods.VK_PRINT:
                    Keys = Keys.Print;
                    break;

                case NativeMethods.VK_EXECUTE:
                    Keys = Keys.Execute;
                    break;

                case NativeMethods.VK_SNAPSHOT:
                    Keys = Keys.Snapshot;
                    break;

                case NativeMethods.VK_INSERT:
                    Keys = Keys.Insert;
                    break;

                case NativeMethods.VK_DELETE:
                    Keys = Keys.Delete;
                    break;

                case NativeMethods.VK_HELP:
                    Keys = Keys.Help;
                    break;

                case NativeMethods.VK_0:
                    Keys = Keys.D0;
                    break;

                case NativeMethods.VK_1:
                    Keys = Keys.D1;
                    break;

                case NativeMethods.VK_2:
                    Keys = Keys.D2;
                    break;

                case NativeMethods.VK_3:
                    Keys = Keys.D3;
                    break;

                case NativeMethods.VK_4:
                    Keys = Keys.D4;
                    break;

                case NativeMethods.VK_5:
                    Keys = Keys.D5;
                    break;

                case NativeMethods.VK_6:
                    Keys = Keys.D6;
                    break;

                case NativeMethods.VK_7:
                    Keys = Keys.D7;
                    break;

                case NativeMethods.VK_8:
                    Keys = Keys.D8;
                    break;

                case NativeMethods.VK_9:
                    Keys = Keys.D9;
                    break;

                case NativeMethods.VK_A:
                    Keys = Keys.A;
                    break;

                case NativeMethods.VK_B:
                    Keys = Keys.B;
                    break;

                case NativeMethods.VK_C:
                    Keys = Keys.C;
                    break;

                case NativeMethods.VK_D:
                    Keys = Keys.D;
                    break;

                case NativeMethods.VK_E:
                    Keys = Keys.E;
                    break;

                case NativeMethods.VK_F:
                    Keys = Keys.F;
                    break;

                case NativeMethods.VK_G:
                    Keys = Keys.G;
                    break;

                case NativeMethods.VK_H:
                    Keys = Keys.H;
                    break;

                case NativeMethods.VK_I:
                    Keys = Keys.I;
                    break;

                case NativeMethods.VK_J:
                    Keys = Keys.J;
                    break;

                case NativeMethods.VK_K:
                    Keys = Keys.K;
                    break;

                case NativeMethods.VK_L:
                    Keys = Keys.L;
                    break;

                case NativeMethods.VK_M:
                    Keys = Keys.M;
                    break;

                case NativeMethods.VK_N:
                    Keys = Keys.N;
                    break;

                case NativeMethods.VK_O:
                    Keys = Keys.O;
                    break;

                case NativeMethods.VK_P:
                    Keys = Keys.P;
                    break;

                case NativeMethods.VK_Q:
                    Keys = Keys.Q;
                    break;

                case NativeMethods.VK_R:
                    Keys = Keys.R;
                    break;

                case NativeMethods.VK_S:
                    Keys = Keys.S;
                    break;

                case NativeMethods.VK_T:
                    Keys = Keys.T;
                    break;

                case NativeMethods.VK_U:
                    Keys = Keys.U;
                    break;

                case NativeMethods.VK_V:
                    Keys = Keys.V;
                    break;

                case NativeMethods.VK_W:
                    Keys = Keys.W;
                    break;

                case NativeMethods.VK_X:
                    Keys = Keys.X;
                    break;

                case NativeMethods.VK_Y:
                    Keys = Keys.Y;
                    break;

                case NativeMethods.VK_Z:
                    Keys = Keys.Z;
                    break;

                case NativeMethods.VK_LWIN:
                    Keys = Keys.LWin;
                    break;

                case NativeMethods.VK_RWIN:
                    Keys = Keys.RWin;
                    break;

                case NativeMethods.VK_APPS:
                    Keys = Keys.Apps;
                    break;

                case NativeMethods.VK_SLEEP:
                    Keys = Keys.Sleep;
                    break;

                case NativeMethods.VK_NUMPAD0:
                    Keys = Keys.NumPad0;
                    break;

                case NativeMethods.VK_NUMPAD1:
                    Keys = Keys.NumPad1;
                    break;

                case NativeMethods.VK_NUMPAD2:
                    Keys = Keys.NumPad2;
                    break;

                case NativeMethods.VK_NUMPAD3:
                    Keys = Keys.NumPad3;
                    break;

                case NativeMethods.VK_NUMPAD4:
                    Keys = Keys.NumPad4;
                    break;

                case NativeMethods.VK_NUMPAD5:
                    Keys = Keys.NumPad5;
                    break;

                case NativeMethods.VK_NUMPAD6:
                    Keys = Keys.NumPad6;
                    break;

                case NativeMethods.VK_NUMPAD7:
                    Keys = Keys.NumPad7;
                    break;

                case NativeMethods.VK_NUMPAD8:
                    Keys = Keys.NumPad8;
                    break;

                case NativeMethods.VK_NUMPAD9:
                    Keys = Keys.NumPad9;
                    break;

                case NativeMethods.VK_MULTIPLY:
                    Keys = Keys.Multiply;
                    break;

                case NativeMethods.VK_ADD:
                    Keys = Keys.Add;
                    break;

                case NativeMethods.VK_SEPARATOR:
                    Keys = Keys.Separator;
                    break;

                case NativeMethods.VK_SUBTRACT:
                    Keys = Keys.Subtract;
                    break;

                case NativeMethods.VK_DECIMAL:
                    Keys = Keys.Decimal;
                    break;

                case NativeMethods.VK_DIVIDE:
                    Keys = Keys.Divide;
                    break;

                case NativeMethods.VK_F1:
                    Keys = Keys.F1;
                    break;

                case NativeMethods.VK_F2:
                    Keys = Keys.F2;
                    break;

                case NativeMethods.VK_F3:
                    Keys = Keys.F3;
                    break;

                case NativeMethods.VK_F4:
                    Keys = Keys.F4;
                    break;

                case NativeMethods.VK_F5:
                    Keys = Keys.F5;
                    break;

                case NativeMethods.VK_F6:
                    Keys = Keys.F6;
                    break;

                case NativeMethods.VK_F7:
                    Keys = Keys.F7;
                    break;

                case NativeMethods.VK_F8:
                    Keys = Keys.F8;
                    break;

                case NativeMethods.VK_F9:
                    Keys = Keys.F9;
                    break;

                case NativeMethods.VK_F10:
                    Keys = Keys.F10;
                    break;

                case NativeMethods.VK_F11:
                    Keys = Keys.F11;
                    break;

                case NativeMethods.VK_F12:
                    Keys = Keys.F12;
                    break;

                case NativeMethods.VK_F13:
                    Keys = Keys.F13;
                    break;

                case NativeMethods.VK_F14:
                    Keys = Keys.F14;
                    break;

                case NativeMethods.VK_F15:
                    Keys = Keys.F15;
                    break;

                case NativeMethods.VK_F16:
                    Keys = Keys.F16;
                    break;

                case NativeMethods.VK_F17:
                    Keys = Keys.F17;
                    break;

                case NativeMethods.VK_F18:
                    Keys = Keys.F18;
                    break;

                case NativeMethods.VK_F19:
                    Keys = Keys.F19;
                    break;

                case NativeMethods.VK_F20:
                    Keys = Keys.F20;
                    break;

                case NativeMethods.VK_F21:
                    Keys = Keys.F21;
                    break;

                case NativeMethods.VK_F22:
                    Keys = Keys.F22;
                    break;

                case NativeMethods.VK_F23:
                    Keys = Keys.F23;
                    break;

                case NativeMethods.VK_F24:
                    Keys = Keys.F24;
                    break;

                case NativeMethods.VK_NUMLOCK:
                    Keys = Keys.NumLock;
                    break;

                case NativeMethods.VK_SCROLL:
                    Keys = Keys.Scroll;
                    break;

                case NativeMethods.VK_LSHIFT:
                    Keys = Keys.LShiftKey;
                    break;

                case NativeMethods.VK_RSHIFT:
                    Keys = Keys.RShiftKey;
                    break;

                case NativeMethods.VK_LCONTROL:
                    Keys = Keys.LControlKey;
                    break;

                case NativeMethods.VK_RCONTROL:
                    Keys = Keys.RControlKey;
                    break;

                case NativeMethods.VK_LMENU:
                    Keys = Keys.Alt;
                    break;

                case NativeMethods.VK_RMENU:
                    Keys = Keys.Alt;
                    break;

                case NativeMethods.VK_BROWSER_BACK:
                    Keys = Keys.BrowserBack;
                    break;

                case NativeMethods.VK_BROWSER_FORWARD:
                    Keys = Keys.BrowserForward;
                    break;

                case NativeMethods.VK_BROWSER_REFRESH:
                    Keys = Keys.BrowserRefresh;
                    break;

                case NativeMethods.VK_BROWSER_STOP:
                    Keys = Keys.BrowserStop;
                    break;

                case NativeMethods.VK_BROWSER_SEARCH:
                    Keys = Keys.BrowserSearch;
                    break;

                case NativeMethods.VK_BROWSER_FAVORITES:
                    Keys = Keys.BrowserFavorites;
                    break;

                case NativeMethods.VK_BROWSER_HOME:
                    Keys = Keys.BrowserHome;
                    break;

                case NativeMethods.VK_VOLUME_MUTE:
                    Keys = Keys.VolumeMute;
                    break;

                case NativeMethods.VK_VOLUME_DOWN:
                    Keys = Keys.VolumeDown;
                    break;

                case NativeMethods.VK_VOLUME_UP:
                    Keys = Keys.VolumeUp;
                    break;

                case NativeMethods.VK_MEDIA_NEXT_TRACK:
                    Keys = Keys.MediaNextTrack;
                    break;

                case NativeMethods.VK_MEDIA_PREV_TRACK:
                    Keys = Keys.MediaPreviousTrack;
                    break;

                case NativeMethods.VK_MEDIA_STOP:
                    Keys = Keys.MediaStop;
                    break;

                case NativeMethods.VK_MEDIA_PLAY_PAUSE:
                    Keys = Keys.MediaPlayPause;
                    break;

                case NativeMethods.VK_LAUNCH_MAIL:
                    Keys = Keys.LaunchMail;
                    break;

                case NativeMethods.VK_LAUNCH_MEDIA_SELECT:
                    Keys = Keys.SelectMedia;
                    break;

                case NativeMethods.VK_LAUNCH_APP1:
                    Keys = Keys.LaunchApplication1;
                    break;

                case NativeMethods.VK_LAUNCH_APP2:
                    Keys = Keys.LaunchApplication2;
                    break;

                case NativeMethods.VK_OEM_1:
                    Keys = Keys.OemSemicolon;
                    break;

                case NativeMethods.VK_OEM_PLUS:
                    Keys = Keys.Oemplus;
                    break;

                case NativeMethods.VK_OEM_COMMA:
                    Keys = Keys.Oemcomma;
                    break;

                case NativeMethods.VK_OEM_MINUS:
                    Keys = Keys.OemMinus;
                    break;

                case NativeMethods.VK_OEM_PERIOD:
                    Keys = Keys.OemPeriod;
                    break;

                case NativeMethods.VK_OEM_2:
                    Keys = Keys.OemQuestion;
                    break;

                case NativeMethods.VK_OEM_3:
                    Keys = Keys.Oem3;
                    break;

                case NativeMethods.VK_OEM_4:
                    Keys = Keys.OemOpenBrackets;
                    break;

                case NativeMethods.VK_OEM_5:
                    Keys = Keys.OemPipe;
                    break;

                case NativeMethods.VK_OEM_6:
                    Keys = Keys.OemCloseBrackets;
                    break;

                case NativeMethods.VK_OEM_7:
                    Keys = Keys.OemQuotes;
                    break;

                case NativeMethods.VK_OEM_8:
                    Keys = Keys.Oem8;
                    break;

                case NativeMethods.VK_OEM_102:
                    Keys = Keys.OemBackslash;
                    break;

                case NativeMethods.VK_ATTN: // VK_DBE_NOROMAN
                    Keys = Keys.Attn;         // DbeNoRoman
                    break;

                case NativeMethods.VK_EREOF: // VK_DBE_FLUSHSTRING
                    Keys = Keys.EraseEof;      // DbeFlushString
                    break;

                case NativeMethods.VK_PLAY: // VK_DBE_CODEINPUT
                    Keys = Keys.Play;         // DbeCodeInput
                    break;

                case NativeMethods.VK_ZOOM: // VK_DBE_NOCODEINPUT
                    Keys = Keys.Zoom;         // DbeNoCodeInput
                    break;

                case NativeMethods.VK_NONAME: // VK_DBE_DETERMINESTRING
                    Keys = Keys.NoName;         // DbeDetermineString
                    break;

                case NativeMethods.VK_PA1: // VK_DBE_ENTERDLGCONVERSIONMODE
                    Keys = Keys.Pa1;         // DbeEnterDlgConversionMode
                    break;

                case NativeMethods.VK_OEM_CLEAR:
                    Keys = Keys.OemClear;
                    break;

                default:
                    Keys = Keys.None;
                    break;
            }

            return Keys;
        }

        /// <summary>
        ///     Convert our Keys enum into a Win32 VirtualKeys.
        /// </summary>
        public static int VirtualKeysFromKeys(Keys Keys)
        {
            int virtualKeys = 0;

            switch (Keys)
            {
                case Keys.Cancel:
                    virtualKeys = NativeMethods.VK_CANCEL;
                    break;

                case Keys.Back:
                    virtualKeys = NativeMethods.VK_BACK;
                    break;

                case Keys.Tab:
                    virtualKeys = NativeMethods.VK_TAB;
                    break;

                case Keys.Clear:
                    virtualKeys = NativeMethods.VK_CLEAR;
                    break;

                case Keys.Return:
                    virtualKeys = NativeMethods.VK_RETURN;
                    break;

                case Keys.Pause:
                    virtualKeys = NativeMethods.VK_PAUSE;
                    break;

                case Keys.Capital:
                    virtualKeys = NativeMethods.VK_CAPITAL;
                    break;

                case Keys.KanaMode:
                    virtualKeys = NativeMethods.VK_KANA;
                    break;

                case Keys.JunjaMode:
                    virtualKeys = NativeMethods.VK_JUNJA;
                    break;

                case Keys.FinalMode:
                    virtualKeys = NativeMethods.VK_FINAL;
                    break;

                case Keys.KanjiMode:
                    virtualKeys = NativeMethods.VK_KANJI;
                    break;

                case Keys.Escape:
                    virtualKeys = NativeMethods.VK_ESCAPE;
                    break;

                case Keys.IMEConvert:
                    virtualKeys = NativeMethods.VK_CONVERT;
                    break;

                case Keys.IMENonconvert:
                    virtualKeys = NativeMethods.VK_NONCONVERT;
                    break;

                case Keys.IMEAccept:
                    virtualKeys = NativeMethods.VK_ACCEPT;
                    break;

                case Keys.IMEModeChange:
                    virtualKeys = NativeMethods.VK_MODECHANGE;
                    break;

                case Keys.Space:
                    virtualKeys = NativeMethods.VK_SPACE;
                    break;

                case Keys.Prior:
                    virtualKeys = NativeMethods.VK_PRIOR;
                    break;

                case Keys.Next:
                    virtualKeys = NativeMethods.VK_NEXT;
                    break;

                case Keys.End:
                    virtualKeys = NativeMethods.VK_END;
                    break;

                case Keys.Home:
                    virtualKeys = NativeMethods.VK_HOME;
                    break;

                case Keys.Left:
                    virtualKeys = NativeMethods.VK_LEFT;
                    break;

                case Keys.Up:
                    virtualKeys = NativeMethods.VK_UP;
                    break;

                case Keys.Right:
                    virtualKeys = NativeMethods.VK_RIGHT;
                    break;

                case Keys.Down:
                    virtualKeys = NativeMethods.VK_DOWN;
                    break;

                case Keys.Select:
                    virtualKeys = NativeMethods.VK_SELECT;
                    break;

                case Keys.Print:
                    virtualKeys = NativeMethods.VK_PRINT;
                    break;

                case Keys.Execute:
                    virtualKeys = NativeMethods.VK_EXECUTE;
                    break;

                case Keys.Snapshot:
                    virtualKeys = NativeMethods.VK_SNAPSHOT;
                    break;

                case Keys.Insert:
                    virtualKeys = NativeMethods.VK_INSERT;
                    break;

                case Keys.Delete:
                    virtualKeys = NativeMethods.VK_DELETE;
                    break;

                case Keys.Help:
                    virtualKeys = NativeMethods.VK_HELP;
                    break;

                case Keys.D0:
                    virtualKeys = NativeMethods.VK_0;
                    break;

                case Keys.D1:
                    virtualKeys = NativeMethods.VK_1;
                    break;

                case Keys.D2:
                    virtualKeys = NativeMethods.VK_2;
                    break;

                case Keys.D3:
                    virtualKeys = NativeMethods.VK_3;
                    break;

                case Keys.D4:
                    virtualKeys = NativeMethods.VK_4;
                    break;

                case Keys.D5:
                    virtualKeys = NativeMethods.VK_5;
                    break;

                case Keys.D6:
                    virtualKeys = NativeMethods.VK_6;
                    break;

                case Keys.D7:
                    virtualKeys = NativeMethods.VK_7;
                    break;

                case Keys.D8:
                    virtualKeys = NativeMethods.VK_8;
                    break;

                case Keys.D9:
                    virtualKeys = NativeMethods.VK_9;
                    break;

                case Keys.A:
                    virtualKeys = NativeMethods.VK_A;
                    break;

                case Keys.B:
                    virtualKeys = NativeMethods.VK_B;
                    break;

                case Keys.C:
                    virtualKeys = NativeMethods.VK_C;
                    break;

                case Keys.D:
                    virtualKeys = NativeMethods.VK_D;
                    break;

                case Keys.E:
                    virtualKeys = NativeMethods.VK_E;
                    break;

                case Keys.F:
                    virtualKeys = NativeMethods.VK_F;
                    break;

                case Keys.G:
                    virtualKeys = NativeMethods.VK_G;
                    break;

                case Keys.H:
                    virtualKeys = NativeMethods.VK_H;
                    break;

                case Keys.I:
                    virtualKeys = NativeMethods.VK_I;
                    break;

                case Keys.J:
                    virtualKeys = NativeMethods.VK_J;
                    break;

                case Keys.K:
                    virtualKeys = NativeMethods.VK_K;
                    break;

                case Keys.L:
                    virtualKeys = NativeMethods.VK_L;
                    break;

                case Keys.M:
                    virtualKeys = NativeMethods.VK_M;
                    break;

                case Keys.N:
                    virtualKeys = NativeMethods.VK_N;
                    break;

                case Keys.O:
                    virtualKeys = NativeMethods.VK_O;
                    break;

                case Keys.P:
                    virtualKeys = NativeMethods.VK_P;
                    break;

                case Keys.Q:
                    virtualKeys = NativeMethods.VK_Q;
                    break;

                case Keys.R:
                    virtualKeys = NativeMethods.VK_R;
                    break;

                case Keys.S:
                    virtualKeys = NativeMethods.VK_S;
                    break;

                case Keys.T:
                    virtualKeys = NativeMethods.VK_T;
                    break;

                case Keys.U:
                    virtualKeys = NativeMethods.VK_U;
                    break;

                case Keys.V:
                    virtualKeys = NativeMethods.VK_V;
                    break;

                case Keys.W:
                    virtualKeys = NativeMethods.VK_W;
                    break;

                case Keys.X:
                    virtualKeys = NativeMethods.VK_X;
                    break;

                case Keys.Y:
                    virtualKeys = NativeMethods.VK_Y;
                    break;

                case Keys.Z:
                    virtualKeys = NativeMethods.VK_Z;
                    break;

                case Keys.LWin:
                    virtualKeys = NativeMethods.VK_LWIN;
                    break;

                case Keys.RWin:
                    virtualKeys = NativeMethods.VK_RWIN;
                    break;

                case Keys.Apps:
                    virtualKeys = NativeMethods.VK_APPS;
                    break;

                case Keys.Sleep:
                    virtualKeys = NativeMethods.VK_SLEEP;
                    break;

                case Keys.NumPad0:
                    virtualKeys = NativeMethods.VK_NUMPAD0;
                    break;

                case Keys.NumPad1:
                    virtualKeys = NativeMethods.VK_NUMPAD1;
                    break;

                case Keys.NumPad2:
                    virtualKeys = NativeMethods.VK_NUMPAD2;
                    break;

                case Keys.NumPad3:
                    virtualKeys = NativeMethods.VK_NUMPAD3;
                    break;

                case Keys.NumPad4:
                    virtualKeys = NativeMethods.VK_NUMPAD4;
                    break;

                case Keys.NumPad5:
                    virtualKeys = NativeMethods.VK_NUMPAD5;
                    break;

                case Keys.NumPad6:
                    virtualKeys = NativeMethods.VK_NUMPAD6;
                    break;

                case Keys.NumPad7:
                    virtualKeys = NativeMethods.VK_NUMPAD7;
                    break;

                case Keys.NumPad8:
                    virtualKeys = NativeMethods.VK_NUMPAD8;
                    break;

                case Keys.NumPad9:
                    virtualKeys = NativeMethods.VK_NUMPAD9;
                    break;

                case Keys.Multiply:
                    virtualKeys = NativeMethods.VK_MULTIPLY;
                    break;

                case Keys.Add:
                    virtualKeys = NativeMethods.VK_ADD;
                    break;

                case Keys.Separator:
                    virtualKeys = NativeMethods.VK_SEPARATOR;
                    break;

                case Keys.Subtract:
                    virtualKeys = NativeMethods.VK_SUBTRACT;
                    break;

                case Keys.Decimal:
                    virtualKeys = NativeMethods.VK_DECIMAL;
                    break;

                case Keys.Divide:
                    virtualKeys = NativeMethods.VK_DIVIDE;
                    break;

                case Keys.F1:
                    virtualKeys = NativeMethods.VK_F1;
                    break;

                case Keys.F2:
                    virtualKeys = NativeMethods.VK_F2;
                    break;

                case Keys.F3:
                    virtualKeys = NativeMethods.VK_F3;
                    break;

                case Keys.F4:
                    virtualKeys = NativeMethods.VK_F4;
                    break;

                case Keys.F5:
                    virtualKeys = NativeMethods.VK_F5;
                    break;

                case Keys.F6:
                    virtualKeys = NativeMethods.VK_F6;
                    break;

                case Keys.F7:
                    virtualKeys = NativeMethods.VK_F7;
                    break;

                case Keys.F8:
                    virtualKeys = NativeMethods.VK_F8;
                    break;

                case Keys.F9:
                    virtualKeys = NativeMethods.VK_F9;
                    break;

                case Keys.F10:
                    virtualKeys = NativeMethods.VK_F10;
                    break;

                case Keys.F11:
                    virtualKeys = NativeMethods.VK_F11;
                    break;

                case Keys.F12:
                    virtualKeys = NativeMethods.VK_F12;
                    break;

                case Keys.F13:
                    virtualKeys = NativeMethods.VK_F13;
                    break;

                case Keys.F14:
                    virtualKeys = NativeMethods.VK_F14;
                    break;

                case Keys.F15:
                    virtualKeys = NativeMethods.VK_F15;
                    break;

                case Keys.F16:
                    virtualKeys = NativeMethods.VK_F16;
                    break;

                case Keys.F17:
                    virtualKeys = NativeMethods.VK_F17;
                    break;

                case Keys.F18:
                    virtualKeys = NativeMethods.VK_F18;
                    break;

                case Keys.F19:
                    virtualKeys = NativeMethods.VK_F19;
                    break;

                case Keys.F20:
                    virtualKeys = NativeMethods.VK_F20;
                    break;

                case Keys.F21:
                    virtualKeys = NativeMethods.VK_F21;
                    break;

                case Keys.F22:
                    virtualKeys = NativeMethods.VK_F22;
                    break;

                case Keys.F23:
                    virtualKeys = NativeMethods.VK_F23;
                    break;

                case Keys.F24:
                    virtualKeys = NativeMethods.VK_F24;
                    break;

                case Keys.NumLock:
                    virtualKeys = NativeMethods.VK_NUMLOCK;
                    break;

                case Keys.Scroll:
                    virtualKeys = NativeMethods.VK_SCROLL;
                    break;

                case Keys.LShiftKey:
                    virtualKeys = NativeMethods.VK_LSHIFT;
                    break;

                case Keys.RShiftKey:
                    virtualKeys = NativeMethods.VK_RSHIFT;
                    break;

                case Keys.LControlKey:
                    virtualKeys = NativeMethods.VK_LCONTROL;
                    break;

                case Keys.RControlKey:
                    virtualKeys = NativeMethods.VK_RCONTROL;
                    break;

                case Keys.Alt:
                    virtualKeys = NativeMethods.VK_LMENU;
                    break;

                case Keys.BrowserBack:
                    virtualKeys = NativeMethods.VK_BROWSER_BACK;
                    break;

                case Keys.BrowserForward:
                    virtualKeys = NativeMethods.VK_BROWSER_FORWARD;
                    break;

                case Keys.BrowserRefresh:
                    virtualKeys = NativeMethods.VK_BROWSER_REFRESH;
                    break;

                case Keys.BrowserStop:
                    virtualKeys = NativeMethods.VK_BROWSER_STOP;
                    break;

                case Keys.BrowserSearch:
                    virtualKeys = NativeMethods.VK_BROWSER_SEARCH;
                    break;

                case Keys.BrowserFavorites:
                    virtualKeys = NativeMethods.VK_BROWSER_FAVORITES;
                    break;

                case Keys.BrowserHome:
                    virtualKeys = NativeMethods.VK_BROWSER_HOME;
                    break;

                case Keys.VolumeMute:
                    virtualKeys = NativeMethods.VK_VOLUME_MUTE;
                    break;

                case Keys.VolumeDown:
                    virtualKeys = NativeMethods.VK_VOLUME_DOWN;
                    break;

                case Keys.VolumeUp:
                    virtualKeys = NativeMethods.VK_VOLUME_UP;
                    break;

                case Keys.MediaNextTrack:
                    virtualKeys = NativeMethods.VK_MEDIA_NEXT_TRACK;
                    break;

                case Keys.MediaPreviousTrack:
                    virtualKeys = NativeMethods.VK_MEDIA_PREV_TRACK;
                    break;

                case Keys.MediaStop:
                    virtualKeys = NativeMethods.VK_MEDIA_STOP;
                    break;

                case Keys.MediaPlayPause:
                    virtualKeys = NativeMethods.VK_MEDIA_PLAY_PAUSE;
                    break;

                case Keys.LaunchMail:
                    virtualKeys = NativeMethods.VK_LAUNCH_MAIL;
                    break;

                case Keys.SelectMedia:
                    virtualKeys = NativeMethods.VK_LAUNCH_MEDIA_SELECT;
                    break;

                case Keys.LaunchApplication1:
                    virtualKeys = NativeMethods.VK_LAUNCH_APP1;
                    break;

                case Keys.LaunchApplication2:
                    virtualKeys = NativeMethods.VK_LAUNCH_APP2;
                    break;

                case Keys.OemSemicolon:
                    virtualKeys = NativeMethods.VK_OEM_1;
                    break;

                case Keys.Oemplus:
                    virtualKeys = NativeMethods.VK_OEM_PLUS;
                    break;

                case Keys.Oemcomma:
                    virtualKeys = NativeMethods.VK_OEM_COMMA;
                    break;

                case Keys.OemMinus:
                    virtualKeys = NativeMethods.VK_OEM_MINUS;
                    break;

                case Keys.OemPeriod:
                    virtualKeys = NativeMethods.VK_OEM_PERIOD;
                    break;

                case Keys.OemQuestion:
                    virtualKeys = NativeMethods.VK_OEM_2;
                    break;

                case Keys.Oemtilde:
                    virtualKeys = NativeMethods.VK_OEM_3;
                    break;

                case Keys.OemOpenBrackets:
                    virtualKeys = NativeMethods.VK_OEM_4;
                    break;

                case Keys.OemPipe:
                    virtualKeys = NativeMethods.VK_OEM_5;
                    break;

                case Keys.OemCloseBrackets:
                    virtualKeys = NativeMethods.VK_OEM_6;
                    break;

                case Keys.OemQuotes:
                    virtualKeys = NativeMethods.VK_OEM_7;
                    break;

                case Keys.Oem8:
                    virtualKeys = NativeMethods.VK_OEM_8;
                    break;

                case Keys.OemBackslash:
                    virtualKeys = NativeMethods.VK_OEM_102;
                    break;

                case Keys.ProcessKey:
                    virtualKeys = NativeMethods.VK_PROCESSKEY;
                    break;

                case Keys.Attn:                           // DbeAlphanumeric
                    virtualKeys = NativeMethods.VK_OEM_ATTN; // VK_DBE_ALPHANUMERIC
                    break;

                case Keys.EraseEof:                       // DbeFlushString
                    virtualKeys = NativeMethods.VK_EREOF; // VK_DBE_FLUSHSTRING
                    break;

                case Keys.Play:                           // DbeCodeInput
                    virtualKeys = NativeMethods.VK_PLAY;  // VK_DBE_CODEINPUT
                    break;

                case Keys.Zoom:                           // DbeNoCodeInput
                    virtualKeys = NativeMethods.VK_ZOOM;  // VK_DBE_NOCODEINPUT
                    break;

                case Keys.NoName:                          // DbeDetermineString
                    virtualKeys = NativeMethods.VK_NONAME; // VK_DBE_DETERMINESTRING
                    break;

                case Keys.Pa1:                          // DbeEnterDlgConversionMode
                    virtualKeys = NativeMethods.VK_PA1; // VK_ENTERDLGCONVERSIONMODE
                    break;

                case Keys.OemClear:
                    virtualKeys = NativeMethods.VK_OEM_CLEAR;
                    break;

                default:
                    virtualKeys = 0;
                    break;
            }

            return virtualKeys;
        }
    }
}
