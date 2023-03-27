using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Games.LeagueOfLegends
{
    public class SpellCastPreference
    {
        public string Name;
        public AbilityCastPreference D;
        public AbilityCastPreference F;

        public AbilityCastPreference this[SpellKey key]
        {
            get
            {
                return (key) switch
                {
                    SpellKey.D => D,
                    SpellKey.F => F,
                    _ => AbilityCastPreference.Invalid
                };
            }
        }
    }
}
