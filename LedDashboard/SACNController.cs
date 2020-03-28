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
        public void SendData(int ledCount, byte[] data)
        {
            if (sender == null) sender = new SACNSender(Guid.NewGuid(), "wled-nico");
            Task.Run(() => sender.Send(1, data));
        }

        public void Dispose() { }
    }
}
