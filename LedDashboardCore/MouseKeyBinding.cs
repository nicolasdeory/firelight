using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FirelightCore
{
    public enum BindType
    { 
        Key, 
        Mouse
    }


    public class MouseKeyBinding
    {
        public BindType BindType { get; private set; }
        public Keys KeyCode { get; private set; }
        public MouseButtons MouseButton { get; private set; }

        public MouseKeyBinding(Keys keycode)
        {
            BindType = BindType.Key;
            KeyCode = keycode;
        }

        public MouseKeyBinding(MouseButtons mouse)
        {
            BindType = BindType.Mouse;
            MouseButton = mouse;
        }

    }
}
