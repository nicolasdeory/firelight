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

        // Constants

        public static HSVColor LoadingColor { get; } = new HSVColor(0.09f, 0.8f, 1f);

        public static HSVColor DeadColor { get; } = new HSVColor(0f, 0.8f, 0.77f);
        public static HSVColor NoManaColor { get; } = new HSVColor(0.52f, 0.66f, 1f);

        

        // Variables

        ulong msSinceLastExternalFrameReceived = 30000;
        ulong msAnimationTimerThreshold = 1500; // how long to wait for animation data until boost bar kicks back in.

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        BoostModule boostModule = new BoostModule();

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
                            if (!CheckForGoal(screenCaptureFrame))
                            {
                                LEDFrame frame = boostModule.DoFrame(screenCaptureFrame);
                                if (frame != null)
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
             if (frame.LastSender != Animator && frame.LastSender != CurrentLEDSource)
                 return; // If it's from a different source that what we're listening to, ignore it*/
            InvokeNewFrameReady(frame);
            msSinceLastExternalFrameReceived = 0;
        }

        /// <summary>
        /// Checks if a goal has been scored
        /// </summary>
        /// <returns></returns>
        private bool CheckForGoal(Bitmap screenFrame)
        {
            // TODO
            return false;
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
