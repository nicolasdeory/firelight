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
    class RedemptionModule : ItemModule
    {
        public const int ITEM_ID = 3107;
        public const string ITEM_NAME = "Redemption";

        // Variables


        // Cooldown

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static RedemptionModule Create(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new RedemptionModule(ledCount, gameState, ITEM_ID, itemSlot, preferredLightMode, preferredCastMode);
        }

        private RedemptionModule(int ledCount, GameState gameState, int itemID, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, itemID, ITEM_NAME, itemSlot, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the item module occurs here.

            ItemCooldownController.SetCooldown(ITEM_ID, 0);
            WaitForItemInfo();
        }

        protected override AbilityCastMode GetItemCastMode() => AbilityCastMode.Normal();

        private void WaitForItemInfo()
        {
            Task.Run(async () =>
            {
                while (!ItemUtils.IsLoaded)
                {
                    Console.WriteLine("Waiting for item info...");
                    await Task.Delay(1000); // wait a bit to retrieve item info
                }
                // Set cooldown duration
                CooldownDuration = (int)(ItemUtils.GetItemAttributes(ITEM_ID).EffectAmounts[5] * 1000); // TODO: Maybe parse the cooldown from item desc?
            });
        }

        protected override void OnItemActivated(object s, EventArgs e) // TODO: Redemption can be used when dead!
        {
            if (!ItemCooldownController.IsOnCooldown(ITEM_ID))
            {
                // Play relevant animations here
                Task.Run(async () =>
                {
                    await RunAnimationOnce("start", true, timeScale: 0.08f);
                    RunAnimationOnce("impact", false, 0.05f);
                });

                ItemCooldownController.SetCooldown(ITEM_ID, CooldownDuration);
            }
        }
    }
}
