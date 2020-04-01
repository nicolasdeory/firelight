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
    class WardingTotemModule : ItemModule
    {

        // Variables


        // Cooldown

        private int cooldownPerCharge = 0;
        private int wardCharges = 1; // TODO: Not always starts with 1, depending of last trinker that the player had.

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static WardingTotemModule Create(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new WardingTotemModule(ledCount, gameState, 3340, itemSlot, preferredLightMode, preferredCastMode);
        }


        private WardingTotemModule(int ledCount, GameState gameState, int itemID, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(itemID, itemSlot, gameState, preferredLightMode)
        {
            // Initialization for the item module occurs here.

            // Set preferred cast mode. It's a player choice (Quick cast, Quick cast with indicator, or Normal cast)
            PreferredCastMode = preferredCastMode;

            ItemCast += OnItemActivated;
            GameStateUpdated += OnGameStateUpdated;
            OnGameStateUpdated(gameState);

            // Set item cast mode.
            // For Oracle Lens, for example:
            // It's Instant Cast (press it, and the trinket activates)
            // For a ward, it's normal cast (press & click)
            ItemCastMode = AbilityCastMode.Normal();

            // Since it's a ward trinket, setup ward recharging
            Task.Run(async () =>
            {
                while (true)
                {
                    if (wardCharges < 2)
                    {
                        await Task.Delay(cooldownPerCharge);
                        wardCharges++;
                    } else
                    {
                        await Task.Delay(300);
                    }
                }
                
            });

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls, mode);

        }

        private void OnItemActivated(object s, EventArgs e)
        {
            if (wardCharges > 0)
            {
                wardCharges--;
                //animator.ColorBurst(HSVColor.FromRGB(new byte[] { 235, 220, 14 })); // TODO: Prettier animation?

                if (wardCharges > 0)
                {
                    CooldownDuration = 500;
                } else
                {
                    CooldownDuration = cooldownPerCharge - 100; // substract some duration to account for other delays;
                }

            }
            
        }

        private void OnGameStateUpdated(GameState state) // TODO: Handle when player buys a different trinket and cooldown gets transferred over
        {
            // Normally you wouldn't need to do this, but since some trinket cooldowns depend on
            // average champion level, we need to contemplate this.
            double averageLevel = state.Champions.Select(x => x.Level).Average();
            cooldownPerCharge = (int)((247.059 - 7.059 * averageLevel) * 1000);
        }
    }
}
