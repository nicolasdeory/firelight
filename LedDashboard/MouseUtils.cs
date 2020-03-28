/* This code was modified from Project Aurora's, and it's subject to the
 * MIT License

Copyright (c) 2016 Anton Pupkov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/



using SharpDX.RawInput;
using MBF = SharpDX.RawInput.MouseButtonFlags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard
{
    public static class MouseUtils
    {

        private static MBF[] LeftButtonFlags { get; } = new[] { MBF.Button1Down, MBF.Button1Up, MBF.LeftButtonDown, MBF.LeftButtonUp };
        private static MBF[] RightButtonFlags { get; } = new[] { MBF.Button2Down, MBF.Button2Up, MBF.RightButtonDown, MBF.RightButtonDown };
        private static MBF[] MiddleButtonFlags { get; } = new[] { MBF.Button3Down, MBF.Button3Up, MBF.MiddleButtonDown, MBF.MiddleButtonUp };
        private static MBF[] XButton1Flags { get; } = new[] { MBF.Button4Down, MBF.Button4Up };
        private static MBF[] XButton2Flags { get; } = new[] { MBF.Button5Down, MBF.Button5Up };

        private static MBF[] ButtonDownFlags { get; } = new[] { MBF.Button1Down, MBF.Button2Down, MBF.Button3Down, MBF.Button4Down, MBF.Button5Down, MBF.LeftButtonDown, MBF.RightButtonDown, MBF.MiddleButtonDown };
        private static MBF[] ButtonUpFlags { get; } = new[] { MBF.Button1Up, MBF.Button2Up, MBF.Button3Up, MBF.Button4Up, MBF.Button5Up, MBF.LeftButtonUp, MBF.RightButtonUp, MBF.MiddleButtonUp };

        /// <summary>
        /// Gets the System.Windows.Forms.MouseButtons button for the button that was pressed/released.
        /// </summary>
        public static MouseButtons GetMouseButton(this MouseInputEventArgs e)
        {
            MBF flags = e.ButtonFlags;
            if (LeftButtonFlags.Contains(flags))
                return MouseButtons.Left;
            else if (RightButtonFlags.Contains(flags))
                return MouseButtons.Right;
            else if (MiddleButtonFlags.Contains(flags))
                return MouseButtons.Middle;
            else if (XButton1Flags.Contains(flags))
                return MouseButtons.XButton1;
            else if (XButton2Flags.Contains(flags))
                return MouseButtons.XButton2;
            else
                return MouseButtons.None;
        }

        /// <summary>
        /// Checks if the event is a MouseDown event.
        /// </summary>
        public static bool IsMouseDownEvent(this MouseInputEventArgs e)
        {
            return ButtonDownFlags.Contains(e.ButtonFlags);
        }

        /// <summary>
        /// Checks if the event is a MouseDown event.
        /// </summary>
        public static bool IsMouseUpEvent(this MouseInputEventArgs e)
        {
            return ButtonUpFlags.Contains(e.ButtonFlags);
        }
    }
}