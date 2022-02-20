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
    [ControllerProperty(Name = "LightController")]
    class LightController : ChromelyController
    {
        [RequestAction(RouteKey = "/lights/lastframe/get")]
        public IChromelyResponse GetLights(IChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = BackendMessageService.GetLights().Result;
            return resp;
        }
    }
}
