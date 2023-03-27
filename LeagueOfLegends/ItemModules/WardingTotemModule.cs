using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using FirelightCore;
using System;

namespace Games.LeagueOfLegends.ItemModules
{
    [Item(ITEM_ID)]
    public sealed class WardingTotemModule : ItemModule
    {
        public const int ITEM_ID = 3340;
        public const string ITEM_NAME = "WardingTotem";


        // Variables


        // Cooldown

        private int cooldownPerCharge = 0;

        public WardingTotemModule(GameState gameState, int itemSlot)
            : base(ITEM_ID, ITEM_NAME, itemSlot, gameState, true)
        {
            // Initialization for the item module occurs here.

            // TODO: This is because of a game bug, IT WILL GET FIXED and this will have to be changed
            ItemCooldownController.SetCooldown(this.ItemID, GetCooldownPerCharge());
        }

        protected override AbilityCastMode GetItemCastMode() => AbilityCastMode.Normal();

        protected override void OnItemActivated(object s, EventArgs e)
        {
            // TODO: Add an animation here and fix the HUD integration.
            int wardCharges = 0;
            int cd = ItemCooldownController.GetCooldownRemaining(this.ItemID);
            int cdpercharge = GetCooldownPerCharge();
            int rechargedSecondCharge = -1;
            if (cd > cdpercharge)
                wardCharges = 0;
            else if (cd > 0)
            {
                wardCharges = 1;
                rechargedSecondCharge = cdpercharge - cd;
            }
            else
            {
                wardCharges = 2;
            }

            if (wardCharges > 0)
            {
                if (wardCharges > 1)
                {
                    ItemCooldownController.SetCooldown(ITEM_ID, GetCooldownPerCharge() + 1800); // Warding small 2s cooldown
                }
                else
                {
                    // some magic here regarding trinket cooldowns to handle edge cases when you swap trinkets.
                    ItemCooldownController.SetCooldown(ITEM_ID, cdpercharge * 2 - rechargedSecondCharge - 100);
                    ItemCooldownController // this trinket affects the other trinket cooldowns
                        .SetCooldown(
                                        FarsightAlterationModule.ITEM_ID,
                                        FarsightAlterationModule.GetCooldownDuration(GameState.AverageChampionLevel) - rechargedSecondCharge - 100);
                    ItemCooldownController
                        .SetCooldown(
                                        OracleLensModule.ITEM_ID,
                                        OracleLensModule.GetCooldownDuration(GameState.AverageChampionLevel) - rechargedSecondCharge - 100);

                    //CooldownDuration = cooldownPerCharge - 100; // substract some duration to account for other delays;
                }
            }
        }

        public bool HasCharge => ItemCooldownController.GetCooldownRemaining(ITEM_ID) < GetCooldownPerCharge();

        private int GetCooldownPerCharge()
        {
            return GetCooldownDuration(GameState.AverageChampionLevel);
        }

        public static int GetCooldownDuration(double averageLevel) => GetCooldownDuration(247.059, 7.059, averageLevel);

        public static WardingTotemModule Current { get; set; } // HACK: Access to current instance
    }
}
