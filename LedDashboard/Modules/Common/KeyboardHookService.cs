using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LedDashboard
{
    public class KeyboardHookService
    {

        private static KeyboardHookService _instance;
        public static KeyboardHookService Instance
        {
            get
            {
                if (_instance == null) _instance = new KeyboardHookService();
                return _instance;
            }
        }

        private IKeyboardMouseEvents m_GlobalHook;

        /// <summary>
        /// Raised when the mouse is clicked.
        /// </summary>
        public event MouseEventHandler OnMouseClicked; // TODO: Check window in focus. i.e for league of legends make sure it's when the client window is in focus.

        /// <summary>
        /// Raised when a key is pressed (down)
        /// </summary>
        public event KeyPressEventHandler OnKeyPressed;

        /// <summary>
        /// Raised when a key is released
        /// </summary>
        public event KeyEventHandler OnKeyReleased;

        
        //public event KeyEventHandler OnKeyDown; // this shouldn't be needed

        public static void Init()
        {
            _instance = new KeyboardHookService();
        }

        private KeyboardHookService()
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseClick += OnMouseClick;
            m_GlobalHook.KeyPress += OnKeyPress;
            m_GlobalHook.KeyUp += OnKeyRelease;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClicked?.Invoke(sender, e);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPressed?.Invoke(sender, e);
        }

        private void OnKeyRelease(object sender, KeyEventArgs e)
        {
            OnKeyReleased?.Invoke(sender, e);
        }

    }
}