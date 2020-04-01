using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.Common;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using SharpDX.RawInput;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard.Modules.LeagueOfLegends.ChampionModules
{
    class VelKozModule : ChampionModule
    {
        
        // Variables
        
        // Champion-specific Variables

        bool qCastInProgress = false;
        bool rCastInProgress = false; // this is used to make the animation for Vel'Koz's R to take preference over other animations


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static VelKozModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new VelKozModule(ledCount, gameState, "Velkoz", preferredLightMode, preferredCastMode);
        }


        private VelKozModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode) 
                            : base(championName, gameState, preferredLightMode)
        {
            // Initialization for the champion module occurs here.

            // Set preferred cast mode. It's a player choice (Quick cast, Quick cast with indicator, or Normal cast)
            PreferredCastMode = preferredCastMode;

            // Set cast modes for abilities.
            // For Vel'Koz, for example:
            // Q -> Normal ability, but it can be recast within 1.15s
            // W -> Normal ability
            // E -> Normal ability
            // R -> Instant ability, it is cast the moment the key is pressed, but it can be recast within 2.3s
            Dictionary<AbilityKey, AbilityCastMode> abilityCastModes = new Dictionary<AbilityKey, AbilityCastMode>()
            {
                [AbilityKey.Q] = AbilityCastMode.Normal(1150), 
                [AbilityKey.W] = AbilityCastMode.Normal(),
                [AbilityKey.E] = AbilityCastMode.Normal(),
                [AbilityKey.R] = AbilityCastMode.Instant(2300),
            };
            AbilityCastModes = abilityCastModes;

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            animator.PreloadAnimation(ANIMATION_PATH + "Vel'Koz/q_start.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Vel'Koz/q_recast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Vel'Koz/w_cast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Vel'Koz/w_close.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Vel'Koz/r_loop.txt");


            ChampionInfoLoaded += OnChampionInfoLoaded;
        }

        /// <summary>
        /// Called when the champion info has been retrieved.
        /// </summary>
        private void OnChampionInfoLoaded(ChampionAttributes champInfo)
        {
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls,mode);
            AbilityCast += OnAbilityCast;
            AbilityRecast += OnAbilityRecast;
        }
        
        /// <summary>
        /// Called when an ability is cast.
        /// </summary>
        private void OnAbilityCast(object s, AbilityKey key)
        {
            if (key == AbilityKey.Q)
            {
                OnCastQ();
            }
            if (key == AbilityKey.W)
            {
                OnCastW();
            }
            if (key == AbilityKey.E)
            {
                OnCastE();
            }
            if (key == AbilityKey.R)
            {
                OnCastR();
            }
        }

        private void OnCastQ()
        {
            // Here you should write code to trigger the appropiate animations to play when the user casts Q.
            // The code will slightly change between each champion, because you might want to implement custom animation logic.

            // Trigger the start animation.
            Task.Run(async () =>
            {
                await Task.Delay(100);
                if (!rCastInProgress) animator.RunAnimationOnce(ANIMATION_PATH + "Vel'Koz/q_start.txt", true);
            });

            // The Q cast is in progress.
            qCastInProgress = true;

            // After 1.15s, if user didn't press Q again already, the Q split animation plays.
            Task.Run(async () => // TODO: Q runs a bit slow.
            {
                await Task.Delay(1150);
                if (!rCastInProgress && qCastInProgress)
                {
                    animator.RunAnimationOnce(ANIMATION_PATH + "Vel'Koz/q_recast.txt");
                }
                qCastInProgress = false;
            });
        }

        private void OnCastW()
        {
            Task.Run(async () =>
            {
                animator.RunAnimationOnce(ANIMATION_PATH + "Vel'Koz/w_cast.txt", true);
                await Task.Delay(1800);
                if (!rCastInProgress) animator.RunAnimationOnce(ANIMATION_PATH + "Vel'Koz/w_close.txt", false, 0.15f);
            });
        }

        private void OnCastE()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                if (!rCastInProgress) _ = animator.ColorBurst(HSVColor.FromRGB(229, 115, 255), 0.15f);
            });
        }

        private void OnCastR()
        {
            animator.StopCurrentAnimation();
            animator.RunAnimationInLoop(ANIMATION_PATH + "Vel'Koz/r_loop.txt", 2300, 0.15f);
            rCastInProgress = true;
            Task.Run(async () =>
            {
                await Task.Delay(2300);
                if (rCastInProgress)
                {
                    rCastInProgress = false;
                }
            });
        }


        /// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        private void OnAbilityRecast(object s, AbilityKey key)
        {
            // Add any abilities that need special logic when they are recasted.

            if (key == AbilityKey.Q)
            {
                if (qCastInProgress)
                {
                    qCastInProgress = false;
                    if (!rCastInProgress) animator.RunAnimationOnce(ANIMATION_PATH + "Vel'Koz/q_recast.txt");
                }
            }

            if (key == AbilityKey.R)
            {
                animator.StopCurrentAnimation();
                rCastInProgress = false;
            }
        }
    }
}
