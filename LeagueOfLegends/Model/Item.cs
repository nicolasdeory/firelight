namespace Games.LeagueOfLegends.Model
{
    public class Item
    {
        public bool CanUse;
        public bool Consumable;
        public int Count;
        public string DisplayName;
        public int ItemID;
        public int Price;

        /// <summary>
        /// zero based
        /// </summary>
        public int Slot;
    }
}
