using LedDashboardCore.Modules.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboardCore.Modules.BasicAnimation
{
    public class AnimationModule : LEDModule
    {
        const int FPS = 33;

        public event LEDModule.FrameReadyHandler NewFrameReady;

        Dictionary<string, Animation> LoadedAnimations = new Dictionary<string, Animation>();

        /// <summary>
        /// Creates a new <see cref="AnimationModule"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of leds in the LED strip</param>
        public static AnimationModule Create()
        {
            return new AnimationModule();
        }

        private AnimationModule()
        {

        }

        /// <summary>
        /// Preloads the given animation so it doesn't need to be loaded on the fly.
        /// </summary>
        /// <param name="animPath">The animation path</param>
        public void PreloadAnimation(string animPath)
        {
            if (!LoadedAnimations.ContainsKey(animPath))
            {
                LoadedAnimations.Add(animPath, AnimationLoader.LoadFromFile(animPath));
            }
        }

        /// <summary>
        /// Plays the specified animation once.
        /// </summary>
        /// <param name="animPath">The animation path</param>
        /// <param name="keepTail">If set true, when the animation ends LEDs won't be set to black</param>
        public void RunAnimationOnce(string animPath, LightZone zones, bool keepTail = false, float fadeoutDuration = 0, float timeScale = 1)
        {
            LEDColorData[] anim = LoadAnimation(animPath).Frames;
            float time = 0;
            LEDData data = null;
            while (time < anim.Length)
            {
                int i = (int)time;
                data = LEDData.FromColors(anim[i]);
                if (i == 0)
                    SendFrame(data, zones, true);
                else
                    SendFrame(data, zones);
                time += 1 * timeScale;
            }
            if (!keepTail)
            {
                if (fadeoutDuration > 0)
                { 
                    // data is last frame
                    FadeOutToBlack(data, zones, fadeoutDuration);
                }
                else
                {
                    LEDData black = LEDData.Empty;
                    SendFrame(black, zones);
                }
            }
        }

        /// <summary>
        /// Plays the specified animation once for <paramref name="loopDuration"/> seconds.
        /// </summary>
        /// <param name="fadeOutAfterRate">Optionally fade out the last animation frame progressively.</param>
        public void RunAnimationInLoop(string animPath, LightZone zones, float loopDuration, float fadeoutDuration = 0, float timeScale = 1)
        {
            LEDColorData[] anim = LoadAnimation(animPath).Frames;
            float animDuration = anim.Length * 0.03f;
            float loopRatio = (int) (loopDuration / animDuration);
            float time = 0;
            LEDData data = null;
            while (time < anim.Length * loopRatio)
            {
                int i = ((int)time) % anim.Length;
                data = LEDData.FromColors(anim[i]);
                if (i == 0)
                    SendFrame(data, zones, true);
                else
                    SendFrame(data, zones);
                time += 1 * timeScale;
            }
            if (fadeoutDuration > 0)
            {
                // data is last frame
                FadeOutToBlack(data, zones, fadeoutDuration);
            }
            else
            {
                LEDData black = LEDData.Empty;
                SendFrame(black, zones);
            }
        }

        public void HoldColor(LightZone zones, HSVColor color, float duration, bool priority = false)
        {
            int frames = (int)Math.Round(duration * FPS);
            for (int i = 0; i < frames; i++)
            {
                LEDData data = LEDData.Empty;
                ApplyColorToZones(data, zones, color);
                SendFrame(data, zones, priority);
            }
        }

        /// <summary>
        /// Cancels the currently running animation and sets the LEDs to black.
        /// </summary>
        public void StopCurrentAnimation()
        {
            LEDData black = LEDData.Empty;
            SendFrame(black, LightZone.None, true);
        }

        /// <summary>
        /// Creates a color burst, starting at the given color and progressively fading to black.
        /// </summary>
        /// <param name="color">The burst color.</param>
        /// <param name="fadeoutRate">The burst fade-out rate.</param>
        /// <param name="destinationColor">The color to progressively fade to after the color burst (black by default)</param>
        /// <returns></returns>
        public void ColorBurst(HSVColor color, LightZone zones, float fadeoutRate = 0.15f, HSVColor destinationColor = default)
        {
            LEDData data = LEDData.Empty;

            // Set all to color
            ApplyColorToZones(data, zones, color);

            SendFrame(data, zones, true);

            // Fade to Black

            if (fadeoutRate > 0)
            {
                if (destinationColor.Equals(HSVColor.Black))
                {
                    FadeOutToBlack(data, zones, fadeoutRate);
                }
                else
                {
                    FadeOutToColor(data, zones, fadeoutRate, destinationColor);
                }
            }
            else
            {
                data = LEDData.Empty;
                if (!destinationColor.Equals(HSVColor.Black))
                {
                    ApplyColorToZones(data, zones, destinationColor);
                }
                SendFrame(data, zones);
            }
        }

        private void ApplyColorToZones(LEDData frameData, LightZone zones, HSVColor color)
        {
            List<Led[]> colArrays = frameData.GetArraysForZones(zones);
            foreach(Led[] arr in colArrays)
            {
                foreach(Led l in arr)
                {
                    l.Color(color);
                }
            }    
        }

        private void SendFrame(LEDData data, LightZone zones, bool priority = false)
        {
            NewFrameReady.Invoke(new LEDFrame(this, data, zones, priority));
        }


        private Animation LoadAnimation(string animPath)
        {
            if (!LoadedAnimations.ContainsKey(animPath))
            {
                LoadedAnimations.Add(animPath, AnimationLoader.LoadFromFile(animPath));
            }
            return LoadedAnimations[animPath];
        }

        private void FadeOutToBlack(LEDData frameData, LightZone zones, float fadeoutDuration)
        {
            FadeOutToColor(frameData, zones, fadeoutDuration, HSVColor.Black);
        }

        // fadeoutDuration in seconds
        private void FadeOutToColor(LEDData frameData, LightZone zones, float fadeoutDuration, HSVColor color)
        {
            int frames = (int)Math.Round(fadeoutDuration * FPS);
            List<Led[]> frameLightArrays = frameData.GetArraysForZones(zones);

            for (int i = 0; i < frames; i++)
            {
                float fadeout = (float)Utils.Scale(i, 0, frames, 0, 1);
                LEDData newFrame = LEDData.Empty;
                List<Led[]> newFrameLightArrays = newFrame.GetArraysForZones(zones);

                for (int j = 0; j < frameLightArrays.Count; j++)
                {
                    Led[] arrCurrent = frameLightArrays[j];
                    Led[] arrNew = newFrameLightArrays[j];
                    for (int k = 0; k < arrCurrent.Length; k++)
                    {
                        Led l = arrCurrent[k];
                        HSVColor fadedColor;
                        if (color.Equals(HSVColor.Black))
                        {
                            fadedColor = l.color.FadeToBlackBy(fadeout);
                        }
                        else
                        {
                            fadedColor = l.color.FadeToColorBy(color, fadeout);
                        }
                        arrNew[k].Color(fadedColor);
                    }
                }
                SendFrame(newFrame, zones);
            }
        }

        public void FadeBetweenTwoColors(LightZone zones, HSVColor col1, HSVColor col2, float rate = 0.15f, float duration = 2)
        {
            int frames = (int)Math.Round(duration * FPS);

            for (int i = 0; i < frames; i++)
            {
                float currentTime = i / FPS;
                float sin = (float)Math.Sin(currentTime * rate);
                HSVColor color = HSVColor.Lerp(col1, col2, sin);
                LEDData newFrame = LEDData.Empty;
                List<Led[]> newFrameLightArrays = newFrame.GetArraysForZones(zones);

                foreach (Led[] arr in newFrameLightArrays)
                {
                    foreach (Led l in arr)
                    {
                        l.Color(color);
                    }
                }
                SendFrame(newFrame, zones);
            }
        }
       
        public void Dispose() { }
    }
}