using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using XTA.Code.State;

namespace XTA
{
    public class GameXTA : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int GAME_STATE_SHOW_LOGO = 0;
        public const int GAME_STATE_SHOW_FRONTEND_START = 1;

        List<GameState> lstGameStates;

        #region - variables - 

        int gameState = 0,
            virtualScreenWidth = 1920,
            virtualScreenHeight = 1080,
            BackBufferWidth = 0,
            BackBufferHeight = 0,
            scanLineSpacing = 5, 
            scanLineSize = 2,
            screenHeight = 0,
            screenWidth = 0;

        Color scanLineColor = new Color(0.07f, 0.07f, 0.07f, 0.07f / 2);

        float scaleX, scaleY;

        Texture2D pixelTexture_ScanLines; 

        string ErrorFile = "error.txt";

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
                graphics.PreferMultiSampling = true;
                graphics.ApplyChanges();

                screenHeight = GraphicsDevice.Viewport.Height;
                screenWidth = GraphicsDevice.Viewport.Width;
                scaleX = (float)GraphicsDevice.Viewport.Width / virtualScreenWidth;
                scaleY = (float)GraphicsDevice.Viewport.Height / virtualScreenHeight;
                BackBufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
                BackBufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

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
                pixelTexture_ScanLines = new Texture2D(GraphicsDevice, 1, 1);
                pixelTexture_ScanLines.SetData(new[] { Color.White });

                spriteBatch = new SpriteBatch(GraphicsDevice);

                // ---------------------------------------------------------

                lstGameStates = new List<GameState>
                {
                    new GameState_ShowLogo(),
                    new GameState_ShowFrontendStart()
                };

                foreach (var item in lstGameStates)
                    item.LoadContent(Content);

                // ---------------------------------------------------------
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
                // ---------------------------------------------------------
                
                var curState = lstGameStates[gameState];
                curState.Update(gameTime);
                if (curState.done)
                    gameState = curState.nextState;

                // ---------------------------------------------------------

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
                GraphicsDevice.Clear(Color.Black);
                GraphicsDevice.Viewport = new Viewport(0, 0, virtualScreenWidth, virtualScreenHeight);
                spriteBatch.Begin(
                    transformMatrix: Matrix.CreateScale(scaleX, scaleY, 1f),
                    samplerState: SamplerState.AnisotropicClamp);

                // ---------------------------------------------------------

                lstGameStates[gameState].Draw(spriteBatch);

                // ---------------------------------------------------------

                spriteBatch.End();
                GraphicsDevice.Viewport = new Viewport(0, 0, BackBufferWidth, BackBufferHeight);
                spriteBatch.Begin();
                for (int y = 0; y < screenHeight; y += scanLineSpacing)
                    spriteBatch.Draw(pixelTexture_ScanLines,
                        new Rectangle(0, y, screenWidth, scanLineSize), scanLineColor);
                spriteBatch.End();
                base.Draw(gameTime);
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }
    }
}
