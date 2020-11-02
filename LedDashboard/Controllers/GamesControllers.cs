using Chromely.Core.Network;
using FirelightCore;
using FirelightUI.ControllerModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirelightUI.Controllers
{
    /// <summary>
    /// Returns a list of compatible games
    /// </summary>
    [AppController]
    class GamesControllers : ChromelyController
    {
        [HttpGet(Route = "/games")]
        public ChromelyResponse GetGameList(ChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = new List<Game>()
            {
                new Game("leagueoflegends", "League of Legends")
            };
            return resp;
        }
    }
}
