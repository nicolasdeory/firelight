using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using LedDashboardCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Games.RocketLeague
{
    class BoostModule
    {
        // Constants

        public static HSVColor BoostColor { get; } = new HSVColor(0.12f, 1f, 1f);

        // Vision constants
        public static Color VisionNormalBoostColor = Color.FromArgb(253, 209, 93);
        public static Color VisionTopBoostColor = Color.FromArgb(255, 255, 255);


        //double[] LUMINOSITY_THRESHOLDS = new double[] { 10, 10, 11, 11, 10, 11, 11, 11, 12, 12, 12, 12, 13, 12, 12, 13, 12, 13, 12, 13, 11, 13, 13, 12, 13, 12, 13, 13 }; // THESE ARE WORKING ON 64x64!!!!
        double[] LUMINOSITY_THRESHOLDS = new double[] { 0.2, 2.2, 2.2, 3.3, 3.1, 4.7, 3.2, 3.7, 4.1, 4.8, 5, 4.9, 4.6, 5.1, 4.5, 5.7, 5.3, 5.6, 5.1, 5.6, 3.9, 7.9, 4.9, 7.9, 4.9, 5.7, 5.2, 4.3 }; // 128x128
        /// <summary>
        /// Gets the LEDFrame for the boost view
        /// </summary>
        public LEDFrame DoFrame(Bitmap screenFrame)
        {
            // Set the cropping region
            // It might change depending on the resolution. Right now it works for 1920x1080
            // ROCKET LEAGUE @ 1920x1080: left 290, top 290, width 220, height 240
            Rectangle cropRect = new Rectangle(1920 - 290, 1080 - 290, 220, 240);

            // Get the screen frame
            if (screenFrame == null)
                return null;

            // Resize the bitmap
            Bitmap target = new Bitmap(128, 128);

            // int left = cropRect.X;
            //int top = cropRect.Y;
            using (Graphics g = Graphics.FromImage(target))
            {
                // int diffWidth = frameBitmap.Width - left;
                // int diffheight = frameBitmap.Height - top;
                g.DrawImage(screenFrame, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            return ProcessFrame(target);
        }

        private static List<int> alreadyTouchedLeds = new List<int>(); // fixes a weird flickering bug
        int ix = 0;
        private LEDFrame ProcessFrame(Bitmap b)
        {
            // REMARK: If capturing DirectX full screen, the bitmap resolution changes (goes small). This may throw off accurate croppings as positions change.
            // Windows appear in the upper left side of the screen at the resolution specified (i.e 640x480)
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                try
                {
                    double[] luminosities;
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory
                            .Load(b)
                            .Filter(MatrixFilters.GreyScale)
                            .Contrast(80)
                            .Save(memStream);//.Save($"debugcaptures/img{ix}.png");
                       // ix++;
                        using (Bitmap bpls = (Bitmap)Bitmap.FromStream(memStream))
                        {
                            luminosities = GetPixelInfo(bpls);
                        }
                        b.Dispose();
                    }
                    return GetLEDFrameFromLuminosities(luminosities);


                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Debug.WriteLine("Error processing frame");
                    return null;
                }

            }
        }

        /// <summary>
        /// Estimates boost amount calculating average luminosity in certain sections of the image
        /// </summary>
        /// <param name="b">64x64 boost grayscale image</param>
        private double[] GetPixelInfo(Bitmap b)
        {
            double centerX = 33*2;
            double centerY = 33*2;
            double radius = 28*2;
            double step = 0.01f*2;

            double[] luminosities = new double[28]; // x increments

            // arc
            double start = Math.PI / 2;
            double end = Math.PI + Math.PI / 2 + Math.PI / 4;

            for (double i = start; i < end; i += step)
            {
                double calcX = Math.Cos(i);
                double calcY = Math.Sin(i);

                int realX = (int)(calcX * radius + centerX);
                int realY = (int)(calcY * radius + centerY);

                double fractionOfCircumference = Math.Max(0, Utils.Scale(i, start, end, 0, 1));

                int index = (int)Math.Floor(fractionOfCircumference * 28);

                double luminosity = b.GetPixel(realX, realY).R / 255.0;//.ColorDifference(VisionNormalBoostColor) / (255.0 * 3);
                // TODO: 80% boost+ is white!
                luminosities[index] += luminosity;
                b.SetPixel(realX, realY, Color.Red);
            }
           // b.Save($"debugcaptures/img{ix}.png");
          //  ix++;
            return luminosities;
        }

        private LEDFrame GetLEDFrameFromLuminosities(double[] luminosities)
        {
            LEDFrame frame = LEDFrame.Empty;
            LEDData data = frame.Leds;

            //Debug.WriteLine(String.Join(",", luminosities));
            int lastLuminositySpot = 0;
            for (int i = 0; i < LUMINOSITY_THRESHOLDS.Length; i++)
            {
                if (luminosities[i] > LUMINOSITY_THRESHOLDS[i])
                    lastLuminositySpot = i;
            }
            // TODO: Detect when it's not a valid frame so lights dont go crazy

            alreadyTouchedLeds.Clear();
            //double lastLuminositySpotNormal = lastLuminositySpot / 20.0;
            //Debug.WriteLine(lastLuminositySpot);
            double boostCurve = lastLuminositySpot / (double)(LUMINOSITY_THRESHOLDS.Length - 1);
            int keyboardBoostLeds = Math.Max(boostCurve > 0 ? 1 : 0, (int)Utils.Scale(boostCurve, 0, 1, 0, 18)); // ease in

            // KEYBOARD

            for (int i = 0; i < 16; i++)
            {
                List<int> listLedsToTurnOn = new List<int>(6); // light the whole column
                for (int j = 0; j < 6; j++)
                {
                    listLedsToTurnOn.Add(KeyUtils.PointToKey(new Point(i, j)));
                }

                if (i < keyboardBoostLeds)
                {
                    foreach (int idx in listLedsToTurnOn.Where(x => x != -1))
                    {
                        if (alreadyTouchedLeds.Contains(idx))
                            continue;
                        data.Keyboard[idx].Color(BoostColor);
                    }
                }
                else
                {
                    foreach (int idx in listLedsToTurnOn.Where(x => x != -1))
                    {
                        if (alreadyTouchedLeds.Contains(idx))
                            continue;
                        if (data.Keyboard[idx].color.AlmostEqual(BoostColor))
                        {
                            data.Keyboard[idx].Color(BoostColor);
                        }
                        else
                        {
                            data.Keyboard[idx].FadeToBlackBy(0.08f);
                        }

                    }
                }
                alreadyTouchedLeds.AddRange(listLedsToTurnOn);

            }

            // MOUSE
            int mouseBoostLeds = (int)Utils.Scale(boostCurve, 0, 1, 0, 8);
            for (int i = 0; i < LEDData.NUMLEDS_MOUSE; i++)
            {
                data.Mouse[i].FadeToBlackBy(0.01f);
            }
            if (boostCurve > 0)
            {
                data.Mouse[0].Color(BoostColor);
                data.Mouse[1].Color(BoostColor);
                for (int i = 0; i < mouseBoostLeds; i++)
                {
                    data.Mouse[2 + 6 - i].Color(BoostColor);
                    data.Mouse[2 + 7 + 6 - i].Color(BoostColor); // both sides of the mouse
                }
            }

            // MOUSEPAD

            int mousepadBoostLeds = (int)Utils.Scale(boostCurve, 0, 1, 0, 17);
            for (int i = 0; i < LEDData.NUMLEDS_MOUSEPAD; i++)
            {
                if (i < mousepadBoostLeds)
                    data.Mousepad[i].Color(BoostColor);
                else
                {
                    if (data.Mousepad[i].color.AlmostEqual(BoostColor))
                    {
                        data.Mousepad[i].Color(BoostColor);
                    }
                    else
                    {
                        data.Mousepad[i].FadeToBlackBy(0.05f);
                    }
                }
            }

            return new LEDFrame(this, data, LightZone.Desk);
        }

    }
}
