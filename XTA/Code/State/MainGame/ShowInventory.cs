using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        void DisplayShadowInvemtoryText(SpriteBatch spriteBatch, string cmdText, int sx, int sy)
        {
            spriteBatch.DrawString(titleAndCursorFont, cmdText, new Vector2(sx + 2, sy + 2), Color.Black);
            spriteBatch.DrawString(titleAndCursorFont, cmdText, new Vector2(sx, sy), Color.White * currentAlphaMainBag * 0.8f);
        }

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
                case "Bag": DisplayShadowInvemtoryText(spriteBatch, cmdText, sx, sy); break;
                case "Map": DisplayShadowInvemtoryText(spriteBatch, cmdText, sx, sy); break;
                case "Quest": DisplayShadowInvemtoryText(spriteBatch, cmdText, sx - 18, sy); break;
                case "Use Item": DisplayShadowInvemtoryText(spriteBatch, cmdText, sx - 30, sy); break;
                case "Give Item": DisplayShadowInvemtoryText(spriteBatch, cmdText, sx - 35, sy); break;
            }

            switch (cmdText)
            {
                default: DisplayText(spriteBatch, "¨-- Player Inventory --¨", new Vector2(sx - 67, sy + 80), cursorHelpFont, 0, currentAlphaMainBag); break;
                case "Map": DisplayText(spriteBatch, "¨-- Available locations --¨", new Vector2(sx - 72, sy + 80), cursorHelpFont, 0, currentAlphaMainBag); break;
                case "Quest": DisplayText(spriteBatch, "¨-- Player Active Quests --¨", new Vector2(sx - 79, sy + 80), cursorHelpFont, 0, currentAlphaMainBag); break;
            }

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
                    case "Bag": cursor_text = "Select item to drop, 'Esc' to close window"; break;
                    case "Use Item": cursor_text = "Select item to use, 'Esc' to close window"; break;
                    case "Give Item": cursor_text = "Select item to give, 'Esc' to close window"; break;
                    case "Map": cursor_text = "Select next location, 'Esc' to close window"; break;
                    case "Quest": cursor_text = "Select quest to view, 'Esc' to close window"; break;
                }

                spriteBatch.DrawString( cursorHelpFont, cursor_text, new Vector2(sx, sy), Color.DarkGray * 0.65f);                
                DisplayCursorText(spriteBatch, new Vector2(sx, sy + 13), titleAndCursorFont);
            }
        }
    }
}
