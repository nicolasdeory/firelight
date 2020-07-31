using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace FirelightCore.Modules.Common
{
    public class AnimationLoader
    {
        /*public static Animation LoadFromFileLegacy(string path)
        {
            string text = "";
            try
            {
                text = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                throw new ArgumentException("File does not exist.", e);
            }

            string[] lines = text.Split('\n');
            string[] data = lines[0].Split(',');
            int  = int.Parse(data[0]);
            int numFrames = int.Parse(data[1]);

            string animationData = lines[1];
            if (lines.Length > 2)
            {
                var animationDataBuilder = new StringBuilder();
                // multiline format
                for (int i = 1; i <= numFrames; i++)
                {
                    lines[i] = lines[i].Replace("\r", "").Replace('\n', ',');
                    animationDataBuilder.Append(lines[i]);
                }
                animationData = animationDataBuilder.ToString();
            }
            string[] bytes = animationData.Split(',');
            List<HSVColor[]> animation = new List<HSVColor[]>();
            for (int i = 0; i < numFrames; i++)
            {
                animation.Add(new HSVColor[numLeds]);
                for (int j = 0; j < numLeds; j++)
                {
                    int baseIndex = i * numLeds * 3 + j * 3;
                    Color rgb = Color.FromArgb(int.Parse(bytes[baseIndex]), int.Parse(bytes[baseIndex + 1]), int.Parse(bytes[baseIndex + 2]));
                    HSVColor c = HSVColor.FromRGB(rgb);
                    animation[i][j] = c;
                }
            }
            return new Animation(animation);
        }*/

        public static Animation LoadFromFile(string path)
        {
            string text = "";
            try
            {
                text = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine("Error loading animation: File does not exist.");
                return Animation.Empty;
                //throw new ArgumentException("File does not exist.", e);
            }

            string[] lines = text.Split('\n');
            string[] data = lines[0].Split(',');
            int version = int.Parse(data[0]);
            int numFrames = int.Parse(data[1]);

            if (version != 2)
            {
                Debug.WriteLine("Error parsing: Unsupported animation format version");
                return Animation.Empty;
                //throw new FileFormatException("Error parsing: Unsupported animation format version");
            }
            List<string> animLines = new List<string>();
            for(int i = 1; i < lines.Length - 1; i++)
            {
                animLines.Add(lines[i]);
            }

            LEDColorData[] frames = new LEDColorData[numFrames];
            for(int f = 0; f < numFrames; f++)
            {
                string[] zones = animLines[f].Split(";");
                if (zones.Length != 7)
                {
                    throw new FileFormatException("Error parsing: Zone length != 7");
                }

                LEDColorData frameData = LEDColorData.Empty;
                for (int i = 0; i < 7; i++)
                {
                    string zone = zones[i];
                    string[] bytes = zone.Split(',');
                    HSVColor[] colArray = new HSVColor[LEDData.LEDCounts[i]];

                    if (bytes.Length != LEDData.LEDCounts[i]*3)
                    {
                        throw new FileFormatException("Error parsing: Unexpected length of light zone array");
                    }
                    for (int j = 0; j < LEDData.LEDCounts[i]; j++)
                    {
                        int baseIndex = j * 3;
                        Color rgb = Color.FromArgb(int.Parse(bytes[baseIndex]), int.Parse(bytes[baseIndex + 1]), int.Parse(bytes[baseIndex + 2]));
                        HSVColor c = HSVColor.FromRGB(rgb);
                        colArray[j] = c;
                    }
                    switch (i)
                    {
                        case 0:
                            frameData.Keyboard = colArray;
                            break;
                        case 1:
                            frameData.Strip = colArray;
                            break;
                        case 2:
                            frameData.Mouse = colArray;
                            break;
                        case 3:
                            frameData.Mousepad = colArray;
                            break;
                        case 4:
                            frameData.Headset = colArray;
                            break;
                        case 5:
                            frameData.Keypad = colArray;
                            break;
                        case 6:
                            frameData.General = colArray;
                            break;
                    }
                }
                frames[f] = frameData;                
            }
            
            return new Animation(frames);
        }
    }
}
