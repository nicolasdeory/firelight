using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Games.LeagueOfLegends.Model
{
    public class ChampionAttributes
    {
        public ChampionCosts Costs;

        public static ChampionAttributes FromData(dynamic data)
        {

            List<SpellInformation> spells = (data.spells as JArray).ToObject<List<SpellInformation>>();
            return new ChampionAttributes()
            {
                Costs = new ChampionCosts()
                {
                    Q_Cooldown = new AbilityCooldown(spells[0].Cooldown),
                    Q_Cost = new ManaCost(spells[0].Cost),
                    W_Cooldown = new AbilityCooldown(spells[1].Cooldown),
                    W_Cost = new ManaCost(spells[1].Cost),
                    E_Cooldown = new AbilityCooldown(spells[2].Cooldown),
                    E_Cost = new ManaCost(spells[2].Cost),
                    R_Cooldown = new AbilityCooldown(spells[3].Cooldown),
                    R_Cost = new ManaCost(spells[3].Cost),

                }
            };
        }

    }
}
