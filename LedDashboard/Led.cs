using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
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
            color.v = color.v - color.v * factor;
            if (color.v <= 0.025f)
            {
                color.v = 0;
            }
        }

        public void FadeToColorBy(HSVColor c, float factor)
        {
            if (color.h < c.h)
            {
                color.h = color.h + (c.h - color.h) * factor;
            } else
            {
                color.h = color.h - (color.h - c.h) * factor;
            }
            if(Math.Abs(color.h - c.h) <= 0.025f)
            {
                color.h = c.h;
            }

            if (color.s < c.s)
            {
                color.s = color.s + (c.s - color.s) * factor;
            }
            else
            {
                color.s = color.s - (color.s - c.s) * factor;
            }
            if (Math.Abs(color.s - c.s) <= 0.025f)
            {
                color.s = c.s;
            }

            if (color.v < c.v)
            {
                color.v = color.v + (c.v - color.v) * factor;
            }
            else
            {
                color.v = color.v - (color.v - c.v) * factor;
            }
            if (Math.Abs(color.h - c.h) <= 0.025f)
            {
                color.v = c.v;
            }
        }

        public void SetBlack()
        {
            color = HSVColor.Black;
        }

        public void Color(HSVColor col)
        {
            color = col;
        }

        public void MixNewColor(HSVColor col, bool additive = false, float rate=0.5f)
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
                } else
                {
                    if (col.v > color.v) color.v = col.v;

                }
            }
        }

    }
}
