using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using FirelightCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirelightCore.Modules.Common;
using FirelightCore.Modules.BasicAnimation;

namespace Games.LeagueOfLegends
{
    public abstract class SummonerSpellModule : GameElementModule
    {
        const string VERSION_ENDPOINT = "https://ddragon.leagueoflegends.com/api/versions.json";
        const string SUMMONER_SPELL_INFO_ENDPOINT = "http://ddragon.leagueoflegends.com/cdn/{0}/data/en_US/summoner.json";

        protected delegate void SpellInfoLoadedHandler(SummonerSpellAttributes attributes);
        /// <summary>
        /// Raised when the player info was updated.
        /// </summary>
        protected event SpellInfoLoadedHandler SummonerSpellInfoLoaded;

        protected event EventHandler<SpellKey> AbilityCast;
        protected event EventHandler<SpellKey> AbilityRecast;

        protected override string ModuleTypeName => "SummonerSpell";

        protected SummonerSpellAttributes SpellInfo;

        protected AbilityCastMode AbilityCastMode;

        private SpellKey SelectedAbility = SpellKey.None; // Currently selected ability (for example, if you pressed Q but you haven't yet clicked LMB to cast the ability)
        private char lastPressedKey = '\0';

        /// <summary>
        /// Keeps track of whether the ability is currently on cooldown. 
        /// </summary>
        public bool AbilityOnCooldown { get; protected set; } = false;

        /// <summary>
        /// Keeps track of whether the spell can currently be RE-CAST (eg. Poro Summoner)
        /// </summary>
        protected int AbilityOnRecast = 0;

        /// <summary>
        /// The preferred cast modes for abilities (e.g. Quick Cast, Smart Cast)
        /// </summary>
        protected AbilityCastPreference spellCastPreference;

        /// <summary>
        /// The key that is assigned to this ability (e.g. D, F)
        /// </summary>
        protected SpellKey AssignedSpellKey = SpellKey.None;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="spellName"></param>
        /// <param name="key"></param>
        /// <param name="animator">We take an animator because we dont want the summoner anims + champion abilities to come from only one animator</param>
        /// <param name="gameState"></param>
        /// <param name="preloadAllAnimations"></param>
        protected SummonerSpellModule(string spellName, SpellKey key, AnimationModule animator, GameState gameState, bool preloadAllAnimations = false)
            : base(spellName, gameState, preloadAllAnimations)
        {
            spellCastPreference = ModuleAttributes.GetSpellCastPreference();

            AbilityCastMode = GetCastMode();

            AssignedSpellKey = key;

            LoadSpellInformation(spellName);

            SummonerSpellInfoLoaded += OnSpellInfoLoaded;
            Animator = animator;
        }

        /// <summary>
        /// Called when the champion info has been retrieved.
        /// </summary>
        protected virtual void OnSpellInfoLoaded(SummonerSpellAttributes spellInfo)
        {
            AddAnimatorEvent();
            AbilityCast += OnAbilityCast;
            AbilityRecast += OnAbilityRecast;
        }

        /// <summary>
        /// Called when an ability is cast.
        /// </summary>
        protected virtual void OnAbilityCast(object s, SpellKey key)
        {
            Task.Run(GetAbilityCastTask());
        }
        /// <summary>
        /// Called when an ability is casted again (few champions have abilities that can be recast, only those with special abilities such as Vel'Koz or Zoes Q)
        /// </summary>
        protected virtual void OnAbilityRecast(object s, SpellKey key)
        {
            Task.Run(GetAbilityRecastTask());
        }

        private Func<Task> GetAbilityCastTask()
        {
            return OnCast;
        }
        private Func<Task> GetAbilityRecastTask()
        {
            return OnRecast;
        }

        protected abstract AbilityCastMode GetCastMode();

        protected virtual Task OnCast() => Task.CompletedTask;
        protected virtual Task OnRecast() => Task.CompletedTask;

        private void LoadSpellInformation(string spellName)
        {
            Task.Run(async () =>
            {
                SpellInfo = await GetSpellInformation(spellName);
                AddInputHandlers();
                SummonerSpellInfoLoaded?.Invoke(SpellInfo);
            });
        }

        /// <summary>
        /// Retrieves the attributes for a given champ (ability cooldowns, mana costs, etc.)
        /// </summary>
        /// <param name="spellName">Internal champion name (i.e. Heal -> SummonerHeal)</param>
        /// <returns></returns>
        private async Task<SummonerSpellAttributes> GetSpellInformation(string spellName)
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

            string allSummonersJSON;
            try
            {
                allSummonersJSON = await WebRequestUtil.GetResponse(String.Format(SUMMONER_SPELL_INFO_ENDPOINT, latestVersion));
            }
            catch (WebException e)
            {
                throw new InvalidOperationException("Error retrieving spell data for '" + spellName + "'", e);
            }
            dynamic allSpellData = JsonConvert.DeserializeObject<dynamic>(allSummonersJSON);
            return SummonerSpellAttributes.FromData(allSpellData.data[spellName]);
        }

        protected override void OnMouseDown(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (SelectedAbility != SpellKey.None && !CanRecastAbility() && !AbilityCastMode.RecastOnKeyUp)
                    SelectedAbility = SpellKey.None;
            }
            else if (e.Button != MouseButtons.Left)
            {
                if (AssignedSpellKey == SpellKey.D && ModuleAttributes.Summoner1Binding.BindType == BindType.Mouse && e.Button == ModuleAttributes.Summoner1Binding.MouseButton)
                {
                    DoCastLogicForAbility(false);
                }
                else if (AssignedSpellKey == SpellKey.F && ModuleAttributes.Summoner2Binding.BindType == BindType.Mouse && e.Button == ModuleAttributes.Summoner2Binding.MouseButton)
                {
                    DoCastLogicForAbility(false);
                }
            }
        }

        protected override void OnMouseUp(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (SelectedAbility != SpellKey.None && !CanRecastAbility() && !AbilityCastMode.RecastOnKeyUp)
                    SelectedAbility = SpellKey.None;
            }
            else if (e.Button == MouseButtons.Left) // cooldowns are accounted for here aswell in case between key press and click user died, or did zhonyas...
            {
                if (SelectedAbility == SpellKey.None) return;

                if (CanCastAbility())
                {
                    if (CanRecastAbility())
                    {
                        // its a recast
                        RecastAbility();
                    }
                    else
                    {
                        CastAbility();
                    }
                }
            }
            else
            {
                if (AssignedSpellKey == SpellKey.D && ModuleAttributes.Summoner1Binding.BindType == BindType.Mouse && e.Button == ModuleAttributes.Summoner1Binding.MouseButton)
                {
                    DoCastLogicForAbility(false);
                }
                else if (AssignedSpellKey == SpellKey.F && ModuleAttributes.Summoner2Binding.BindType == BindType.Key && e.Button == ModuleAttributes.Summoner2Binding.MouseButton)
                {
                    DoCastLogicForAbility(false);
                }

            }
        }

        protected override void OnKeyRelease(object s, KeyEventArgs e)
        {
            ProcessKeyPress(s, e.KeyCode, true);
        }

        protected override void OnKeyPress(object s, KeyEventArgs e)
        {
            ProcessKeyPress(s, e.KeyCode);
        }

        protected override void ProcessKeyPress(object s, Keys key, bool keyUp = false)
        {
            base.ProcessKeyPress(s, key, keyUp);
            // TODO: quick cast with indicator bug - repro: hold w, then hold q, then right click, then release w, then release q. The ability is cast, even when it shouldn't.
            // Debug.WriteLine("Keypressed. Selected: " + SelectedAbility);
            if (AssignedSpellKey == SpellKey.D && ModuleAttributes.Summoner1Binding.BindType == BindType.Key && key == ModuleAttributes.Summoner1Binding.KeyCode)
            {
                DoCastLogicForAbility(keyUp);
            }
            if (AssignedSpellKey == SpellKey.F && ModuleAttributes.Summoner2Binding.BindType == BindType.Key && key == ModuleAttributes.Summoner2Binding.KeyCode)
            {
                DoCastLogicForAbility(keyUp);
            }
        }

        private void DoCastLogicForAbility(bool keyUp)
        {
            if (keyUp && SelectedAbility != AssignedSpellKey)
                return; // keyUp event shouldn't trigger anything if the ability is not selected.

            if (!CanCastAbility())
                return;

            AbilityCastMode castMode = AbilityCastMode;
            //Debug.WriteLine(key + " " + (keyUp ? "up" : "down"));

            if (castMode.HasRecast && AbilityOnRecast > 0)
            {
                //Debug.WriteLine(castMode);
                if (castMode.RecastMode.IsInstant)
                {
                    RecastAbility();
                    return;
                }
                if (spellCastPreference == AbilityCastPreference.Normal)
                {
                    if (castMode.RecastMode.IsNormal)
                    {
                        SelectedAbility = AssignedSpellKey;
                        // RECAST SELECTED
                    }
                    if (castMode.RecastMode.RecastOnKeyUp && !keyUp)
                    {
                        RecastAbility();
                    }
                    return;
                }
                if (spellCastPreference == AbilityCastPreference.Quick)
                {
                    RecastAbility();
                    return;
                }
                if (spellCastPreference == AbilityCastPreference.QuickWithIndicator)
                {
                    if (castMode.RecastMode.RecastOnKeyUp && keyUp && SelectedAbility == AssignedSpellKey)
                    {
                        RecastAbility();
                    }
                    if (castMode.RecastMode.IsNormal)
                    {
                        SelectedAbility = AssignedSpellKey;
                        // RECAST SELECTED
                    }
                    if (castMode.RecastMode.IsNormal && keyUp && SelectedAbility == AssignedSpellKey)
                    {
                        if (CanCastAbility())
                        {
                            RecastAbility();
                        }
                    }
                    return;
                }
                return;
            }

            if (castMode.IsInstant) // ability is cast with just pressing down the key
            {
                CastAbility();
                return;
            }

            if (castMode.IsNormal) // ability has normal cast
            {
                if (spellCastPreference == AbilityCastPreference.Normal)
                {
                    SelectedAbility = AssignedSpellKey;
                    return;
                }

                if (spellCastPreference == AbilityCastPreference.Quick)
                {
                    CastAbility();
                    return;
                }

                if (spellCastPreference == AbilityCastPreference.QuickWithIndicator)
                {
                    if (keyUp && SelectedAbility == AssignedSpellKey) // Key released, so CAST IT if it's selected
                    {
                        CastAbility();
                    }
                    else // Key down, so select it
                    {
                        SelectedAbility = AssignedSpellKey;
                    }
                }
            }
        }

        private void CastAbility()
        {
            AbilityCast?.Invoke(this, AssignedSpellKey);
            if (AbilityCastMode.HasRecast)
            {
                StartRecastTimer();
            }
            else
            {
                if (!AbilityCastMode.IsPointAndClick) // no cooldown for point and clicks
                    StartCooldownTimer();
            }
            if (AbilityCastMode.RecastMode != null && AbilityCastMode.RecastMode.RecastOnKeyUp)
                SelectedAbility = AssignedSpellKey;
            else
                SelectedAbility = SpellKey.None;
        }
        private void RecastAbility()
        {
            AbilityRecast?.Invoke(this, AssignedSpellKey);
            AbilityOnRecast--;
            if (AbilityOnRecast == 0)
            {
                if (AbilityCastMode.RecastMode.RecastOnKeyUp)
                    SelectedAbility = SpellKey.None;
                StartCooldownTimer();
            }

        }

        /// <summary>
        /// Returns the cooldown in milliseconds for a given ability, after applying cooldown reduction.
        /// </summary>
        protected int GetCooldownForAbility()
        {
            AbilityLoadout abilities = GameState.ActivePlayer.Abilities;
            AbilityCooldown costs = SpellInfo.Cooldown;
            // TODO: SummonerSpell Haste is not supported for now. Maybe check if boots are in the inventory instead.
            return costs[0];
        }

        /// <summary>
        /// Returns true if the ability can be cast at the moment (i.e. it's not on cooldown, the player is not dead or under zhonyas)
        /// </summary>
        protected bool CanCastAbility()
        {
            if (GameState.ActivePlayer.IsDead || !AbilityCastMode.Castable)
                return false;
            if (AbilityOnCooldown)
                return false;
            return true;
        }

        protected bool CanRecastAbility()
        {
            return AbilityCastMode.HasRecast && AbilityOnRecast > 0;
        }

        /// <summary>
        /// Starts the cooldown timer for an ability. It should be called after an ability is cast.
        /// </summary>
        protected void StartCooldownTimer(int overrideTime = 0)
        {
            // TODO: Refactor this into tracking cooldowns accurately, 
            // if this method is called twice (needed for Xerath or others that have different cooldowns on different circumstances), it won't work properly
            Task.Run(async () =>
            {
                AbilityOnCooldown = true;
                int cd = overrideTime > 0 ? overrideTime : GetCooldownForAbility();
                await Task.Delay(cd - 350); // a bit less cooldown than the real one (if the user spams)
                AbilityOnCooldown = false;
            });
        }

        private void StartRecastTimer()
        {
            Task.Run(async () =>
            {
                AbilityOnRecast = AbilityCastMode.MaxRecasts;
                await Task.Delay(AbilityCastMode.RecastTime);
                if (AbilityOnRecast > 0) // if user hasn't recast yet
                {
                    AbilityOnRecast = 0;
                    StartCooldownTimer();
                }
            });
        }

        protected void CancelRecast(SpellKey ability)
        {
            AbilityOnRecast = 0;
        }

    }
}
