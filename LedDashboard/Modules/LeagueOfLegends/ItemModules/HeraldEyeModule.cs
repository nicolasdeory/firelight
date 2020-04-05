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
                            : base(itemID, itemSlot, gameState, preferredLightMode)
        {
            // Initialization for the item module occurs here.

            PreferredCastMode = preferredCastMode;

            ItemCast += OnItemActivated;
            GameStateUpdated += OnGameStateUpdated;

            ItemCastMode = AbilityCastMode.Instant();

            ItemCooldownController.SetCooldown(ITEM_ID, 240000);

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls, mode);

            animator.PreloadAnimation(ITEM_ANIMATION_PATH + "HeraldEye/anim_1.txt");
            animator.PreloadAnimation(ITEM_ANIMATION_PATH + "HeraldEye/anim_2.txt");
            animator.PreloadAnimation(ITEM_ANIMATION_PATH + "HeraldEye/anim_3.txt");
            animator.PreloadAnimation(ITEM_ANIMATION_PATH + "HeraldEye/anim_4.txt");

            

        }

        private void OnItemActivated(object s, EventArgs e)
        {

            // Play relevant animations here
            wasCast = true;
            Task.Run(async () =>
            {
                await animator.RunAnimationOnce(ITEM_ANIMATION_PATH + "HeraldEye/anim_1.txt", true);
                await Task.Delay(1000);
                await animator.RunAnimationOnce(ITEM_ANIMATION_PATH + "HeraldEye/anim_2.txt", true);
                await Task.Delay(1000);
                await animator.RunAnimationOnce(ITEM_ANIMATION_PATH + "HeraldEye/anim_3.txt", true);
                await Task.Delay(1300);
                await animator.RunAnimationOnce(ITEM_ANIMATION_PATH + "HeraldEye/anim_4.txt",false, 0.08f);
            });

        }

        private void OnGameStateUpdated(GameState state) // TODO: Handle when player buys a different trinket and cooldown gets transferred over
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
                        if (wasCast) return;
                        await animator.HoldColor(PurpleColor, 300);
                        if (wasCast) return;
                        animator.StopCurrentAnimation();
                        await Task.Delay(300);
                    }
                }); 
            }
        }

    }
}
