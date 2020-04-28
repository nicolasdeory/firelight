using System;
using System.Collections.Generic;
using System.Text;

namespace LedDashboardCore
{
    public class LEDFrame
    {
        public object Sender { get; }
        public LEDData Leds { get; }
        public LightZone Zones { get; }
        public bool Priority { get; }

        public LEDFrame(object sender, LEDData ledData, LightZone zones, bool priority = false)
        {
            this.Sender = sender;
            this.Leds = ledData;
            this.Zones = zones;
            this.Priority = priority;
        }

        public static LEDFrame Empty 
        { 
            get
            {
                return new LEDFrame(null, LEDData.Empty, LightZone.None);
            } 
        }

    }
}
