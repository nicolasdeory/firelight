using Games.LeagueOfLegends.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Games.LeagueOfLegends
{
    public static class ItemUtils
    {
        const string VERSION_ENDPOINT = "https://ddragon.leagueoflegends.com/api/versions.json";
        const string ITEM_INFO_ENDPOINT = "http://ddragon.leagueoflegends.com/cdn/{0}/data/en_US/item.json";

        static Dictionary<int, ItemAttributes> itemAttributeDict;

        public static bool IsLoaded => itemAttributeDict.Keys.Count > 0;

        public static ItemAttributes GetItemAttributes(int itemID)
        {
            if (!itemAttributeDict.ContainsKey(itemID))
            {
                return null; // Either an invalid itemID is given, or dictionary hasn't loaded yet (race condition).
            }
            return itemAttributeDict[itemID];
        }
        
        public static void Init()
        {
            itemAttributeDict = new Dictionary<int, ItemAttributes>();
            Task.Run(RetrieveItemInfo);
        }

        private static async void RetrieveItemInfo()
        {
            string latestVersion;
            try
            {
                string versionJSON = await WebRequestUtil.GetResponse(VERSION_ENDPOINT);
                List<string> versions = JsonConvert.DeserializeObject<List<string>>(versionJSON);
                latestVersion = versions[0];
            }
            catch (WebException e)
            {
                throw new InvalidOperationException("Error retrieving game version", e);
            }

            string itemsJSON;
            try
            {
                itemsJSON = await WebRequestUtil.GetResponse(String.Format(ITEM_INFO_ENDPOINT, latestVersion));

            }
            catch (WebException e)
            {
                throw new InvalidOperationException("Error retrieving item data", e);
            }
            dynamic itemsData = JsonConvert.DeserializeObject<dynamic>(itemsJSON);
            ParseItemInfo(itemsData);
        }

        private static void ParseItemInfo(dynamic itemsInfo)
        {
            JObject itemsData = itemsInfo.data as JObject;
            foreach(var k in itemsData.Properties())
            {
                int itemID = int.Parse(k.Name);
                ItemAttributes itemData = ItemAttributes.FromData(k.Value);
                if (itemAttributeDict == null) break;
                itemAttributeDict.Add(itemID, itemData);
            }
        }

        public static void Dispose()
        {
            itemAttributeDict = null;
        }

    }
}
