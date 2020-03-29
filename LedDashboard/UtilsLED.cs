using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    static class UtilsLED
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

        public static void AddColorToLedsAround(this Led[] leds, int ledNum, HSVColor colHSV, int colorMixSpread = 5)
        {
            leds[ledNum].MixNewColor(colHSV);
            if (colHSV.v == 0) return;
            for (int i = 1; i < colorMixSpread; i++)
            {
                double cVal = colHSV.v - Utils.Scale(i, 0, colorMixSpread - 1, 0, colHSV.v);
                HSVColor c = new HSVColor(colHSV.h, colHSV.s, (float)cVal);
                if (ledNum + i < leds.Length)
                {
                    leds[ledNum + i].MixNewColor(c);
                }
                if (ledNum - i >= 0)
                {
                    leds[ledNum - i].MixNewColor(c);
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
                led.FadeToColorBy(color,fadeToColorFactor);
            }
        }

        public static Led GetLedIn2D(this Led[] leds, int x, int y, int rowSize=21)
        {
            return leds[y * rowSize + x];
        }

        public static HSVColor ValueToHSV(int numLeds, int led, double val)
        {
            // Rainbow
            double normalizedLed = Utils.Scale(led, 0, numLeds, 0, 1);
            HSVColor hsv = HSVColor.FromRGB(Gradient.RainbowPalette[normalizedLed]);
            HSVColor hsvCol = new HSVColor(hsv.h, hsv.s, val <= 0.05f ? 0 : (float)val);
            return hsvCol;
        }

        public static void SetAllToColor(this Led[] leds, HSVColor col)
        {
            foreach (var led in leds)
            {
                led.Color(col);
            }
        }
        public static void SetAllToBlack(this Led[] leds)
        {
            leds.SetAllToColor(HSVColor.Black);
        }
    }
}
