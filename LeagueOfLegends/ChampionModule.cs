using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games.LeagueOfLegends
{
    public abstract class ChampionModule : GameElementModule
    {
        const string VERSION_ENDPOINT = "https://ddragon.leagueoflegends.com/api/versions.json";
        const string CHAMPION_INFO_ENDPOINT = "http://ddragon.leagueoflegends.com/cdn/{0}/data/en_US/champion/{1}.json";

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

        protected event EventHandler<AbilityKey> AbilityCast;
        protected event EventHandler<AbilityKey> AbilityRecast;

        protected override string ModuleTypeName => "Champion";

        protected ChampionAttributes ChampionInfo;

        protected Dictionary<AbilityKey, AbilityCastMode> AbilityCastModes;

        private AbilityKey SelectedAbility = AbilityKey.None; // Currently selected ability (for example, if you pressed Q but you haven't yet clicked LMB to cast the ability)
        private char lastPressedKey = '\0';

        /// <summary>
        /// Mana that the champion had in the last frame. Useful for point and click cast detection
        /// </summary>
        protected float lastManaAmount = 0;

        /// <summary>
        /// Dictionary that keeps track of which abilities are currently on cooldown. 
        /// </summary>
        public Dictionary<AbilityKey, bool> AbilitiesOnCooldown { get; protected set; } = new Dictionary<AbilityKey, bool>()
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

        protected ChampionModule(string champName, GameState gameState, AbilityCastPreference preferredCastMode, bool preloadAllAnimations = false) // TODO: Pass gamestate instead of active player
            : base(champName, gameState, preferredCastMode, preloadAllAnimations)
        {
            AbilityCastModes = new Dictionary<AbilityKey, AbilityCastMode>
            {
                [AbilityKey.Q] = GetQCastMode(),
                [AbilityKey.W] = GetWCastMode(),
                [AbilityKey.E] = GetECastMode(),
                [AbilityKey.R] = GetRCastMode(),
            };

            LoadChampionInformation(champName);

            ChampionInfoLoaded += OnChampionInfoLoaded;
        }

        /// <summary>
        /// Called when the champion info has been retrieved.
        /// </summary>
        protected virtual void OnChampionInfoLoaded(ChampionAttributes champInfo)
        {
            AddAnimatorEvent();
            AbilityCast += OnAbilityCast;
            AbilityRecast += OnAbilityRecast;
        }

        /// <summary>
        /// Called when an ability is cast.
        /// </summary>
        protected virtual void OnAbilityCast(object s, AbilityKey key)
        {
            Task.Run(GetAbilityCastTask(key));
        }
        /// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        protected virtual void OnAbilityRecast(object s, AbilityKey key)
        {
            Task.Run(GetAbilityRecastTask(key));
        }

        private Func<Task> GetAbilityCastTask(AbilityKey key)
        {
            switch (key)
            {
                case AbilityKey.Q:
                    return OnCastQ;
                case AbilityKey.W:
                    return OnCastW;
                case AbilityKey.E:
                    return OnCastE;
                case AbilityKey.R:
                    return OnCastR;
            }
            // Should never happen
            return null;
        }
        private Func<Task> GetAbilityRecastTask(AbilityKey key)
        {
            switch (key)
            {
                case AbilityKey.Q:
                    return OnRecastQ;
                case AbilityKey.W:
                    return OnRecastW;
                case AbilityKey.E:
                    return OnRecastE;
                case AbilityKey.R:
                    return OnRecastR;
            }
            // Should never happen
            return null;
        }

        protected abstract AbilityCastMode GetQCastMode();
        protected abstract AbilityCastMode GetWCastMode();
        protected abstract AbilityCastMode GetECastMode();
        protected abstract AbilityCastMode GetRCastMode();

        protected virtual async Task OnCastQ() { }
        protected virtual async Task OnCastW() { }
        protected virtual async Task OnCastE() { }
        protected virtual async Task OnCastR() { }
        protected virtual async Task OnRecastQ() { }
        protected virtual async Task OnRecastW() { }
        protected virtual async Task OnRecastE() { }
        protected virtual async Task OnRecastR() { }

        private void LoadChampionInformation(string champName)
        {
            Task.Run(async () =>
            {
                ChampionInfo = await GetChampionInformation(champName);
                AddInputHandlers();
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

        protected override void OnMouseClick(object s, MouseEventArgs e)
        {
            //Debug.WriteLine("Mouse click. Selected: " + SelectedAbility);
            if (e.Button == MouseButtons.Right)
            {
                if (SelectedAbility != AbilityKey.None && CanRecastAbility(SelectedAbility) && !AbilityCastModes[SelectedAbility].RecastOnKeyUp)
                    SelectedAbility = AbilityKey.None;
            }
            else if (e.Button == MouseButtons.Left) // cooldowns are accounted for here aswell in case between key press and click user died, or did zhonyas...
            {
                if (SelectedAbility == AbilityKey.None) return;

                if (CanCastAbility(SelectedAbility))
                {
                    if (CanRecastAbility(SelectedAbility))
                    {
                        // its a recast
                        RecastAbility(SelectedAbility);
                    }
                    else
                    {
                        CastAbility(SelectedAbility);
                    }
                }
            }
        }

        protected override void OnKeyRelease(object s, KeyEventArgs e)
        {
            ProcessKeyPress(s, e.KeyCode.ToString().ToLower()[0], true);
        }

        protected override void OnKeyPress(object s, KeyPressEventArgs e)
        {
            ProcessKeyPress(s, e.KeyChar);
        }

        protected override void ProcessKeyPress(object s, char keyChar, bool keyUp = false)
        {
            base.ProcessKeyPress(s, keyChar, keyUp);
            // TODO: quick cast with indicator bug - repro: hold w, then hold q, then right click, then release w, then release q. The ability is cast, even when it shouldn't.
            // Debug.WriteLine("Keypressed. Selected: " + SelectedAbility);
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
        }

        private void DoCastLogicForAbility(AbilityKey key, bool keyUp)
        {
            if (keyUp && SelectedAbility != key)
                return; // keyUp event shouldn't trigger anything if the ability is not selected.

            if (!CanCastAbility(key))
                return;

            AbilityCastMode castMode = AbilityCastModes[key];
            //Debug.WriteLine(key + " " + (keyUp ? "up" : "down"));

            if (castMode.HasRecast && AbilitiesOnRecast[key] > 0)
            {
                //Debug.WriteLine(castMode);
                if (castMode.RecastMode.IsInstant)
                {
                    RecastAbility(key);
                    return;
                }
                if (PreferredCastMode == AbilityCastPreference.Normal)
                {
                    if (castMode.RecastMode.IsNormal)
                    {
                        SelectedAbility = key;
                        // RECAST SELECTED
                    }
                    if (castMode.RecastMode.RecastOnKeyUp && !keyUp)
                    {
                        RecastAbility(key);
                    }
                    return;
                }
                if (PreferredCastMode == AbilityCastPreference.Quick)
                {
                    RecastAbility(key);
                    return;
                }
                if (PreferredCastMode == AbilityCastPreference.QuickWithIndicator)
                {
                    if (castMode.RecastMode.RecastOnKeyUp && keyUp && SelectedAbility == key)
                    {
                        RecastAbility(key);
                    }
                    if (castMode.RecastMode.IsNormal)
                    {
                        SelectedAbility = key;
                        // RECAST SELECTED
                    }
                    if (castMode.RecastMode.IsNormal && keyUp && SelectedAbility == key)
                    {
                        if (CanCastAbility(key))
                        {
                            RecastAbility(key);
                        }
                    }
                    return;
                }
                return;
            }

            if (castMode.IsInstant) // ability is cast with just pressing down the key
            {
                CastAbility(key);
                return;
            }

            if (castMode.IsNormal) // ability has normal cast
            {
                if (PreferredCastMode == AbilityCastPreference.Normal)
                {
                    SelectedAbility = key;
                    return;
                }

                if (PreferredCastMode == AbilityCastPreference.Quick)
                {
                    CastAbility(key);
                    return;
                }

                if (PreferredCastMode == AbilityCastPreference.QuickWithIndicator)
                {
                    if (keyUp && SelectedAbility == key) // Key released, so CAST IT if it's selected
                    {
                        CastAbility(key);
                    }
                    else // Key down, so select it
                    {
                        SelectedAbility = key;
                    }
                }
            }
        }

        private void CastAbility(AbilityKey key)
        {
            Task.Run(async () =>
            {
                /*if (AbilityCastModes[key].IsPointAndClick)
                {
                    // check if mana was substracted, right after casting the ability
                    
                    lastManaAmount = LeagueOfLegendsModule.CurrentGameState.ActivePlayer.Stats.ResourceValue;
                   // Debug.WriteLine("A: " + lastManaAmount);
                    // TODO: Find an alternative method for point and click
                    await Task.Delay(300); // This is very slow, but if you put less time, the mana change won't be detected. There seems to be about 300ms delay in stats.
                  //  Debug.WriteLine("B: " + LeagueOfLegendsModule.CurrentGameState.ActivePlayer.Stats.ResourceValue);

                    if (LeagueOfLegendsModule.CurrentGameState.ActivePlayer.Stats.ResourceValue >= lastManaAmount) 
                    {
                        // mana wasn't consumed, so no ability was cast. Maybe this trick doesn't always work. E.g. Anivia E while having R enabled?
                        SelectedAbility = AbilityKey.None;
                        return;
                    }
                }*/
                AbilityCast?.Invoke(this, key);
                if (AbilityCastModes[key].HasRecast)
                {
                    StartRecastTimer(key);
                }
                else
                {
                    if (!AbilityCastModes[key].IsPointAndClick) // no cooldown for point and clicks
                        StartCooldownTimer(key);
                }
                if (AbilityCastModes[key].RecastMode != null && AbilityCastModes[key].RecastMode.RecastOnKeyUp)
                    SelectedAbility = key;
                else
                    SelectedAbility = AbilityKey.None;
            });

        }
        private void RecastAbility(AbilityKey key)
        {
            AbilityRecast?.Invoke(this, key);
            AbilitiesOnRecast[key]--;
            if (AbilitiesOnRecast[key] == 0)
            {
                if (AbilityCastModes[key].RecastMode.RecastOnKeyUp)
                    SelectedAbility = AbilityKey.None;
                StartCooldownTimer(key);
            }

        }

        /// <summary>
        /// Returns the cooldown in milliseconds for a given ability, after applying cooldown reduction.
        /// </summary>
        protected int GetCooldownForAbility(AbilityKey ability)
        {
            AbilityLoadout abilities = GameState.ActivePlayer.Abilities;
            ChampionCosts costs = ChampionInfo.Costs;
            float cdr = GameState.ActivePlayer.Stats.CooldownReduction;
            return ability switch
            {
                AbilityKey.Q => (int)(costs.Q_Cooldown[abilities.Q_Level - 1] * (1 - cdr)),
                AbilityKey.W => (int)(costs.W_Cooldown[abilities.W_Level - 1] * (1 - cdr)),
                AbilityKey.E => (int)(costs.E_Cooldown[abilities.E_Level - 1] * (1 - cdr)),
                AbilityKey.R => (int)(costs.R_Cooldown[abilities.R_Level - 1] * (1 - cdr)),

                _ => 0,
            };
        }

        /// <summary>
        /// Returns true if the ability can be cast at the moment (i.e. it's not on cooldown, the player is not dead or under zhonyas)
        /// </summary>
        protected bool CanCastAbility(AbilityKey spellKey)
        {
            if (GameState.ActivePlayer.IsDead || !AbilityCastModes[spellKey].Castable)
                return false;
            if (GameState.ActivePlayer.Abilities.GetAbilityLevel(spellKey) == 0)
                return false;
            if (AbilitiesOnCooldown[spellKey])
                return false;
            int manaCost = ChampionInfo.Costs.GetManaCost(spellKey, GameState.ActivePlayer.Abilities.GetAbilityLevel(spellKey));
            if (GameState.ActivePlayer.Stats.ResourceValue < manaCost)
            {
                // raise not enough mana event
                TriedToCastOutOfMana?.Invoke();
                return false;
            }
            return true;
        }

        protected bool CanRecastAbility(AbilityKey key)
        {
            return AbilityCastModes[key].HasRecast && AbilitiesOnRecast[key] > 0;
        }

        /// <summary>
        /// Starts the cooldown timer for an ability. It should be called after an ability is cast.
        /// </summary>
        protected void StartCooldownTimer(AbilityKey ability, int overrideTime = 0)
        {
            // TODO: Refactor this into tracking cooldowns accurately, 
            // if this method is called twice (needed for Xerath or others that have different cooldowns on different circumstances), it won't work properly
            Task.Run(async () =>
            {
                AbilitiesOnCooldown[ability] = true;
                int cd = overrideTime > 0 ? overrideTime : GetCooldownForAbility(ability);
                await Task.Delay(cd - 350); // a bit less cooldown than the real one (if the user spams)
                AbilitiesOnCooldown[ability] = false;
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

        protected void CancelRecast(AbilityKey ability)
        {
            AbilitiesOnRecast[ability] = 0;
        }

    }
}
