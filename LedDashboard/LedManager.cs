using Games.LeagueOfLegends;
using Games.RocketLeague;
using LedDashboardCore;
using LedDashboardCore.Modules.BlinkWhite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboard
{

    class LedManager
    {
        /// <summary>
        /// Null until it's initialized
        /// </summary>
        public static LedManager Instance { get; private set; }

        public delegate void UpdateDisplayHandler(LEDFrame frame);

        /// <summary>
        /// Raised when the LED display is updated.
        /// </summary>
        public event UpdateDisplayHandler DisplayUpdated;

        bool reverseOrder;
        List<LightController> lightControllers = new List<LightController>();

        private Dictionary<string, Dictionary<string, string>> ModuleOptions = new Dictionary<string, Dictionary<string, string>>();

        Queue<LEDFrame> FrameQueue = new Queue<LEDFrame>();

        CancellationTokenSource updateLoopCancelToken = new CancellationTokenSource();

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

        public LEDFrame LastDisplayedFrame { get; private set; } = LEDFrame.Empty;

        /// <summary>
        /// Initializes the LED Manager
        /// </summary>
        public static void Init()
        {
            Instance = new LedManager();
        }

        /// <summary>
        /// Starts the LED Manager in keyboard mode by default. Use <seealso cref="SetController"/> to further customize settings, especially for LED strips
        /// </summary>
        public LedManager() // by default starts in keyboard mode
        {
            InitLeds();

            ProcessListenerService.ProcessInFocusChanged += OnProcessChanged;
            ProcessListenerService.Start();
            ProcessListenerService.Register("League of Legends"); // Listen when league of legends is opened
            ProcessListenerService.Register("RocketLeague");

            UpdateLEDDisplay(LEDFrame.CreateEmpty(this));
            Task.Run(UpdateLoop).CatchExceptions();

            DoLightingTest();

        }

        /// <param name="ledCount">Number of lights in the LED strip</param>
        /// <param name="reverseOrder">Set to true if you want the lights to be reverse in order (i.e. Color for LED 0 will be applied to the last LED in the strip)</param>
        private void InitLeds(bool reverseOrder = false)
        {
            lightControllers.Add(RazerChromaController.Create());
            lightControllers.Add(SACNController.Create(reverseOrder));
        }

        private void UninitLeds()
        {
            updateLoopCancelToken.Cancel();
            lightControllers.ForEach(lc => lc.Dispose());
        }

        private void OnProcessChanged(string name, int pid)
        {
            if (name == "League of Legends" && !(CurrentLEDModule is LeagueOfLegendsModule)) // TODO: Account for client disconnections
            {
                LEDModule lolModule = LeagueOfLegendsModule.Create(ModuleOptions.ContainsKey("lol") ? ModuleOptions["lol"] : new Dictionary<string, string>());
                lolModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = lolModule;
            }
            else if (name == "RocketLeague" && !(CurrentLEDModule is RocketLeagueModule)) // TODO: Account for client disconnections
            {
                LEDModule rlModule = RocketLeagueModule.Create(ModuleOptions.ContainsKey("rocketleague") ? ModuleOptions["rocketleague"] : new Dictionary<string, string>());
                rlModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = rlModule;
            }
            else if (name.Length == 0)
            {
                if (!(CurrentLEDModule is BlinkWhiteModule)) // if we're not testing
                    CurrentLEDModule = null;
                return;
            }
        }

        /// <summary>
        /// Briefly tests the lighting and returns it to the previously active module after a few seconds
        /// </summary>
        public void DoLightingTest()
        {

            // TODO: Broken, doesn't return to previous module

            if (CurrentLEDModule is BlinkWhiteModule)
                return;

            Task.Run(async () =>
            {
                Debug.WriteLine("Testing lights");
                ProcessListenerService.Stop();
                await Task.Delay(100);
                LEDModule lastActiveModule = CurrentLEDModule;
                LEDModule blinkModule = BlinkWhiteModule.Create();
                blinkModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = blinkModule;
                await Task.Delay(5000);
                ProcessListenerService.Start();
                if (CurrentLEDModule is BlinkWhiteModule)
                    CurrentLEDModule = lastActiveModule;
            }).ContinueWith((t) => Debug.WriteLine(t.Exception.Message + " // " + t.Exception.StackTrace), TaskContinuationOptions.OnlyOnFaulted);

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
            //Debug.WriteLine("Frame received. " + frame.LastSender.GetType().Name + " Queue=" + FrameQueue.Count);
        }

        private void CheckFrame(LEDFrame frame)
        {
            LEDData data = frame.Leds;
            if (data.Keyboard.Length != LEDData.NUMLEDS_KEYBOARD)
                Debug.WriteLine("SEVERE: Keyboard frame does not match expected length");
            if (data.Strip.Length != LEDData.NUMLEDS_STRIP)
                Debug.WriteLine("SEVERE: Strip frame does not match expected length");
            if (data.Mouse.Length != LEDData.NUMLEDS_MOUSE)
                Debug.WriteLine("SEVERE: Mouse frame does not match expected length");
            if (data.Mousepad.Length != LEDData.NUMLEDS_MOUSEPAD)
                Debug.WriteLine("SEVERE: Mousepad frame does not match expected length");
            if (data.Headset.Length != LEDData.NUMLEDS_HEADSET)
                Debug.WriteLine("SEVERE: Headset frame does not match expected length");
            if (data.Keypad.Length != LEDData.NUMLEDS_KEYPAD)
                Debug.WriteLine("SEVERE: Keypad frame does not match expected length");
            if (data.General.Length != LEDData.NUMLEDS_GENERAL)
                Debug.WriteLine("SEVERE: General frame does not match expected length");
        }

        private async Task UpdateLoop()
        {
            while (true)
            {
                if (updateLoopCancelToken.IsCancellationRequested)
                    return;
                if (FrameQueue.Count > 0)
                {
                    LEDFrame next = FrameQueue.Dequeue();
                    //this.leds = next.Leds;
                    if (next != null)
                        SendLedData(next);
                }
                await Task.Delay(30); // 33 fps
                                      // await Task.Delay(15);
            }
        }

        private void SendLedData(LEDFrame frame)
        {
            LastDisplayedFrame = frame;
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

        private void RestartManager() // TODO: Keep game state (i.e. league of legends cooldowns etc)
        {
            UninitLeds();
            CurrentLEDModule = null; // restart the whole service (force module reload)
            ProcessListenerService.Stop();
            InitLeds(reverseOrder);
            ProcessListenerService.Start();
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
