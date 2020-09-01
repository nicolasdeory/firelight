using kadmium_sacn_core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace FirelightCore
{
    public class SACNController : LightController, IDisposable
    {
        public static SACNController Create(bool reverseOrder)
        {
            return new SACNController(reverseOrder);
        }

        SACNSender sender;
        bool reverseOrder;


        const int TOTAL_STRIP_LEDS = 300;
        const int OFFSET = 100; // must be >=0
        const bool REVERSE_ORDER = false;
        const bool MIRROR_MODE = false; // TODO

        private SACNController(bool reverseOrder)
        {
            this.reverseOrder = reverseOrder;
            // If the method throws no exceptions, consider directly initializing the sender variable
            try
            {
                sender = new SACNSender(Guid.NewGuid(), "wled-nico");
            }
            catch
            {
                Console.Error.WriteLine("Cound not initialize a SACN sender.");
            }
        }

        public void SendData(LEDFrame frame)
        {
            if (!frame.Zones.HasFlag(LightZone.Strip))
                return;
            LEDData data = frame.Leds;
            //sender.Send(1, SanitizeDataArray(ledCount, data, mode))
            sender.Send(1, GetByteArray(data.Strip));
        }

        byte[] GetByteArray(Led[] leds)
        {
            byte[] data = new byte[TOTAL_STRIP_LEDS * 3];
            if (REVERSE_ORDER)
            {
                for (int i = leds.Length * 3 - 1; i >= 0; i -= 3)
                {
                    if (i - OFFSET*3 - 2 < 0)
                        break;
                    int index = (leds.Length * 3 - 1) - i ;
                    byte[] col = leds[index / 3].color.ToRGB();
                    data[i - OFFSET * 3] = col[2];
                    data[i - OFFSET * 3 - 1] = col[1];
                    data[i - OFFSET * 3 - 2] = col[0];
                }
            }
            else
            {
                for (int i = 0; i < leds.Length * 3; i += 3)
                {
                    if (i + OFFSET * 3 > data.Length - 3)
                        break;
                    byte[] col = leds[i / 3].color.ToRGB();
                    data[i + OFFSET*3] = col[0];
                    data[i + OFFSET * 3 + 1] = col[1];
                    data[i + OFFSET * 3 + 2] = col[2];
                }
            }

            return data;
        }

        public void Dispose() { }
        public bool Enabled { get; set; }

        public bool Errored => false;

        public string ErrorCode => null;
    }
}
