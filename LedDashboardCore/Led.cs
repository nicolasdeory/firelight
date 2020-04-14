using System;

namespace LedDashboardCore
{
    public class Led
    {
        public HSVColor color;
        public Led()
        {
            color = new HSVColor(0, 0, 0);
        }

        public void FadeToBlackBy(float factor)
        {
            color.v *= 1 - factor;
            if (color.v <= 0.025f)
            {
                color.v = 0;
            }
        }

        public void FadeToColorBy(HSVColor c, float factor)
        {
            color.h = FadeHSVProperty(color.h, c.h, factor);
            color.s = FadeHSVProperty(color.s, c.s, factor);
            color.v = FadeHSVProperty(color.v, c.v, factor);
        }

        private static float FadeHSVProperty(float a, float b, float factor)
        {
            if (a < b)
            {
                a += (b - a) * factor;
            }
            else
            {
                a -= (a - b) * factor;
            }
            if (Math.Abs(a - b) <= 0.025f)
            {
                a = b;
            }
            return a;
        }

        public void SetBlack()
        {
            color = HSVColor.Black;
        }

        public void Color(HSVColor col)
        {
            color = col;
        }

        public void MixNewColor(HSVColor col, bool additive = false, float rate = 0.5f)
        {
            if (!additive && color.Equals(HSVColor.Black))
            {
                color.h = col.h;
                color.s = col.s;
                color.v = col.v;
                // color.v = 0.5f * col.v;
            }
            else
            {
                color.h = (1 - rate) * color.h + rate * col.h;
                color.s = (1 - rate) * color.s + rate * col.s;
                if (additive)
                {
                    color.v = 0.5f * color.v + 0.5f * col.v;
                }
                else
                {
                    if (col.v > color.v) color.v = col.v;

                }
            }
        }


    }
}
