using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.Model
{
    public class GameState
    {
        public ActivePlayer ActivePlayer;
        public List<Champion> Champions;
        public Champion PlayerChampion;
        public List<Event> GameEvents;
        public Dictionary<AbilityKey, bool> PlayerAbilityCooldowns;
        // public bool[] PlayerItemCooldowns = new bool[7];

        public double AverageChampionLevel => Champions.Select(x => x.Level).Average();
    }
}
