using kadmium_sacn_core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using OpenRGB.NET.Models;
using OpenRGB.NET;
using OpenRGB.NET.Enums;
using System.Linq;
using System.Diagnostics;

namespace FirelightCore
{
    public class OpenRGBController : LightController, IDisposable
    {
        public static OpenRGBController Create()
        {
            return new OpenRGBController();
        }

        OpenRGBClient client;
        Device[] devices;
        bool isDisposed;
        bool errored;
        string errorCode;

        private OpenRGBController()
        {
            try
            {
                client = new OpenRGB.NET.OpenRGBClient(name: "Firelight", autoconnect: true, timeout: 1000);
            } catch (Exception ex)
            {
                errored = true;
                errorCode = "Error connecting to OpenRGB. Is it running?";
                Debug.WriteLine(ex.Message);
                return;
            }

            this.devices = client.GetAllControllerData();
        }


        public void SendData(LEDFrame frame)
        {
            if (!frame.Zones.HasFlag(LightZone.General))
            {
                return;
            }
            for(int i=0; i < devices.Length; i++)
            {
                var cols = devices[i].Colors;
                if (devices[i].Name.Contains("Razer"))
                    continue;
                foreach(var col in cols)
                {
                    byte[] c = frame.Leds.General[0].color.ToRGB();
                    col.R = c[0];
                    col.G = c[1];
                    col.B = c[2];
                }
                client.UpdateLeds(i, cols);
            }
        }

        public void Dispose() {
            isDisposed = true;
        }
        public bool Enabled { get; set; }

        public bool Errored => errored;

        public string ErrorCode => errorCode;
    }
}
