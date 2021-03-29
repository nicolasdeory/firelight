using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using FirelightCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class EzrealModule : ChampionModule
    {
        public const string CHAMPION_NAME = "Ezreal";
        // Variables

        // Champion-specific Variables


        public EzrealModule(GameState gameState)
            : base(CHAMPION_NAME, gameState, true)
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
            RunAnimationOnce("q_cast", LightZone.Keyboard, timeScale: 0.8f);
        }
        protected override async Task OnCastW()
        {
            await Task.Delay(150);
            RunAnimationOnce("w_cast", LightZone.Keyboard);
        }
        protected override async Task OnCastE()
        {
            await Task.Delay(250);
            RunAnimationOnce("e_cast", LightZone.Keyboard, 1f);
        }
        protected override async Task OnCastR()
        {
            RunAnimationOnce("r_channel", LightZone.Keyboard);
            Animator.HoldLastFrame(LightZone.Keyboard, 0.7f);
            RunAnimationOnce("r_launch", LightZone.Keyboard, timeScale: 0.7f);
        }
    }
}
