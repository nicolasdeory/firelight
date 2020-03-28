using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    /// <summary>
    /// Interface that should be used for any class that generates LED data.
    /// The Dispose() method should be used to get rid of periodic tasks such as event loops.
    /// </summary>
    public interface LEDModule : IDisposable
    { 

        public delegate void FrameReadyHandler(object s, Led[] ls);
        /// <summary>
        /// Raised when a new frame is ready to be processed and sent to the LED strip.
        /// </summary>
        public event FrameReadyHandler NewFrameReady;
    }
}
