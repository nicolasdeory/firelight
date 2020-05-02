using System;

namespace LedDashboardCore
{
    public interface LightController : IDisposable
    {
        public bool Enabled { get; set; }

        //public void SendData(int ledCount, byte[] data, LightingMode mode);
        public void SendData(LEDData data);
    }
}
