using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.Model
{
    public class AbilityCooldown
    {
        int level0;
        int level1;
        int level2;
        int level3;
        int level4;

        public AbilityCooldown(List<float> cds) // Cooldowns should be expressed in milliseconds
        {
            level0 = (int)(cds[0] * 1000); 
            level1 = (int)(cds[1] * 1000);
            level2 = (int)(cds[2] * 1000);
            if(cds.Count > 3)
            {
                level3 = (int)(cds[3] * 1000);
                level4 = (int)(cds[4] * 1000);
            } else
            {
                level3 = level4 = 0;
            }
        }

        /// <summary>
        /// Gets the cooldown duration for the given ability level
        /// </summary>
        public int this[int level]
        {
            get 
            {
                if (level == 0) return level0;
                if (level == 1) return level1;
                if (level == 2) return level2;
                if (level == 3) return level3;
                if (level == 4) return level4;
                return 0;
            }
            set
            {
                switch (level)
                {
                    case 0:
                        level0 = level;
                        break;
                    case 1:
                        level1 = level;
                        break;
                    case 2:
                        level2 = level;
                        break;
                    case 3:
                        level3 = level;
                        break;
                    case 4:
                        level4 = level;
                        break;
                    default:
                        throw new ArgumentException("Invalid level set");
                }
            }
        }
    }
}
