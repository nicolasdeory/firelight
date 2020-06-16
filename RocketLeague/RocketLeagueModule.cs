using LedDashboardCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Games.RocketLeague
{
    // TODO: Refactor this into a separate project
    public class RocketLeague : BaseRLModule
    {

        // Constants

        public static HSVColor LoadingColor { get; } = new HSVColor(0.09f, 0.8f, 1f);

        public static HSVColor DeadColor { get; } = new HSVColor(0f, 0.8f, 0.77f);
        public static HSVColor NoManaColor { get; } = new HSVColor(0.52f, 0.66f, 1f);

        public static HSVColor KillColor { get; } = new HSVColor(0.06f, 0.96f, 1f);

        // Variables

        ulong msSinceLastExternalFrameReceived = 30000;
        ulong msAnimationTimerThreshold = 1500; // how long to wait for animation data until health bar kicks back in.
        //double currentGameTimestamp = 0;

        CancellationTokenSource masterCancelToken = new CancellationTokenSource();


        // Events

        /// <summary>
        /// Creates a new <see cref="RocketLeague"/> instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        public static RocketLeague Create(Dictionary<string, string> options)
        {
            return new RocketLeague();
        }

        private RocketLeague()
            : base()
        {
            // League of Legends integration Initialization

            AddAnimatorEvent();

            //PlayLoadingAnimation();
            _ = FrameTimer();
        }

        // plays it indefinitely
        private void PlayLoadingAnimation()
        {
            Animator.StopCurrentAnimation();
            Animator.HoldColor(NoManaColor, LightZone.All, 1);
        }

        /// <summary>
        /// Queries updated boost data
        /// </summary>
        private void QueryBoostInfo()
        {
            
        }

        /// <summary>
        /// Task that periodically updates the health bar.
        /// </summary>
        private async Task FrameTimer()
        {
            while (true)
            {
                if (masterCancelToken.IsCancellationRequested)
                    return;
                if (msSinceLastExternalFrameReceived >= msAnimationTimerThreshold)
                {
                    QueryBoostInfo();
                }
                await Task.Delay(30);
                msSinceLastExternalFrameReceived += 30;
            }
        }

       /* /// <summary>
        /// Sets a custom champion kill animation. If the player kills a champion within <paramref name="duration"/> ms,
        /// this animation will play instead of the default one. Useful for garen ult or chogath, for example.
        /// </summary>
        /// <param name="animPath">The animation path</param>
        public void SetCustomKillAnim(string animPath, int duration)
        {
            customKillAnimation = animPath;
            Task.Run(async () =>
            {
                await Task.Delay(duration);
                customKillAnimation = null;
            });
        }*/

        /// <summary>
        /// Called by a <see cref="LEDModule"/> when a new frame is available to be processed.
        /// </summary>
        /// <param name="s">Module that sent the message</param>
        /// <param name="data">LED data</param>
        protected override void NewFrameReadyHandler(LEDFrame frame)
        {
            // TODO: Make sure frame Sender is ChampionModule and NOT animationModule
           /* if ((frame.LastSender is ChampionModule && CurrentLEDSource is ItemModule item && !item.IsPriorityItem)) // Champion modules take priority over item casts... for the moment
            {
                CurrentLEDSource = (LEDModule)frame.LastSender;
            }
            if (frame.LastSender != Animator && frame.LastSender != CurrentLEDSource)
                return; // If it's from a different source that what we're listening to, ignore it*/
            InvokeNewFrameReady(frame);
            msSinceLastExternalFrameReceived = 0;
        }

        /// <summary>
        /// Processes game events such as kills
        /// </summary>
        /*private void ProcessGameEvents(bool firstTime = false)
        {
            if (firstTime)
            {
                if (GameState.GameEvents.Count > 0)
                    currentGameTimestamp = GameState.GameEvents.Last().EventTime;

                return;
            }
            foreach (Event ev in GameState.GameEvents)
            {
                if (ev.EventTime <= currentGameTimestamp)
                    continue;
                currentGameTimestamp = ev.EventTime;
                switch (ev.EventName)
                {
                    case "ChampionKill":
                        OnChampionKill(ev);
                        break;
                    default:
                        break;
                }
            }
        }*/

        /// <summary>
        /// Checks if a goal has been scored
        /// </summary>
        /// <returns></returns>
        private bool CheckForGoal()
        {
            // TODO
            throw new NotImplementedException();
        }

        private void OnGoal(bool whatTeam)
        {
           // TODO
        }
        
        public override void Dispose()
        {
            masterCancelToken.Cancel();
            Animator.StopCurrentAnimation();
            //championModule?.Dispose();
        }
    }
}
