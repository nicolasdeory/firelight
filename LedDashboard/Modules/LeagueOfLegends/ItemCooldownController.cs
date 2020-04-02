using LedDashboard.Modules.LeagueOfLegends.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
{
    class ItemCooldownController
    {
        /// <summary>
        /// Key: ItemID<br></br>
        /// Value: Current Cooldown
        /// </summary>
        static Dictionary<int, int> cooldownDict = new Dictionary<int, int>();

        /// <summary>
        /// Index: Item Slot<br></br>
        /// Value: ItemID
        /// </summary>
        static int[] slotItemIdDict = new int[7];


        private static CancellationTokenSource cancellationToken;

        public static void Init()
        {
            cancellationToken = new CancellationTokenSource();
            Task.Run(() => CooldownLoop(cancellationToken.Token));
        }

        public static void SetCooldown(int itemID, int cooldownDuration) 
        {
            if (!cooldownDict.ContainsKey(itemID))
            {
                cooldownDict.Add(itemID, cooldownDuration);
            } else
            {
                cooldownDict[itemID] = cooldownDuration;
            }
        }

        public static async Task CooldownLoop(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                List<int> keys = cooldownDict.Keys.ToList(); // in order to prevent "collection modified"-related errors
                foreach(var k in keys)
                {
                    cooldownDict[k] -= 1000;
                    cooldownDict[k] = cooldownDict[k] < 0 ? 0 : cooldownDict[k];
                }
                await Task.Delay(1000);
            }
        }

        public static bool IsOnCooldown(int itemID)
        {
            if (!cooldownDict.ContainsKey(itemID))
            {
               // Console.WriteLine("Tried to get " + itemID + " which is not an item with tracked cooldowns. Returning false");
               // it's expected behavior so don't print anything
                return true;
            } else
            {
                return cooldownDict[itemID] > 0;
            }
        }

        public static bool IsSlotOnCooldown(int slot)
        {
            return IsOnCooldown(slotItemIdDict[slot]);
        }



        public static void AssignItemIdToSlot(int slot, int itemID)
        {
            slotItemIdDict[slot] = itemID;
        }

        public static int GetCooldownRemaining(int itemID)
        {
            if (!cooldownDict.ContainsKey(itemID))
            {
                Console.WriteLine("Tried to get " + itemID + " which is not an item with tracked cooldowns. Returning 0");
                return 0;
            }
            else
            {
                return cooldownDict[itemID];
            }
        }

        public static double GetAverageChampionLevel(GameState state)
        {
            return state.Champions.Select(x => x.Level).Average();
        }

        public static void Dispose()
        {
            cancellationToken.Cancel();
        }

    }
}
