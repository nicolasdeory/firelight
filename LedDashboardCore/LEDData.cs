using System;
using System.Collections.Generic;
using System.Text;

namespace LedDashboardCore
{
    public enum KeyboardMode
    {
        NoNumpad, Numpad
    }

    public class LEDData
    {
        public KeyboardMode KeyboardLEDMode { get; }
        public Led[] Keyboard { get; } // 88 or 88 + numpad
        public Led[] Mouse { get; } // 1 ?
        public Led[] Strip { get; } // 170

        public static LEDData Empty
        {
            get
            {
                return new LEDData(new Led[88], KeyboardMode.NoNumpad, new Led[1], new Led[170]);
            }
        }

        public LEDData(Led[] keyboard, KeyboardMode keyboardMode, Led[] mouse, Led[] strip)
        {
            if (keyboard != null && keyboard.Length != 88)
            {
                throw new ArgumentException("Keyboard LED array length must be 88");
            }
            this.Keyboard = keyboard;
            this.KeyboardLEDMode = keyboardMode;
            if (mouse != null && mouse.Length != 1)
            {
                throw new ArgumentException("Mouse LED array length must be 1");
            }
            this.Mouse = mouse;
            if (strip != null && strip.Length != 170)
            {
                throw new ArgumentException("Mouse LED array length must be 170");
            }
            this.Strip = Strip;
        }
    }
}
