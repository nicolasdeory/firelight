using Games.LeagueOfLegends;
using Games.RocketLeague;
using FirelightCore;
using FirelightCore.Modules.BlinkWhite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirelightCore.Modules.FourierAudioLED;
using Games.Fortnite;
using Gma.System.MouseKeyHook;
using System.Runtime.Versioning;

namespace FirelightService
{
    class LightManager
    {
        /// <summary>
        /// Null until it's initialized
        /// </summary>
        public static LightManager Instance { get; private set; }

        public delegate void UpdateDisplayHandler(LEDFrame frame);

        List<LightController> lightControllers = new List<LightController>();

        Queue<LEDFrame> FrameQueue = new Queue<LEDFrame>();

        CancellationTokenSource updateLoopCancelToken;

        public LEDModule CurrentLEDModule
        {
            get
            {
                return _currentLEDModule;
            }
            private set
            {
                SetEnabled(value != null);

                _currentLEDModule?.Dispose();
                _currentLEDModule = value;
            }
        }
        LEDModule _currentLEDModule;

        bool enabled = false;

        public LEDFrame LastDisplayedFrame { get; private set; } = LEDFrame.Empty;

        /// <summary>
        /// Initializes the LED Manager
        /// </summary>
        public static void Init()
        {
            Instance = new LightManager();
        }

        /// <summary>
        /// Starts the LED Manager in keyboard mode by default. Use <seealso cref="SetController"/> to further customize settings, especially for LED strips
        /// </summary>
        public LightManager()
        {
            Debug.WriteLine("Initializing LedManager");
            //InitLeds

            GlobalHooker.Init();

            ModuleManager.Init(new List<ModuleAttributes>()
            {
                new LeagueOfLegendsModuleAttributes()
            });
            ModuleManager.LoadSettings();
            ModuleManager.SettingChanged += (s, e) => RestartManager();

            ProcessListenerService.ProcessInFocusChanged += OnProcessChanged;
            ProcessListenerService.Start();
            ProcessListenerService.Register("League of Legends"); // Listen when league of legends is opened
            ProcessListenerService.Register("RocketLeague");
            ProcessListenerService.Register("FortniteClient-Win64-Shipping");

            UpdateLEDDisplay(LEDFrame.CreateEmpty(this));
            DoLightingTest();

        }

        /// <param name="ledCount">Number of lights in the LED strip</param>
        /// <param name="reverseOrder">Set to true if you want the lights to be reverse in order (i.e. Color for LED 0 will be applied to the last LED in the strip)</param>
        private void InitLeds(bool reverseOrder = false)
        {
            updateLoopCancelToken = new CancellationTokenSource();
            Task.Run(UpdateLoop).CatchExceptions();
            lightControllers.Add(RazerChromaController.Create());
            //lightControllers.Add(NZXTController.Create());
            lightControllers.Add(OpenRGBController.Create());
            lightControllers.Add(SACNController.Create(reverseOrder)); // TODO: Load this from module attributes. This will come when LED strip settings are implemented
        }

        private void UninitLeds()
        {
            updateLoopCancelToken.Cancel();
            lightControllers.ForEach(lc => lc.Dispose());
            lightControllers.Clear();
        }

        private void OnProcessChanged(string name, int pid)
        {
            if (name == "League of Legends" && !(CurrentLEDModule is LeagueOfLegendsModule)) // TODO: Account for client disconnections
            {
                LEDModule lolModule = LeagueOfLegendsModule.Create();
                lolModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = lolModule;
            }
            else if (name == "RocketLeague" && !(CurrentLEDModule is RocketLeagueModule)) // TODO: Account for client disconnections
            {
                LEDModule rlModule = RocketLeagueModule.Create();
                rlModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = rlModule;
            }
            else if (name == "FortniteClient-Win64-Shipping" && !(CurrentLEDModule is FortniteModule)) // TODO: Account for client disconnections
            {
                LEDModule fortniteModule = FortniteModule.Create();
                fortniteModule.NewFrameReady += UpdateLEDDisplay;
                CurrentLEDModule = fortniteModule;
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
                // await Task.Delay(31000);
                await Task.Delay(30000);
                ProcessListenerService.Start();
                if (CurrentLEDModule is BlinkWhiteModule)
                    CurrentLEDModule = lastActiveModule;
            }).CatchExceptions();

        }

        private void SetEnabled(bool enable) // If set to false, deattach from razer chroma
        {
            if (this.enabled != enable)
            {
                this.enabled = enable;
                if (enabled)
                    InitLeds();
                else
                    UninitLeds();
                //lightControllers.ForEach(c => c.Enabled = enable);
            }
        }

        /// <summary>
        /// Updates the LED display
        /// </summary>
        /// <param name="s">Module that sent the update command</param>
        /// <param name="ls">Array containing values for each LED in the strip</param>
        public void UpdateLEDDisplay(LEDFrame frame)
        {
            CheckFrame(frame);
            if (frame.Priority)
            {
                FrameQueue.Clear();
            }
            FrameQueue.Enqueue(frame);
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
            // TODO: Investigate LAG
            while (true)
            {
                if (updateLoopCancelToken.IsCancellationRequested)
                    return;
                if (FrameQueue.Count > 0)
                {
                    try
                    {
                        LEDFrame next = FrameQueue.Dequeue();
                        if (next != null)
                            SendLedData(next);
                    }
                    catch { /* solves race condition */ }

                }
                await Task.Delay(30); // 33 fps
                                      // await Task.Delay(15);
            }
        }

        private void SendLedData(LEDFrame frame)
        {
            LastDisplayedFrame = frame;
            lightControllers.ForEach(controller =>
            {
                if (controller.Errored)
                {
                    string controllerName = controller.GetType().Name;
                    FrontendMessageService.SendError("hardware-"+ controllerName, controller.ErrorCode);
                } else
                {
                    controller.SendData(frame);
                }
            });

           // DisplayUpdated?.Invoke(frame);
        }

        private bool isRestarting; // this is to wait for a bit and avoid very frequent restarts
        private CancellationTokenSource restartCancellationTokenSource;
        private void RestartManager() // TODO: Keep game state (i.e. league of legends cooldowns etc)
        {
            if (isRestarting)
            {
                restartCancellationTokenSource.Cancel();
            }
            isRestarting = true;
            restartCancellationTokenSource = new CancellationTokenSource();
            Task t = Task.Run(async () =>
            {
                await Task.Delay(3000);
                if (restartCancellationTokenSource.IsCancellationRequested)
                    return;
                SetEnabled(false);
                //UninitLeds();
                CurrentLEDModule = null; // restart the whole service (force module reload)
                await Task.Delay(500);
                ProcessListenerService.Stop();
                //SetEnabled(true);
                //InitLeds();
                ProcessListenerService.Start();
                isRestarting = false;
            }).CatchExceptions(true);
            
        }

        //public void SetModuleOption(string moduleId, string option, string value)
        //{
        //    if (!ModuleOptions.ContainsKey(moduleId))
        //    {
        //        ModuleOptions.Add(moduleId, new Dictionary<string, string>());
        //    }
        //    if (ModuleOptions[moduleId].ContainsKey(option))
        //    {
        //        ModuleOptions[moduleId][option] = value;
        //    }
        //    else
        //    {
        //        ModuleOptions[moduleId].Add(option, value);
        //    }
        //    RestartManager();
        //}

    }
}
