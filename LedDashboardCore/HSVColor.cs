using System;
using System.Drawing;

namespace LedDashboardCore
{
    public struct HSVColor
    {
        public float h;
        public float s;
        public float v;

        public readonly static HSVColor Black = new HSVColor(0, 0, 0);
        public readonly static HSVColor White = new HSVColor(0, 0, 1);
        public HSVColor(float h, float s, float v)
        {
            this.h = h;
            this.s = s;
            this.v = v;
        }

        public override string ToString()
        {
            return $"HSV {h} {s} {v}";
        }

        public static HSVColor FromRGB(Color color)
        {
            HSVColor toReturn = new HSVColor();

            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            toReturn.h = (float)Math.Round(color.GetHue(), 2);
            toReturn.s = (float)((max == 0) ? 0 : 1d - (1d * min / max)) * 100;
            toReturn.s = (float)Math.Round(toReturn.s, 2);
            toReturn.v = (float)Math.Round(((max / 255d) * 100), 2);

            toReturn.h = toReturn.h / 360f;
            toReturn.s = toReturn.s / 100f;
            toReturn.v = toReturn.v / 100f;

            return toReturn;
        }

        public static HSVColor FromRGB(byte[] color)
        {
            Color col = Color.FromArgb(color[0], color[1], color[2]);
            return FromRGB(col);
        }

        public static HSVColor FromRGB(byte r, byte g, byte b)
        {
            Color col = Color.FromArgb(r, g, b);
            return FromRGB(col);
        }

        public byte[] ToRGB()
        {
            var arr = new byte[3];
            HsvToRgb(this.h, this.s, this.v, out arr[0], out arr[1], out arr[2]);
            return arr;
        }

        public static HSVColor Lerp(HSVColor c1, HSVColor c2, float t)
        {
            return new HSVColor()
            {
                h = c1.h + (c2.h - c1.h) * t,
                s = c1.s + (c2.s - c1.s) * t,
                v = c1.v + (c2.v - c1.v) * t,
            };
        }

        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        void HsvToRgb(double h, double S, double V, out byte r, out byte g, out byte b)
        {
            double H = h * 360;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((byte)(R * 255.0));
            g = Clamp((byte)(G * 255.0));
            b = Clamp((byte)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        static byte Clamp(byte i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        public override bool Equals(object obj)
        {
            return obj is HSVColor color &&
                   h == color.h &&
                   s == color.s &&
                   v == color.v;
        }

        public bool AlmostEqual(HSVColor col, float threshold = 0.1f)
        {
            return Math.Abs(h - col.h) < threshold && Math.Abs(s - col.s) < threshold && Math.Abs(v - col.v) < threshold;
        }

        public override int GetHashCode()
        {
            var hashCode = -716921630;
            hashCode = hashCode * -1521134295 + h.GetHashCode();
            hashCode = hashCode * -1521134295 + s.GetHashCode();
            hashCode = hashCode * -1521134295 + v.GetHashCode();
            return hashCode;
        }

    }
}
