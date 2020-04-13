using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.Model
{
    public class SummonerSpell
    {
        public string RawDisplayName;
    }
    public class SummonerSpellLoadout
    {
        public SummonerSpell SummonerSpellOne;
        public SummonerSpell SummonerSpellTwo;
    }
}
