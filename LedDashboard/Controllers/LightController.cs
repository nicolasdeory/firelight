using Chromely.Core.Network;
using LedDashboardCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LedDashboard.Controllers
{
    class LightController : ChromelyController
    {
        [HttpGet(Route = "/lights/lastframe")]
        public ChromelyResponse GetLights(ChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            LEDData data;
            if (LedManager.Instance == null || LedManager.Instance.CurrentLEDModule == null)
                data = LEDData.Empty;
            else
                data = LedManager.Instance.LastDisplayedFrame.Leds;
            //Debug.WriteLine(string.Join(',',(object[])data.Keyboard));
            List<string[]> ledList = new List<string[]>();
            for (int i = 0; i < 6; i++)
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
            resp.Data = ledList;
            return resp;
        }

        bool initialized = false;
        [HttpGet(Route = "/lights/initialize")]
        public ChromelyResponse InitLights(ChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            if (!initialized)
            {
                initialized = true;
                LedManager.Init();
                resp.Data = "initialized";
            } else
            {
                resp.Data = "already initialized";
            }
            return resp;

        }
    }
}
