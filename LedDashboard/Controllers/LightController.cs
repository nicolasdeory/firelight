using Chromely.Core.Network;
using FirelightCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirelightUI.Controllers
{
    class LightController : ChromelyController
    {
        [HttpGet(Route = "/lights/lastframe")]
        public async Task<ChromelyResponse> GetLights(ChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = await BackendMessageService.GetLights();
            return resp;
        }

       /* bool initialized = false;
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

        }*/
    }
}
