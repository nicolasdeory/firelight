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
        Led[] leds;

        CancellationTokenSource currentlyRunningAnim = new CancellationTokenSource();
        //bool isAnimationRunning = false;

        /// <summary>
        /// Creates a new <see cref="AnimationModule"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of leds in the LED strip</param>
        public static AnimationModule Create(int ledCount)
        {
            return new AnimationModule(ledCount);
        }

        private AnimationModule(int ledCount)
        {
            this.leds = new Led[ledCount];
            for (int i = 0; i < ledCount; i++)
                leds[i] = new Led();

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
        public Task RunAnimationOnce(string animPath, bool keepTail = false, float fadeOutAfterRate = 0, float timeScale = 1)
        {
            CleanCancellationToken();
            Animation anim = LoadAnimation(animPath);
            CancellationToken token = currentlyRunningAnim.Token;
            //isAnimationRunning = true;
            return TaskRunner.RunAsync(Task.Run(() => PlayOnce(anim, token, timeScale)).ContinueWith(async (t) =>
            {
                if (t.IsCanceled) return; // don't continue if task was cancelled
                if (!keepTail)
                {
                    if (fadeOutAfterRate > 0)
                    {
                        await FadeOutToBlack(fadeOutAfterRate, token, anim.AnimationMode);
                    }
                    else
                    {
                        this.leds.SetAllToBlack();
                        NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
                    }
                }
                //isAnimationRunning = false;
            }));
        }

        /// <summary>
        /// Plays the specified animation once for <paramref name="loopDuration"/> milliseconds.
        /// </summary>
        /// <param name="fadeOutAfterRate">Optionally fade out the last animation frame progressively.</param>
        public void RunAnimationInLoop(string animPath, int loopDuration, float fadeOutAfterRate = 0, float timeScale = 1)
        {
            CleanCancellationToken();
            Animation anim = LoadAnimation(animPath);
            CancellationToken token = currentlyRunningAnim.Token;
            var t = TaskRunner.RunAsync(Task.Run(() => PlayLoop(anim, token, loopDuration, timeScale)).ContinueWith(async (t) =>
            {
                if (t.IsCanceled) return; // don't continue if task was cancelled
                if (fadeOutAfterRate > 0)
                {
                    await FadeOutToBlack(fadeOutAfterRate, token, anim.AnimationMode);
                }
                else
                {
                    this.leds.SetAllToBlack();
                    NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
                }
                //isAnimationRunning = false;
            }));
        }

        private void CleanCancellationToken()
        {
            currentlyRunningAnim?.Cancel();
            currentlyRunningAnim = new CancellationTokenSource();
        }

        public Task HoldColor(HSVColor col, int durationMS)
        {
            CleanCancellationToken();
            //isAnimationRunning = true;
            return TaskRunner.RunAsync(Task.Run(async () =>
            {
                CancellationToken token = currentlyRunningAnim.Token;
                foreach (Led l in this.leds)
                {
                    l.Color(col);
                }
                int msCounter = 0;
                while (msCounter < durationMS)
                {
                    if (token.IsCancellationRequested) throw new TaskCanceledException();
                    NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
                    await Task.Delay(50);
                    msCounter += 50;
                }
            }));
        }

        /// <summary>
        /// Cancels the currently running animation and sets the LEDs to black.
        /// </summary>
        public void StopCurrentAnimation()
        {
            if (currentlyRunningAnim == null)
                return;

            currentlyRunningAnim.Cancel();
            this.leds.SetAllToBlack();
            NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
            TaskRunner.RunAsync(Task.Run(async () =>
            {
                // wait a bit for the current frame
                await Task.Delay(50);
                NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
            }));

            //isAnimationRunning = false;
        }

        public void AlternateBetweenTwoColors(HSVColor col1, HSVColor col2, float duration = 2000, float fadeRate = 0.15f) // -1 duration for indefinite
        {
            CleanCancellationToken();
            TaskRunner.RunAsync(Task.Run(() => FadeBetweenTwoColors(fadeRate, col1, col2, duration, currentlyRunningAnim.Token)));
        }

        /// <summary>
        /// Creates a color burst, starting at the given color and progressively fading to black.
        /// </summary>
        /// <param name="color">The burst color.</param>
        /// <param name="fadeoutRate">The burst fade-out rate.</param>
        /// <param name="destinationColor">The color to progressively fade to after the color burst (black by default)</param>
        /// <returns></returns>
        public Task ColorBurst(HSVColor color, LightZone zones, float fadeoutRate = 0.15f, HSVColor destinationColor = default)
        {
            LEDData data = LEDData.Empty;

            // Set all to color
            ApplyColorToZones(data, zones, color);

            SendFrame(new LEDFrame(this, data, zones, true));

            // Fade to Black

            if (fadeoutRate > 0)
            {
                if (destinationColor.Equals(HSVColor.Black))
                {
                    FadeOutToBlack(data, zones, fadeoutRate);
                }
                else
                {
                    FadeOutToColor(fadeoutRate, destinationColor, token);
                }
            }
            else
            {
                data = LEDData.Empty;
                if (!destinationColor.Equals(HSVColor.Black))
                {
                    ApplyColorToZones(data, zones, destinationColor);
                }
                SendFrame(new LEDFrame(this, data, zones));
            }


            // CleanCancellationToken();

            return TaskRunner.RunAsync(Task.Run(async () =>
            {
                CancellationToken token = currentlyRunningAnim.Token;
                foreach (Led l in this.leds)
                {
                    l.Color(color);
                }
                NewFrameReady.Invoke(new LEDFrame(this, new LEDData()))
                NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
                if (fadeoutRate > 0)
                {
                    if (destinationColor.Equals(HSVColor.Black))
                    {
                        await FadeOutToBlack(fadeoutRate, token, LightingMode.Line);
                    }
                    else
                    {
                        await FadeOutToColor(fadeoutRate, destinationColor, token);
                    }
                }
                else
                {
                    if (!destinationColor.Equals(HSVColor.Black))
                    {
                        this.leds.SetAllToColor(destinationColor);
                    }
                    else
                    {
                        this.leds.SetAllToBlack();
                    }
                    NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
                }
            }));
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

        private void SendFrame(LEDFrame frame)
        {
            NewFrameReady.Invoke(frame);
        }


        private Animation LoadAnimation(string animPath)
        {
            if (!LoadedAnimations.ContainsKey(animPath))
            {
                LoadedAnimations.Add(animPath, AnimationLoader.LoadFromFile(animPath));
            }
            return LoadedAnimations[animPath];
        }

        /// <summary>
        /// Plays the specified animation once.
        /// </summary>
        private async Task PlayOnce(Animation anim, CancellationToken cancelToken, float timeScale)
        {
            this.leds = new Led[anim.AnimationMode == LightingMode.Keyboard ? 88 : anim.FrameLength];
            for (int j = 0; j < leds.Length; j++)
            {
                this.leds[j] = new Led();
            }

            int frameTime = (int)(30 / timeScale);
            for (int i = 0; i < anim.FrameCount; i++)
            {
                for (int j = 0; j < this.leds.Length; j++)
                {
                    if (this.leds.Length == anim.FrameLength)
                    {
                        this.leds[j].Color(anim[i][j]);
                    }
                    else
                    {
                        // Scaling animation to fit led count
                        this.leds[j].Color(HSVColor.Black);
                        int index = (int)Utils.Scale(j, 0, this.leds.Length, 0, anim.FrameLength);
                        this.leds.AddColorToLedsAround(j, anim[i][index], 5);
                    }
                }
                if (cancelToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
                NewFrameReady.Invoke(this, this.leds, anim.AnimationMode);
                await Task.Delay(frameTime);
                if (cancelToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
            }
            //currentlyRunningAnim = null;
        }

        /// <summary>
        /// Plays the specified animation in loop for <paramref name="durationMs"/> milliseconds.
        /// </summary>
        private async Task PlayLoop(Animation anim, CancellationToken cancelToken, int durationMs, float timeScale)
        {
            this.leds = new Led[anim.AnimationMode == LightingMode.Keyboard ? 88 : anim.FrameLength];
            for (int j = 0; j < leds.Length; j++)
            {
                this.leds[j] = new Led();
            }

            int msCounter = 0;
            int frameTime = (int)(30 / timeScale);
            for (int i = 0; i < anim.FrameCount && msCounter < durationMs; i++)
            {
                for (int j = 0; j < this.leds.Length; j++)
                {
                    if (this.leds.Length == anim.FrameLength)
                    {
                        this.leds[j].Color(anim[i][j]);
                    }
                    else
                    {
                        // Scaling animation to fit led count
                        this.leds[j].Color(HSVColor.Black);
                        int index = (int)Utils.Scale(j, 0, this.leds.Length, 0, anim.FrameLength);
                        this.leds.AddColorToLedsAround(j, anim[i][index], 5);
                    }
                }
                if (cancelToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
                NewFrameReady.Invoke(this, this.leds, anim.AnimationMode);
                await Task.Delay(frameTime);
                if (cancelToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
                msCounter += frameTime;
            }
            //currentlyRunningAnim = null;            
        }


        private void FadeOutToBlack(LEDData frameData, LightZone zones, float fadeoutDuration)
        {
            FadeOutToColor(frameData, zones, fadeoutDuration, HSVColor.Black);
        }

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
                SendFrame(new LEDFrame(this, newFrame, zones));
            }
        }

        // Set duration to -1 for indefinite
        private async Task FadeBetweenTwoColors(float rate, HSVColor col1, HSVColor col2, float duration, CancellationToken cancelToken)
        {
            int msCounter = 0;
            while ((duration >= 0 && msCounter < duration) || duration < 0)
            {
                float sin = (float)Math.Sin((msCounter / 1000f) * rate);
                this.leds.SetAllToColor(HSVColor.Lerp(col1, col2, sin));
                if (cancelToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
                NewFrameReady.Invoke(this, this.leds, LightingMode.Line);
                await Task.Delay(30);
                msCounter += 30;
            }
        }

        /// <summary>
        /// Returns the time that it would take a led to fade out from max intensity value to black at the given rate.
        /// </summary>
        /// <param name="rate">The fadeout rate</param>
        /// <param name="ms">MS per frame</param>
        private float GetFadeToBlackTime(float rate, int ms = 30)
        {
            return (4 / rate) * ((float)ms / 1000); // 4 is a constant which is approximately ln(0.01). A formula to determine the approximate fadeout time.

        }

        public void Dispose()
        {
            currentlyRunningAnim.Cancel();
        }
    }
}