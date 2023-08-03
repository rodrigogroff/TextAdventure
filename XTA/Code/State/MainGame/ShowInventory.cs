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

            switch (cmdText)
            {
                case "Bag":
                    spriteBatch.DrawString(titleAndCursorFont, "Bag", new Vector2(sx + 2, sy + 2), Color.Black);
                    spriteBatch.DrawString(titleAndCursorFont, "Bag", new Vector2(sx, sy), Color.White * currentAlphaMainBag * 0.8f);
                    break;

                case "Use Item":
                    spriteBatch.DrawString(titleAndCursorFont, "Use Item", new Vector2(sx + 2 - 30, sy + 2), Color.Black);
                    spriteBatch.DrawString(titleAndCursorFont, "Use Item", new Vector2(sx - 30, sy), Color.White * currentAlphaMainBag * 0.8f);
                    break;

                case "Give Item":
                    spriteBatch.DrawString(titleAndCursorFont, "Give Item", new Vector2(sx + 2 - 35, sy + 2), Color.Black);
                    spriteBatch.DrawString(titleAndCursorFont, "Give Item", new Vector2(sx - 35, sy), Color.White * currentAlphaMainBag * 0.8f);
                    break;
            }
            
            DisplayText ( 
                spriteBatch,
                "¨-- Player Inventory --¨",
                new Vector2(sx - 67, sy + 80), 
                cursorHelpFont, 
                0, 
                currentAlphaMainBag );

            for (int i = 0; i < 10; i++)
            {
                var msg = "[" + (i + 1) + "] Bone";

                spriteBatch.DrawString ( 
                    statsFont, 
                    msg, 
                    new Vector2(sx - 305 + 2, sy + 165 + 2 + i * 32), 
                    Color.Black);

                spriteBatch.DrawString ( 
                    statsFont, 
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

                var cursor_text = "";

                switch (cmdText)
                {
                    case "Bag":
                        cursor_text = "Select item to drop, 'Esc' to close window";
                        break;

                    case "Use Item":
                        cursor_text = "Select item to use, 'Esc' to close window";
                        break;

                    case "Give Item":
                        cursor_text = "Select item to give, 'Esc' to close window";
                        break;
                }

                spriteBatch.DrawString( cursorHelpFont, cursor_text, new Vector2(sx, sy), Color.DarkGray * 0.65f);                
                DisplayCursorText(spriteBatch, new Vector2(sx, sy + 13), titleAndCursorFont);
            }
        }
    }
}
