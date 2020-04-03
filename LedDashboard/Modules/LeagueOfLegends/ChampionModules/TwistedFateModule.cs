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
    class TwistedFateModule : ChampionModule
    {

        public const string CHAMPION_NAME = "TwistedFate";
        // Variables

        // Champion-specific Variables
        HSVColor RColor = new HSVColor(0.81f, 0.43f, 1);
        HSVColor RColor2 = new HSVColor(0.91f, 0.87f, 1);

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static TwistedFateModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new TwistedFateModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private TwistedFateModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(ledCount, championName, gameState, preferredLightMode)
        {
            // Initialization for the champion module occurs here.

            // Set preferred cast mode. It's a player choice (Quick cast, Quick cast with indicator, or Normal cast)
            PreferredCastMode = preferredCastMode;

            // Set cast modes for abilities.
            Dictionary<AbilityKey, AbilityCastMode> abilityCastModes = new Dictionary<AbilityKey, AbilityCastMode>()
            {
                [AbilityKey.Q] = AbilityCastMode.Normal(),
                [AbilityKey.W] = AbilityCastMode.Instant(6000,1),
                [AbilityKey.E] = AbilityCastMode.UnCastable(),
                [AbilityKey.R] = AbilityCastMode.Instant(6000,1,AbilityCastMode.Normal()),
            };
            AbilityCastModes = abilityCastModes;

            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".
            animator.PreloadAnimation(ANIMATION_PATH + "TwistedFate/q_cast.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "TwistedFate/w_loop.txt");
            animator.PreloadAnimation(ANIMATION_PATH + "TwistedFate/r_cast.txt");

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
                await Task.Delay(100);
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "TwistedFate/q_cast.txt", timeScale: 0.4f);
            });
            
        }

        private void OnCastW()
        {
            animator.RunAnimationInLoop(ANIMATION_PATH + "TwistedFate/w_loop.txt", 5500, 0.1f, 0.08f);
            
        }

        private void OnCastE()
        {
           
        }

        private void OnCastR()
        {
            animator.ColorBurst(RColor, 0.05f, RColor2);
        }

        /// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        private void OnAbilityRecast(object s, AbilityKey key)
        {
            if (key == AbilityKey.W)
            {
                animator.ColorBurst(new HSVColor(0, 0, 1));
            }
            if (key == AbilityKey.R)
            {
                _ = animator.RunAnimationOnce(ANIMATION_PATH + "TwistedFate/r_cast.txt", fadeOutAfterRate: 0.1f, timeScale: 0.2f);
            }
        }

    }
}
