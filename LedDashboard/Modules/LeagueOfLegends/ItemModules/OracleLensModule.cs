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
    class OracleLensModule : ItemModule
    {

        // Variables


        // Cooldown

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

            ItemCast += OnItemActivated;
            GameStateUpdated += OnGameStateUpdated;
            // Set item cast mode.
            // For Oracle Lens, for example:
            // It's Instant Cast (press it, and the trinket activates)
            // For a ward, it's normal cast (press & click)
            ItemCastMode = AbilityCastMode.Instant();

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls, mode);

            animator.PreloadAnimation(ITEM_ANIMATION_PATH + "OracleLens/activation.txt");

        }

        private void OnItemActivated(object s, EventArgs e)
        {
            // Play relevant animations here
            animator.RunAnimationInLoop(ITEM_ANIMATION_PATH + "OracleLens/activation.txt", 8500);
        }

        private void OnGameStateUpdated(GameState state) // TODO: Handle when player buys a different trinket and cooldown gets transferred over
        {
            // Normally you wouldn't need to do this, but since some trinket cooldowns depend on
            // average champion level, we need to contemplate this.
            double averageLevel = state.Champions.Select(x => x.Level).Average();
            CooldownDuration = (int)((91.765 - 1.765 * averageLevel) * 1000);
        }
    }
}
