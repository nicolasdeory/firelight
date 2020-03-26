using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    public static class Utils
    {
        public static string ToByteString(this byte[] b)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i] + " ");
            }
            return sb.ToString();
        }

        public static double Scale(double value, double leftMin, double leftMax, double rightMin, double rightMax)
        {
            double leftSpan = leftMax - leftMin;
            double rightSpan = rightMax - rightMin;
            double valueScaled = Clamp((value - leftMin) / leftSpan, rightMin, rightMax);
            return rightMin + (valueScaled * rightSpan);
        }

        public static double Clamp(double val, double min, double max)
        {
            val = val < min ? min : val;
            val = val > max ? max : val;
            return val;
        }

    }
}
