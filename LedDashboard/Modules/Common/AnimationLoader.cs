using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LedDashboard.Modules.Common
{
    public class AnimationLoader
    {
        public static Animation LoadFromFile(string path)
        {
            string text = "";
            try
            {
                text = File.ReadAllText(path);
            } catch (IOException)
            {
                throw new ArgumentException("File does not exist.");
            }
            
            string[] lines = text.Split('\n');
            string[] data = lines[0].Split(',');
            int numLeds = int.Parse(data[0]);
            int numFrames = int.Parse(data[1]);
            string animationData = "";
            if(lines.Length > 2)
            {
                // multiline format
                for (int i = 1; i <= numFrames; i++)
                {
                    lines[i] = lines[i].Replace("\r", "");
                    lines[i] = lines[i].Replace("\n", ",");
                    animationData += lines[i];
                }
            } else
            {
                animationData = lines[1];
            }
            string[] bytes = animationData.Split(',');
            List<HSVColor[]> animation = new List<HSVColor[]>();
            for (int i = 0; i < numFrames; i++)
            {
                animation.Add(new HSVColor[numLeds]);
                for (int j = 0; j < numLeds; j++)
                {
                    Color rgb = Color.FromArgb(int.Parse(bytes[i * numLeds * 3 + j * 3 + 0]), int.Parse(bytes[i * numLeds * 3 + j * 3 + 1]), int.Parse(bytes[i * numLeds * 3 + j * 3 + 2]));
                    HSVColor c = HSVColor.FromRGB(rgb);
                    animation[i][j] = c;
                }
            }
            return new Animation(animation);
        }
    }
}
