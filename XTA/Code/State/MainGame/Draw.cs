using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public override void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(pngTexture_wallpaper, wallpaperPosition, Color.White * currentAlphaWallpaper);

            switch (internalState)
            {
                case MAIN_START_FADEIN_MAINDIALOG:
                    spriteBatch.Draw(pngTexture_dialog, mainDialogPos, Color.White * currentAlphaMainDialog);
                    break;

                case MAIN_COMPLETE:

                    if (bDeath)
                    {
                        ShowDeath(spriteBatch);
                    }
                    else
                    {
                        if (main.bUltraWideMode)
                        {
                            if (bShowStats)
                                ShowStat(spriteBatch);
                            if (bShowBag)
                                ShowInventory(spriteBatch);
                        }
                        else
                        {
                            if (bShowBag)
                                ShowInventory(spriteBatch);
                            else if (bShowStats)
                                ShowStat(spriteBatch);
                        }
                        ShowMainDialog(spriteBatch);
                    }
                    break;
            }
        }
    }
}
