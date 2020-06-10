using System;

namespace Games.LeagueOfLegends
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ChampionAttribute : Attribute
    {
        public string ChampionName { get; }

        public ChampionAttribute(string name)
        {
            ChampionName = name;
        }
    }
}
