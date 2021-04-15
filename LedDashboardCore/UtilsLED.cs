using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace FirelightCore
{
    public static class UtilsLED
    {
        /// <summary>
        /// Used by FourierAudioLED. TODO. No range checking! ledNum must be less than half the led array
        /// </summary>
        public static void AddSymmetricColorAroundLeds(this Led[] leds, int ledNum, HSVColor colHSV, int colorMixSpread = 5)
        {
            int ledCountHalf = leds.Length / 2;
            leds[ledCountHalf - 1 + ledNum].MixNewColor(colHSV);
            leds[ledCountHalf - 1 - ledNum].MixNewColor(colHSV);
            for (int i = 1; i < colorMixSpread; i++)
            {
                double cVal = colHSV.v - Utils.Scale(i, 0, colorMixSpread - 1, 0, colHSV.v);
                HSVColor c = new HSVColor(colHSV.h, colHSV.s, (float)cVal);
                if (ledCountHalf - 1 + ledNum + i < leds.Length)
                {
                    leds[ledCountHalf - 1 + ledNum + i].MixNewColor(c);
                    leds[ledCountHalf - 1 - ledNum + i].MixNewColor(c);
                }
                if (ledCountHalf - 1 - ledNum - i >= 0)
                {
                    leds[ledCountHalf - 1 + ledNum - i].MixNewColor(c);
                    leds[ledCountHalf - 1 - ledNum - i].MixNewColor(c);
                }
            }
        }

        public static void AddColorToLedsAround(this Led[] leds, int ledNum, HSVColor colHSV, int colorMixSpread = 5, bool overrideColor = false)
        {
            leds[ledNum].MixNewColor(colHSV);
            if (colHSV.v == 0) return;
            for (int i = 1; i < colorMixSpread; i++)
            {
                double cVal = colHSV.v - Utils.Scale(i, 0, colorMixSpread - 1, 0, colHSV.v);
                HSVColor c = new HSVColor(colHSV.h, colHSV.s, (float)cVal);
                if (ledNum + i < leds.Length)
                {
                    if (overrideColor)
                    {
                        leds[ledNum + i].Color(colHSV);
                    }
                    else
                    {
                        leds[ledNum + i].MixNewColor(c);
                    }
                }
                if (ledNum - i >= 0)
                {
                    if (overrideColor)
                    {
                        leds[ledNum - i].Color(colHSV);
                    }
                    else
                    {
                        leds[ledNum - i].MixNewColor(c);
                    }
                }
            }
        }

        public static void FadeToBlackAllLeds(this Led[] leds, float fadeToBlackFactor = 0.3f)
        {
            foreach (var led in leds)
            {
                led.FadeToBlackBy(fadeToBlackFactor);
            }
        }

        public static void FadeToColorAllLeds(this Led[] leds, HSVColor color, float fadeToColorFactor = 0.3f)
        {
            foreach (var led in leds)
            {
                led.FadeToColorBy(color, fadeToColorFactor);
            }
        }

        public static Led GetLedIn2D(this Led[] leds, int x, int y, int rowSize = 21)
        {
            return leds[y * rowSize + x];
        }


        public static Led[] CloneLeds(this Led[] leds)
        {
            return leds.Select(l => l.Clone()).ToArray();
        }

        public static HSVColor ValueToHSV(int numLeds, int led, double val)
        {
            // Rainbow
            double normalizedLed = Utils.Scale(led, 0, numLeds, 0, 1);
            HSVColor hsv = HSVColor.FromRGB(Gradient.RainbowPalette[normalizedLed]);
            HSVColor hsvCol = new HSVColor(hsv.h, hsv.s, val <= 0.05f ? 0 : (float)val);
            return hsvCol;
        }

        public static HSVColor FadeToBlackBy(this HSVColor c, float factor)
        {
            HSVColor c1 = new HSVColor(c.h, c.s, c.v);
            c1.v *= 1 - factor;
            if (c1.v <= 0.05f)
            {
                c1.v = 0;
            }
            return c1;
        }

        public static HSVColor FadeToColorBy(this HSVColor c, HSVColor color, float factor)
        {
            HSVColor c1 = new HSVColor(c.h, c.s, c.v);
            c1.h = Utils.FadeProperty(c1.h, color.h, factor);
            c1.s = Utils.FadeProperty(c1.s, color.s, factor);
            c1.v = Utils.FadeProperty(c1.v, color.v, factor);
            return c1;
        }


        public static void SetAllToColor(this Led[] leds, HSVColor col)
        {
            foreach (var led in leds)
            {
                led.Color(col);
            }
        }

        public static void SetAllToColor(this LEDData ledData, HSVColor col)
        {
            ledData.Keyboard.SetAllToColor(col);
            ledData.Mouse.SetAllToColor(col);
            ledData.Mousepad.SetAllToColor(col);
            ledData.Keypad.SetAllToColor(col);
            ledData.Headset.SetAllToColor(col);
            ledData.Strip.SetAllToColor(col);
            ledData.General.SetAllToColor(col);
        }

        public static void SetAllToBlack(this Led[] leds)
        {
            leds.SetAllToColor(HSVColor.Black);
        }

        public static Led[] LedsFromBytes(int ledCount, byte[] data)
        {
            Led[] leds = new Led[ledCount];
            for (int i = 0; i < ledCount; i++)
            {
                leds[i] = new Led();
                leds[i].Color(HSVColor.FromRGB(data[i * 3], data[i * 3 + 1], data[i * 3 + 2]));
            }
            return leds;
        }

        public static int ColorDifference(this Color c1, Color c2)
        {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B);
        }


        /// <summary>
        /// Returns the LED array as a byte array contaning Red, Green and Blue value bytes for each LED in the strip.
        /// </summary>
        /// <param name="reverseOrder"></param>
        /// <returns>A byte array of length ledCount * 3</returns>
        public static byte[] ToByteArray(this Led[] leds, bool reverseOrder = false)
        {
            byte[] data = new byte[leds.Length * 3];
            if (reverseOrder)
            {
                for (int i = leds.Length * 3 - 1; i >= 0; i -= 3)
                {
                    int index = (leds.Length * 3 - 1) - i;
                    byte[] col = leds[index / 3].color.ToRGB();
                    data[i] = col[2];
                    data[i - 1] = col[1];
                    data[i - 2] = col[0];
                }
            }
            else
            {
                for (int i = 0; i < leds.Length * 3; i += 3)
                {
                    byte[] col = leds[i / 3].color.ToRGB();
                    data[i] = col[0];
                    data[i + 1] = col[1];
                    data[i + 2] = col[2];
                }
            }

            return data;
        }
    }
}
