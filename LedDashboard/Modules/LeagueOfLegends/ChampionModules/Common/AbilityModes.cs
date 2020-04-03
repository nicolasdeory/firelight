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

        public AbilityCastMode RecastMode { get; private set; }

        public int RecastTime { get; private set; }

        public int MaxRecasts { get; private set; }

        /// <summary>
        /// Returns an ability that cannot be cast.
        /// </summary>
        /// <returns></returns>
        public static AbilityCastMode UnCastable()
        {
            return new AbilityCastMode()
            {
                Castable = false
            };
        }

        /// <summary>
        /// Returns a normal cast mode.
        /// </summary>
        /// <param name="recastTime">Max time the player has to recast the ability</param>
        /// <param name="maxRecasts">Max number of times the ability can be recast (e.g. for Ahri R it's 3, for most it's just 1)</param>
        /// <param name="recastMode">Cast mode for the ability recast. If <paramref name="recastMode"/> is null, by default the recast is on Instant mode.</param>
        public static AbilityCastMode Normal(int recastTime = -1, int maxCasts = 1, AbilityCastMode recastMode = null)
        {
            return new AbilityCastMode()
            {
                IsNormal = true,
                HasRecast = recastTime > 0,
                RecastMode = recastTime > 0 ? recastMode ?? AbilityCastMode.Instant() : null,
                RecastTime = recastTime,
                MaxRecasts = maxCasts
            };
        }

        /// <summary>
        /// Returns an instant cast mode.
        /// </summary>
        /// <param name="recastTime">Max time the player has to recast the ability</param>
        /// <param name="maxRecasts">Max number of times the ability can be recast (e.g. for Ahri R it's 3, for most it's just 1)</param>
        /// <param name="recastMode">Cast mode for the ability recast. If <paramref name="recastMode"/> is null, by default the recast is on Instant mode.</param>
        public static AbilityCastMode Instant(int recastTime = -1, int maxRecasts = 1, AbilityCastMode recastMode = null)
        {
            return new AbilityCastMode()
            {
                IsInstant = true,
                HasRecast = recastTime > 0,
                RecastMode = recastTime > 0 ? recastMode ?? AbilityCastMode.Instant() : null,
                RecastTime = recastTime,
                MaxRecasts = maxRecasts
            };
        }
    }

    public enum AbilityCastPreference
    {
        Normal = 1, Quick = 2, QuickWithIndicator = 4
    }
}
