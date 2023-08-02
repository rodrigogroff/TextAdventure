using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public override void LoadContent(ContentManager Content, GraphicsDevice Device) 
        {
            arialFont = Content.Load<SpriteFont>("ArialN");
            textFont = Content.Load<SpriteFont>("File");
            titleFont = Content.Load<SpriteFont>("Merriweather");
            lucidaBigFont = Content.Load<SpriteFont>("LucidaBig");
            textStatsFont = Content.Load<SpriteFont>("File3");

            pngTexture_wallpaper = Content.Load<Texture2D>("bg18");
            pngTexture_dialog = Content.Load<Texture2D>("MainDialog");
            pngTexture_stats = Content.Load<Texture2D>("MainStats");
            pngTexture_deathPage = Content.Load<Texture2D>("DeathPage");
            pngTexture_bag = Content.Load<Texture2D>("Bag");

            pixelTexture_ScanLines = new Texture2D(Device, 1, 1);
            pixelTexture_ScanLines.SetData(new[] { Color.White });

            if (main.bUltraWideMode)
            {
                wallpaperPosition = 
                    new Vector2(main.virtualScreenUltraWidth / 2 - pngTexture_wallpaper.Width / 2, 
                                main.virtualScreenUltraHeight / 2 - pngTexture_wallpaper.Height / 2);

                mainDialogPos = 
                    new Vector2(main.virtualScreenUltraWidth / 2 - pngTexture_dialog.Width / 2, 0);

                mainStatsPos = new Vector2(1050, 60);
                mainBagPos = new Vector2(-40, 60);
                deathPos = new Vector2(main.virtualScreenUltraWidth / 2 - pngTexture_deathPage.Width / 2, 0);
            }
            else
            {
                wallpaperPosition = 
                    new Vector2(main.virtualScreenWidth / 2 - pngTexture_wallpaper.Width / 2, 
                                main.virtualScreenHeight / 2 - pngTexture_wallpaper.Height / 2);

                mainDialogPos = new Vector2(70, 0);
                mainStatsPos = new Vector2(1050, 60);
                mainBagPos = new Vector2(1000, 60);                
                deathPos = new Vector2(main.virtualScreenWidth / 2 - pngTexture_deathPage.Width / 2, 0);
            }
        }
    }
}
