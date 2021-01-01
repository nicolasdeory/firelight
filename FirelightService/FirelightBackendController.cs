using FirelightService;
using FirelightCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;

namespace FirelightService
{
    class FirelightBackendController : IBackendController
    {
        public List<string[]> GetLights()
        {
            LEDData data;
            if (LightManager.Instance == null || LightManager.Instance.CurrentLEDModule == null)
                data = LEDData.Empty;
            else
                data = LightManager.Instance.LastDisplayedFrame.Leds;
            //Debug.WriteLine(string.Join(',',(object[])data.Keyboard));
            List<string[]> ledList = new List<string[]>();
            for (int i = 0; i <= 6; i++)
            {
                Led[] arr;
                switch (i)
                {
                    case 0:
                        arr = data.Keyboard;
                        break;
                    case 1:
                        arr = data.Strip;
                        break;
                    case 2:
                        arr = data.Mouse;
                        break;
                    case 3:
                        arr = data.Mousepad;
                        break;
                    case 4:
                        arr = data.Headset;
                        break;
                    case 5:
                        arr = data.Keypad;
                        break;
                    case 6:
                        arr = data.General;
                        break;
                    default:
                        arr = new Led[0];
                        break;
                }
                ledList.Add(arr.Select(x => x.color.ToHex()).ToArray());
            }
            return ledList;
        }

        public void LogMessage(string message)
        {
            Debug.WriteLine($"[UI Client] {message}");
        }

        public Dictionary<string, string> GetSettings(string gameId)
        {
            return ModuleManager.AttributeDict[gameId].SettingsDictionary;
        }

        public void UpdateSettings(string gameId, IDictionary<string, string> settings)
        {
            foreach (var kp in settings)
            {
                ModuleManager.AttributeDict[gameId].SettingsDictionary[kp.Key] = kp.Value;
            }
        }
    }
}
