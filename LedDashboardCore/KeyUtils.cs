using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboardCore
{
    public class KeyUtils
    {
        /// <summary>
        /// Returns the key that would be in the given 2D position, on a keyboard. Returns -1 if no key is there.
        /// </summary>
        /// <param name="p">2D Position</param>
        /// <returns></returns>
        public static int PointToKey(Point p)
        {
            return pointToKeyArray[p.Y,p.X];
        }

        private static void KeyToPoints()
        {
            if (keyToPointsList == null)
            {
                keyToPointsList = new List<List<Point>>();
                for(int i = 0; i < 88; i++)
                {
                    if (keyToPointsList.Count <= i)
                    {
                        keyToPointsList.Add(new List<Point>());
                    }
                    for(int j = 0; j < 6; j++)
                    {
                        for (int k = 0; k < 19; k++)
                        {
                            if (pointToKeyArray[j,k] == i)
                            {
                                keyToPointsList[i].Add(new Point(k, j));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns several points because a key can span two points or more (e.g. return key)
        /// </summary>
        public static List<Point> KeyToPoint(int key)
        {
            if (keyToPointsList == null) KeyToPoints();
            return keyToPointsList[key];
        }

        private static List<List<Point>> keyToPointsList;
        
        // hardcoded array that maps 2D points to key indices (from the indexKeyMap)
        // used to show properly positioned leds. Positioning may vary for each keyboard model. That should be something to keep in mind.
        private static int[,] pointToKeyArray = new int[6, 19] 
        {
            {0,-1,1,2,3,4,-1,5,6,7,8,-1,9,10,11,12,13,14,15 },
            {16,17,18,19,20,21,22,23,24,25,26,26,27,28,29,29,30,31,32 },
            {33,34,35,36,37,38,38,39,40,41,42,43,44,45,45,46,47,48,49 },
            {50,51,52,53,54,55,55,56,57,58,59,60,61,62,62,46,-1,-1,-1 },
            {63,64,65,66,67,68,69,69,70,71,72,73,74,-1,75,75,-1,76,-1 },
            {77,78,79,79,-1,80,80,80,80,-1,-1,81,82,83,83,84,85,86,87 }
        };
    }
}
