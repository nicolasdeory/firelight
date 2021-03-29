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
        const int OFFSET = 0; // must be >=0
        const bool REVERSE_ORDER = true;
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
                for (int i = 0; i < leds.Length; i++)
                {
                    int idx = data.Length - 1 - i * 3;
                    byte[] col = leds[leds.Length - 1 - i].color.ToRGB();
                    if (data.Length - 3 - i*3 - OFFSET*3 < 0)
                        break;
                    data[idx - OFFSET * 3] = col[2];
                    data[idx - OFFSET * 3 - 1] = col[1];
                    data[idx - OFFSET * 3 - 2] = col[0];
                }
            }
            else
            {
                for (int i = 0; i < leds.Length; i++)
                {
                    int idx = i * 3;
                    if (i + OFFSET * 3 > data.Length - 3)
                        break;
                    byte[] col = leds[i].color.ToRGB();
                    data[idx + OFFSET*3] = col[0];
                    data[idx + OFFSET * 3 + 1] = col[1];
                    data[idx + OFFSET * 3 + 2] = col[2];
                }
            }

            return data;
        }

        public void Dispose() { }
        public bool Enabled { get; set; } = true;

        public bool Errored => false;

        public string ErrorCode => null;
    }
}
