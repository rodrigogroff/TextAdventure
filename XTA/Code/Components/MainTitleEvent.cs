using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XTA.Code.Infra;

namespace XTA.Code.Components
{
    public class MainTitleEvent : GameEvent
    {
        public const int DELAY_TO_PRESENT = 0;
        public const int FADEIN_PRESENT = 1;
        public const int PRESENT = 2;
        
        public int myState = DELAY_TO_PRESENT;

        public int framesStart = 1;

        public float currentAlpha = 0.0f;

        Texture2D pngWallpaper;
        Texture2D pngGameLogo;

        public int indexer = 0;
        public float[] curve;

        Vector2 wallpaperPosition = new Vector2(0, 0);
        Vector2 position = new Vector2(0, 0);

        public override void LoadContent(ContentManager Content, GameXTA main)
        {
            pngGameLogo = Content.Load<Texture2D>(GetProperName("Hellfire", main));
            pngWallpaper = Content.Load<Texture2D>(GetProperName("bg19", main));

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
