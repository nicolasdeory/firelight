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
    class EzrealModule : ChampionModule
    {

        public const string CHAMPION_NAME = "Ezreal";
        // Variables

        // Champion-specific Variables


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static EzrealModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new EzrealModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private EzrealModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(ledCount, championName, gameState, preferredLightMode, true)
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
                [AbilityKey.W] = AbilityCastMode.Normal(),
                [AbilityKey.E] = AbilityCastMode.Normal(),
                [AbilityKey.R] = AbilityCastMode.Normal(),
            };
            AbilityCastModes = abilityCastModes;

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
                await Task.Delay(150);
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "Ezreal/q_cast.txt", timeScale: 0.8f);
            });
            
        }

        private void OnCastW()
        {
            Task.Run(async () =>
            {
                await Task.Delay(150);
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "Ezreal/w_cast.txt");
            });
            
        }

        private void OnCastE()
        {
            Task.Run(async () =>
            {
                await Task.Delay(250);
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "Ezreal/e_cast.txt", false, 0.15f);
            });
            
        }

        private void OnCastR()
        {
            Task.Run(async () =>
            {
                await animator.RunAnimationOnce(ANIMATION_PATH + "Ezreal/r_channel.txt", true);
                await Task.Delay(700);
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "Ezreal/r_launch.txt", timeScale: 0.7f);
            });

        }

        /// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        private void OnAbilityRecast(object s, AbilityKey key)
        {

        }

    }
}
