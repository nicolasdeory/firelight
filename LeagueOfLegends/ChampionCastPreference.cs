using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Games.LeagueOfLegends
{
    public class ChampionCastPreference
    {
        public string Name;
        public AbilityCastPreference Q;
        public AbilityCastPreference W;
        public AbilityCastPreference E;
        public AbilityCastPreference R;

        public AbilityCastPreference this[AbilityKey key]
        {
            get
            {
                return (key) switch
                {
                    AbilityKey.Q => Q,
                    AbilityKey.W => W,
                    AbilityKey.E => E,
                    AbilityKey.R => R,
                    _ => AbilityCastPreference.Invalid
                };
            }
        }
    }
}
