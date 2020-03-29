using kadmium_sacn_core;
using System;
using System.Collections.Generic;
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
            Task.Run(() => sender.Send(1, data));
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
            if (mode == LightingMode.Keyboard)
            {
                throw new NotImplementedException();
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
