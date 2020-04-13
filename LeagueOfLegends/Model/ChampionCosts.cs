using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.Model
{
    public class ChampionCosts
    {
        public AbilityCooldown Q_Cooldown;
        public ManaCost Q_Cost;
        public AbilityCooldown W_Cooldown;
        public ManaCost W_Cost;
        public AbilityCooldown E_Cooldown;
        public ManaCost E_Cost;
        public AbilityCooldown R_Cooldown;
        public ManaCost R_Cost;

        public AbilityCooldown GetCooldown(AbilityKey key)
        {
            return key switch
            {
                AbilityKey.Q => Q_Cooldown,
                AbilityKey.W => W_Cooldown,
                AbilityKey.E => E_Cooldown,
                AbilityKey.R => R_Cooldown,
                _ => null
            };
        }

        public int GetManaCost(AbilityKey key, int level)
        {
            return key switch
            {
                AbilityKey.Q => Q_Cost[level],
                AbilityKey.W => W_Cost[level],
                AbilityKey.E => E_Cost[level],
                AbilityKey.R => R_Cost[level],
                _ => 0
            };
        }
    }
}
