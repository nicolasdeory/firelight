using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FirelightCore
{
    public static class GlobalHooker
    {

        public static event EventHandler<KeyEventArgs> OnKeyDown;
        public static event EventHandler<KeyEventArgs> OnKeyUp;

        public static void Init()
        {
            IKeyboardMouseEvents globalEvents = Hook.GlobalEvents();
            globalEvents.KeyDown += (s, e) => { OnKeyDown?.Invoke(s, e); };
            globalEvents.KeyUp += (s, e) => { OnKeyUp?.Invoke(s, e); };
        }
    }
}
