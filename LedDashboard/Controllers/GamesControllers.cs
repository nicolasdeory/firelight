using Chromely.Core.Network;
using FirelightCore;
using FirelightUI.ControllerModel;
using Newtonsoft.Json;
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
    [ControllerProperty(Name = "GamesController")]
    class GamesControllers : ChromelyController
    {

        static List<Game> Games = new List<Game>()
        {
            new Game("leagueoflegends", "League of Legends")
        };

        public GamesControllers()
        {
            foreach(Game g in Games)
            {
                RegisterRequestAsync("/games/" + g.Id + "/settings/get", async (req) => await GetGameSettings(req, g));
                RegisterRequest("/games/" + g.Id + "/settings/post", (req) => PostGameSettings(req, g));
            }
        }

        [RequestAction(RouteKey = "/games/get")]
        public IChromelyResponse GetGameList(IChromelyRequest request)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = Games;
            return resp;
        }

        public async Task<IChromelyResponse> GetGameSettings(IChromelyRequest request, Game g)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = await BackendMessageService.GetSettings(g);
            return resp;
        }

        public IChromelyResponse PostGameSettings(IChromelyRequest request, Game g)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            var obj = request.PostData as Dictionary<string, string>;
            BackendMessageService.UpdateSettings(g, obj);
            return resp;
        }
    }
}
