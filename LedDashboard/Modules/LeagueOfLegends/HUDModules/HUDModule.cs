using LedDashboard.Modules.LeagueOfLegends.Constants;
using LedDashboard.Modules.LeagueOfLegends.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.HUDModules
{
    /// <summary>
    /// The league of legends HUD lights are handled here. This is responsible for rendering
    /// the HUD elements, such as the health bar or ward view.
    /// </summary>
    class HUDModule
    {
        const int GoldNotificationThreshold = 1300; // When player has more than this gold, keyboard lights up yellow

        static HSVColor HealthColor = new HSVColor(0.29f, 0.79f, 1f);
        static HSVColor HurtColor = new HSVColor(0.09f, 0.8f, 1f);

        static HSVColor YellowTrinketColor = new HSVColor(0.15f, 0.8f, 1f);
        static HSVColor RedTrinketColor = new HSVColor(0.01f, 0.8f, 1f);
        static HSVColor BlueTrinketColor = new HSVColor(0.58f, 0.8f, 1f);
        static HSVColor HeraldColor = new HSVColor(0.81f, 0.8f, 1);

        static HSVColor GoldColor = new HSVColor(0.11f, 0.8f, 1f);

        static readonly List<int> trinketKeys = new List<int>()
        {
            KeyUtils.PointToKey(new Point(16,0)),
            KeyUtils.PointToKey(new Point(16,1)),
            KeyUtils.PointToKey(new Point(16,2)),
            KeyUtils.PointToKey(new Point(17,0)),
            KeyUtils.PointToKey(new Point(17,1)),
            KeyUtils.PointToKey(new Point(17,2)),
            KeyUtils.PointToKey(new Point(18,0)),
            KeyUtils.PointToKey(new Point(18,1)),
            KeyUtils.PointToKey(new Point(18,2)),
        };

        static readonly List<int> goldKeys = new List<int>()
        {
            KeyUtils.PointToKey(new Point(17,4)),
            KeyUtils.PointToKey(new Point(16,5)),
            KeyUtils.PointToKey(new Point(17,5)),
            KeyUtils.PointToKey(new Point(18,5)),
        };

        public static void DoFrame(Led[] leds, LightingMode lightMode, GameState gameState)
        {
            HealthBar(leds, lightMode, gameState);
            WardView(leds, lightMode, gameState);
            GoldView(leds, lightMode, gameState);
        }

        private static void GoldView(Led[] leds, LightingMode lightMode, GameState gameState)
        {
            if (lightMode == LightingMode.Keyboard)
            {
                HSVColor col = HSVColor.Black;
                if (gameState.ActivePlayer.CurrentGold >= GoldNotificationThreshold)
                {
                    col = GoldColor;
                }
                foreach (int k in goldKeys)
                {
                    leds[k].Color(col);
                }
            }
            
        }

        private static void WardView(Led[] leds, LightingMode lightMode, GameState gameState)
        {
            if (lightMode != LightingMode.Keyboard) return; // TODO: Implement some sort of notification for LED strip perhaps

            Item trinket = gameState.PlayerChampion.Items.FirstOrDefault(x => x.Slot == 6);
            if (trinket == null || 
                                    (ItemCooldownController.IsSlotOnCooldown(6) 
                                    && trinket.ItemID != ItemModules.WardingTotemModule.ITEM_ID 
                                    && trinket.ItemID != ItemModules.HeraldEyeModule.ITEM_ID)) 
            {
                // if trinket is on cooldown, set black
                foreach (int k in trinketKeys)
                {
                    leds[k].SetBlack();
                }
            }
            else
            {
                HSVColor col = HSVColor.Black;
                if (trinket.ItemID == (int)TrinketItemID.YellowTrinket)
                {
                    if (ItemModules.WardingTotemModule.Current.HasCharge)
                        col = YellowTrinketColor;
                }
                else if (trinket.ItemID == (int)TrinketItemID.RedTrinket)
                {
                    col = RedTrinketColor;
                }
                else if (trinket.ItemID == (int)TrinketItemID.BlueTrinket)
                {
                    col = BlueTrinketColor;
                }
                else if (trinket.ItemID == (int)TrinketItemID.RiftHerald)
                {
                    col = HeraldColor;
                }
                // TODO: HANDLE HERALD EYE
                foreach (int k in trinketKeys)
                {
                    leds[k].Color(col);
                }
            }
            
        }

        private static List<int> alreadyTouchedLeds = new List<int>(); // fixes a weird flickering bug
        private static void HealthBar(Led[] leds, LightingMode lightMode, GameState gameState)
        {
            float maxHealth = gameState.ActivePlayer.Stats.MaxHealth;
            float currentHealth = gameState.ActivePlayer.Stats.CurrentHealth;
            float healthPercentage = currentHealth / maxHealth;
            alreadyTouchedLeds.Clear();
            if (lightMode == LightingMode.Keyboard)
            {
                int greenHPLeds = Math.Max((int)Utils.Scale(healthPercentage, 0, 1, 0, 16), 1); // at least one led active when player is alive
                for (int i = 0; i < 16; i++)
                {
                    List<int> ledsToTurnOn = new List<int>(6); // light the whole column
                    for (int j = 0; j < 6; j++)
                    {
                        ledsToTurnOn.Add(KeyUtils.PointToKey(new Point(i, j)));
                    }

                    if (i < greenHPLeds)
                    {
                        foreach (int idx in ledsToTurnOn.Where(x => x != -1))
                        {
                            if (alreadyTouchedLeds.Contains(idx))
                                continue;
                            leds[idx].MixNewColor(HealthColor, true, 0.2f);
                        }
                    }
                    else
                    {
                        foreach (int idx in ledsToTurnOn.Where(x => x != -1))
                        {
                            if (alreadyTouchedLeds.Contains(idx))
                                continue;
                            if (leds[idx].color.AlmostEqual(HealthColor))
                            {
                                leds[idx].Color(HurtColor);
                            }
                            else
                            {
                                leds[idx].FadeToBlackBy(0.08f);
                            }

                        }
                    }
                    alreadyTouchedLeds.AddRange(ledsToTurnOn);

                }
            }
            else
            {
                int ledsToTurnOn = Math.Max((int)(healthPercentage * leds.Length), 1);
                for (int i = 0; i < leds.Length; i++)
                {
                    if (i < ledsToTurnOn)
                        leds[i].MixNewColor(HealthColor, true, 0.2f);
                    else
                    {
                        if (leds[i].color.AlmostEqual(HealthColor))
                        {
                            leds[i].Color(HurtColor);
                        }
                        else
                        {
                            leds[i].FadeToBlackBy(0.05f);
                        }
                    }
                }
            }
        }
    }
}
