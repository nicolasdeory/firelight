using kadmium_sacn_core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using OpenRGB.NET;
using System.Linq;
using System.Diagnostics;

namespace FirelightCore
{
    public class OpenRGBController : LightController, IDisposable
    {
        static Dictionary<string, Tuple<LightZone, int>> KEYBOARD_DICTIONARY = new Dictionary<string, Tuple<LightZone, int>>
        {
            { "Keyboard LED 1", Tuple.Create(LightZone.General, 0) },
            { "Key: Escape", Tuple.Create(LightZone.Keyboard, 1) },
            { "Keyboard LED 3", Tuple.Create(LightZone.General, 0) },
            { "Key: F1", Tuple.Create(LightZone.Keyboard, 1) },
            { "Key: F2", Tuple.Create(LightZone.Keyboard, 2) },
            { "Key: F3", Tuple.Create(LightZone.Keyboard, 3) },
            { "Key: F4", Tuple.Create(LightZone.Keyboard, 4) },
            { "Key: F5", Tuple.Create(LightZone.Keyboard, 5) },
            { "Key: F6", Tuple.Create(LightZone.Keyboard, 6) },
            { "Key: F7", Tuple.Create(LightZone.Keyboard, 7) },
            { "Key: F8", Tuple.Create(LightZone.Keyboard, 8) },
            { "Key: F9", Tuple.Create(LightZone.Keyboard, 9) },
            { "Key: F10", Tuple.Create(LightZone.Keyboard, 10) },
            { "Key: F11", Tuple.Create(LightZone.Keyboard, 11) },
            { "Key: F12", Tuple.Create(LightZone.Keyboard, 12) },
            { "Key: Print Screen", Tuple.Create(LightZone.Keyboard, 13) },
            { "Key: Scroll Lock", Tuple.Create(LightZone.Keyboard, 14) },
            { "Key: Pause/Break", Tuple.Create(LightZone.Keyboard, 15) },
            { "Keyboard LED 19", Tuple.Create(LightZone.General, 0) },
            { "Keyboard LED 20", Tuple.Create(LightZone.General, 0) },
            { "Keyboard LED 21", Tuple.Create(LightZone.General, 0) },
            { "Keyboard LED 22", Tuple.Create(LightZone.General, 0) },
            { "Key: `", Tuple.Create(LightZone.Keyboard, 16) },
            { "Key: 1", Tuple.Create(LightZone.Keyboard, 17) },
            { "Key: 2", Tuple.Create(LightZone.Keyboard, 18) },
            { "Key: 3", Tuple.Create(LightZone.Keyboard, 19) },
            { "Key: 4", Tuple.Create(LightZone.Keyboard, 20) },
            { "Key: 5", Tuple.Create(LightZone.Keyboard, 21) },
            { "Key: 6", Tuple.Create(LightZone.Keyboard, 22) },
            { "Key: 7", Tuple.Create(LightZone.Keyboard, 23) },
            { "Key: 8", Tuple.Create(LightZone.Keyboard, 24) },
            { "Key: 9", Tuple.Create(LightZone.Keyboard, 25) },
            { "Key: 0", Tuple.Create(LightZone.Keyboard, 26) },
            { "Key: -", Tuple.Create(LightZone.Keyboard, 27) },
            { "Key: =", Tuple.Create(LightZone.Keyboard, 28) },
            { "Key: Insert", Tuple.Create(LightZone.Keyboard, 30) },
            { "Key: Home", Tuple.Create(LightZone.Keyboard, 31) },
            { "Key: Page Up", Tuple.Create(LightZone.Keyboard, 32) },
            //{ "Key: Num Lock", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad /", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad *", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad -", (LightZone.Keyboard, 1) },
            { "Key: Tab", Tuple.Create(LightZone.Keyboard, 33) },
            { "Key: Q", Tuple.Create(LightZone.Keyboard, 34) },
            { "Key: W", Tuple.Create(LightZone.Keyboard, 35) },
            { "Key: E", Tuple.Create(LightZone.Keyboard, 36) },
            { "Key: R", Tuple.Create(LightZone.Keyboard, 37) },
            { "Key: T", Tuple.Create(LightZone.Keyboard, 38) },
            { "Key: Y", Tuple.Create(LightZone.Keyboard, 39) },
            { "Key: U", Tuple.Create(LightZone.Keyboard, 40) },
            { "Key: I", Tuple.Create(LightZone.Keyboard, 41) },
            { "Key: O", Tuple.Create(LightZone.Keyboard, 42) },
            { "Key: P", Tuple.Create(LightZone.Keyboard, 43) },
            { "Key: [", Tuple.Create(LightZone.Keyboard, 44) },
            { "Key: ]", Tuple.Create(LightZone.Keyboard, 45) },
            { "Key: Enter", Tuple.Create(LightZone.Keyboard, 46) },
            //{ "Key: \\ (ANSI)", (LightZone.Keyboard, 1) },
            { "Key: Delete", Tuple.Create(LightZone.Keyboard, 47) },
            { "Key: End", Tuple.Create(LightZone.Keyboard, 48) },
            { "Key: Page Down", Tuple.Create(LightZone.Keyboard, 49) },
            //{ "Key: Number Pad 7", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 8", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 9", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad +", (LightZone.Keyboard, 1) },
            { "Key: Caps Lock", Tuple.Create(LightZone.Keyboard, 50) },
            { "Key: A", Tuple.Create(LightZone.Keyboard, 51) },
            { "Key: S", Tuple.Create(LightZone.Keyboard, 52) },
            { "Key: D", Tuple.Create(LightZone.Keyboard, 53) },
            { "Key: F", Tuple.Create(LightZone.Keyboard, 54) },
            { "Key: G", Tuple.Create(LightZone.Keyboard, 55) },
            { "Key: H", Tuple.Create(LightZone.Keyboard, 56) },
            { "Key: J", Tuple.Create(LightZone.Keyboard, 57) },
            { "Key: K", Tuple.Create(LightZone.Keyboard, 58) },
            { "Key: L", Tuple.Create(LightZone.Keyboard, 59) },
            { "Key: ;", Tuple.Create(LightZone.Keyboard, 60) },
            { "Key: '", Tuple.Create(LightZone.Keyboard, 61) },
            { "Key: #", Tuple.Create(LightZone.Keyboard, 62) },
            //{ "Keyboard LED 16", (LightZone.Keyboard, 1) },
            //{ "Keyboard LED 17", (LightZone.Keyboard, 1) },
            //{ "Keyboard LED 18", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 4", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 5", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 6", (LightZone.Keyboard, 1) },
            { "Key: Left Shift", Tuple.Create(LightZone.Keyboard, 63) },
            { "Key: \\ (ISO)", Tuple.Create(LightZone.Keyboard, 64) },
            { "Key: Z", Tuple.Create(LightZone.Keyboard, 65) },
            { "Key: X", Tuple.Create(LightZone.Keyboard, 66) },
            { "Key: C", Tuple.Create(LightZone.Keyboard, 67) },
            { "Key: V", Tuple.Create(LightZone.Keyboard, 68) },
            { "Key: B", Tuple.Create(LightZone.Keyboard, 69) },
            { "Key: N", Tuple.Create(LightZone.Keyboard, 70) },
            { "Key: M", Tuple.Create(LightZone.Keyboard, 71) },
            { "Key: ,", Tuple.Create(LightZone.Keyboard, 72) },
            { "Key: .", Tuple.Create(LightZone.Keyboard, 73) },
            { "Key: /", Tuple.Create(LightZone.Keyboard, 74) },
            //{ "Keyboard LED 14", (LightZone.Keyboard, 1) },
            { "Key: Right Shift", Tuple.Create(LightZone.Keyboard, 75) },
            { "Key: Up Arrow", Tuple.Create(LightZone.Keyboard, 76) },
            //{ "Key: Number Pad 1", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 2", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 3", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad Enter", (LightZone.Keyboard, 1) },
            { "Key: Left Control", Tuple.Create(LightZone.Keyboard, 77) },
            { "Key: Left Windows", Tuple.Create(LightZone.Keyboard, 78) },
            { "Key: Left Alt", Tuple.Create(LightZone.Keyboard, 79) },
            //{ "Keyboard LED 5", (LightZone.Keyboard, 1) },
            //{ "Keyboard LED 6", (LightZone.Keyboard, 1) },
            //{ "Keyboard LED 7", (LightZone.Keyboard, 1) },
            { "Key: Space", Tuple.Create(LightZone.Keyboard, 80) },
            //{ "Keyboard LED 9", (LightZone.Keyboard, 1) },
            //{ "Keyboard LED 10", (LightZone.Keyboard, 1) },
            //{ "Keyboard LED 11", (LightZone.Keyboard, 1) },
            { "Key: Right Alt", Tuple.Create(LightZone.Keyboard, 81) },
            { "Key: Right Fn", Tuple.Create(LightZone.Keyboard, 82) },
            { "Key: Menu", Tuple.Create(LightZone.Keyboard, 83) },
            { "Key: Right Control", Tuple.Create(LightZone.Keyboard, 84) },
            { "Key: Left Arrow", Tuple.Create(LightZone.Keyboard, 85) },
            { "Key: Down Arrow", Tuple.Create(LightZone.Keyboard, 86) },
            { "Key: Right Arrow", Tuple.Create(LightZone.Keyboard, 87) },
            //{ "Keyboard LED 19", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad 0", (LightZone.Keyboard, 1) },
            //{ "Key: Number Pad .", (LightZone.Keyboard, 1) },

        };

        public static OpenRGBController Create()
        {
            return new OpenRGBController();
        }

        OpenRgbClient client;
        Device[] devices;
        bool isDisposed;
        bool errored;
        string errorCode;

        private OpenRGBController()
        {
            try
            {
                client = new OpenRgbClient(name: "Firelight", autoConnect: true, timeoutMs: 1000);
            }
            catch (Exception ex)
            {
                errored = true;
                errorCode = "Error connecting to OpenRGB. Is it running?";
                Debug.WriteLine(ex.Message);
                return;
            }

            this.devices = client.GetAllControllerData();
            for (int i = 0; i < devices.Length; i++)
            {
                var device = devices[i];
                var directMode = device.Modes.ToList().Find(x => x.Name == "Direct");
                var leds = device.Leds.Select(x => x.Name);
                //Debug.WriteLine(string.Join(",", leds.ToList()));
                client.UpdateMode(device.Index, directMode.Index);
            }
        }


        public void SendData(LEDFrame frame)
        {
            // TODO: Add mouse
            if (!frame.Zones.HasFlag(LightZone.General) && !frame.Zones.HasFlag(LightZone.Keyboard))
            {
                return;
            }
            // Disable razer maybe if synapse is running?
            for (int i = 0; i < devices.Length; i++)
            {
                var device = devices[i];
                var colorArray = new OpenRGB.NET.Color[devices[i].Colors.Length];
                var fixedCol = frame.Leds.General[0].color.ToRGB();
                for (int j = 0; j < devices[i].Leds.Length; j++)
                {
                    var led = device.Leds[j];
                    if (KEYBOARD_DICTIONARY.ContainsKey(led.Name))
                    {
                        var tuple = KEYBOARD_DICTIONARY[led.Name];
                        if (tuple.Item1.HasFlag(LightZone.Keyboard))
                        {
                            var color = frame.Leds.Keyboard[tuple.Item2].color.ToRGB();
                            colorArray[j] = new OpenRGB.NET.Color(color[0], color[1], color[2]);
                        } else
                        {
                            colorArray[j] = new OpenRGB.NET.Color(fixedCol[0], fixedCol[1], fixedCol[2]);
                        }
                    }
                }
                client.UpdateLeds(i, colorArray);
            }
        }

        public void Dispose()
        {
            isDisposed = true;
            client.Dispose();
        }
        public bool Enabled { get; set; }

        public bool Errored => errored;

        public string ErrorCode => errorCode;
    }
}
