using System;

namespace Games.LeagueOfLegends
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class SpellAttribute : Attribute
    {
        public string SpellName { get; }

        public SpellAttribute(string name)
        {
            SpellName = name;
        }
    }
}
