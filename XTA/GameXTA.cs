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
        
        public const int 
            GAME_STATE_SHOW_LOGO = 0,
            GAME_STATE_SHOW_FRONTEND_START = 1,
            GAME_STATE_SHOW_MAIN_GAME = 2,
            GAME_STATE_RESET = 3,
            GAME_STATE_OPTION = 4;

        List<GameState> lstGameStates;

        #region - variables - 

        SpriteFont versionFont;
        Texture2D pixelTexture_ScanLines;
        TimeSpan elapsedTime = TimeSpan.Zero;

        int gameState = 0,
            frameRate,
            frameCounter,
            virtualScreenWidth = 1920,
            virtualScreenHeight = 1080,
            virtualScreenUltraWidth = 2560,
            virtualScreenUltraHeight = 1080,
            BackBufferWidth = 0,
            BackBufferHeight = 0,
            scanLineSpacing = 2, 
            scanLineSize = 1,
            screenHeight = 0,
            screenWidth = 0,
            letterboxedWidth = 0,
            letterboxedHeight = 0,
            offsetX = 0,
            offsetY = 0;

        bool bUltraWideMode = false;

        Color scanLineColor = new Color(0.1f, 0.1f, 0.1f, 0.005f);

        float scaleX, 
            scaleY,
            actualAspectRatio,
            virtualAspectRatio, 
            scale;

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
                scaleX = GraphicsDevice.Viewport.Width / virtualScreenWidth;
                scaleY = GraphicsDevice.Viewport.Height / virtualScreenHeight;
                BackBufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
                BackBufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

                actualAspectRatio = GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height;
                virtualAspectRatio = virtualScreenWidth / virtualScreenHeight;                
                scale = actualAspectRatio < virtualAspectRatio
                    ? GraphicsDevice.Viewport.Width / virtualScreenWidth
                    : GraphicsDevice.Viewport.Height / virtualScreenHeight;
                letterboxedWidth = (int)(virtualScreenWidth * scale);
                letterboxedHeight = (int)(virtualScreenHeight * scale);
                offsetX = (GraphicsDevice.Viewport.Width - letterboxedWidth) / 2;
                offsetY = (GraphicsDevice.Viewport.Height - letterboxedHeight) / 2;

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

                lstGameStates = new List<GameState>
                {
                    new GameState_ShowLogo(),
                    new GameState_ShowFrontendStart(),
                    new GameState_ShowMainGame()
                };

                foreach (var item in lstGameStates)
                    item.LoadContent(Content, GraphicsDevice);

                versionFont = Content.Load<SpriteFont>("File2");
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
                var curState = lstGameStates[gameState];
                curState.Update(gameTime);
                if (curState.done)
                    gameState = curState.nextState;

                if (gameState == GAME_STATE_RESET)
                {
                    lstGameStates = new List<GameState>
                    {
                        new GameState_ShowLogo(),
                        new GameState_ShowFrontendStart(),
                        new GameState_ShowMainGame()
                    };

                    foreach (var item in lstGameStates)
                        item.LoadContent(Content, GraphicsDevice);

                    gameState = GAME_STATE_SHOW_FRONTEND_START;
                }

                base.Update(gameTime);
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                if (actualAspectRatio > 1.5f)
                {                    
                    DrawScaled(gameTime);
                }
                else
                {
                    if (bUltraWideMode)
                        DrawUltraWide(gameTime);
                    else
                        DrawBoxedUltraWide(gameTime);
                }

                frameCounter++;
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        public void DrawScaled(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                GraphicsDevice.Viewport = new Viewport(0, 0, virtualScreenWidth, virtualScreenHeight);

                spriteBatch.Begin(
                    transformMatrix: Matrix.CreateScale(scaleX, scaleY, 1f),
                    samplerState: SamplerState.AnisotropicClamp);

                DrawGameCode();

                spriteBatch.End();

                GraphicsDevice.Viewport = new Viewport(0, 0, BackBufferWidth, BackBufferHeight);

                DrawScanLines();

                base.Draw(gameTime);
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        public void DrawUltraWide(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                GraphicsDevice.Viewport = new Viewport(0, 0, virtualScreenUltraWidth, virtualScreenUltraHeight);

                spriteBatch.Begin(
                    transformMatrix: Matrix.CreateScale(scaleX, scaleY, 1f),
                    samplerState: SamplerState.AnisotropicClamp);

                DrawGameCode();

                spriteBatch.End();

                GraphicsDevice.Viewport = new Viewport(0, 0, BackBufferWidth, BackBufferHeight);

                DrawScanLines();

                base.Draw(gameTime);
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        public void DrawBoxedUltraWide(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                
                GraphicsDevice.Viewport = new Viewport(offsetX, offsetY, letterboxedWidth, letterboxedHeight);
                spriteBatch.Begin();

                DrawGameCode();
                
                spriteBatch.End();
                GraphicsDevice.Viewport = 
                    new Viewport(0, 0, 
                        GraphicsDevice.PresentationParameters.BackBufferWidth, 
                        GraphicsDevice.PresentationParameters.BackBufferHeight);

                DrawScanLines();

                base.Draw(gameTime);                
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        public void DrawGameCode()
        {
            lstGameStates[gameState].Draw(spriteBatch);
            spriteBatch.DrawString(versionFont, "v0.1.25 / Fps: " + frameRate, new Vector2(0, 0), Color.Yellow);
        }

        public void DrawScanLines()
        {
            spriteBatch.Begin();

            for (int y = 0; y < screenHeight; y += scanLineSpacing)
                spriteBatch.Draw(pixelTexture_ScanLines,
                    new Rectangle(0, y, screenWidth, scanLineSize), scanLineColor);

            spriteBatch.End();
        }
    }
}
