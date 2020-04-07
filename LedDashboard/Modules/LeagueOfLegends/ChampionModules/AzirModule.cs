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
    [Champion(CHAMPION_NAME)]
    class AzirModule : ChampionModule
    {

        // Change to whatever champion you want to implement
        public const string CHAMPION_NAME = "Azir";

        // Variables

        // Champion-specific Variables


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static AzirModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new AzirModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private AzirModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(ledCount, championName, gameState, preferredLightMode)
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
                [AbilityKey.Q] = AbilityCastMode.Instant(),
                [AbilityKey.W] = AbilityCastMode.Instant(),
                [AbilityKey.E] = AbilityCastMode.Instant(),
                [AbilityKey.R] = AbilityCastMode.Instant(),
            };
            AbilityCastModes = abilityCastModes;

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator.PreloadAnimation(ANIMATION_PATH + "Azir/q_cast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Azir/w_cast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Azir/e_cast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "Azir/r_cast.txt");


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
                animator.RunAnimationOnce(ANIMATION_PATH + "Azir/q_start.txt");
            });
        }

        private void OnCastW()
        {
            Task.Run(async () =>
            {
                animator.RunAnimationOnce(ANIMATION_PATH + "Azir/w_cast.txt");
            });
        }

        private void OnCastE()
        {
            Task.Run(async () =>
            {
                animator.RunAnimationOnce(ANIMATION_PATH + "Azir/e_cast.txt");
            });
        }

        private void OnCastR()
        {
            // Trigger the start animation.

            animator.RunAnimationOnce(ANIMATION_PATH + "Azir/r_cast.txt");
        }
    }
}