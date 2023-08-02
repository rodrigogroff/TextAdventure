using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void ShowInventory(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pngTexture_bag, mainBagPos, Color.White * currentAlphaMainBag);

            int sx = 0, sy = 143;

            if (main.bUltraWideMode)
                sx = 410;
            else
                sx = 1453;

            spriteBatch.DrawString(titleFont, "Bag", new Vector2(sx + 2, sy + 2), Color.Black);
            spriteBatch.DrawString(titleFont, "Bag", new Vector2(sx, sy), Color.White * currentAlphaMainBag * 0.8f);

            DisplayText ( 
                spriteBatch,
                "                 ¨-- Player Inventory --¨",
                new Vector2(sx - 180, sy + 80), 
                textFont, 
                0, 
                currentAlphaMainBag );

            for (int i = 0; i < 10; i++)
            {
                var msg = "[" + (i + 1) + "] Bone";

                spriteBatch.DrawString ( 
                    lucidaBigFont, 
                    msg, 
                    new Vector2(sx - 305 + 2, sy + 165 + 2 + i * 32), 
                    Color.Black);

                spriteBatch.DrawString ( 
                    lucidaBigFont, 
                    msg, 
                    new Vector2(sx - 305, sy + 165 + i * 32), 
                    Color.White * currentAlphaMainBag * 0.8f);
            }

            if (currentAlphaMainBag >= 1)
            {
                sy = 856;
                if (main.bUltraWideMode)
                    sx = 260;
                else
                    sx = 1300;
                
                spriteBatch.DrawString(
                    textFont,
                    "Select item to drop, 'Esc' to close window", 
                    new Vector2(sx, sy), 
                    Color.DarkGray * 0.65f);
                
                DisplayCursorText(spriteBatch, new Vector2(sx, sy + 13), lucidaBigFont);
            }
        }
    }
}
