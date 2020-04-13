using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.Common;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard.Modules.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    class JaxModule : ChampionModule
    {

        public const string CHAMPION_NAME = "Jax";
        // Variables

        // Champion-specific Variables

        static HSVColor RColor = new HSVColor(0.17f, 0.83f, 0.93f);
        bool castingE;
        bool canRecastE;


        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="gameState">Game state data</param>
        /// <param name="preferredLightMode">Preferred light mode</param>
        /// <param name="preferredCastMode">Preferred ability cast mode (Normal, Quick Cast, Quick Cast with Indicator)</param>
        public static JaxModule Create(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode = AbilityCastPreference.Normal)
        {
            return new JaxModule(ledCount, gameState, CHAMPION_NAME, preferredLightMode, preferredCastMode);
        }


        private JaxModule(int ledCount, GameState gameState, string championName, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
                            : base(ledCount, championName, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.

            // Set preferred cast mode. It's a player choice (Quick cast, Quick cast with indicator, or Normal cast)
            /*PreferredCastMode = preferredCastMode;

            // Set cast modes for abilities.
            // For Vel'Koz, for example:
            // Q -> Normal ability, but it can be recast within 1.15s
            // W -> Normal ability
            // E -> Normal ability
            // R -> Instant ability, it is cast the moment the key is pressed, but it can be recast within 2.3s
            Dictionary<AbilityKey, AbilityCastMode> abilityCastModes = new Dictionary<AbilityKey, AbilityCastMode>()
            {
                [AbilityKey.Q] = AbilityCastMode.PointAndClick(),
                [AbilityKey.W] = AbilityCastMode.Instant(),
                [AbilityKey.E] = AbilityCastMode.Instant(recastTime: 3000),
                [AbilityKey.R] = AbilityCastMode.Instant(),
            };
            AbilityCastModes = abilityCastModes;

            ChampionInfoLoaded += OnChampionInfoLoaded;*/
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.PointAndClick();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Instant(recastTime: 3000);
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant();

      /*  /// <summary>
        /// Called when the champion info has been retrieved.
        /// </summary>
        private void OnChampionInfoLoaded(ChampionAttributes champInfo)
        {
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls, mode);
            AbilityCast += OnAbilityCast;
            AbilityRecast += OnAbilityRecast;
        }*/

       /* /// <summary>
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
        }*/

        protected override async Task OnCastQ()
        {
            RunAnimationOnce("q_cast", timeScale: 1.7f);
        }

        protected override async Task OnCastW()
        {
            RunAnimationInLoop("w_cast_loop", 500);
            await Task.Delay(500);
            RunAnimationOnce("w_cast_end");

        }

        protected override async Task OnCastE()
        {
            castingE = true;
            RunAnimationInLoop("e_cast_loop", 2000, 0.15f);
            await Task.Delay(1000);
            canRecastE = true;
            await Task.Delay(1000);
            if (castingE)
            {
                RunAnimationOnce("e_recast_end");
                castingE = false;
                canRecastE = false;
            }
        }

        protected override async Task OnCastR()
        {
            Animator.ColorBurst(RColor, 0.1f);
        }

        protected override async Task OnRecastE()
        {
            if (canRecastE)
            {
                castingE = false;
                await Task.Delay(200);
                RunAnimationOnce("e_recast_end");
            }
        }
        
        /*/// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        private void OnAbilityRecast(object s, AbilityKey key)
        {
            if (key == AbilityKey.E && canRecastE)
            {
                Task.Run(async () =>
                {
                    castingE = false;
                    await Task.Delay(200);
                    _ = animator.RunAnimationOnce(ANIMATION_PATH + "Jax/e_recast_end.txt");
                });
                
            }
        }*/

    }
}
