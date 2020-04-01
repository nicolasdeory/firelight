using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends.Model
{
    public class ItemAttributes
    {
        public string Name;
        public int GoldBaseCost;
        public int GoldTotalCost;
        public List<float> EffectAmounts;

        public static ItemAttributes FromData(dynamic data)
        {
            return new ItemAttributes()
            {
                Name = data.name,
                GoldBaseCost =  data.gold.@base,
                GoldTotalCost = data.gold.total,
                EffectAmounts = (data.effect as JObject)?.Properties().Select(x => (float)(x.Value)).ToList()
            };
        }
    }
}
