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
    [Champion("Ahri")]
    class AhriModule : ChampionModule
    {

        // Variables

        // Champion-specific Variables

        int rCastInProgress = 0; // this is used to make the animation for Vel'Koz's R to take preference over other animations


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static AhriModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new AhriModule(ledCount, gameState, "Ahri", preferredLightMode, preferredCastMode);
        }


        private AhriModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
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
                [AbilityKey.Q] = AbilityCastMode.Normal(),
                [AbilityKey.W] = AbilityCastMode.Instant(),
                [AbilityKey.E] = AbilityCastMode.Normal(),
                [AbilityKey.R] = AbilityCastMode.Instant(10000,2),
            };
            AbilityCastModes = abilityCastModes;

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            animator.PreloadAnimation(ANIMATION_PATH + "Ahri/q_start.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Ahri/q_end.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Ahri/w_cast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Ahri/e_cast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Ahri/r_right.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Ahri/r_left.txt");

            ChampionInfoLoaded += OnChampionInfoLoaded;
        }

        /// <summary>
        /// Called when the champion info has been retrieved.
        /// </summary>
        private void OnChampionInfoLoaded(ChampionAttributes champInfo)
        {
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls, mode);
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
            Task.Run(async () =>
            {
                animator.RunAnimationOnce(ANIMATION_PATH + "Ahri/q_start.txt", true);
                await Task.Delay(1000);
                animator.RunAnimationOnce(ANIMATION_PATH + "Ahri/q_end.txt", true);
            });
        }

        private void OnCastW()
        {
            Task.Run(async () =>
            {
                animator.RunAnimationOnce(ANIMATION_PATH + "Ahri/w_cast.txt", false, 0.08f);
            });
        }

        private void OnCastE()
        {
            Task.Run(async () =>
            {
                await Task.Delay(100);
                animator.RunAnimationOnce(ANIMATION_PATH + "Ahri/e_cast.txt");
            });
        }

        private void OnCastR()
        {

            // Trigger the start animation.

            animator.RunAnimationOnce(ANIMATION_PATH + "Ahri/r_right.txt");

            // The R cast is in progress.
            rCastInProgress = 1;

            Task.Run(async () =>
            {
                await Task.Delay(7000); // if after 7s no recast, effect disappears
                rCastInProgress = 0;
            });

        }


        /// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        private void OnAbilityRecast(object s, AbilityKey key)
        {
            // Add any abilities that need special logic when they are recasted.

            if (key == AbilityKey.R)
            {
                if (rCastInProgress == 1)
                {
                    animator.RunAnimationOnce(ANIMATION_PATH + "Ahri/r_left.txt");
                    rCastInProgress++;
                } else if (rCastInProgress == 2)
                {
                    animator.RunAnimationOnce(ANIMATION_PATH + "Ahri/r_right.txt");
                    rCastInProgress = 0; // done, all 3 casts have been used.
                }
                
            }
        }
    }
}
