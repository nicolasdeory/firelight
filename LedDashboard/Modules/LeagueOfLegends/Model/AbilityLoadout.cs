using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.Model
{
    public class AbilityLoadout
    {
        public string Passive_Id;
        public string Q_Id;
        public string W_Id;
        public string E_Id;
        public string R_Id;

        public int Q_Level;
        public int W_Level;
        public int E_Level;
        public int R_Level;

        public static AbilityLoadout FromData(dynamic data)
        {
            return new AbilityLoadout()
            {
                Passive_Id = data.Passive.id,
                Q_Id = data.Q.id,
                Q_Level = data.Q.abilityLevel,
                W_Id = data.W.id,
                W_Level = data.W.abilityLevel,
                E_Id = data.E.id,
                E_Level = data.E.abilityLevel,
                R_Id = data.R.id,
                R_Level = data.R.abilityLevel
            };
        }

        public int GetAbilityLevel(AbilityKey ability)
        {
            return ability switch
            {
                AbilityKey.Q => Q_Level,
                AbilityKey.W => W_Level,
                AbilityKey.E => E_Level,
                AbilityKey.R => R_Level,
                _ => 0,
            };
        }
    }
}
