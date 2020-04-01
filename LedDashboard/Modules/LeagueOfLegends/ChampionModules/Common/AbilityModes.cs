using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common
{
    /// <summary>
    /// Instant means the moment the key is pressed down, the ability is cast (e.g summoner spell, Vel'Koz's R)
    /// </summary>
    public class AbilityCastMode
    {
        public bool Castable { get; private set; } = true;
        public bool IsNormal { get; private set; }
        public bool IsInstant { get; private set; }
        public bool HasRecast { get; private set; }
        public int RecastTime { get; private set; }

        public int MaxRecasts { get; private set; }

        public static AbilityCastMode UnCastable()
        {
            return new AbilityCastMode()
            {
                Castable = false
            };
        }

        public static AbilityCastMode Normal(int recastTime = -1, int maxCasts = 1)
        {
            return new AbilityCastMode()
            {
                IsNormal = true,
                HasRecast = recastTime > 0,
                RecastTime = recastTime,
                MaxRecasts = maxCasts
            };
        }

        public static AbilityCastMode Instant(int recastTime = -1, int maxRecasts = 1)
        {
            return new AbilityCastMode()
            {
                IsInstant = true,
                HasRecast = recastTime > 0,
                RecastTime = recastTime,
                MaxRecasts = maxRecasts
            };
        }
    }

    public enum AbilityCastPreference
    {
        Normal, Quick, QuickWithIndicator
    }
}
