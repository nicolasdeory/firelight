using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class EzrealModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Ezreal";
        // Variables

        // Champion-specific Variables


        public EzrealModule(GameState gameState, AbilityCastPreference preferredCastMode)
            : base(CHAMPION_NAME, gameState, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Normal();

        protected override async Task OnCastQ()
        {
            await Task.Delay(150);
            RunAnimationOnce("q_cast", timeScale: 0.8f);
        }
        protected override async Task OnCastW()
        {
            await Task.Delay(150);
            RunAnimationOnce("w_cast");
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(250);
            RunAnimationOnce("e_cast", false, 0.15f);
        }
        protected override async Task OnCastR()
        {
            RunAnimationOnce("r_channel", true);
            await Task.Delay(700);
            RunAnimationOnce("r_launch", timeScale: 0.7f);
        }
    }
}
