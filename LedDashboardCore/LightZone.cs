namespace LedDashboardCore
{
    /// <summary>
    /// Flag used for specifying what zones the lighting frame should be applied to. If a flag isn't set, this frame won't override the color information that was present before.
    /// </summary>
    public enum LightZone
    {
        None = 0,
        Keyboard = 1, 
        Strip = 2, 
        Mouse = 4, 
        Mousepad = 8, 
        Headset = 16, 
        Keypad = 32, 
        General = 64,
        All = Keyboard | Strip | Mouse | Mousepad | Headset | Keypad | General,
        Desk = Keyboard | Mouse | Mousepad | Keypad,
        MouseKey = Keyboard | Mouse
    }
}
