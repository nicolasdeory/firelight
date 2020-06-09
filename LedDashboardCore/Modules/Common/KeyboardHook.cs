using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace LedDashboardCore
{
    public class KeyboardHook
    {

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

        public KeyboardHook()
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