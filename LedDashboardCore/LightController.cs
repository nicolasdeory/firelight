using System;

namespace FirelightCore
{
    public interface LightController : IDisposable
    {
        public bool Enabled { get; set; }

        //public void SendData(int ledCount, byte[] data, LightingMode mode);
        public void SendData(LEDFrame data);
    }
}
