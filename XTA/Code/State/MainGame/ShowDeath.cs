using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void ShowDeath(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pngTexture_deathPage, deathPos, Color.White * currentAlphaDeath);

            int sy = 650, sx = 0;

            if (main.bUltraWideMode)
                sx = main.virtualScreenUltraWidth / 2 - 99;
            else
                sx = main.virtualScreenWidth / 2 - 150;

            spriteBatch.DrawString(cursorHelpFont, "Press 'Esc' to continue...", new Vector2(sx, sy), Color.White);
        }
    }
}
