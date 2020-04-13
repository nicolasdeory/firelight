using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LedDashboardCore.Modules.Common
{
    public class AnimationLoader
    {
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
                throw new ArgumentException("File does not exist.",e);
            }
            
            string[] lines = text.Split('\n');
            string[] data = lines[0].Split(',');
            int numLeds = int.Parse(data[0]);
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
        }
    }
}
