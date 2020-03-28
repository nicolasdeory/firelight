using LedDashboard.Modules.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboard.Modules.BasicAnimation
{
    class AnimationModule : LEDModule
    {
        public event LEDModule.FrameReadyHandler NewFrameReady;

        Dictionary<string,Animation> LoadedAnimations = new Dictionary<string, Animation>();
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
        public void RunAnimationOnce(string animPath, bool keepTail = false, float fadeOutAfterRate = 0)
        {
            Animation anim = LoadAnimation(animPath);

            if (currentlyRunningAnim != null) currentlyRunningAnim.Cancel();
            currentlyRunningAnim = new CancellationTokenSource();
            //isAnimationRunning = true;
            Task.Run(() => PlayOnce(anim, currentlyRunningAnim.Token)).ContinueWith(async (t) =>
            {
                if (!keepTail)
                {
                    if (fadeOutAfterRate > 0)
                    {
                        await FadeOutToBlack(fadeOutAfterRate, currentlyRunningAnim.Token);
                    } else
                    {
                        this.leds.SetAllToBlack();
                        NewFrameReady.Invoke(this, this.leds);
                    }
                }
                //isAnimationRunning = false;
            });
        }

        /// <summary>
        /// Plays the specified animation once for <paramref name="loopDuration"/> milliseconds.
        /// </summary>
        /// <param name="fadeOutAfterRate">Optionally fade out the last animation frame progressively.</param>
        public void RunAnimationInLoop(string animPath, int loopDuration, float fadeOutAfterRate = 0)
        {

            Animation anim = LoadAnimation(animPath);

            if (currentlyRunningAnim != null) currentlyRunningAnim.Cancel();
            currentlyRunningAnim = new CancellationTokenSource();
            //isAnimationRunning = true;
            var t = Task.Run(() => PlayLoop(anim, currentlyRunningAnim.Token, loopDuration)).ContinueWith(async (t) =>
            {

                if (fadeOutAfterRate > 0)
                {
                    await FadeOutToBlack(fadeOutAfterRate, currentlyRunningAnim.Token);
                }
                else
                {
                    this.leds.SetAllToBlack();
                    NewFrameReady.Invoke(this, this.leds);
                }
                //isAnimationRunning = false;
            });


        }
        
        /// <summary>
        /// Cancels the currently running animation and sets the LEDs to black.
        /// </summary>
        public void StopCurrentAnimation()
        {
            if (currentlyRunningAnim != null)
            {
                currentlyRunningAnim.Cancel();
                this.leds.SetAllToBlack();
                NewFrameReady.Invoke(this, this.leds);
                Task.Run(async () =>
                {
                    // wait a bit for the current frame
                    await Task.Delay(50);
                    NewFrameReady.Invoke(this, this.leds);
                });
                
                //isAnimationRunning = false;
            }
        }

        public void AlternateBetweenTwoColors(HSVColor col1, HSVColor col2, float duration = 2000, float fadeRate = 0.15f) // -1 duration for indefinite
        {
            if (currentlyRunningAnim != null) currentlyRunningAnim.Cancel();
            currentlyRunningAnim = new CancellationTokenSource();
            Task.Run(() => FadeBetweenTwoColors(fadeRate, col1, col2, duration, currentlyRunningAnim.Token));
        }

        /// <summary>
        /// Creates a color burst, starting at the given color and progressively fading to black.
        /// </summary>
        /// <param name="color">The burst color.</param>
        /// <param name="fadeoutRate">The burst fade-out rate.</param>
        /// <param name="destinationColor">The color to progressively fade to after the color burst (black by default)</param>
        /// <returns></returns>
        public Task ColorBurst(HSVColor color, float fadeoutRate = 0.15f, HSVColor destinationColor = default)
        {
            if (currentlyRunningAnim != null) currentlyRunningAnim.Cancel();
            currentlyRunningAnim = new CancellationTokenSource();
            //isAnimationRunning = true;
            return Task.Run(async () =>
            {
                foreach (Led l in this.leds)
                {
                    l.Color(color);
                }
                NewFrameReady.Invoke(this, this.leds);
                if (fadeoutRate > 0)
                {
                    if (destinationColor.Equals(HSVColor.Black))
                    {
                        await FadeOutToBlack(fadeoutRate, currentlyRunningAnim.Token);
                    } else
                    {
                        await FadeOutToColor(fadeoutRate, destinationColor, currentlyRunningAnim.Token);
                    }
                    
                }
                else
                {
                    if (!destinationColor.Equals(HSVColor.Black))
                    {
                        this.leds.SetAllToColor(destinationColor);
                    } else
                    {
                        this.leds.SetAllToBlack();
                    }
                    NewFrameReady.Invoke(this, this.leds);
                }
            });
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
        private async Task PlayOnce(Animation anim, CancellationToken cancelToken)
        {

                int i = 0;
                while (true)
                {
                    for (int j = 0; j < leds.Length; j++)
                    {
                        if (leds.Length == anim.FrameLength)
                        {
                            leds[j].Color(anim[i][j]);
                        }
                        else
                        {
                            // Scaling animation to fit led count
                            leds[j].Color(HSVColor.Black);
                            int index = (int)Utils.Scale(j, 0, leds.Length, 0, anim.FrameLength);
                            leds.AddColorToLedsAround(j, anim[i][index], 5);

                        }
                    }
                    i++;
                    if (cancelToken.IsCancellationRequested) throw new TaskCanceledException();
                    NewFrameReady.Invoke(this, this.leds);
                    await Task.Delay(30);
                    if (i == anim.FrameCount) break;
                }
                //currentlyRunningAnim = null;

            
        }

        /// <summary>
        /// Plays the specified animation in loop for <paramref name="durationMs"/> milliseconds.
        /// </summary>
        private async Task PlayLoop(Animation anim, CancellationToken cancelToken, int durationMs = 1000)
        {

                int i = 0;
                int msCounter = 0;
                while (true)
                {
                    for (int j = 0; j < leds.Length; j++)
                    {
                        if (leds.Length == anim.FrameLength)
                        {
                            leds[j].Color(anim[i][j]);
                        }
                        else
                        {
                            // Scaling animation to fit led count
                            leds[j].Color(HSVColor.Black);
                            int index = (int)Utils.Scale(j, 0, leds.Length, 0, anim.FrameLength);
                            leds.AddColorToLedsAround(j, anim[i][index], 5);

                        }
                    }
                    i = (i + 1) % anim.FrameCount;
                    if (cancelToken.IsCancellationRequested) throw new TaskCanceledException();
                    NewFrameReady.Invoke(this, this.leds);
                    await Task.Delay(30);
                    msCounter += 30;
                    if (msCounter >= durationMs) break;
                }
                //currentlyRunningAnim = null;            
        }


        private async Task FadeOutToBlack(float rate, CancellationToken cancelToken)
        {
            int msCounter = 0;
            int fadeoutTime = (int)(GetFadeToBlackTime(rate) * 1000);
            while(msCounter < fadeoutTime) // TODO: calculate animation duration (rn its 1s) 
            {
                this.leds.FadeToBlackAllLeds(rate);
                if (cancelToken.IsCancellationRequested) throw new TaskCanceledException();
                NewFrameReady.Invoke(this, this.leds);
                await Task.Delay(30);
                msCounter += 30;
            }
            this.leds.SetAllToBlack();

        }

        private async Task FadeOutToColor(float rate, HSVColor color, CancellationToken cancelToken) 
        {
            int msCounter = 0;
            int fadeoutTime = (int)(GetFadeToBlackTime(rate) * 1000);
            while (msCounter < fadeoutTime) // TODO: calculate animation duration (rn its 1s) 
            {
                this.leds.FadeToColorAllLeds(color,rate);
                if (cancelToken.IsCancellationRequested) throw new TaskCanceledException();
                NewFrameReady.Invoke(this, this.leds);
                await Task.Delay(30);
                msCounter += 30;
            }
            this.leds.SetAllToColor(color);
        }

        // Set duration to -1 for indefinite
        private async Task FadeBetweenTwoColors(float rate, HSVColor col1, HSVColor col2, float duration, CancellationToken cancelToken)
        {
            int msCounter = 0;
            while ((duration >= 0 && msCounter < duration) || duration < 0)
            {
                float sin = (float)Math.Sin((msCounter/1000f) * rate);
                this.leds.SetAllToColor(HSVColor.Lerp(col1, col2, sin));
                if (cancelToken.IsCancellationRequested) throw new TaskCanceledException();
                NewFrameReady.Invoke(this, this.leds);
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
            return (4 / rate) * ((float)ms/1000); // 4 is a constant which is approximately ln(0.01). A formula to determine the approximate fadeout time.

        }

        public void Dispose()
        {
            currentlyRunningAnim.Cancel();
        }
    }
}
