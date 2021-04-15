using DesktopDuplication;
using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using FirelightCore;
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

        // Constants

        public static HSVColor LoadingColor { get; } = new HSVColor(0.09f, 0.8f, 1f);

        public static HSVColor DeadColor { get; } = new HSVColor(0f, 0.8f, 0.77f);
        public static HSVColor NoManaColor { get; } = new HSVColor(0.52f, 0.66f, 1f);



        // Variables

        ulong msSinceLastExternalFrameReceived = 30000;
        ulong msAnimationTimerThreshold = 1500; // how long to wait for animation data until boost bar kicks back in.

        ulong ingameTimerThreshold = 500; // how long we should have "INGAME" status until boost & goal start working. Intended to smooth out misdetections
        ulong msSinceLastNotIngameDetected = 20000; // how long we should have "INGAME" status until boost & goal start working. Intended to smooth out misdetections

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        BoostModule boostModule = new BoostModule();
        GoalModule goalModule = new GoalModule();

        /// <summary>
        /// Creates a new <see cref="RocketLeague"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        public static RocketLeagueModule Create()
        {
            return new RocketLeagueModule();
        }

        private RocketLeagueModule()
            : base()
        {
            // Rocket League module initialization

            AddAnimatorEvent();

            goalModule.NewFrameReady += NewFrameReadyHandler;

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
                    // Get the screen frame
                    using (Bitmap screenCaptureFrame = ScreenCaptureModule.GetNextFrame())
                    {
                        if (screenCaptureFrame != null)
                        {
                            //Debug.WriteLine(msSinceLastNotIngameDetected);
                            if (IsIngame(screenCaptureFrame))
                            {
                                msSinceLastNotIngameDetected += 30;
                                if (msSinceLastNotIngameDetected > ingameTimerThreshold)
                                {
                                    goalModule.DoFrame(screenCaptureFrame);
                                    if (!goalModule.IsPlayingAnimation)
                                    {
                                        LEDFrame frame = boostModule.DoFrame(screenCaptureFrame); // TODO: Idle animation after goal or when not ingame
                                        if (frame != null)
                                            InvokeNewFrameReady(frame);
                                    }
                                }
                            } else
                            {
                                msSinceLastNotIngameDetected = 0;
                                LEDFrame frame = GenerateIdleFrame();
                                InvokeNewFrameReady(frame);
                            }
                            
                        }
                    }


                }
                await Task.Delay(30);
                msSinceLastExternalFrameReceived += 30;
            }
        }

        /// <summary>
        /// Called by a <see cref="LEDModule"/> when a new frame is available to be processed.
        /// </summary>
        /// <param name="s">Module that sent the message</param>
        /// <param name="data">LED data</param>
        protected override void NewFrameReadyHandler(LEDFrame frame)
        {
            /*if (frame.LastSender != Animator && frame.LastSender != CurrentLEDSource)
                return; // If it's from a different source that what we're listening to, ignore it*/
            InvokeNewFrameReady(frame);
            msSinceLastExternalFrameReceived = 0;
        }


        private bool IsIngame(Bitmap frame)
        {
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {

                using (MemoryStream memStream = new MemoryStream())
                {
                    // Load, resize, set the format and quality and save an image.
                    imageFactory
                        .Load(frame)
                        .Filter(MatrixFilters.GreyScale)
                        .Save(memStream);

                    Bitmap grayscaleFrame = new Bitmap(memStream); // TODO: Refactor all grayscale images, pass to grayscale once.


                    // This will check if theres a black bar at the bottom, which means the player has paused.
                    double luminositySum = 0;

                    for (int i = 0; i < 1920; i++) // TODO: It's hardcoded for 1920x1080!!
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Color c = grayscaleFrame.GetPixel(i, 1080 - 1 - j);
                            luminositySum += c.R; // it's grayscale so it doesn't matter which channel
                        }
                    }
                    double averageLuminosity = luminositySum / (1920.0 * 5 * 255);

                    // This will check that the middle part of the score counter is kinda black
                    double luminosityScoreCounterSum = 0;
                    int half = 1920 / 2;
                    for (int i = half - 20; i < half + 20; i++)
                    {
                        for (int j = 2; j < 20; j++)
                        {
                            luminosityScoreCounterSum += grayscaleFrame.GetPixel(i, j).R;
                        }
                    }
                    double averageLuminosityScoreCounter = luminosityScoreCounterSum / (40 * 18);

                    //Debug.WriteLine(averageLuminosityScoreCounter);
                    Debug.WriteLine("scorecounter " + averageLuminosityScoreCounter + " blackbar " + averageLuminosity);
                    if (averageLuminosity < 0.16 || averageLuminosityScoreCounter > 45) // NOT SURE ABOUT DOING AND/OR CONDITION... each generates their own artifacts
                    {
                        //Debug.WriteLine(averageLuminosity);
                        //   Debug.WriteLine("You are not in a game. Did you pause?");
                        return false;
                    }
                    else
                    {
                        //Debug.Write
                        return true;
                    }
                   // return false;
                }
            }
        }

        private LEDFrame GenerateIdleFrame()
        {
            LEDData data = LEDData.Empty;
            data.SetAllToColor(HSVColor.White);
            return new LEDFrame(this, data, LightZone.Desk);
        }

        public override void Dispose()
        {
            masterCancelToken.Cancel();
            Animator.StopCurrentAnimation();
            //championModule?.Dispose();
        }
    }
}
