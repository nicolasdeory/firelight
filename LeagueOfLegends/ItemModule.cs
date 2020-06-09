using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System;
using System.Windows.Forms;

namespace Games.LeagueOfLegends
{
    public abstract class ItemModule : GameElementModule
    {
        protected const string ITEM_ANIMATION_PATH = @"Animations/LeagueOfLegends/Items/";

        public bool OnCooldown { get; private set; }

        private bool itemIsSelected = false;

        /// <summary>
        /// To be set by inherited class. It can change over time for some items such as ward trinkets.
        /// Most of the time, though, it's a constant value
        /// Expressed in milliseconds.
        /// </summary>
        protected int CooldownDuration = 0;

        /// <summary>
        /// Set to true if the animation for casting this item has more priority than others.
        /// (e.g. Eye of the Herald animation has more priority than other spells)
        /// </summary>
        public bool IsPriorityItem { get; protected set; }

        protected override string ModuleTypeName => "Item";

        ItemAttributes ItemAttributes;

        /*  protected delegate void ItemInfoRetrievedHandler();
          protected event ItemInfoRetrievedHandler ItemInfoRetrieved;*/

        public event EventHandler ItemCast;

        public event EventHandler RequestActivation;

        protected AbilityCastMode ItemCastMode;

        private int itemSlot;

        private char activationKey;

        private char lastPressedKey = '\0';

        public int ItemID { get; protected set; }

        protected ItemModule(int itemID, string name, int itemSlot, GameState state, AbilityCastPreference preferredCastMode, bool preloadAllAnimations = false)
            : base(name, state, preferredCastMode, preloadAllAnimations)
        {
            ItemAttributes = ItemUtils.GetItemAttributes(itemID);
            this.ItemID = itemID;
            this.itemSlot = itemSlot;
            this.activationKey = GetKeyForItemSlot(itemSlot); // TODO: Handle key rebinds...

            ItemCast += OnItemActivated;
            ItemCastMode = GetItemCastMode();

            AddAnimatorEvent();
            // Should this be needed for all items modules? Only active ones
            AddInputHandlers();
        }

        protected abstract AbilityCastMode GetItemCastMode();

        protected abstract void OnItemActivated(object s, EventArgs e);

        protected override void OnMouseClick(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                itemIsSelected = false;
            }
            else if (e.Button == MouseButtons.Left) // cooldowns are accounted for here aswell in case between key press and click user died, or did zhonyas...
            {
                if (itemIsSelected)
                {
                    if (CanActivateItem)
                        ActivateItem();
                }
            }
        }

        protected override void OnKeyRelease(object s, KeyEventArgs e)
        {
            var keyCode = e.KeyCode; // HACK why dont just use Keys enum as event arg instead of this
            int index = 0;
            if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
            {
                index = 1;
            }
            char keyChar = char.ToLower(keyCode.ToString()[index]);
            ProcessKeyPress(s, keyChar, true);
        }

        protected override void OnKeyPress(object s, KeyEventArgs e)
        {
            ProcessKeyPress(s, e.KeyCode.ToString().ToLower()[0]);
        }

        protected override void ProcessKeyPress(object s, char keyChar, bool keyUp = false)
        {
            if (keyChar == lastPressedKey && !keyUp)
                return; // prevent duplicate calls. Without this, this gets called every frame a key is pressed.
            lastPressedKey = keyUp ? '\0' : keyChar;
            if (keyChar == activationKey)
            {
                DoCastLogic(keyUp);
            }
        }

        private void DoCastLogic(bool keyUp)
        {
            if (keyUp && !itemIsSelected)
                return; // keyUp event shouldn't trigger anything if the ability is not selected.

            if (!CanActivateItem)
                return;

            if (ItemCastMode.IsInstant) // item is activated with just pressing down the key
            {
                ActivateItem();
            }

            if (ItemCastMode.IsNormal) // item has normal activation
            {
                if (PreferredCastMode == AbilityCastPreference.Normal)
                {
                    itemIsSelected = true;
                }
                if (PreferredCastMode == AbilityCastPreference.Quick)
                {
                    ActivateItem();
                }
                if (PreferredCastMode == AbilityCastPreference.QuickWithIndicator)
                {
                    if (keyUp && itemIsSelected) // Key released, so CAST IT if it's selected
                    {
                        ActivateItem();
                    }
                    else // Key down, so select it
                    {
                        itemIsSelected = true;
                    }
                }
            }
        }

        private void ActivateItem()
        {
            RequestLEDActivation();
            ItemCast?.Invoke(this, null);
            //StartCooldownTimer();
            itemIsSelected = false;
        }

        /// <summary>
        /// Tell to the LoL module to listen for data from this module
        /// </summary>
        protected void RequestLEDActivation()
        {
            RequestActivation?.Invoke(this, null);
        }

        /// <summary>
        /// Returns true if the ability can be cast at the moment (i.e. it's not on cooldown, the player is not dead or under zhonyas)
        /// </summary>
        protected bool CanActivateItem => !GameState.ActivePlayer.IsDead && ItemCastMode.Castable && !OnCooldown;

        /* /// <summary>
         /// Starts the cooldown timer for an ability. It should be called after an ability is cast.
         /// </summary>
         protected void StartCooldownTimer()
         {
             Task.Run(async () =>
             {
                 OnCooldown = true;
                 await Task.Delay(CooldownDuration - 350); // a bit less cooldown than the real one (if the user spams)
                 OnCooldown = false;
             });
         }*/

        protected static int GetCooldownDuration(double baseCooldown, double levelReduction, double averageLevel)
        {
            return (int)((baseCooldown - levelReduction * averageLevel) * 1000);
        }

        private static char GetKeyForItemSlot(int slot)
        {
            return slot switch
            {
                0 => '1',
                1 => '2',
                2 => '3',
                3 => '5',
                4 => '6',
                5 => '7',
                6 => '4',
                _ => '\0'
            };
        }
    }
}
