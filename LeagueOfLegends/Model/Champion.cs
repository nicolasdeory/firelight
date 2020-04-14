using System.Collections.Generic;

namespace Games.LeagueOfLegends.Model
{
    public class Champion
    {
        public string ChampionName;
        public bool IsDead;
        public bool IsBot;
        public List<Item> Items;
        public int Level;
        public string Position;
        public string RawChampionName;
        public float RespawnTimer;
        //public Rune Runes; // Not really needed right now
        public Score Scores;
        public int SkinID;
        public string SummonerName;
        public SummonerSpellLoadout SummonerSpells;
        public string Team;
    }
}
