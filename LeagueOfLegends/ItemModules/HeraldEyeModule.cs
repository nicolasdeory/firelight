using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ItemModules
{
    [Item(ITEM_ID)]
    public sealed class HeraldEyeModule : ItemModule
    {
        public const int ITEM_ID = 3513;
        public const string ITEM_NAME = "HeraldEye";

        // Variables

        HSVColor PurpleColor = new HSVColor(0.81f, 0.8f, 1);
        bool wasCast = false;
        bool didWarning = false;

        // Cooldown

        public HeraldEyeModule(GameState gameState, int itemSlot, AbilityCastPreference preferredCastMode)
            : base(ITEM_ID, ITEM_NAME, itemSlot, gameState, preferredCastMode, true)
        {
            // Initialization for the item module occurs here.

            ItemCooldownController.SetCooldown(ITEM_ID, 240000);
        }

        protected override AbilityCastMode GetItemCastMode() => AbilityCastMode.Instant();

        protected override void OnItemActivated(object s, EventArgs e)
        {
            // Play relevant animations here
            wasCast = true;
            RunAnimationOnce("anim_1", LightZone.Keyboard);
            Animator.HoldLastFrame(LightZone.Keyboard, 1.2f);
            RunAnimationOnce("anim_2", LightZone.Keyboard);
            Animator.HoldLastFrame(LightZone.Keyboard, 1.2f);
            RunAnimationOnce("anim_3", LightZone.Keyboard);
            Animator.HoldLastFrame(LightZone.Keyboard, 1.6f);
            RunAnimationOnce("anim_4", LightZone.Keyboard, 3f);
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
                        Animator.HoldColor(PurpleColor, LightZone.All, 0.3f);
                        if (wasCast)
                            return;
                        Animator.HoldColor(HSVColor.Black, LightZone.All, 0.3f);
                        await Task.Delay(300);
                    }
                });
            }
        }
    }
}
