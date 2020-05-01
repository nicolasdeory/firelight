using LedDashboardCore.Modules.BasicAnimation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboardCore.Modules.BlinkWhite
{
    class BlinkWhiteModule : LEDModule
    {
        public event LEDModule.FrameReadyHandler NewFrameReady;

        Led[] leds;

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        LightZone allZones = LightZone.Keyboard | LightZone.Strip | LightZone.Mouse | LightZone.Mousepad | LightZone.Headset | LightZone.Keypad | LightZone.General;

        AnimationModule animation = AnimationModule.Create();

        public static LEDModule Create()
        {
            return new BlinkWhiteModule();
        }

        private BlinkWhiteModule()
        {
            Task.Run(() => Blinker(500));
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
                    animation.HoldColor(allZones, HSVColor.Black, 0.5f, true);
                    on = false;
                }
                else
                {
                    animation.HoldColor(allZones, new HSVColor(0.2f, 1f, 1f), 0.5f, true);
                    on = true;
                }
                await Task.Delay(intervalMS);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
