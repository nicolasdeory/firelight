using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Games.LeagueOfLegends.Model
{
    public class SummonerSpellAttributes
    {
        public AbilityCooldown Cooldown;

        public static SummonerSpellAttributes FromData(dynamic data)
        {

            SpellInformation spell = data.ToObject<SpellInformation>();
            return new SummonerSpellAttributes()
            {
                Cooldown = new AbilityCooldown(spell.Cooldown),
            };
        }

    }
}
