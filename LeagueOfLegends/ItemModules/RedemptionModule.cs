using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ItemModules
{
    [Item(ITEM_ID)]
    public sealed class RedemptionModule : ItemModule
    {
        public const int ITEM_ID = 3107;
        public const string ITEM_NAME = "Redemption";

        // Variables


        // Cooldown

        public RedemptionModule(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, ITEM_ID, ITEM_NAME, itemSlot, gameState, preferredLightMode, preferredCastMode, true)
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
                    Debug.WriteLine("Waiting for item info...");
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
                    RunAnimationOnce("start", true, timeScale: 0.08f);
                    RunAnimationOnce("impact", false, 0.05f);
                });

                ItemCooldownController.SetCooldown(ITEM_ID, CooldownDuration);
            }
        }
    }
}
