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
    [AppController]
    class LightController : ChromelyController
    {
        [HttpGet(Route = "/lights/lastframe")]
        public async Task<ChromelyResponse> GetLights(ChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = await BackendMessageService.GetLights();
            return resp;
        }
    }
}
