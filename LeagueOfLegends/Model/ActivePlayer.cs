using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Games.LeagueOfLegends.Model
{
    public class ActivePlayer
    {
        public AbilityLoadout Abilities { get; set; }
        public ChampionStats Stats { get; set; }

        public float CurrentGold;
        public List<Rune> Runes;
        public int Level;
        public string SummonerName;

        public bool IsDead;
        public bool IsOnZhonyas;

        private ActivePlayer(bool isDead = false, bool isOnZhonyas = false)
        {
            IsDead = isDead;
            IsOnZhonyas = isOnZhonyas;
        }

        public static ActivePlayer FromData(dynamic data, bool isDead = false, bool isOnZhonyas = false)
        {
            var player = new ActivePlayer(isDead, isOnZhonyas);
            player.UpdateFromData(data);
            return player;
        }

        public void UpdateFromData(dynamic data)
        {
            this.Abilities = AbilityLoadout.FromData(data.abilities);
            this.Stats = (data.championStats as JObject).ToObject<ChampionStats>();
            this.CurrentGold = data.currentGold;
            this.Runes = (data.fullRunes.generalRunes as JArray).ToObject<List<Rune>>();
            this.Level = data.level;
            this.SummonerName = data.summonerName;
        }
    }
}
