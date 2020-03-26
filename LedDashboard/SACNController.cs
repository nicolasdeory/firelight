using kadmium_sacn_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    class SACNController
    {
        static SACNSender sender;
        public static async Task Send(byte[] data)
        {
            if (sender == null) sender = new SACNSender(Guid.NewGuid(), "wled-nico");
            await sender.Send(1, data);
        }
    }
}
