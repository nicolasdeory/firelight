using System.Collections.Generic;

namespace LedDashboardCore.Modules.Common
{
    public class Animation
    {
        private LEDColorData[] frames;
        public int FrameCount => frames.Length;

        public Animation(LEDColorData[] anim)
        {
            frames = anim;
        }

        public LEDColorData this[int index]
        {
            get => frames[index];
        }
    }
}
