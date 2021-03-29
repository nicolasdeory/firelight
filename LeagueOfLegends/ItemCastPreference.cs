using Games.LeagueOfLegends.ChampionModules.Common;

namespace Games.LeagueOfLegends
{
    public class ItemCastPreference
    {
        public AbilityCastPreference Item1;
        public AbilityCastPreference Item2;
        public AbilityCastPreference Item3;
        public AbilityCastPreference Item4;
        public AbilityCastPreference Item5;
        public AbilityCastPreference Item6;
        public AbilityCastPreference Item7;

        public AbilityCastPreference this[int slot]
        {
            get
            {
                return (slot) switch
                {
                    0 => Item1,
                    1 => Item2,
                    2 => Item3,
                    3 => Item4,
                    4 => Item5,
                    5 => Item6,
                    6 => Item7,
                    _ => AbilityCastPreference.Invalid
                };
            }
        }
    }
}
