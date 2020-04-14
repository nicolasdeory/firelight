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
    class FarsightAlterationModule : ItemModule
    {
        public const int ITEM_ID = 3363;
        public const string ITEM_NAME = "FarsightAlteration";

        // Variables


        // Cooldown

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static FarsightAlterationModule Create(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new FarsightAlterationModule(ledCount, gameState, itemSlot, preferredLightMode, preferredCastMode);
        }

        private FarsightAlterationModule(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, ITEM_ID, ITEM_NAME, itemSlot, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the item module occurs here.
        }

        protected override AbilityCastMode GetItemCastMode() => AbilityCastMode.Normal();

        protected override void OnItemActivated(object s, EventArgs e)
        {
            // Play relevant animations here
            RunAnimationOnce("activation", false, 0.05f);

            double avgChampLevel = GameState.AverageChampionLevel;
            ItemCooldownController.SetCooldown(ITEM_ID, GetCooldownDuration(avgChampLevel));
            ItemCooldownController.SetCooldown(OracleLensModule.ITEM_ID,
                                               OracleLensModule.GetCooldownDuration(avgChampLevel));
        }

        protected override void OnGameStateUpdated(GameState state) // TODO: Handle when player buys a different trinket and cooldown gets transferred over
        {
            // Normally you wouldn't need to do this, but since some trinket cooldowns depend on
            // average champion level, we need to contemplate this.
            CooldownDuration = OracleLensModule.GetCooldownDuration(state.AverageChampionLevel);
        }

        public static int GetCooldownDuration(double averageLevel) => GetCooldownDuration(203.824, 5.824, averageLevel);
    }
}
