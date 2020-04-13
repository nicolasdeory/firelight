using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.Model
{
    public class AbilityCooldown
    {
        private int[] levels;

        public AbilityCooldown(List<float> cds) // Cooldowns should be expressed in milliseconds
        {
            levels = new int[cds.Count];
            for (int i = 0; i < cds.Count; i++)
                levels[i] = (int)(cds[i] * 1000);
        }

        /// <summary>
        /// Gets the cooldown duration for the given ability level
        /// </summary>
        public int this[int level]
        {
            get 
            {
                return level < levels.Length ? levels[level] : 0;
            }
            set
            {
                if (level < levels.Length)
                {
                    levels[level] = value;
                }
                else
                {
                    Console.Error.WriteLine("Invalid level set: " + level);
                    throw new ArgumentException("Invalid level set");
                }
            }
        }
    }
}
