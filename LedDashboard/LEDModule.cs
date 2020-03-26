using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    public interface LEDModule
    { 

        public delegate void FrameReadyHandler(object s, Led[] ls);
        /// <summary>
        /// Raised when a new frame is ready to be processed and sent to the LED strip.
        /// </summary>
        public event FrameReadyHandler NewFrameReady;
    }
}
