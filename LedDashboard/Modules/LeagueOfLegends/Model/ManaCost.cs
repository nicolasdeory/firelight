using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.Model
{
    public class ManaCost
    {
        int level0;
        int level1;
        int level2;
        int level3;
        int level4;

        public ManaCost(List<int> costs)
        {
            level0 = costs[0];
            level1 = costs[1];
            level2 = costs[2];
            if (costs.Count > 3)
            {
                level3 = costs[3];
                level4 = costs[4];
            } else
            {
                level3 = level4 = 0;
            }
            
        }
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
