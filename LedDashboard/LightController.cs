using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    public interface LightController : IDisposable
    {
        public void SendData(int ledCount, byte[] data);
        public void SetEnabled(bool enabled);
        public bool IsEnabled();
    }
}
