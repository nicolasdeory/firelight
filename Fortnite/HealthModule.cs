using FirelightCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Games.Fortnite
{
    class HealthModule
    {
        // Constants

        public static HSVColor HealthColor { get; } = new HSVColor(0.28f, 1f, 1f);
        public static HSVColor ShieldColor { get; } = new HSVColor(0.55f, 1f, 1f);
        public static HSVColor DeadColor { get; } = new HSVColor(0.95f, 1f, 1f);
        public static HSVColor HurtColor { get; } = new HSVColor(0.1f, 1f, 1f);

        static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        private float[] ExtractStats(Bitmap screenFrame)
        {

            float shieldI = 1;
            for (float i = 0; i < 1; i += 0.01f)
            {
                Color c = screenFrame.GetPixel((int)(107 + (510 - 107) * i), 950);
                float h = c.GetHue();
                float s = c.GetSaturation();
                float br = c.GetBrightness();
                float brtol = Lerp(0.3f, 0.5f, i);
                float htol = Lerp(210f, 190f, i);
                if (br > brtol - 0.1f && br < brtol + 0.1f && h > htol - 10f && h < htol + 10f && s > 0.95f)
                    continue;
                shieldI = i;
                //Debug.WriteLine("i=" + i + " Br was " + br + " Hue was " + h + " Sat was " + s);
                break;
            }

            float hpI = 1;
            for (float i = 0; i < 1; i += 0.01f)
            {
                Color c = screenFrame.GetPixel((int)(107 + (510 - 107) * i), 980);
                float h = c.GetHue();
                float s = c.GetSaturation();
                float br = c.GetBrightness();
                float brtol = Lerp(0.43f, 0.65f, i);
                float htol = Lerp(122f, 90f, i);
                if (br > brtol - 0.1f && br < brtol + 0.1f && h > htol - 7f && h < htol + 7f && s > 0.5f)
                    continue;
                hpI = i;
                //Debug.WriteLine("i=" + i + " Br was " + br + " Hue was " + h + " Sat was " + s + " brtol " + brtol + " htol " + htol);
                break;
            }

            if (hpI != 0)
            {
                return new float[] { hpI, shieldI, -1 };
            }
            else
            {
                // DEAD
                float deadI = 1;
                for (float i = 0; i < 1; i += 0.01f)
                {
                    Color c = screenFrame.GetPixel((int)(107 + (510 - 107) * i), 980);
                    float h = c.GetHue();
                    float s = c.GetSaturation();
                    float br = c.GetBrightness();
                    float brtol = Lerp(0.43f, 0.5f, i);
                    float htol = Lerp(346f, 335f, i);
                    if (br > brtol - 0.1f && br < brtol + 0.1f && h > htol - 5f && h < htol + 5f && s > 0.9f)
                        continue;
                    deadI = i;
                    //Debug.WriteLine("i=" + i + " Br was " + br + " Hue was " + h + " Sat was " + s + " brtol " + brtol + " htol " + htol);
                    break;
                }

                return new float[] { hpI, shieldI, deadI };
            }
        }

        /// <summary>
        /// Gets the LEDFrame for the health view
        /// </summary>
        public LEDFrame DoFrame(Bitmap screenFrame)
        {
            float[] stats = ExtractStats(screenFrame);
            return GetLedFrameFromStats(stats);
        }

        private static List<int> alreadyTouchedLeds = new List<int>(); // fixes a weird flickering bug

        private LEDFrame GetLedFrameFromStats(float[] stats)
        {
            LEDFrame frame = LEDFrame.Empty;
            LEDData data = frame.Leds;


            alreadyTouchedLeds.Clear();
            double health = stats[0];
            double shield = stats[1];
            double dead = stats[2];

            // KEYBOARD

            if (health == 0 && shield == 0 && dead == 0)
                data.Keyboard.SetAllToColor(HSVColor.White);
            int keyboardHealthLeds = Math.Max(health > 0 ? 1 : 0, (int)Utils.Scale(health, 0, 1, 0, 18)); // ease in
            int keyboardShieldLeds = Math.Max(shield > 0 ? 1 : 0, (int)Utils.Scale(shield, 0, 1, 0, 18)); // ease in
            int keyboardDeadLeds = Math.Max(shield > 0 ? 1 : 0, (int)Utils.Scale(dead, 0, 1, 0, 18)); // ease in

            if (dead > 0)
            {
                FillKeyboardData(data, keyboardDeadLeds, 0, 6, DeadColor);
            } else
            {
                FillKeyboardData(data, keyboardShieldLeds, 0, 3, ShieldColor);
                FillKeyboardData(data, keyboardHealthLeds, 3, 6, HealthColor);
            }

            // MOUSE
            if (health == 0 && shield == 0 && dead == 0)
                data.Mouse.SetAllToColor(HSVColor.White);
            int mouseHealthLeds = (int)Utils.Scale(health, 0, 1, 0, 8);
            int mouseShieldLeds = (int)Utils.Scale(shield, 0, 1, 0, 8);
            int mouseDeadLeds = (int)Utils.Scale(dead, 0, 1, 0, 8);
            for (int i = 0; i < LEDData.NUMLEDS_MOUSE; i++)
            {
                data.Mouse[i].FadeToBlackBy(0.01f);
            }
            if (dead > 0)
            {
                data.Mouse[0].Color(DeadColor);
                data.Mouse[1].Color(DeadColor);
                for (int i = 0; i < mouseDeadLeds; i++)
                {
                    data.Mouse[2 + 6 - i].Color(DeadColor);
                    data.Mouse[2 + 7 + 6 - i].Color(DeadColor); // both sides of the mouse
                }
            } else
            {
                if (shield > 0)
                {
                    data.Mouse[0].Color(ShieldColor);
                    data.Mouse[1].Color(ShieldColor);
                } else
                {
                    data.Mouse[0].Color(HealthColor);
                    data.Mouse[1].Color(HealthColor);
                }
                for (int i = 0; i < mouseHealthLeds; i++)
                {
                    data.Mouse[2 + 6 - i].Color(HealthColor);
                    data.Mouse[2 + 7 + 6 - i].Color(HealthColor);
                }
                for (int i = 0; i < mouseShieldLeds; i++)
                {
                    data.Mouse[2 + 6 - i].Color(ShieldColor);
                    data.Mouse[2 + 7 + 6 - i].Color(ShieldColor);
                }
            }

            // MOUSEPAD

            if (health == 0 && shield == 0 && dead == 0)
                data.Mousepad.SetAllToColor(HSVColor.White);
            int mousepadHealthLeds = (int)Utils.Scale(health, 0, 1, 0, 17);
            int mousepadShieldLeds = (int)Utils.Scale(shield, 0, 1, 0, 17);
            int mousepadDeadLeds = (int)Utils.Scale(dead, 0, 1, 0, 17);
            if (dead > 0)
            {
                for (int i = 0; i < LEDData.NUMLEDS_MOUSEPAD; i++)
                {
                    if (i < mousepadDeadLeds)
                        data.Mousepad[i].Color(DeadColor);
                    else
                    {
                        if (data.Mousepad[i].color.AlmostEqual(DeadColor))
                        {
                            data.Mousepad[i].Color(HurtColor);
                        }
                        else
                        {
                            data.Mousepad[i].FadeToBlackBy(0.05f);
                        }
                    }
                }
            } else
            {
                for (int i = 0; i < mousepadHealthLeds; i++)
                {
                    data.Mousepad[i].Color(HealthColor);
                }

                for (int i = 0; i < mousepadShieldLeds; i++)
                {
                    data.Mousepad[i].Color(ShieldColor);
                }
            }

            // LED STRIP

            if (health == 0 && shield == 0 && dead == 0)
                data.Strip.SetAllToColor(HSVColor.White);
            int stripHealthLeds = (int)Utils.Scale(health, 0, 1, 0, LEDData.NUMLEDS_STRIP);
            int stripShieldLeds = (int)Utils.Scale(shield, 0, 1, 0, LEDData.NUMLEDS_STRIP);
            int stripDeadLeds = (int)Utils.Scale(dead, 0, 1, 0, LEDData.NUMLEDS_STRIP);
            if (dead > 0)
            {
                for (int i = 0; i < stripDeadLeds; i++)
                {
                    data.Strip[i].Color(DeadColor);
                }
            } else
            {
                for (int i = 0; i < stripHealthLeds; i++)
                {
                    data.Strip[i].Color(HealthColor);
                }
                for (int i = 0; i < stripShieldLeds; i++)
                {
                    data.Strip[i].Color(ShieldColor);
                }
            }

            // HEADSET

            if (health == 0 && shield == 0 && dead == 0)
                data.Headset.SetAllToColor(HSVColor.White);
            else
            {
                if (dead > 0)
                {
                    data.Headset.SetAllToColor(DeadColor);
                }
                else if (shield > 0)
                {
                    data.Headset.SetAllToColor(ShieldColor);
                }
                else
                {
                    data.Headset.SetAllToColor(HealthColor);
                }
            }

            // GENERAL

            if (health == 0 && shield == 0 && dead == 0)
                data.General.SetAllToColor(HSVColor.White);
            else
            {
                if (dead > 0)
                {
                    data.General.SetAllToColor(DeadColor);
                }
                else if (shield > 0)
                {
                    data.General.SetAllToColor(ShieldColor);
                }
                else
                {
                    data.General.SetAllToColor(HealthColor);
                }
            }

            return new LEDFrame(this, data, LightZone.All);
        }

        private static void FillKeyboardData(LEDData data, int endColumnn, int fromRow, int toRow, HSVColor color)
        {
            for (int i = 0; i < 16; i++)
            {
                List<int> listLedsToTurnOn = new List<int>(toRow-fromRow); // light the whole column
                for (int j = fromRow; j < toRow; j++)
                {
                    listLedsToTurnOn.Add(KeyUtils.PointToKey(new Point(i, j)));
                }

                listLedsToTurnOn = listLedsToTurnOn.Where(x => x != -1).ToList();

                if (i < endColumnn)
                {
                    foreach (int idx in listLedsToTurnOn)
                    {
                        if (alreadyTouchedLeds.Contains(idx))
                            continue;
                        data.Keyboard[idx].Color(color);
                    }
                }
                else
                {
                    foreach (int idx in listLedsToTurnOn)
                    {
                        if (alreadyTouchedLeds.Contains(idx))
                            continue;
                        if (data.Keyboard[idx].color.AlmostEqual(color))
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
        }
    }
}
