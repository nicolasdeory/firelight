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
    /// <summary>
    /// Handles global routes such as app errors
    /// </summary>
    [ControllerProperty(Name ="GlobalController")]
    class GlobalController : ChromelyController
    {
        [RequestAction(RouteKey = "/global/errors/get")]
        public IChromelyResponse GetErrors(IChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = GlobalAppState.Errors;
            return resp;
        }
    }
}
