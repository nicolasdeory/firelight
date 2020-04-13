using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboardCore
{
    public interface LightController : IDisposable
    {
        public bool Enabled { get; set; }

        public void SendData(int ledCount, byte[] data, LightingMode mode);
    }
}
