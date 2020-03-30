using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboard.Modules.BlinkWhite
{
    class BlinkWhiteModule : LEDModule
    {
        public event LEDModule.FrameReadyHandler NewFrameReady;

        Led[] leds;

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();

        public static LEDModule Create(int ledCount)
        {

            return new BlinkWhiteModule(ledCount);

        }

        private BlinkWhiteModule(int ledCount)
        {
            this.leds = new Led[ledCount];
            for (int i = 0; i < ledCount; i++)
                leds[i] = new Led();

            Task.Run(() => Blinker(500));
        }
        public async Task Blinker(int intervalMS)
        {
            bool on = false;
            while (true)
            {
                if (masterCancelToken.IsCancellationRequested) return;
                if (on)
                {
                    foreach (Led l in this.leds)
                    {
                        l.Color(HSVColor.Black);
                    }
                    on = false;
                } else
                {
                    foreach(Led l in this.leds) {
                        l.Color(new HSVColor(0.2f, 1f, 1f));
                    }
                    on = true;
                }
                NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
                await Task.Delay(intervalMS);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
