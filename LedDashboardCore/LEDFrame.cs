using System;
using System.Collections.Generic;
using System.Text;

namespace LedDashboardCore
{
    public class LEDFrame
    {
        public object Sender { get; }
        public LEDData Leds { get; }
        //public LightZone Zone { get; }
        public bool Priority { get; }

        public LEDFrame(object sender, LEDData ledData, bool priority = false)
        {
            this.Sender = sender;
            this.Leds = ledData;
            //this.Zone = zone;
            this.Priority = priority;
        }

        public static LEDFrame Empty 
        { 
            get
            {
                return new LEDFrame(null, LEDData.Empty);
            } 
        }

    }
}
