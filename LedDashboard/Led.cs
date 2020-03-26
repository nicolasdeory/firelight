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
            if (color.v <= 0.05f)
            {
                color.v = 0;
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

        public void MixNewColor(HSVColor col, bool additive = false)
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
                color.h = 0.5f * color.h + 0.5f * col.h;
                color.s = 0.5f * color.s + 0.5f * col.s;
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
