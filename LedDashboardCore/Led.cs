using System;

namespace FirelightCore
{
    public class Led
    {
        public HSVColor color;
        public Led()
        {
            color = new HSVColor(0, 0, 0);
        }

        public Led(HSVColor color)
        {
            this.color = color;
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
            color.h = Utils.FadeProperty(color.h, c.h, factor);
            color.s = Utils.FadeProperty(color.s, c.s, factor);
            color.v = Utils.FadeProperty(color.v, c.v, factor);
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

        public Led Clone()
        {
            return new Led(this.color);
        }

        public override string ToString()
        {
            return this.color.ToString();
        }
    }
}
