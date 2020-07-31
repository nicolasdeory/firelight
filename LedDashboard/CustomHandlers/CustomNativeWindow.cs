using Chromely.Native;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using static Chromely.Native.WinNativeMethods;

namespace FirelightService.CustomHandlers
{
    public struct MINMAXINFO
    {
        public Point ptReserved;
        public Point ptMaxSize;
        public Point ptMaxPosition;
        public Point ptMinTrackSize;
        public Point ptMaxTrackSize;
    }

    public class CustomNativeWindow : WinAPIHost
    {
        protected override IntPtr WndProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            WM msg = (WM)message;
            switch (msg)
            {
                case WM.GETMINMAXINFO:
                    {
                        MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

                        // Minimum window size 1200x700

                        mmi.ptMinTrackSize.X = 1300;
                        mmi.ptMinTrackSize.Y = 800;
                       // mmi.ptMaxTrackSize.X = 1500;
                       // mmi.ptMaxTrackSize.Y = 1500;

                        Marshal.StructureToPtr(mmi, lParam, true);
                        return IntPtr.Zero;
                    }
                /*case WM.SIZING:
                    return IntPtr.Zero;*/
                default:
                    return base.WndProc(hWnd, message, wParam, lParam);
            }
        }
    }
}