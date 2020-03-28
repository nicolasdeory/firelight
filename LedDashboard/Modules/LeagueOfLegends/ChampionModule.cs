using LedDashboard.Modules.LeagueOfLegends.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
{
    public abstract class ChampionModule : LEDModule
    {
        const string VERSION_ENDPOINT = "https://ddragon.leagueoflegends.com/api/versions.json";
        const string CHAMPION_INFO_ENDPOINT = "http://ddragon.leagueoflegends.com/cdn/{0}/data/en_US/champion/{1}.json";

        public event LEDModule.FrameReadyHandler NewFrameReady;

        protected delegate void PlayerInfoUpdatedHandle(ActivePlayer updatedPlayer);
        /// <summary>
        /// Raised when the player info was updated.
        /// </summary>
        protected event PlayerInfoUpdatedHandle PlayerInfoUpdated;

        protected delegate void ChampionInfoLoadedHandler(ChampionAttributes attributes);
        /// <summary>
        /// Raised when the player info was updated.
        /// </summary>
        protected event ChampionInfoLoadedHandler ChampionInfoLoaded;

        public delegate void OutOfManaHandler();
        /// <summary>
        /// Raised when the user tried to cast an ability but was out of mana.
        /// </summary>
        public event OutOfManaHandler TriedToCastOutOfMana;

        public string Name;
        protected ChampionAttributes ChampionInfo;
        protected ActivePlayer PlayerInfo;

        /// <summary>
        /// Dictionary that keeps track of which abilities are currently on cooldown. 
        /// </summary>
        protected Dictionary<AbilityKey, bool> AbilitiesOnCooldowns = new Dictionary<AbilityKey, bool>()
        {
            [AbilityKey.Q] = false,
            [AbilityKey.W] = false,
            [AbilityKey.E] = false,
            [AbilityKey.R] = false,
            [AbilityKey.Passive] = false
        };

        protected ChampionModule(string champName, ActivePlayer playerInfo)
        {
            Name = champName;
            PlayerInfo = playerInfo;
            LoadChampionInformation(champName);
        }

        private void LoadChampionInformation(string champName)
        {
            Task.Run(async () =>
            {
                ChampionInfo = await GetChampionInformation(champName);
                ChampionInfoLoaded?.Invoke(ChampionInfo);
            });
        }

        /// <summary>
        /// Retrieves the attributes for a given champ (ability cooldowns, mana costs, etc.)
        /// </summary>
        /// <param name="championName">Internal champion name (i.e. Vel'Koz -> Velkoz)</param>
        /// <returns></returns>
        private async Task<ChampionAttributes> GetChampionInformation(string championName)
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

            string championJSON;
            try
            {
                championJSON = await WebRequestUtil.GetResponse(String.Format(CHAMPION_INFO_ENDPOINT, latestVersion, championName));
                
            }
            catch (WebException e)
            {
                throw new InvalidOperationException("Error retrieving champion data for '" + championName + "'", e);
            }
            dynamic championData = JsonConvert.DeserializeObject<dynamic>(championJSON);
            return ChampionAttributes.FromData(championData.data[championName]);
        }

        /// <summary>
        /// Dispatches a frame with the given LED data, raising the NewFrameReady event.
        /// </summary>
        protected void DispatchNewFrame(Led[] ls)
        {
            NewFrameReady?.Invoke(this, ls);
        }

        /// <summary>
        /// Updates player info and raises the appropiate events.
        /// </summary>
        public void UpdatePlayerInfo(ActivePlayer updatedPlayerInfo)
        {
            PlayerInfo = updatedPlayerInfo;
            PlayerInfoUpdated?.Invoke(updatedPlayerInfo);
        }

        /// <summary>
        /// Returns the cooldown in milliseconds for a given ability, after applying cooldown reduction.
        /// </summary>
        protected int GetCooldownForAbility(AbilityKey ability)
        {
            return ability switch
            {
                AbilityKey.Q => (int)(ChampionInfo.Costs.Q_Cooldown[PlayerInfo.AbilityLoadout.Q_Level-1]
                                   + ChampionInfo.Costs.Q_Cooldown[PlayerInfo.AbilityLoadout.Q_Level - 1] * PlayerInfo.Stats.CooldownReduction),
                AbilityKey.W => (int)(ChampionInfo.Costs.W_Cooldown[PlayerInfo.AbilityLoadout.W_Level - 1]
                                    + ChampionInfo.Costs.W_Cooldown[PlayerInfo.AbilityLoadout.W_Level - 1] * PlayerInfo.Stats.CooldownReduction),
                AbilityKey.E => (int)(ChampionInfo.Costs.E_Cooldown[PlayerInfo.AbilityLoadout.E_Level - 1]
                                    + ChampionInfo.Costs.E_Cooldown[PlayerInfo.AbilityLoadout.E_Level - 1] * PlayerInfo.Stats.CooldownReduction),
                AbilityKey.R => (int)(ChampionInfo.Costs.R_Cooldown[PlayerInfo.AbilityLoadout.R_Level - 1]
                                    + ChampionInfo.Costs.R_Cooldown[PlayerInfo.AbilityLoadout.R_Level - 1] * PlayerInfo.Stats.CooldownReduction),
                _ => 0,
            };
        }

        /// <summary>
        /// Returns true if the ability can be cast at the moment (i.e. it's not on cooldown, the player is not dead or under zhonyas)
        /// </summary>
        protected bool CanCastAbility(AbilityKey spellKey)
        {
            if (PlayerInfo.IsDead) return false;
            if (PlayerInfo.AbilityLoadout.GetAbilityLevel(spellKey) == 0) return false;
            if (AbilitiesOnCooldowns[spellKey]) return false;
            if (PlayerInfo.Stats.ResourceValue < ChampionInfo.Costs.GetManaCost(spellKey, PlayerInfo.AbilityLoadout.GetAbilityLevel(spellKey)))
            {
                // raise not enough mana event
                TriedToCastOutOfMana?.Invoke();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Starts the cooldown timer for an ability. It should be called after an ability is cast.
        /// </summary>
        protected void StartCooldownTimer(AbilityKey ability)
        {
            Task.Run(async () =>
            {
                AbilitiesOnCooldowns[ability] = true;
                await Task.Delay(GetCooldownForAbility(ability) - 350); // a bit less cooldown than the real one (if the user spams)
                AbilitiesOnCooldowns[ability] = false;
            });
        }

        public void Dispose()
        {
            //
        }
    }
}
