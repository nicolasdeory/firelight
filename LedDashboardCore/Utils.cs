﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FirelightCore
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
            return (value - leftMin) / (leftMax - leftMin) * (rightMax - rightMin) + rightMin;
        }

        public static double Clamp(double val, double min, double max)
        {
            val = val < min ? min : val;
            val = val > max ? max : val;
            return val;
        }

        public static float FadeProperty(float a, float b, float factor)
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

        public static List<Type> GetTypesWithAttribute<T>()
            where T : Attribute
        {
            // TODO: This is a generally useful function that uses reflection, must be abstracted elsewhere
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttributes(typeof(T), true).Length > 0).ToList();
        }

    }
}
