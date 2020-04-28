namespace LedDashboardCore
{
    /// <summary>
    /// Flag used for specifying what zones the lighting frame should be applied to. If a flag isn't set, this frame won't override the color information that was present before.
    /// </summary>
    public enum LightZone
    {
        Keyboard, Strip, Mouse, Mousepad, Headset, Keypad, General
    }
}
