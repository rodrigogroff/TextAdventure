using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void ShowStat(SpriteBatch spriteBatch)
        {
            int idx = curve.Length - indexAlphaCurveStats;

            if (idx >= 0 && idx < curve.Length)
            {
                if (main.bUltraWideMode)
                    mainStatsPos.X = (main.virtualScreenUltraWidth - pngTexture_stats.Width - 40) + 400 * curve[idx];
                else
                    mainStatsPos.X = 1050 + 400 * curve[idx];
            }
            else
            {
                if (main.bUltraWideMode)
                    mainStatsPos.X = (main.virtualScreenUltraWidth - pngTexture_stats.Width - 40);
                else
                    mainStatsPos.X = 1050;
            }

            spriteBatch.Draw(pngTexture_stats, mainStatsPos, Color.White * currentAlphaMainStats);

            string name = "^Name^ Marco Polo", pType = "^Type^ Elf Warrior form";

            var lstStrings = new List<string>
                {
                    "                 ¨-- Character Stats --¨",
                    "                            ",
                    "".PadLeft((28 - name.Length/2), ' ') + name,
                    "".PadLeft((28 - pType.Length/2), ' ') + pType,
                    "",
                    "",
                    "                    ¨-- Attributes --¨",
                    "                           None",
                    "",
                    "",
                    "                      ¨-- Traits --¨",
                    "                           None",
                    "",
                };

            idx = 0;
            foreach (string str in lstStrings)
            {
                DisplayText ( 
                    spriteBatch, 
                    str, 
                    new Vector2(mainStatsPos.X + 84, mainStatsPos.Y + 220 + (idx++ * 20)), 
                    lucidaBigFont, 
                    12, 
                    currentAlphaMainStats);
            }
        }
    }
}
