using Games.LeagueOfLegends;
using LedDashboardCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LedDashboard
{

    class LedManager
    {
        /// <summary>
        /// Array that contains LED color data for the LED strip.
        /// </summary>
        private Led[] leds;
        private int ledCount;

        public delegate void UpdateDisplayHandler(Led[] leds, LightingMode mode);

        /// <summary>
        /// Raised when the LED display is updated.
        /// </summary>
        public event UpdateDisplayHandler DisplayUpdated;

        bool reverseOrder;
        LightController lightController;

        private Dictionary<string, Dictionary<string, string>> ModuleOptions = new Dictionary<string, Dictionary<string, string>>();

        LEDModule CurrentLEDModule
        {
            get
            {
                return _currentLEDModule;
            }
            set
            {
                SetEnabled(value != null);
                _currentLEDModule?.Dispose();
                _currentLEDModule = value;
            }
        }
        LEDModule _currentLEDModule;

        bool enabled = true;

        LightingMode preferredMode;

        
        /// <summary>
        /// Starts the LED Manager in keyboard mode by default. Use <seealso cref="SetController"/> to further customize settings, especially for LED strips
        /// </summary>
        public LedManager() // by default starts in keyboard mode
        {
            
            lightController = RazerChromaController.Create();

            InitLeds(LightingMode.Keyboard);

            KeyboardHookService.Init();

            ProcessListenerService.ProcessInFocusChanged += OnProcessChanged;
            ProcessListenerService.Start();
            ProcessListenerService.Register("League of Legends"); // Listen when league of legends is opened

            UpdateLEDDisplay(this, this.leds, preferredMode);

        }

        /// <param name="ledCount">Number of lights in the LED strip</param>
        /// <param name="reverseOrder">Set to true if you want the lights to be reverse in order (i.e. Color for LED 0 will be applied to the last LED in the strip)</param>
        private void InitLeds(LightingMode preferredMode, int ledCount = 0, bool reverseOrder = false)
        {
            this.preferredMode = preferredMode;
            this.ledCount = preferredMode == LightingMode.Keyboard ? 88 : ledCount;
            this.leds = new Led[this.ledCount];
            for (int i = 0; i < this.leds.Length; i++)
            {
                this.leds[i] = new Led();
            }
            this.reverseOrder = reverseOrder;
        }

        private void OnProcessChanged(string name)
        {
            if (name == "League of Legends" && !(CurrentLEDModule is LeagueOfLegendsModule)) // TODO: Account for client disconnections
            {
                LEDModule lolModule = LeagueOfLegendsModule.Create(preferredMode, ledCount, ModuleOptions.ContainsKey("lol") ? ModuleOptions["lol"] : new Dictionary<string, string>());
                lolModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = lolModule;
            } else if (name.Length == 0)
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
                lightController.Enabled = enable;
            }
        }

        bool updatingDisplay = false;
        /// <summary>
        /// Updates the LED display
        /// </summary>
        /// <param name="s">Module that sent the update command</param>
        /// <param name="ls">Array containing values for each LED in the strip</param>
        public void UpdateLEDDisplay(object s, Led[] ls, LightingMode mode)
        {
            if (updatingDisplay)
            {
                Debug.WriteLine("SEVERE: UpdateLEDDisplay was called at the same time as an ongoing one");
                return;
            }
            updatingDisplay = true;
            this.leds = ls;
            if (mode == LightingMode.Keyboard && ls.Length != 88)
            {
                Debug.WriteLine("SEVERE: Tried to send a keyboard frame but LED array had incorrect length");
            }
            DisplayUpdated?.Invoke(this.leds, mode);
            SendData(mode);
            updatingDisplay = false;
        }

        
        
        /// <summary>
        /// Sets the light controller to be used
        /// </summary>
        public void SetController(LightControllerType type, int ledCount = 0, bool reverseOrder = false)
        {
            ((IDisposable)lightController).Dispose();
            if (type == LightControllerType.LED_Strip)
            {
                lightController = SACNController.Create();
                this.preferredMode = LightingMode.Line;
            } else if (type == LightControllerType.RazerChroma)
            {
                lightController = RazerChromaController.Create();
                this.preferredMode = LightingMode.Keyboard;
            }
            RestartManager(this.preferredMode, ledCount, reverseOrder);
        }

        private void RestartManager(LightingMode preferredMode, int ledCount, bool reverseOrder) // TODO: Keep game state (i.e. league of legends cooldowns etc)
        {
            InitLeds(this.preferredMode, ledCount, reverseOrder);
            CurrentLEDModule = null; // restart the whole service (force module reload)
            ProcessListenerService.Restart();
        }

        private void RestartManager()
        {
            InitLeds(this.preferredMode, this.ledCount, this.reverseOrder);
            CurrentLEDModule = null;
            ProcessListenerService.Restart();
        }

        /// <summary>
        /// Sends LED data to a wireless LED strip using the E1.31 sACN protocol.
        /// </summary>
        public void SendData(LightingMode mode)
        {
            lightController.SendData(this.leds.Length, this.leds.ToByteArray(this.reverseOrder), mode);
        }

        public void SetModuleOption(string moduleId, string option, string value)
        {
            if (!ModuleOptions.ContainsKey(moduleId))
            {
                ModuleOptions.Add(moduleId, new Dictionary<string, string>());
            }
            if(ModuleOptions[moduleId].ContainsKey(option))
            {
                ModuleOptions[moduleId][option] = value;
            } else
            {
                ModuleOptions[moduleId].Add(option, value);
            }
            RestartManager();
            
        }

    }
}
