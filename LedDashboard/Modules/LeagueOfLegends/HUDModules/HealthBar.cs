using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LedDashboard.Modules.LeagueOfLegends.Model;

namespace LedDashboard.Modules.LeagueOfLegends.HUDModules
{
    class HealthBar : HUDModule
    {
        

        //bool wasDeadLastFrame = false;
        public void DoFrame(Led[] leds, LightingMode lightMode, GameState gameState)
        {
          /*  if (gameState.PlayerChampion.IsDead)
            {
                for (int i = 0; i < leds.Length; i++)
                {
                    leds[i].Color(DeadColor);
                }
                wasDeadLastFrame = true;
                NewFrameReady?.Invoke(this, this.leds, LightingMode.Line);
            }
            else
            {*/
              /*  if (wasDeadLastFrame)
                {
                    leds.SetAllToBlack();
                    wasDeadLastFrame = false;
                }*/
                

           // }
        }
    }
}
