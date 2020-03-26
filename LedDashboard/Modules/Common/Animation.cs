using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.Common
{
    public class Animation
    {
        private List<HSVColor[]> frames;
        public int FrameLength { get { return frames[0].Length; } }
        public int FrameCount { get { return frames.Count; } }

        public Animation(List<HSVColor[]> anim)
        {
            frames = anim;
        }

        public HSVColor[] this[int index] 
        { 
            get => frames[index];
        }
    }
}
