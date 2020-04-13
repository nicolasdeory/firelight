using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboardCore.Modules.Common
{
    public class Animation
    {
        private List<HSVColor[]> frames;
        public int FrameLength => frames[0].Length;
        public int FrameCount => frames.Count;

        public Animation(List<HSVColor[]> anim)
        {
            frames = anim;
        }

        public HSVColor[] this[int index] 
        { 
            get => frames[index];
        }

        public LightingMode AnimationMode
        {
            get
            {
                return frames[0].Length == 88 ? LightingMode.Keyboard : LightingMode.Line; // if there is the number of elements in the array that matches a keyboard frame 
            }
        }
    }
}
