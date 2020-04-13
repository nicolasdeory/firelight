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
    class HeraldEyeModule : ItemModule
    {
        public const int ITEM_ID = 3513;
        public const string ITEM_NAME = "HeraldEye";

        // Variables

        HSVColor PurpleColor = new HSVColor(0.81f, 0.8f, 1);
        bool wasCast = false;
        bool didWarning = false;

        // Cooldown

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static HeraldEyeModule Create(int ledCount, GameState gameState, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new HeraldEyeModule(ledCount, gameState, ITEM_ID, itemSlot, preferredLightMode, preferredCastMode);
        }

        private HeraldEyeModule(int ledCount, GameState gameState, int itemID, int itemSlot, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, itemID, ITEM_NAME, itemSlot, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the item module occurs here.

            ItemCooldownController.SetCooldown(ITEM_ID, 240000);
        }

        protected override AbilityCastMode GetItemCastMode() => AbilityCastMode.Instant();

        protected override void OnItemActivated(object s, EventArgs e)
        {
            // Play relevant animations here
            wasCast = true;
            Task.Run(async () =>
            {
                await RunAnimationOnce("anim_1", true);
                await Task.Delay(1200);
                await RunAnimationOnce("anim_2", true);
                await Task.Delay(1200);
                await RunAnimationOnce("anim_3", true);
                await Task.Delay(1600);
                await RunAnimationOnce("anim_4", false, 0.08f);
            });
        }

        protected override void OnGameStateUpdated(GameState state) // TODO: Handle when player buys a different trinket and cooldown gets transferred over
        {
            // Check the cooldown
            if (ItemCooldownController.GetCooldownRemaining(ITEM_ID) < 30000 && !didWarning)
            {
                didWarning = true;
                RequestLEDActivation(); // needed for showing animations
                Task.Run(async () =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (wasCast)
                            return;
                        await Animator.HoldColor(PurpleColor, 300);
                        if (wasCast)
                            return;
                        Animator.StopCurrentAnimation();
                        await Task.Delay(300);
                    }
                }); 
            }
        }
    }
}
