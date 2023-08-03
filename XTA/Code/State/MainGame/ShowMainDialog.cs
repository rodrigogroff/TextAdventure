using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void ShowMainDialog(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                pngTexture_dialog, 
                mainDialogPos, 
                Color.White * currentAlphaMainDialog);

            int sx = 0, sy = 0;
            
            if (main.bUltraWideMode)
                sx = main.virtualScreenUltraWidth / 2 - 55;
            else
                sx = 509;

            MainTitle = "This is very big text so deal with it";

            float titleWidth = cursorHelpFont.MeasureString(MainTitle).X;

            spriteBatch.DrawString(
                titleAndCursorFont, 
                MainTitle, 
                new Vector2(sx - titleWidth / 2, 143), 
                Color.Red * currentAlphaMainDialog * 0.8f);

            sy = 290;

            if (main.bUltraWideMode)
                sx = main.virtualScreenUltraWidth / 2 - 376;
            else
                sx = 190;

            DisplayText(spriteBatch, textToDisplay, new Vector2(sx, sy), roomTextFont);

            sy = 914;

            if (main.bUltraWideMode)
                sx = main.virtualScreenUltraWidth / 2 - 294;
            else
                sx = 270;

            if (bTextDisplayed)
            {
                if (!bShowBag)
                {
                    spriteBatch.DrawString(
                        cursorHelpFont, 
                        "Type 'Help' for available commands; 'Enter' to continue", 
                        new Vector2(sx, sy), 
                        Color.DarkGray * 0.35f);

                    DisplayCursorText (spriteBatch, new Vector2(sx, sy + 13), titleAndCursorFont);
                }
            }
            else
            {
                spriteBatch.DrawString(
                    cursorHelpFont, 
                    "Press 'Space' key to skip text, 'Esc' to finish", 
                    new Vector2(sx, sy), 
                    Color.DarkGray * 0.65f);
            }
        }
    }
}
