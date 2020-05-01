using System;
using System.Collections.Generic;
using System.Text;

namespace LedDashboardCore
{
    public class LEDData
    {
        public const int NUMLEDS_KEYBOARD = 109;
        public const int NUMLEDS_STRIP = 170;
        public const int NUMLEDS_MOUSE = 16;
        public const int NUMLEDS_MOUSEPAD = 16;
        public const int NUMLEDS_HEADSET = 2;
        public const int NUMLEDS_KEYPAD = 20;
        public const int NUMLEDS_GENERAL = 5;

        public static int[] LEDCounts { get; } = new int[]
        { NUMLEDS_KEYBOARD, NUMLEDS_STRIP, NUMLEDS_MOUSE, NUMLEDS_MOUSEPAD, NUMLEDS_HEADSET, NUMLEDS_KEYPAD, NUMLEDS_GENERAL };

        private Led[] keyboard;
        public Led[] Keyboard
        {
            get
            {
                return keyboard;
            }
            set
            {
                if (value.Length != NUMLEDS_KEYBOARD)
                    throw new ArgumentException("Keyboard LED array length must be " + NUMLEDS_KEYBOARD);
                keyboard = value;
            }
        }

        private Led[] strip;
        public Led[] Strip
        {
            get
            {
                return strip;
            }
            set
            {
                if (value.Length != NUMLEDS_STRIP)
                    throw new ArgumentException("Strip LED array length must be " + NUMLEDS_STRIP);
                strip = value;
            }
        }

        private Led[] mouse;
        public Led[] Mouse
        {
            get
            {
                return mouse;
            }
            set
            {
                if (value.Length != NUMLEDS_MOUSE)
                    throw new ArgumentException("Mouse LED array length must be " + NUMLEDS_MOUSE);
                mouse = value;
            }
        }

        private Led[] mousepad;
        public Led[] Mousepad
        {
            get
            {
                return mousepad;
            }
            set
            {
                if (value.Length != NUMLEDS_MOUSEPAD)
                    throw new ArgumentException("Mousepad LED array length must be " + NUMLEDS_MOUSEPAD);
                mousepad = value;
            }
        }

        private Led[] headset;
        public Led[] Headset
        {
            get
            {
                return headset;
            }
            set
            {
                if (value.Length != NUMLEDS_HEADSET)
                    throw new ArgumentException("Headset LED array length must be " + NUMLEDS_HEADSET);
                headset = value;
            }
        }

        private Led[] keypad;
        public Led[] Keypad
        {
            get
            {
                return keypad;
            }
            set
            {
                if (value.Length != NUMLEDS_KEYPAD)
                    throw new ArgumentException("Keypad LED array length must be " + NUMLEDS_KEYPAD);
                keypad = value;
            }
        }

        private Led[] general;
        public Led[] General
        {
            get
            {
                return general;
            }
            set
            {
                if (value.Length != NUMLEDS_GENERAL)
                    throw new ArgumentException("General LED array length must be " + NUMLEDS_GENERAL);
                general = value;
            }
        }

        public static LEDData Empty { 
            get
            { 
               return new LEDData()
               {
                   Keyboard = new Led[NUMLEDS_KEYBOARD],
                   Mouse = new Led[NUMLEDS_MOUSE],
                   Strip = new Led[NUMLEDS_STRIP],
                   Mousepad = new Led[NUMLEDS_MOUSEPAD],
                   Headset = new Led[NUMLEDS_HEADSET],
                   Keypad = new Led[NUMLEDS_KEYPAD],
                   General = new Led[NUMLEDS_GENERAL]
               };
            }
        }

        public List<Led[]> GetArraysForZones(LightZone zones)
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
        }


        private LEDData() { }
    }
}
