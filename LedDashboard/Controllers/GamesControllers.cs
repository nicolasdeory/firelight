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
    [AppController]
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
                RegisterGetRequestAsync("/games/" + g.Id, async (req) => await GetGameSettings(req, g));
                RegisterPostRequest("/games/" + g.Id, (req) => PostGameSettings(req, g));
            }
        }

        [HttpGet(Route = "/games")]
        public ChromelyResponse GetGameList(ChromelyRequest request)
        {
            Debug.WriteLine("dasasd");
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = Games;
            return resp;
        }

        public async Task<ChromelyResponse> GetGameSettings(ChromelyRequest request, Game g)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            resp.Data = await BackendMessageService.GetSettings(g);
            return resp;
        }

        public ChromelyResponse PostGameSettings(ChromelyRequest request, Game g)
        {
            ChromelyResponse resp = new ChromelyResponse(request.Id);
            string json = request.PostData.ToJson();
            var obj = JsonConvert.DeserializeObject<Dictionary<string,string>>(json);
            BackendMessageService.UpdateSettings(g, obj);
            return resp;
        }
    }
}
