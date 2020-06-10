using System;
using System.Collections.Generic;
using System.Text;

namespace LedDashboardCore
{
    public class LEDFrame
    {
        public List<object> SenderChain { get; }
        public LEDData Leds { get; }
        public LightZone Zones { get; }
        public bool Priority { get; }
        
        public object LastSender { get { return SenderChain[SenderChain.Count - 1]; } }

        public LEDFrame(object sender, LEDData ledData, LightZone zones, bool priority = false)
        {
            if (sender == null)
                this.SenderChain = new List<object>();
            else
                this.SenderChain = new List<object>() { sender };
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

        public static LEDFrame CreateEmpty(object sender)
        {
            return new LEDFrame(sender, LEDData.Empty, LightZone.None);
        }

    }
}
