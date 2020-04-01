using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class ItemAttribute : Attribute
    {
        readonly int _itemName;

        public ItemAttribute(int id)
        {
            this._itemName = id;
        }

        public int ItemID
        {
            get { return _itemName; }
        }
    }
}
