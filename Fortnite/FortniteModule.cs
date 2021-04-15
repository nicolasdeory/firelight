using DesktopDuplication;
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

namespace Games.Fortnite
{
    // TODO: Refactor this into a separate project
    public class FortniteModule : BaseGameModule
    {
        public const string GAME_ID = "Fortnite";
        // Constants

        public static HSVColor LoadingColor { get; } = new HSVColor(0.09f, 0.8f, 1f);

        public static HSVColor DeadColor { get; } = new HSVColor(0f, 0.8f, 0.77f);
        public static HSVColor NoManaColor { get; } = new HSVColor(0.52f, 0.66f, 1f);



        // Variables

        ulong msSinceLastExternalFrameReceived = 30000;
        ulong msAnimationTimerThreshold = 1500; // how long to wait for animation data until boost bar kicks back in.

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        HealthModule healthModule = new HealthModule();
        DanceModule danceModule = new DanceModule();

        /// <summary>
        /// Creates a new <see cref="RocketLeague"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        public static FortniteModule Create()
        {
            return new FortniteModule();
        }

        private FortniteModule()
            : base(GAME_ID)
        {
            // Fortnite module initialization

            AddAnimatorEvent();

            danceModule.NewFrameReady += NewFrameReadyHandler;

            //PlayLoadingAnimation();
            _ = FrameTimer().CatchExceptions();
        }

        // plays it indefinitely
        //private void PlayLoadingAnimation()
        //{
        //    Animator.StopCurrentAnimation();
        //    Animator.HoldColor(NoManaColor, LightZone.All, 1);
        //}

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
                            LEDFrame frame = healthModule.DoFrame(screenCaptureFrame); // TODO: Idle animation after goal or when not ingame
                            if (frame != null)
                                InvokeNewFrameReady(frame);
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


        public override void Dispose()
        {
            masterCancelToken.Cancel();
            Animator.StopCurrentAnimation();
            //championModule?.Dispose();
        }
    }
}
