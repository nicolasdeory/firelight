using ChromaSDK;
using NzxtRGB;
using NzxtRGB.LedDevices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChromaSDK.Keyboard;
using static ChromaSDK.Mouse;

namespace FirelightCore
{
    /// <summary>
    /// Controller that adds support for Smart Device V2
    /// </summary>
    public class NZXTController : LightController
    {

        public bool Errored { get; private set; }
        public string ErrorCode { get; private set; }


        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value) return;
                if (!value)
                {
                    Dispose();
                }
                else
                {
                    Init();
                }
                enabled = value;
            }
        }

        private bool initialized = false;

        bool enabled = true;
        private bool disposed;

        private SmartDeviceV2 device;

        public static NZXTController Create()
        {
            return new NZXTController();
        }

        private NZXTController() // TODO: Dispose afterr a while if no data is received
        {
            Init();
        }

        private void Init()
        {
            Debug.WriteLine("Initializing NZXTController");

            NzxtDeviceConfig config = new NzxtDeviceConfig();
            config.RestartNzxtCamOnFail = true;
            config.RestartNzxtOnClose = true;

            Task.Run(async () =>
            {
                device = await SmartDeviceV2.OpenDeviceAsync(config);
                if (device == null)
                    Debug.WriteLine("[NZXTController] No devices detected");
                initialized = true;
            });
            disposed = false;


        }

        public void SendData(LEDFrame frame)
        {
            if (!enabled || disposed) return;
            LEDData data = frame.Leds;

            // GENERAL LEDS
            if (!frame.Zones.HasFlag(LightZone.General))
                return;

            SendSmartDeviceData(data);

        }

        private void SendSmartDeviceData(LEDData data)
        {
            if (!(device is SmartDeviceV2))
                return;

            for (int i = 1; i <= 2; i++)
            {
                NzxtLedDevice ledDevice = null;
                if (i == 1)
                    ledDevice = device.Channel1;
                else if (i == 2)
                    ledDevice = device.Channel2;

                List<byte> colors = new List<byte>();
                if (ledDevice is Aer2)
                {
                    // We should decide what general led are we going to use... I'm going to settle on CL2 and CL3 for the fans
                    for (int j = 0; j < 8; j++)
                    {
                        colors.AddRange(data.General[1].color.ToRGB());
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        colors.AddRange(data.General[2].color.ToRGB());
                    }
                }
                else if (ledDevice is StripV2)
                {
                    // For now we're going to settle for monocolor on the strip (CL1)
                    for (int j = 0; j < ledDevice.LedCount; j++)
                    {
                        colors.AddRange(data.General[0].color.ToRGB());
                    }
                }

                device.SendRGB((byte)i, colors.ToArray());
            }

        }


        public void Dispose()
        {
            if (Errored)
                return;
            disposed = true;
            device?.Dispose(); // TODO: Do this only after a timeout, we don't wanna dispose of this too often to avoid restarting the program too often
        }

    }
}
