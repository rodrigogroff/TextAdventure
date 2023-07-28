using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using XTA.Code.Infra;

namespace XTA
{
    public class GameXTA : Game
    {
        public const int GAME_STATE_SHOW_LOGO = 0;
        List<GameEvent> pipeline_logo = new List<GameEvent>();

        int gameState = GAME_STATE_SHOW_LOGO;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool cursorVisible = true;
        double cursorBlinkTime = 0.5;
        double cursorElapsed = 0;

        public GameXTA()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var lgo = new LogoEvent();
            lgo.LoadContent(Content);

            pipeline_logo.Add(lgo);
        }

        protected override void Update(GameTime gameTime)
        {
            cursorElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (cursorElapsed >= cursorBlinkTime)
            {
                cursorVisible = !cursorVisible;
                cursorElapsed -= cursorBlinkTime;
            }

            switch(gameState)
            {
                case GAME_STATE_SHOW_LOGO:

                    foreach (GameEvent e in pipeline_logo.Where(y => y.IsActive == true))
                    {
                        e.Update();
                    }

                    break;
            }           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            int virtualScreenWidth = 1920;
            int virtualScreenHeight = 1080;

            float scaleX = (float)GraphicsDevice.Viewport.Width / virtualScreenWidth;
            float scaleY = (float)GraphicsDevice.Viewport.Height / virtualScreenHeight;

            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = new Viewport(0, 0, virtualScreenWidth, virtualScreenHeight);

            spriteBatch.Begin(
                transformMatrix: Matrix.CreateScale(scaleX, scaleY, 1f), 
                samplerState: SamplerState.LinearClamp);

            switch (gameState)
            {
                case GAME_STATE_SHOW_LOGO:

                    foreach (GameEvent e in pipeline_logo.Where(y => y.IsActive == true))
                    {
                        e.Draw(spriteBatch);
                    }

                    break;
            }

            spriteBatch.End();

            GraphicsDevice.Viewport = new Viewport(0, 0, 
                GraphicsDevice.PresentationParameters.BackBufferWidth, 
                GraphicsDevice.PresentationParameters.BackBufferHeight);

            base.Draw(gameTime);
        }
    }
}
