using LedDashboardCore.Modules.BasicAnimation;
using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LedDashboardCore;

namespace Games.LeagueOfLegends.ItemModules
{
    [Item(ITEM_ID)]
    class OracleLensModule : ItemModule
    {
        public const int ITEM_ID = 3364;
        public const string ITEM_NAME = "OracleLens";

        // Variables


        // Cooldown

        public OracleLensModule(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, ITEM_ID, ITEM_NAME, itemSlot, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the item module occurs here.
        }

        protected override AbilityCastMode GetItemCastMode() => AbilityCastMode.Instant();

        protected override void OnItemActivated(object s, EventArgs e)
        {
            if (ItemCooldownController.IsOnCooldown(ITEM_ID)) return; // If the item is on cooldown, nothing to do here.
            // Play relevant animations here
            RunAnimationInLoop("activation", 8500);

            double avgChampLevel = GameState.AverageChampionLevel;
            ItemCooldownController.SetCooldown(ITEM_ID, GetCooldownDuration(avgChampLevel));
            ItemCooldownController.SetCooldown(FarsightAlterationModule.ITEM_ID, 
                                               FarsightAlterationModule.GetCooldownDuration(avgChampLevel));
        }

        protected override void OnGameStateUpdated(GameState state) // TODO: Handle when player buys a different trinket and cooldown gets transferred over
        {
            // Normally you wouldn't need to do this, but since some trinket cooldowns depend on
            // average champion level, we need to contemplate this.
            CooldownDuration = GetCooldownDuration(state.AverageChampionLevel);
        }

        public static int GetCooldownDuration(double averageLevel) => GetCooldownDuration(91.765, 1.765, averageLevel);
    }
}
