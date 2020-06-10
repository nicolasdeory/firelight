using LedDashboardCore.Modules.BasicAnimation;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboardCore.Modules.BlinkWhite
{
    public class BlinkWhiteModule : LEDModule
    {
        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        AnimationModule animation = AnimationModule.Create();

        public event LEDModule.FrameReadyHandler NewFrameReady;

        public static LEDModule Create()
        {
            return new BlinkWhiteModule();
        }

        private BlinkWhiteModule()
        {
            animation.NewFrameReady += FrameReceived;
            Task.Run(() => Blinker(500)).CatchExceptions();
        }

        private void FrameReceived(LEDFrame frame)
        {
            NewFrameReady.Invoke(frame);
        }

        public async Task Blinker(int intervalMS)
        {
            bool on = false;
            while (true)
            {
                if (masterCancelToken.IsCancellationRequested)
                    return;
                if (on)
                {
                    animation.HoldColor(HSVColor.Black, LightZone.All, 0.5f, true);
                    on = false;
                }
                else
                {
                    animation.HoldColor(new HSVColor(0.2f, 1f, 1f), LightZone.All, 0.5f, true);
                    on = true;
                }
                await Task.Delay(intervalMS);
            }
        }

        public void Dispose()
        {
            masterCancelToken.Cancel();
        }
    }
}
