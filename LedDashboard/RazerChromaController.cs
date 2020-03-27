using RazerChroma.Net;
using RazerChromaFrameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    class RazerChromaController
    {
        public static void Init()
        {
            NativeRazerApi api = new NativeRazerApi();
            keyboardFrame = new KeyboradFrame(api);
        }

        private static KeyboradFrame keyboardFrame;


        public static void SendData(int ledCount, byte[] colorArray)
        {
            if (keyboardFrame == null) Init();
            List<Point> points = new List<Point>();
            for (int i = 0; i < ledCount; i++)
            {
                Color c = Color.FromArgb(colorArray[i * 3], colorArray[i * 3 + 1], colorArray[i * 3 + 2]);
                int x = (int)Utils.Scale(i, 0, ledCount, 0, 22);
                points.Clear();
                points.Add(new Point(x, 0));
                points.Add(new Point(x, 1));
                points.Add(new Point(x, 2));
                points.Add(new Point(x, 3));
                points.Add(new Point(x, 4));
                points.Add(new Point(x, 5));
                keyboardFrame.SetKeys(points, c);
            }
            keyboardFrame.Update();
            
        }

    }
}
