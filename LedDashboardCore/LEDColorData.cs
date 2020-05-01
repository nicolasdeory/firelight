using System;
using System.Collections.Generic;
using System.Text;

namespace LedDashboardCore
{
    public class LEDColorData
    {

        private HSVColor[] keyboard;
        public HSVColor[] Keyboard
        {
            get
            {
                return keyboard;
            }
            set
            {
                if (value.Length != LEDData.NUMLEDS_KEYBOARD)
                    throw new ArgumentException("Keyboard LED array length must be " + LEDData.NUMLEDS_KEYBOARD);
                keyboard = value;
            }
        }

        private HSVColor[] strip;
        public HSVColor[] Strip
        {
            get
            {
                return strip;
            }
            set
            {
                if (value.Length != LEDData.NUMLEDS_STRIP)
                    throw new ArgumentException("Strip LED array length must be " + LEDData.NUMLEDS_STRIP);
                strip = value;
            }
        }

        private HSVColor[] mouse;
        public HSVColor[] Mouse
        {
            get
            {
                return mouse;
            }
            set
            {
                if (value.Length != LEDData.NUMLEDS_MOUSE)
                    throw new ArgumentException("Mouse LED array length must be " + LEDData.NUMLEDS_MOUSE);
                mouse = value;
            }
        }

        private HSVColor[] mousepad;
        public HSVColor[] Mousepad
        {
            get
            {
                return mousepad;
            }
            set
            {
                if (value.Length != LEDData.NUMLEDS_MOUSEPAD)
                    throw new ArgumentException("Mousepad LED array length must be " + LEDData.NUMLEDS_MOUSEPAD);
                mousepad = value;
            }
        }

        private HSVColor[] headset;
        public HSVColor[] Headset
        {
            get
            {
                return headset;
            }
            set
            {
                if (value.Length != LEDData.NUMLEDS_HEADSET)
                    throw new ArgumentException("Headset LED array length must be " + LEDData.NUMLEDS_HEADSET);
                headset = value;
            }
        }

        private HSVColor[] keypad;
        public HSVColor[] Keypad
        {
            get
            {
                return keypad;
            }
            set
            {
                if (value.Length != LEDData.NUMLEDS_KEYPAD)
                    throw new ArgumentException("Keypad LED array length must be " + LEDData.NUMLEDS_KEYPAD);
                keypad = value;
            }
        }

        private HSVColor[] general;
        public HSVColor[] General
        {
            get
            {
                return general;
            }
            set
            {
                if (value.Length != LEDData.NUMLEDS_GENERAL)
                    throw new ArgumentException("General LED array length must be " + LEDData.NUMLEDS_GENERAL);
                general = value;
            }
        }

        public static LEDColorData Empty { 
            get
            { 
               return new LEDColorData()
               {
                   Keyboard = new HSVColor[LEDData.NUMLEDS_KEYBOARD],
                   Mouse = new HSVColor[LEDData.NUMLEDS_MOUSE],
                   Strip = new HSVColor[LEDData.NUMLEDS_STRIP],
                   Mousepad = new HSVColor[LEDData.NUMLEDS_MOUSEPAD],
                   Headset = new HSVColor[LEDData.NUMLEDS_HEADSET],
                   Keypad = new HSVColor[LEDData.NUMLEDS_KEYPAD],
                   General = new HSVColor[LEDData.NUMLEDS_GENERAL]
               };
            }
        }

       /* public List<Led[]> GetArraysForZones(LightZone zones)
        {
            List<Led[]> list = new List<Led[]>();
            if (zones.HasFlag(LightZone.Keyboard))
            {
                list.Add(Keyboard);
            }
            if (zones.HasFlag(LightZone.Strip))
            {
                list.Add(Strip);
            }
            if (zones.HasFlag(LightZone.Mouse))
            {
                list.Add(Mouse);
            }
            if (zones.HasFlag(LightZone.Mousepad))
            {
                list.Add(Mousepad);
            }
            if (zones.HasFlag(LightZone.Headset))
            {
                list.Add(Headset);
            }
            if (zones.HasFlag(LightZone.Keypad))
            {
                list.Add(Keypad);
            }
            if (zones.HasFlag(LightZone.General))
            {
                list.Add(General);
            }
            return list;
        }*/


        private LEDColorData() { }
    }
}
