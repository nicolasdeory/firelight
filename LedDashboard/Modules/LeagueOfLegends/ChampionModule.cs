using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
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
using System.Windows.Forms;

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

        public event EventHandler<AbilityKey> AbilityCast;
        public event EventHandler<AbilityKey> AbilityRecast;

        public string Name;

        protected ChampionAttributes ChampionInfo;
        protected ActivePlayer PlayerInfo;
        protected LightingMode LightingMode; // Preferred lighting mode. If set to keyboard, it should try to provide animations that look cooler on keyboards.

        protected AbilityCastPreference PreferredCastMode; // User defined setting, preferred cast mode.
        protected Dictionary<AbilityKey,AbilityCastMode> AbilityCastModes;

        private AbilityKey SelectedAbility = AbilityKey.None; // Currently selected ability (for example, if you pressed Q but you haven't yet clicked LMB to cast the ability)
        private char lastPressedKey = '\0';

        public Dictionary<AbilityKey, bool> AbilitiesOnCooldown => _AbilitiesOnCooldown;

        /// <summary>
        /// Dictionary that keeps track of which abilities are currently on cooldown. 
        /// </summary>
        protected Dictionary<AbilityKey, bool> _AbilitiesOnCooldown = new Dictionary<AbilityKey, bool>()
        {
            [AbilityKey.Q] = false,
            [AbilityKey.W] = false,
            [AbilityKey.E] = false,
            [AbilityKey.R] = false,
            [AbilityKey.Passive] = false
        };

        /// <summary>
        /// Dictionary that keeps track of which abilities can currently be RE-CAST (eg. Zoe or Vel'Kozs Q)
        /// </summary>
        protected Dictionary<AbilityKey, int> AbilitiesOnRecast = new Dictionary<AbilityKey, int>()
        {
            [AbilityKey.Q] = 0,
            [AbilityKey.W] = 0,
            [AbilityKey.E] = 0,
            [AbilityKey.R] = 0,
            [AbilityKey.Passive] = 0
        };

        // TODO: Handle champions with cooldown resets?

        protected ChampionModule(string champName, ActivePlayer playerInfo, LightingMode preferredLightingMode)
        {
            Name = champName;
            PlayerInfo = playerInfo;
            LightingMode = preferredLightingMode;
            LoadChampionInformation(champName);
        }

        private void LoadChampionInformation(string champName)
        {
            Task.Run(async () =>
            {
                ChampionInfo = await GetChampionInformation(champName);
                KeyboardHookService.Instance.OnMouseClicked += OnMouseClick; // TODO. Abstract this to league of legends module, so it pairs with summoner spells and items.
                KeyboardHookService.Instance.OnKeyPressed += OnKeyPress;
                KeyboardHookService.Instance.OnKeyReleased += OnKeyRelease;
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
        protected void DispatchNewFrame(Led[] ls, LightingMode mode)
        {
            NewFrameReady?.Invoke(this, ls, mode);
        }

        private void OnMouseClick(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SelectedAbility = AbilityKey.None;
            }
            else if (e.Button == MouseButtons.Left) // cooldowns are accounted for here aswell in case between key press and click user died, or did zhonyas...
            {
                // CODE FOR Q
                if (SelectedAbility == AbilityKey.Q)
                {
                    if (CanCastAbility(AbilityKey.Q))
                        CastAbility(AbilityKey.Q);
                }

                // CODE FOR W
                if (SelectedAbility == AbilityKey.W)
                {
                    if (CanCastAbility(AbilityKey.W))
                        CastAbility(AbilityKey.W);
                }

                // CODE FOR E
                if (SelectedAbility == AbilityKey.E)
                {
                    if (CanCastAbility(AbilityKey.E))
                        CastAbility(AbilityKey.E);
                }

                // CODE FOR R
                if (SelectedAbility == AbilityKey.R)
                {
                    if (CanCastAbility(AbilityKey.R))
                        CastAbility(AbilityKey.R);
                }
            }
            //OnMouseClicked?.Invoke(s,e);
        }

        private void OnKeyRelease(object s, KeyEventArgs e)
        {
            ProcessKeyPress(s, e.KeyCode.ToString().ToLower()[0], true);
        }

        private void OnKeyPress(object s, KeyPressEventArgs e)
        {
            ProcessKeyPress(s, e.KeyChar);
        }

        private void ProcessKeyPress(object s, char keyChar, bool keyUp = false)
        {
            if (keyChar == lastPressedKey && !keyUp) return; // prevent duplicate calls. Without this, this gets called every frame a key is pressed.
            lastPressedKey = keyUp ? '\0' : keyChar; 
            // TODO: quick cast with indicator bug - repro: hold w, then hold q, then right click, then release w, then release q. The ability is cast, even when it shouldn't.

            if (keyChar == 'q')
            {
                DoCastLogicForAbility(AbilityKey.Q, keyUp);
            }
            if (keyChar == 'w')
            {
                DoCastLogicForAbility(AbilityKey.W, keyUp);
            }
            if (keyChar == 'e')
            {
                DoCastLogicForAbility(AbilityKey.E, keyUp);
            }
            if (keyChar == 'r')
            {
                DoCastLogicForAbility(AbilityKey.R, keyUp);
            }
            /*if (e.KeyChar == 'f') // TODO: Refactor this into LeagueOfLegendsModule, or a new SummonerSpells module. Also take cooldown into consideration.
            {
                animator.ColorBurst(HSVColor.FromRGB(255, 237, 41), 0.1f);
            }*/
            //OnKeyPressed?.Invoke(s,e);
        }

        private void DoCastLogicForAbility(AbilityKey key, bool keyUp)
        {
            if (keyUp && SelectedAbility != key) return; // keyUp event shouldn't trigger anything if the ability is not selected.

            AbilityCastMode castMode = AbilityCastModes[key];

            if (castMode.HasRecast && AbilitiesOnRecast[key] > 0)
            {
                if (CanCastAbility(key)) // We must check if CanCastAbility is true. Players can't recast abilities if they're dead or in zhonyas.
                {
                    RecastAbility(key);
                }
                return;
            }

            if (castMode.IsInstant) // ability is cast with just pressing down the key
            {
                if (CanCastAbility(key))
                {
                    CastAbility(key);
                }
                return;
            }

            if (castMode.IsNormal) // ability has normal cast
            {
                if (PreferredCastMode == AbilityCastPreference.Normal)
                {
                    if (CanCastAbility(key)) // normal press & click cast, typical
                    {
                        SelectedAbility = key;
                    }
                    return;

                }

                if (PreferredCastMode == AbilityCastPreference.Quick)
                {
                    if (CanCastAbility(key))
                    {
                        CastAbility(key);
                    }
                    return;
                }

                if (PreferredCastMode == AbilityCastPreference.QuickWithIndicator)
                {
                    if (CanCastAbility(key))
                    {
                        if (keyUp && SelectedAbility == key) // Key released, so CAST IT if it's selected
                        {
                            if (CanCastAbility(key))
                            {
                                CastAbility(key);
                            }
                        }
                        else // Key down, so select it
                        {
                            if (CanCastAbility(key))
                            {
                                SelectedAbility = key;
                            }
                        }
                    }
                }
            }
        }

        private void CastAbility(AbilityKey key)
        {
            AbilityCast?.Invoke(this, key);
            if (AbilityCastModes[key].HasRecast)
            {
                StartRecastTimer(key);
            } else
            {
                StartCooldownTimer(key);
            }
            SelectedAbility = AbilityKey.None;
        }
        private void RecastAbility(AbilityKey key)
        {
            AbilityRecast?.Invoke(this, key);
            AbilitiesOnRecast[key]--;
            if (AbilitiesOnRecast[key] == 0) StartCooldownTimer(key);
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
            if (_AbilitiesOnCooldown[spellKey]) return false;
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
                _AbilitiesOnCooldown[ability] = true;
                await Task.Delay(GetCooldownForAbility(ability) - 350); // a bit less cooldown than the real one (if the user spams)
                _AbilitiesOnCooldown[ability] = false;
            });
        }

        private void StartRecastTimer(AbilityKey ability)
        {
            Task.Run(async () =>
            {
                AbilitiesOnRecast[ability] = AbilityCastModes[ability].MaxRecasts;
                await Task.Delay(AbilityCastModes[ability].RecastTime);
                if (AbilitiesOnRecast[ability] > 0) // if user hasn't recast yet
                {
                    AbilitiesOnRecast[ability] = 0;
                    StartCooldownTimer(ability);
                }
            });
        }

        public void Dispose()
        {
            KeyboardHookService.Instance.OnMouseClicked -= OnMouseClick;
            KeyboardHookService.Instance.OnKeyPressed -= OnKeyPress;
            KeyboardHookService.Instance.OnKeyReleased -= OnKeyRelease;
        }
    }
}
