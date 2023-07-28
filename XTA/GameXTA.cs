using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XTA.Code.Components;
using XTA.Code.Infra;

namespace XTA
{
    public class GameXTA : Game
    {
        public const int GAME_STATE_SHOW_LOGO = 0;
        public const int GAME_STATE_SHOW_FRONTEND_START = 1;

        List<GameEvent> pipeline_logo = new List<GameEvent>();

        int gameState = GAME_STATE_SHOW_LOGO;

        #region - main variables -

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        string ErrorFile = "error.txt";

        #region - cursor management -

        bool cursorVisible = true;
        double cursorBlinkTime = 0.5;
        double cursorElapsed = 0;

        #endregion

        #endregion

        public GameXTA()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            if (File.Exists(ErrorFile))
                File.Delete(ErrorFile);
        }

        void ShutdownWithError(Exception ex)
        {
            File.WriteAllText(ErrorFile, ex.ToString());
            Environment.Exit(1);
        }

        protected override void Initialize()
        {
            try
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();

                base.Initialize();
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        protected override void LoadContent()
        {
            try
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);

                var lgo = new LogoEvent();
                lgo.LoadContent(Content);

                pipeline_logo.Add(lgo);
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
                cursorElapsed += gameTime.ElapsedGameTime.TotalSeconds;

                if (cursorElapsed >= cursorBlinkTime)
                {
                    cursorVisible = !cursorVisible;
                    cursorElapsed -= cursorBlinkTime;
                }

                switch (gameState)
                {
                    case GAME_STATE_SHOW_LOGO:

                        foreach (GameEvent e in pipeline_logo.Where(y => y.IsActive == true))
                        {
                            e.Update();
                        }

                        if (!pipeline_logo.Any(y => y.IsActive == true))
                        {
                            gameState++;
                        }

                        break;
                }

                base.Update(gameTime);
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            try
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
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }
    }
}
