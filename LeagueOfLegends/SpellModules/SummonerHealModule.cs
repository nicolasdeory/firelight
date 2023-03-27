using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using FirelightCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using FirelightCore.Modules.Common;
using FirelightCore.Modules.BasicAnimation;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Spell(SPELL_NAME)]
    public sealed class SummonerHealModule : SummonerSpellModule
    {
        public const string SPELL_NAME = "SummonerHeal";

        // Variables

        public SummonerHealModule(GameState gameState, SpellKey assignedKey, AnimationModule animator)
            : base(SPELL_NAME, assignedKey, animator, gameState, true)
        {
            // Initialization for the summoner module module occurs here.
        }

        protected override AbilityCastMode GetCastMode() => AbilityCastMode.Instant();

        protected override Task OnCast()
        {
            Animator.ColorBurst(HSVColor.FromRGB(153, 255, 153), LightZone.All, 0.8f);
            return Task.CompletedTask;
        }
    }
}
