using Games.LeagueOfLegends.ItemModules;
using Games.LeagueOfLegends.Model;
using FirelightCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace Games.LeagueOfLegends
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

        static readonly List<int> goldMouseLights = new List<int>()
        {
            0,1
        };

        private LEDData lastFrame;

        public LEDFrame DoFrame(GameState gameState)
        {
            LEDData data = LEDData.Empty;
            HealthBar(data, lastFrame, gameState);
            WardView(data, gameState);
            GoldView(data, gameState);
            lastFrame = data;
            //data.Mouse[0].Color(HealthColor);
            return new LEDFrame(this, data, LightZone.All, true);
        }

        private void GoldView(LEDData data, GameState gameState)
        {
            HSVColor col = HSVColor.Black;
            if (gameState.ActivePlayer.CurrentGold >= GoldNotificationThreshold)
            {
                col = GoldColor;
                foreach (int k in goldKeys)
                {
                    data.Keyboard[k].Color(col);
                }

                // mouse
                for (int i = 0; i < LEDData.NUMLEDS_MOUSE; i++)
                {
                    data.Mouse[i].Color(GoldColor);
                }
            }
        }

        private static void WardView(LEDData data, GameState gameState)
        {
            //if (lightMode != LightingMode.Keyboard) return; // TODO: Implement some sort of notification for LED strip perhaps

            Item trinket = gameState.PlayerChampion.Items.FirstOrDefault(x => x.Slot == 6);
            if (trinket == null)
            {
                // if there is no trinket, set to black
                foreach (int k in trinketKeys)
                {
                    data.Keyboard[k].SetBlack();
                }
            }
            else
            {
                HSVColor col = HSVColor.Black;
                if (trinket.ItemID == WardingTotemModule.ITEM_ID)
                {
                    col = YellowTrinketColor;
                }
                else if (trinket.ItemID == OracleLensModule.ITEM_ID)
                {
                    col = RedTrinketColor;
                }
                else if (trinket.ItemID == FarsightAlterationModule.ITEM_ID)
                {
                    col = BlueTrinketColor;
                }
                else if (trinket.ItemID == HeraldEyeModule.ITEM_ID)
                {
                    col = HeraldColor;
                }
                // TODO: HANDLE HERALD EYE
                foreach (int k in trinketKeys)
                {
                    data.Keyboard[k].Color(col);
                }
            }

        }

        private static void UpdateHealthLed(Led[] arr, int i, int ledsToTurnOn, bool reversed = false)
        {
            int index = i;
            if (reversed)
                index = arr.Length - 1 - i;
            if (i < ledsToTurnOn)
                arr[index].MixNewColor(HealthColor, true, 0.2f);
            else
            {
                if (arr[index].color.AlmostEqual(HealthColor))
                {
                    arr[index].Color(HurtColor);
                }
                else
                {
                    arr[index].FadeToBlackBy(0.05f);
                }
            }
        }

        private static List<int> alreadyTouchedLeds = new List<int>(); // fixes a weird flickering bug
        private static void HealthBar(LEDData data, LEDData lastFrame, GameState gameState)
        {
            if (lastFrame != null)
            {
                data.Keyboard = lastFrame.Keyboard;
                data.Mouse = lastFrame.Mouse;
                data.Mousepad = lastFrame.Mousepad;
                data.Strip = lastFrame.Strip;
            }
            float maxHealth = gameState.ActivePlayer.Stats.MaxHealth;
            float currentHealth = gameState.ActivePlayer.Stats.CurrentHealth;
            float healthPercentage = currentHealth / maxHealth;
            alreadyTouchedLeds.Clear();

            // KEYBOARD LIGHTING
            int greenHPLeds = Math.Max((int)Utils.Scale(healthPercentage, 0, 1, 0, 16), 1); // at least one led active when player is alive
            for (int i = 0; i < 16; i++)
            {
                List<int> listLedsToTurnOn = new List<int>(6); // light the whole column
                for (int j = 0; j < 6; j++)
                {
                    listLedsToTurnOn.Add(KeyUtils.PointToKey(new Point(i, j)));
                }

                if (i < greenHPLeds)
                {
                    foreach (int idx in listLedsToTurnOn.Where(x => x != -1))
                    {
                        if (alreadyTouchedLeds.Contains(idx))
                            continue;
                        data.Keyboard[idx].MixNewColor(HealthColor, true, 0.2f);
                    }
                }
                else
                {
                    foreach (int idx in listLedsToTurnOn.Where(x => x != -1))
                    {
                        if (alreadyTouchedLeds.Contains(idx))
                            continue;
                        if (data.Keyboard[idx].color.AlmostEqual(HealthColor))
                        {
                            data.Keyboard[idx].Color(HurtColor);
                        }
                        else
                        {
                            data.Keyboard[idx].FadeToBlackBy(0.08f);
                        }

                    }
                }
                alreadyTouchedLeds.AddRange(listLedsToTurnOn);

            }
            int ledsToTurnOn;
            // MOUSE LIGHTING
            if (gameState.ActivePlayer.CurrentGold < GoldNotificationThreshold)
            {
                data.Mouse[0].Color(HealthColor);
                data.Mouse[1].Color(HealthColor);
                ledsToTurnOn = Math.Max((int)(healthPercentage * 7), 1);
                for (int i = 0; i < 7; i++)
                {
                    UpdateHealthLed(data.Mouse, i, ledsToTurnOn, true);
                }
                for (int i = 0; i < 7; i++)
                {
                    UpdateHealthLed(data.Mouse, i + 7, ledsToTurnOn + 7, true);
                }
            }

            // MOUSEPAD LIGHTING
            ledsToTurnOn = Math.Max((int)(healthPercentage * LEDData.NUMLEDS_MOUSEPAD), 1);
            for (int i = 0; i < LEDData.NUMLEDS_MOUSEPAD; i++)
            {
                UpdateHealthLed(data.Mousepad, i, ledsToTurnOn);
            }

            // LED STRIP LIGHTING
            ledsToTurnOn = Math.Max((int)(healthPercentage * LEDData.NUMLEDS_STRIP), 1);
            for (int i = 0; i < LEDData.NUMLEDS_STRIP; i++)
            {
                UpdateHealthLed(data.Strip, i, ledsToTurnOn);
            }
        }
    }
}
