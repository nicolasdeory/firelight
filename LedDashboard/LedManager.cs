
using LedDashboard.Modules.BlinkWhite;
using LedDashboard.Modules.LeagueOfLegends;
using System;

namespace LedDashboard
{

    class LedManager
    {
        /// <summary>
        /// Array that contains LED color data for the LED strip.
        /// </summary>
        private Led[] leds;

        public delegate void UpdateDisplayHandler(Led[] leds);

        /// <summary>
        /// Raised when the LED display is updated.
        /// </summary>
        public event UpdateDisplayHandler UpdateDisplay;

        bool reverseOrder;

        LightController lightController;


        /// <param name="ledCount">Number of lights in the LED strip</param>
        /// <param name="reverseOrder">Set to true if you want the lights to be reverse in order (i.e. Color for LED 0 will be applied to the last LED in the strip)</param>
        public LedManager(int ledCount, bool reverseOrder)
        {
            lightController = RazerChromaController.Create();

            this.leds = new Led[ledCount];
            for (int i = 0; i < this.leds.Length; i++)
            {
                this.leds[i] = new Led();
            }
            this.reverseOrder = reverseOrder;

            LEDModule lolModule = LeagueOfLegendsModule.Create(ledCount);
            lolModule.NewFrameReady += UpdateLEDDisplay;
            /*LEDModule blinkModule = BlinkWhiteModule.Create(leds.Length);
            blinkModule.NewFrameReady += UpdateLEDDisplay;*/
            UpdateLEDDisplay(this, this.leds);

        }

        /// <summary>
        /// Updates the LED display
        /// </summary>
        /// <param name="s">Module that sent the update command</param>
        /// <param name="ls">Array containing values for each LED in the strip</param>
        public void UpdateLEDDisplay(object s, Led[] ls)
        {
            this.leds = ls;
            UpdateDisplay?.Invoke(this.leds);
            SendData();
        }

        /// <summary>
        /// Returns the LED array as a byte array contaning Red, Green and Blue value bytes for each LED in the strip.
        /// </summary>
        /// <param name="reverseOrder"></param>
        /// <returns>A byte array of length ledCount * 3</returns>
        public byte[] ToByteArray(bool reverseOrder = false)
        {
            byte[] data = new byte[leds.Length * 3];
            if (reverseOrder)
            {
                for (int i = leds.Length * 3 - 1; i >= 0; i -= 3)
                {
                    int index = (leds.Length * 3 - 1) - i;
                    byte[] col = leds[index / 3].color.ToRGB();
                    data[i] = col[2];
                    data[i - 1] = col[1];
                    data[i - 2] = col[0];
                }
            }
            else
            {
                for (int i = 0; i < leds.Length * 3; i += 3)
                {
                    byte[] col = leds[i / 3].color.ToRGB();
                    data[i] = col[0];
                    data[i + 1] = col[1];
                    data[i + 2] = col[2];
                }
            }

            return data;
        }
        
        /// <summary>
        /// Sets the light controller to be used
        /// </summary>
        public void SetController(LightControllerType type)
        {
            ((IDisposable)lightController).Dispose();
            if (type == LightControllerType.LED_Strip)
            {
                lightController = SACNController.Create();
            } else if (type == LightControllerType.RazerChroma)
            {
                lightController = RazerChromaController.Create();
            }
        }

        /// <summary>
        /// Sends LED data to a wireless LED strip using the E1.31 sACN protocol.
        /// </summary>
        public async void SendData()
        {
            //await SACNController.Send(this.ToByteArray(reverseOrder));
            lightController.SendData(this.leds.Length, this.ToByteArray());
        }

    }
}
