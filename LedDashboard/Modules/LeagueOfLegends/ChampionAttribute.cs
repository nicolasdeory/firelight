using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
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
