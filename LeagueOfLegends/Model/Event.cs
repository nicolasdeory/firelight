using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends.Model
{
    public class Event
    {
        public int EventID;
        public String EventName;
        public double EventTime;
        public string KillerName;
        public string VictimName;
        public List<string> Assisters;
        public string Recipient;
    }
}
