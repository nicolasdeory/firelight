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
    [Item(ITEM_ID)]
    class WardingTotemModule : ItemModule
    {

        public const int ITEM_ID = 3340;


        // Variables


        // Cooldown

        private int cooldownPerCharge = 0;

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static WardingTotemModule Create(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new WardingTotemModule(ledCount, gameState, ITEM_ID, itemSlot, preferredLightMode, preferredCastMode);
        }


        private WardingTotemModule(int ledCount, GameState gameState, int itemID, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(itemID, itemSlot, gameState, preferredLightMode)
        {
            // Initialization for the item module occurs here.

            // Set preferred cast mode. It's a player choice (Quick cast, Quick cast with indicator, or Normal cast)
            PreferredCastMode = preferredCastMode;

            ItemCast += OnItemActivated;
            /*GameStateUpdated += OnGameStateUpdated;
            OnGameStateUpdated(gameState);*/

            // Set item cast mode.
            // For Oracle Lens, for example:
            // It's Instant Cast (press it, and the trinket activates)
            // For a ward, it's normal cast (press & click)
            ItemCastMode = AbilityCastMode.Normal();

            ItemCooldownController.SetCooldown(this.ItemID, GetCooldownPerCharge(gameState)); // Game bug? Set the cooldown to 0, because everytime

            // Since it's a ward trinket, setup ward recharging
            /*Task.Run(async () =>
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
                
            });*/

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls, mode);

        }

        private void OnItemActivated(object s, EventArgs e)
        {
            int wardCharges = 0;
            int cd = ItemCooldownController.GetCooldownRemaining(this.ItemID);
            int cdpercharge = GetCooldownPerCharge(GameState);
            int rechargedSecondCharge = -1;
            if (cd > cdpercharge)
                wardCharges = 0;
            else if (cd > 0)
            {
                wardCharges = 1;
                rechargedSecondCharge = cdpercharge - cd;
            } else
            {
                wardCharges = 2;
            }

            if (wardCharges > 0)
            {
                if (wardCharges > 1)
                {
                    ItemCooldownController.SetCooldown(ITEM_ID, GetCooldownPerCharge(GameState) + 1800); // Warding small 2s cooldown
                }
                else
                {
                    // some magic here regarding trinket cooldowns to handle edge cases when you swap trinkets.
                    ItemCooldownController.SetCooldown(ITEM_ID, GetCooldownPerCharge(GameState) * 2 - 100);
                    ItemCooldownController // this trinket affects the other trinket cooldowns
                        .SetCooldown(
                                        FarsightAlterationModule.ITEM_ID,
                                        FarsightAlterationModule.GetCooldownDuration(ItemCooldownController.GetAverageChampionLevel(GameState)) - rechargedSecondCharge - 100);
                    ItemCooldownController
                        .SetCooldown(
                                        OracleLensModule.ITEM_ID,
                                        OracleLensModule.GetCooldownDuration(ItemCooldownController.GetAverageChampionLevel(GameState)) - rechargedSecondCharge - 100);

                    //CooldownDuration = cooldownPerCharge - 100; // substract some duration to account for other delays;
                }
                wardCharges--;

            }
            
        }

        public bool HasCharge => ItemCooldownController.GetCooldownRemaining(ITEM_ID) < GetCooldownPerCharge(GameState);

        private int GetCooldownPerCharge(GameState state)
        {
            return GetCooldownDuration(ItemCooldownController.GetAverageChampionLevel(state));
        }

        public static int GetCooldownDuration(double averageLevel)
        {
            return (int)((247.059 - 7.059 * averageLevel) * 1000);
        }

        public static WardingTotemModule Current { get; set; } // HACK: Access to current instance
    }
}
