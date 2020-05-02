using kadmium_sacn_core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace LedDashboardCore
{
    public class SACNController : LightController, IDisposable
    {
        public static SACNController Create(bool reverseOrder)
        {
            return new SACNController(reverseOrder);
        }

        SACNSender sender;
        bool reverseOrder;

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

      /*  public void SendData(int ledCount, byte[] data, LightingMode mode)
        {
            Task.Run(() => sender.Send(1, SanitizeDataArray(ledCount, data, mode)));
        }*/

        public void SendData(LEDData data)
        {
            //sender.Send(1, SanitizeDataArray(ledCount, data, mode))
            sender.Send(1, data.Strip.ToByteArray(this.reverseOrder));
        }

        /*private byte[] SanitizeDataArray(int ledCount, byte[] data, LightingMode mode)
        {
            switch (mode)
            {
                case LightingMode.Line:
                    return data;
                case LightingMode.Point:
                    var bytes = new List<byte>();
                    for (int i = 0; i < ledCount; i++)
                    {
                        bytes.Add(data[0]);
                        bytes.Add(data[1]);
                        bytes.Add(data[2]);
                    }
                    return bytes.ToArray();
                case LightingMode.Keyboard:
                    return GetKeyboardToLedStripArray(data).ToByteArray();
                default:
                    Console.Error.WriteLine("SACN: Invalid lighting mode");
                    throw new ArgumentException("Invalid lighting mode");
            }
        }*/

        /*public static Led[] GetKeyboardToLedStripArray(byte[] data)
        {
            int ledStripLength = 170; // TODO: hardcoded number, standard led strip length?
            Led[] ls = new Led[ledStripLength];
            for (int i = 0; i < ledStripLength; i++)
            {
                ls[i] = new Led();
            }
            List<Point> pts;
            for (int i = 0; i < ledStripLength; i++)
            {

                int x = GetNearestXForLed(i);
                for (int j = 0; j < 6; j++)
                {
                    int key = KeyUtils.PointToKey(new Point(x, j));
                    if (key == -1) continue;
                    byte[] col = new byte[3] { data[key * 3], data[key * 3 + 1], data[key * 3 + 2] };
                    ls[i].MixNewColor(HSVColor.FromRGB(col));
                }
            }
            return ls;
        }

        private static int GetNearestXForLed(int led)
        {
            return (int)Utils.Scale(led, 0, 170, 0, 19);
        }*/

        public void Dispose() { }
        public bool Enabled { get; set; }
    }
}
