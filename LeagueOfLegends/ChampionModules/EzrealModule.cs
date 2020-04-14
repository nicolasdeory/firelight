using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class EzrealModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Ezreal";
        // Variables

        // Champion-specific Variables


        public EzrealModule(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, CHAMPION_NAME, gameState, preferredLightMode, preferredCastMode, true)
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
            await RunAnimationOnce("q_cast", timeScale: 0.8f);
        }
        protected override async Task OnCastW()
        {
            await Task.Delay(150);
            await RunAnimationOnce("w_cast");
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(250);
            await RunAnimationOnce("e_cast", false, 0.15f);
        }
        protected override async Task OnCastR()
        {
            await RunAnimationOnce("r_channel", true);
            await Task.Delay(700);
            await RunAnimationOnce("r_launch", timeScale: 0.7f);
        }
    }
}
