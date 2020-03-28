using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    public interface LightController
    {
        public void SendData(int ledCount, byte[] data);
    }
}
