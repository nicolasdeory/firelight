using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.LeagueOfLegends.ChampionModules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard.Modules.LeagueOfLegends
{
    class ItemModule : LEDModule // TODO: Very similar to ChampionModule, refactor some of that stuff inside a common class.
    {
        protected const string ITEM_ANIMATION_PATH = @"Animations/LeagueOfLegends/Items/";

        protected AnimationModule animator; // Animator module that will be useful to display animations

        public bool OnCooldown => _OnCooldown;
        private bool _OnCooldown = false;

        private bool itemIsSelected = false;

        /// <summary>
        /// To be set by inherited class. It can change over time for some items such as ward trinkets.
        /// Most of the time, though, it's a constant value
        /// Expressed in milliseconds.
        /// </summary>
        protected int CooldownDuration = 0;
        protected GameState GameState;

        /// <summary>
        /// Set to true if the animation for casting this item has more priority than others.
        /// (e.g. Eye of the Herald animation has more priority than other spells)
        /// </summary>
        protected bool _IsPriorityItem; // TODO: Fully implement
        public bool IsPriorityItem => _IsPriorityItem;

        ItemAttributes ItemAttributes;

        LightingMode LightingMode;

        public event LEDModule.FrameReadyHandler NewFrameReady;

        protected delegate void GameStateUpdatedHandler(GameState newState);
        /// <summary>
        /// Raised when the player info was updated.
        /// </summary>
        protected event GameStateUpdatedHandler GameStateUpdated;

      /*  protected delegate void ItemInfoRetrievedHandler();
        protected event ItemInfoRetrievedHandler ItemInfoRetrieved;*/

        public event EventHandler ItemCast;

        protected AbilityCastPreference PreferredCastMode; // User defined setting, preferred cast mode.
        protected AbilityCastMode ItemCastMode;

        private int itemSlot;

        private char activationKey;

        private char lastPressedKey = '\0';

        protected int _ItemID;
        public int ItemID => _ItemID;

        protected ItemModule(int itemID, int itemSlot, GameState state, LightingMode preferredMode)
        {
            LightingMode = preferredMode;
            ItemAttributes = ItemUtils.GetItemAttributes(itemID);
            GameState = state;
            this._ItemID = itemID;
            this.itemSlot = itemSlot;
            this.activationKey = GetKeyForItemSlot(itemSlot); // TODO: Handle key rebinds...

            // Should this be needed for all items modules? Only active ones
            KeyboardHookService.Instance.OnMouseClicked += OnMouseClick; // TODO. Abstract this to league of legends module, so it pairs with summoner spells and items.
            KeyboardHookService.Instance.OnKeyPressed += OnKeyPress;
            KeyboardHookService.Instance.OnKeyReleased += OnKeyRelease;
           /* Task.Run(async () =>
            {
                while (true)
                {
                    Console.WriteLine(PreferredCastMode);
                    var xx = PreferredCastMode;
                    await Task.Delay(1000);
                }
            });*/

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
                itemIsSelected = false;
            }
            else if (e.Button == MouseButtons.Left) // cooldowns are accounted for here aswell in case between key press and click user died, or did zhonyas...
            {
                if (itemIsSelected)
                {
                    if (CanActivateItem())
                        ActivateItem();
                }
            }
        }

        private void OnKeyRelease(object s, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode; // HACK why dont just use Keys enum as event arg instead of this
            char keyChar;
            if (keyCode >= 48 && keyCode <= 57) 
            {
                keyChar = e.KeyCode.ToString()[1];
            } else
            {
                keyChar = e.KeyCode.ToString().ToLower()[0];
            }
            ProcessKeyPress(s, keyChar, true);
        }

        private void OnKeyPress(object s, KeyPressEventArgs e)
        {
            ProcessKeyPress(s, e.KeyChar);
        }

        private void ProcessKeyPress(object s, char keyChar, bool keyUp = false)
        {
            if (keyChar == lastPressedKey && !keyUp) return; // prevent duplicate calls. Without this, this gets called every frame a key is pressed.
            lastPressedKey = keyUp ? '\0' : keyChar;
            if (keyChar == activationKey)
            {
                DoCastLogic(keyUp);
            }
        }

        private void DoCastLogic(bool keyUp)
        {
            if (keyUp && !itemIsSelected) return; // keyUp event shouldn't trigger anything if the ability is not selected.

            
            if (ItemCastMode.IsInstant) // item is activated with just pressing down the key
            {
                if (CanActivateItem())
                {
                    ActivateItem();
                }
                return;
            }

            if (ItemCastMode.IsNormal) // item has normal activation
            {
                if (PreferredCastMode == AbilityCastPreference.Normal)
                {
                    if (CanActivateItem()) // normal press & click cast, typical
                    {
                        itemIsSelected = true;
                    }
                    return;

                }

                if (PreferredCastMode == AbilityCastPreference.Quick)
                {
                    if (CanActivateItem())
                    {
                        ActivateItem();
                    }
                    return;
                }

                if (PreferredCastMode == AbilityCastPreference.QuickWithIndicator)
                {
                    if (CanActivateItem())
                    {
                        if (keyUp && itemIsSelected) // Key released, so CAST IT if it's selected
                        {
                            if (CanActivateItem())
                            {
                                ActivateItem();
                            }
                        }
                        else // Key down, so select it
                        {
                            if (CanActivateItem())
                            {
                                itemIsSelected = true;
                            }
                        }
                    }
                }
            }
        }

        private void ActivateItem()
        {
            ItemCast?.Invoke(this, null);
            //StartCooldownTimer();
            itemIsSelected = false;
        }

        /// <summary>
        /// Updates player info and raises the appropiate events.
        /// </summary>
        public void UpdateGameState(GameState updatedGameState)
        {
            GameState = updatedGameState;
            GameStateUpdated?.Invoke(updatedGameState);
        }

        /// <summary>
        /// Returns true if the ability can be cast at the moment (i.e. it's not on cooldown, the player is not dead or under zhonyas)
        /// </summary>
        protected bool CanActivateItem()
        {
            if (GameState.ActivePlayer.IsDead || !ItemCastMode.Castable) return false;
            if (_OnCooldown) return false;
            return true;
        }

       /* /// <summary>
        /// Starts the cooldown timer for an ability. It should be called after an ability is cast.
        /// </summary>
        protected void StartCooldownTimer()
        {
            Task.Run(async () =>
            {
                _OnCooldown = true;
                await Task.Delay(CooldownDuration - 350); // a bit less cooldown than the real one (if the user spams)
                _OnCooldown = false;
            });
        }*/

        public void Dispose()
        {
            animator.Dispose();
            KeyboardHookService.Instance.OnMouseClicked -= OnMouseClick;
            KeyboardHookService.Instance.OnKeyPressed -= OnKeyPress;
            KeyboardHookService.Instance.OnKeyReleased -= OnKeyRelease;
        }

        public void StopAnimations()
        {
            animator.StopCurrentAnimation();
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
