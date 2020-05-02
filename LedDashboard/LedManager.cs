using Games.LeagueOfLegends;
using LedDashboardCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LedDashboard
{

    class LedManager
    {

        public delegate void UpdateDisplayHandler(LEDFrame frame);

        /// <summary>
        /// Raised when the LED display is updated.
        /// </summary>
        public event UpdateDisplayHandler DisplayUpdated;

        bool reverseOrder;
        List<LightController> lightControllers = new List<LightController>();

        private Dictionary<string, Dictionary<string, string>> ModuleOptions = new Dictionary<string, Dictionary<string, string>>();

        Queue<LEDFrame> FrameQueue = new Queue<LEDFrame>();

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
            InitLeds();

            KeyboardHookService.Init();

            ProcessListenerService.ProcessInFocusChanged += OnProcessChanged;
            ProcessListenerService.Start();
            ProcessListenerService.Register("League of Legends"); // Listen when league of legends is opened

            UpdateLEDDisplay(LEDFrame.CreateEmpty(this));
            Task.Run(UpdateLoop);

        }

        /// <param name="ledCount">Number of lights in the LED strip</param>
        /// <param name="reverseOrder">Set to true if you want the lights to be reverse in order (i.e. Color for LED 0 will be applied to the last LED in the strip)</param>
        private void InitLeds(bool reverseOrder = false)
        {
            lightControllers.Add(RazerChromaController.Create());
            lightControllers.Add(SACNController.Create(reverseOrder));
        }

        private void OnProcessChanged(string name)
        {
            if (name == "League of Legends" && !(CurrentLEDModule is LeagueOfLegendsModule)) // TODO: Account for client disconnections
            {
                LEDModule lolModule = LeagueOfLegendsModule.Create(ModuleOptions.ContainsKey("lol") ? ModuleOptions["lol"] : new Dictionary<string, string>());
                lolModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = lolModule;
            }
            else if (name.Length == 0)
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
                lightControllers.ForEach(c => c.Enabled = enable);
            }
        }

        //bool updatingDisplay = false;
        /// <summary>
        /// Updates the LED display
        /// </summary>
        /// <param name="s">Module that sent the update command</param>
        /// <param name="ls">Array containing values for each LED in the strip</param>
        public void UpdateLEDDisplay(LEDFrame frame)
        {
            CheckFrame(frame);
            if (frame.Priority) FrameQueue.Clear();
            FrameQueue.Enqueue(frame);
            Debug.WriteLine("Frame received. " + frame.LastSender.GetType().Name + " Queue=" + FrameQueue.Count);
        }

        private void CheckFrame(LEDFrame frame)
        {
            LEDData data = frame.Leds;
            if (data.Keyboard.Length != LEDData.NUMLEDS_KEYBOARD)
                throw new ArgumentException("Keyboard frame does not match expected length");
            if (data.Strip.Length != LEDData.NUMLEDS_STRIP)
                throw new ArgumentException("Strip frame does not match expected length");
            if (data.Mouse.Length != LEDData.NUMLEDS_MOUSE)
                throw new ArgumentException("Mouse frame does not match expected length");
            if (data.Mousepad.Length != LEDData.NUMLEDS_MOUSEPAD)
                throw new ArgumentException("Mousepad frame does not match expected length");
            if (data.Headset.Length != LEDData.NUMLEDS_HEADSET)
                throw new ArgumentException("Headset frame does not match expected length");
            if (data.Keypad.Length != LEDData.NUMLEDS_KEYPAD)
                throw new ArgumentException("Keypad frame does not match expected length");
            if (data.General.Length != LEDData.NUMLEDS_GENERAL)
                throw new ArgumentException("General frame does not match expected length");
        }

        private async Task UpdateLoop()
        {
            while (true)
            {
                if (FrameQueue.Count > 0)
                {
                    LEDFrame next = FrameQueue.Dequeue();
                    //this.leds = next.Leds;
                    SendLedData(next);
                }
                await Task.Delay(30); // 33 fps
            }
        }

        private void SendLedData(LEDFrame frame)
        {
            lightControllers.ForEach(controller => controller.SendData(frame));
            DisplayUpdated?.Invoke(frame);
        }


        /*/// <summary>
        /// Sets the light controller to be used
        /// </summary>
        public void SetController(LightControllerType type, int ledCount = 0, bool reverseOrder = false)
        {
            ((IDisposable)lightController).Dispose();
            if (type == LightControllerType.LED_Strip)
            {
                lightController = SACNController.Create();
                this.preferredMode = LightingMode.Line;
            }
            else if (type == LightControllerType.RazerChroma)
            {
                lightController = RazerChromaController.Create();
                this.preferredMode = LightingMode.Keyboard;
            }
            RestartManager(this.preferredMode, ledCount, reverseOrder);
        }*/

        private void RestartManager(bool reverseOrder) // TODO: Keep game state (i.e. league of legends cooldowns etc)
        {
            InitLeds(reverseOrder);
            CurrentLEDModule = null; // restart the whole service (force module reload)
            ProcessListenerService.Restart();
        }

        private void RestartManager()
        {
            InitLeds(this.reverseOrder);
            CurrentLEDModule = null;
            ProcessListenerService.Restart();
        }

        /*/// <summary>
        /// Sends LED data to a wireless LED strip using the E1.31 sACN protocol.
        /// </summary>
        public void SendData(LightingMode mode)
        {
            lightController.SendData(this.leds.Strip.Length, this.leds.Strip.ToByteArray(this.reverseOrder), mode);
        }*/

        public void SetModuleOption(string moduleId, string option, string value)
        {
            if (!ModuleOptions.ContainsKey(moduleId))
            {
                ModuleOptions.Add(moduleId, new Dictionary<string, string>());
            }
            if (ModuleOptions[moduleId].ContainsKey(option))
            {
                ModuleOptions[moduleId][option] = value;
            }
            else
            {
                ModuleOptions[moduleId].Add(option, value);
            }
            RestartManager();

        }

    }
}
