using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XTA.Code.Infra;

namespace XTA.Code.Components
{
    public class MainTitleEvent : GameEvent
    {
        public const int 
            DELAY_TO_PRESENT = 0,
            FADEIN_PRESENT = 1,
            PRESENT = 2;
        
        public int 
            myState = DELAY_TO_PRESENT,
            indexer = 0,
            framesStart = 1;

        public float[] curve;
        public float currentAlpha = 0.0f;

        Texture2D 
            pngWallpaper,
            pngGameLogo;

        Vector2 
            wallpaperPosition = new Vector2(0, 0),
            position = new Vector2(0, 0);

        public override void LoadContent(ContentManager Content, GameXTA main, GraphicsDevice d)
        {
            pngGameLogo = LoadTexture(d, "Hellfire");
            pngWallpaper = LoadTexture(d, "bg19");

            curve = new GameFunctions().GenerateLogarithmicArray(300);

            if (main.bUltraWideMode)
            {
                position = new Vector2(main.virtualScreenUltraWidth / 2 - pngGameLogo.Width / 2, main.virtualScreenUltraHeight / 2 - pngGameLogo.Height / 2 - 180);
                wallpaperPosition = new Vector2(main.virtualScreenUltraWidth / 2 - pngWallpaper.Width / 2, main.virtualScreenUltraHeight / 2 - pngWallpaper.Height / 2);
            }
            else
            {
                position = new Vector2(main.virtualScreenWidth / 2 - pngGameLogo.Width / 2, main.virtualScreenHeight / 2 - pngGameLogo.Height / 2 - 180);
                wallpaperPosition = new Vector2(main.virtualScreenWidth / 2 - pngWallpaper.Width / 2, main.virtualScreenHeight / 2 - pngWallpaper.Height / 2);
            }
        }

        public override void Update()
        {
            switch (myState)
            {
                case DELAY_TO_PRESENT:
                    if (--framesStart == 0)
                        myState++;
                    break;

                case FADEIN_PRESENT:

                    if (currentAlpha < 1)
                        currentAlpha += curve[indexer++];

                    if (currentAlpha >= 1)
                    {
                        myState++;
                    }

                    break;

                case PRESENT:
                    break;                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (myState)
            {
                case FADEIN_PRESENT:
                case PRESENT:
                    spriteBatch.Draw(pngWallpaper, wallpaperPosition, Color.White * currentAlpha);
                    spriteBatch.Draw(pngGameLogo, position, Color.White * currentAlpha);
                    break;
            }
        }
    }
}
