using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class ChampionAttribute : Attribute
    {
        readonly string _championName;

        public ChampionAttribute(string name)
        {
            this._championName = name;
        }

        public string ChampionName
        {
            get { return _championName; }
        }
    }
}
