using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.ItemModules
{
    public class OracleLensModule : ItemModule
    {

        // Variables

        AnimationModule animator; // Animator module that will be useful to display animations

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static OracleLensModule Create(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new OracleLensModule(ledCount, gameState, 3364, itemSlot, preferredLightMode, preferredCastMode);
        }


        private OracleLensModule(int ledCount, GameState gameState, int itemID, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(itemID, itemSlot, gameState, preferredLightMode)
        {
            // Initialization for the item module occurs here.

            // Set preferred cast mode. It's a player choice (Quick cast, Quick cast with indicator, or Normal cast)
            PreferredCastMode = preferredCastMode;

            // Set item cast mode.
            // For Oracle Lens, for example:
            // It's Instant Cast (press it, and the trinket activates)
            // For a ward, 
            ItemCastMode = AbilityCastMode.Instant();

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            /*animator.PreloadAnimation(@"Animations/Vel'Koz/q_start.txt");
            animator.PreloadAnimation(@"Animations/Vel'Koz/q_recast.txt");
            animator.PreloadAnimation(@"Animations/Vel'Koz/w_cast.txt");
            animator.PreloadAnimation(@"Animations/Vel'Koz/w_close.txt");
            animator.PreloadAnimation(@"Animations/Vel'Koz/r_cast.txt");*/

        }
    }
}
