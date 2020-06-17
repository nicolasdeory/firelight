using DesktopDuplication;
using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using LedDashboardCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games.RocketLeague
{
    // TODO: Refactor this into a separate project
    public class RocketLeagueModule : BaseRLModule
    {

        private DesktopDuplicator desktopDuplicator;

        // Constants

        public static HSVColor LoadingColor { get; } = new HSVColor(0.09f, 0.8f, 1f);

        public static HSVColor DeadColor { get; } = new HSVColor(0f, 0.8f, 0.77f);
        public static HSVColor NoManaColor { get; } = new HSVColor(0.52f, 0.66f, 1f);

        public static HSVColor BoostColor { get; } = new HSVColor(0.12f, 1f, 1f);

        // Variables

        ulong msSinceLastExternalFrameReceived = 30000;
        ulong msAnimationTimerThreshold = 1500; // how long to wait for animation data until health bar kicks back in.
        //double currentGameTimestamp = 0;

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        // TODO: Maybe add more precision, but this is delicate, the threshold should be adjusted as well.
        const double LUMINOSITY_THRESHOLD = 10;



        /// <summary>
        /// Creates a new <see cref="RocketLeague"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        public static RocketLeagueModule Create(Dictionary<string, string> options)
        {
            return new RocketLeagueModule();
        }

        private RocketLeagueModule()
            : base()
        {
            // Rocket League module initialization

            // DesktopDuplication
            try
            {
                desktopDuplicator = new DesktopDuplicator(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            AddAnimatorEvent();

            //PlayLoadingAnimation();
            _ = FrameTimer().CatchExceptions();
        }

        // plays it indefinitely
        private void PlayLoadingAnimation()
        {
            Animator.StopCurrentAnimation();
            Animator.HoldColor(NoManaColor, LightZone.All, 1);
        }

        /// <summary>
        /// Queries updated boost data
        /// </summary>
        private LEDFrame QueryBoostInfo()
        {
            Bitmap lastFrame = null;
            DesktopFrame frame = null;
            try
            {
                frame = desktopDuplicator.GetLatestFrame();
                if (frame != null && frame.DesktopImage != lastFrame)
                {
                    Bitmap frameBitmap = frame.DesktopImage;
                    Bitmap target = new Bitmap(64, 64);
                    // crop area, it might change depending on the resolution. Right now it works for 1920x1080
                    int left = 290;
                    int top = 290;
                    int padRight = 70;
                    int padBottom = 50;
                    using (Graphics g = Graphics.FromImage(target))
                    {
                        int diffWidth = frameBitmap.Width - left;
                        int diffheight = frameBitmap.Height - top;
                        g.DrawImage(frameBitmap, new Rectangle(0, 0, target.Width, target.Height),
                                         new Rectangle(diffWidth, diffheight, left - padRight, top - padBottom),
                                         GraphicsUnit.Pixel);
                    }

                    return ProcessFrame(target);

                }
            }
            catch (Exception ex)
            {
                desktopDuplicator = new DesktopDuplicator(0);
                Debug.WriteLine("Exception in DesktopDuplication API occurred");
                //throw;
            }
            return null;
        }


        private static List<int> alreadyTouchedLeds = new List<int>(); // fixes a weird flickering bug
        private LEDFrame ProcessFrame(Bitmap b)
        {
            // REMARK: If capturing DirectX full screen, the bitmap resolution changes (goes small). This may throw off accurate croppings as positions change.
            // Windows appear in the upper left side of the screen at the resolution specified (i.e 640x480)
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                try
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory
                            .Load(b)
                            .Filter(MatrixFilters.GreyScale)
                            .Contrast(80).Save(memStream);

                        Bitmap bpls = (Bitmap)Bitmap.FromStream(memStream);
                        double[] luminosities = GetPixelInfo(bpls);
                        b.Dispose();
                        LEDFrame frame = LEDFrame.Empty;
                        LEDData data = frame.Leds;

                        double minLuminosity = 10000000;
                        
                        for (int i = 0; i < luminosities.Length; i++)
                        {
                            if (luminosities[i] < minLuminosity)
                                minLuminosity = luminosities[i];
                        }

                        int lastLuminositySpot = 0;
                        for (int i = 0; i < luminosities.Length; i++)
                        {
                            if (luminosities[i] > minLuminosity && luminosities[i] > LUMINOSITY_THRESHOLD)
                                lastLuminositySpot = i;
                        }
                       // Debug.WriteLine(lastLuminositySpot);
                        // TODO: Detect when it's not a valid frame so lights dont go crazy

                        alreadyTouchedLeds.Clear();

                        // if (lastLuminositySpot > 0) // so first led isn't always lit
                        //{
                        int boostLeds = (int)Utils.Scale(lastLuminositySpot / 20.0, 0, 1, 0, 17);

                            for (int i = 0; i < 16; i++)
                            {
                                List<int> listLedsToTurnOn = new List<int>(6); // light the whole column
                                for (int j = 0; j < 6; j++)
                                {
                                    listLedsToTurnOn.Add(KeyUtils.PointToKey(new Point(i, j)));
                                }

                                if (i < boostLeds)
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
                      //  }
                        return new LEDFrame(this, data, LightZone.Desk);
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Debug.WriteLine("Couldnt save image");
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
            double centerX = 35;
            double centerY = 32;
            double radius = 30;
            double step = 0.01f;

            double[] luminosities = new double[20]; // 5 increments

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

                int index = (int)Math.Floor(fractionOfCircumference * 20);

                double brightness = b.GetPixel(realX, realY).R / 255.0;
                luminosities[index] += brightness;

            }
            return luminosities;
            /*double minLuminosity = 10000000;
            for (int i = 0; i < luminosities.Length; i++)
            {
                if (luminosities[i] < minLuminosity)
                    minLuminosity = luminosities[i];
            }

            for (double i = start; i < end; i += step)
            {
                double calcX = Math.Cos(i);
                double calcY = Math.Sin(i);

                int realX = (int)(calcX * radius + centerX);
                int realY = (int)(calcY * radius + centerY);

                double fractionOfCircumference = Math.Max(0, Scale(i, start, end, 0, 1));
                int index = (int)Math.Floor(fractionOfCircumference * 20);
                int col = luminosities[index] > minLuminosity ? 255 : 0;
                b.SetPixel(realX, realY, Color.FromArgb(col, 255, 0));

            }
            return b;*/
        }

        /// <summary>
        /// Task that periodically updates the health bar.
        /// </summary>
        private async Task FrameTimer()
        {
            while (true)
            {
                if (masterCancelToken.IsCancellationRequested)
                    return;
                if (msSinceLastExternalFrameReceived >= msAnimationTimerThreshold)
                {
                    LEDFrame frame = QueryBoostInfo();
                    if (frame != null)
                        InvokeNewFrameReady(frame);
                }
                await Task.Delay(30);
                msSinceLastExternalFrameReceived += 30;
            }
        }

        /* /// <summary>
         /// Sets a custom champion kill animation. If the player kills a champion within <paramref name="duration"/> ms,
         /// this animation will play instead of the default one. Useful for garen ult or chogath, for example.
         /// </summary>
         /// <param name="animPath">The animation path</param>
         public void SetCustomKillAnim(string animPath, int duration)
         {
             customKillAnimation = animPath;
             Task.Run(async () =>
             {
                 await Task.Delay(duration);
                 customKillAnimation = null;
             });
         }*/

        /// <summary>
        /// Called by a <see cref="LEDModule"/> when a new frame is available to be processed.
        /// </summary>
        /// <param name="s">Module that sent the message</param>
        /// <param name="data">LED data</param>
        protected override void NewFrameReadyHandler(LEDFrame frame)
        {
            // TODO: Make sure frame Sender is ChampionModule and NOT animationModule
            /* if ((frame.LastSender is ChampionModule && CurrentLEDSource is ItemModule item && !item.IsPriorityItem)) // Champion modules take priority over item casts... for the moment
             {
                 CurrentLEDSource = (LEDModule)frame.LastSender;
             }
             if (frame.LastSender != Animator && frame.LastSender != CurrentLEDSource)
                 return; // If it's from a different source that what we're listening to, ignore it*/
            InvokeNewFrameReady(frame);
            msSinceLastExternalFrameReceived = 0;
        }

        /// <summary>
        /// Processes game events such as kills
        /// </summary>
        /*private void ProcessGameEvents(bool firstTime = false)
        {
            if (firstTime)
            {
                if (GameState.GameEvents.Count > 0)
                    currentGameTimestamp = GameState.GameEvents.Last().EventTime;

                return;
            }
            foreach (Event ev in GameState.GameEvents)
            {
                if (ev.EventTime <= currentGameTimestamp)
                    continue;
                currentGameTimestamp = ev.EventTime;
                switch (ev.EventName)
                {
                    case "ChampionKill":
                        OnChampionKill(ev);
                        break;
                    default:
                        break;
                }
            }
        }*/

        /// <summary>
        /// Checks if a goal has been scored
        /// </summary>
        /// <returns></returns>
        private bool CheckForGoal()
        {
            // TODO
            throw new NotImplementedException();
        }

        private void OnGoal(bool whatTeam)
        {
            // TODO
        }

        public override void Dispose()
        {
            masterCancelToken.Cancel();
            Animator.StopCurrentAnimation();
            //championModule?.Dispose();
        }
    }
}
