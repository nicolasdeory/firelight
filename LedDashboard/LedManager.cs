
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
        private int ledCount;

        public delegate void UpdateDisplayHandler(Led[] leds);

        /// <summary>
        /// Raised when the LED display is updated.
        /// </summary>
        public event UpdateDisplayHandler UpdateDisplay;

        bool reverseOrder;
        LightController lightController;

        LEDModule CurrentLEDModule
        {
            get
            {
                return _currentLEDModule;
            }
            set
            {
                if (value == null)
                {
                    SetEnabled(false);
                } else
                {
                    SetEnabled(true);
                }
                if (_currentLEDModule != null) _currentLEDModule.Dispose();
                _currentLEDModule = value;
            }
        }
        LEDModule _currentLEDModule;

        bool enabled = true;

        /// <param name="ledCount">Number of lights in the LED strip</param>
        /// <param name="reverseOrder">Set to true if you want the lights to be reverse in order (i.e. Color for LED 0 will be applied to the last LED in the strip)</param>
        public LedManager(int ledCount, bool reverseOrder)
        {
            this.ledCount = ledCount;
            lightController = RazerChromaController.Create();

            this.leds = new Led[ledCount];
            for (int i = 0; i < this.leds.Length; i++)
            {
                this.leds[i] = new Led();
            }
            this.reverseOrder = reverseOrder;

            /*LEDModule lolModule = LeagueOfLegendsModule.Create(ledCount);
            lolModule.NewFrameReady += UpdateLEDDisplay;*/
            /*LEDModule blinkModule = BlinkWhiteModule.Create(leds.Length);
            blinkModule.NewFrameReady += UpdateLEDDisplay;*/
            KeyboardHookService.Init();

            ProcessListenerService.ProcessInFocusChanged += OnProcessChanged;
            ProcessListenerService.Start();
            ProcessListenerService.Register("League of Legends"); // Listen when league of legends is opened

            UpdateLEDDisplay(this, this.leds);

        }

        private void OnProcessChanged(string name)
        {
            if (name == "League of Legends" && !(CurrentLEDModule is LeagueOfLegendsModule)) // TODO: Account for client disconnections
            {
                LEDModule lolModule = LeagueOfLegendsModule.Create(LightingMode.Line, ledCount);
                lolModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = lolModule;
            } else if (name == "")
            {
                CurrentLEDModule = null;
                return;
            }
        }

        private void SetEnabled(bool enable) // If set to false, deattach from razer chroma
        {
            if (this.enabled != enable)
            {
                this.enabled = enable;
                if(enable)
                {
                    lightController.SetEnabled(true);
                } else
                {
                    lightController.SetEnabled(false);
                }
            }
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
            bool wasEnabled = lightController.IsEnabled();
            if (type == LightControllerType.LED_Strip)
            {
                lightController = SACNController.Create();
            } else if (type == LightControllerType.RazerChroma)
            {
                lightController = RazerChromaController.Create();
            }
            lightController.SetEnabled(wasEnabled);
        }

        /// <summary>
        /// Sends LED data to a wireless LED strip using the E1.31 sACN protocol.
        /// </summary>
        public async void SendData()
        {
            //await SACNController.Send(this.ToByteArray(reverseOrder));
            lightController.SendData(this.leds.Length, this.ToByteArray(), LightingMode.Line);
        }

    }
}
