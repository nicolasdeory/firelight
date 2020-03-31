using kadmium_sacn_core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    class SACNController : LightController, IDisposable
    {
        public static SACNController Create()
        {
            return new SACNController();
        }

        static SACNSender sender;
        public void SendData(int ledCount, byte[] data, LightingMode mode) // todo: when on keyboard mode, ledCount still has to be correct for the strip!
        {
            if (sender == null) sender = new SACNSender(Guid.NewGuid(), "wled-nico");
            Task.Run(() => sender.Send(1, SanitizeDataArray(ledCount,data,mode)));
        }

        private byte[] SanitizeDataArray(int ledCount, byte[] data, LightingMode mode)
        {
            if (mode == LightingMode.Line) return data;
            if (mode == LightingMode.Point)
            {
                List<byte> bytes = new List<byte>();
                for(int i = 0; i < ledCount; i++)
                {
                    bytes.Add(data[0]);
                    bytes.Add(data[1]);
                    bytes.Add(data[2]);
                }
                return bytes.ToArray();
            }
            if (mode == LightingMode.Keyboard) // TODO: Keyboard lighting mode support for led strip
            {
                int ledStripLength = 170; // TODO: hardcoded number, standard led strip length?
                Led[] ls = new Led[ledStripLength]; 
                                         /* List<Point> pts = new List<Point>();
                                          List<byte> bytes = new List<byte>();*/
                for (int i = 0; i < ledStripLength; i++)
                {
                    ls[i] = new Led();
                }
                List<Point> pts;
                for (int i = 0; i < 88; i++)
                {
                    byte[] col = new byte[3] { data[i * 3], data[i * 3 + 1], data[i * 3 + 2] };
                    pts = KeyUtils.KeyToPoints(i);
                    foreach(Point pt in pts)
                    {
                        int xScaled = (int) Utils.Scale(pt.X, 0, 19, 0, ledStripLength);
                        ls.AddColorToLedsAround(xScaled, HSVColor.FromRGB(col),10);
                    }
                }
                return ls.ToByteArray();
            }
            else
            {
                throw new ArgumentException("Invalid lighting mode");
            }
        }

        public void Dispose() { }

        bool enabled; 
        public void SetEnabled(bool enabled) { this.enabled = enabled; } // Kind of irrelevant for this controller / protocol but well.

        public bool IsEnabled()
        {
            return enabled;
        }
    }
}
