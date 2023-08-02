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
            GAME_STATE_RESET = 3;

        List<GameState> lstGameStates;

        public bool
            bUltraWideMode = false,
            b4KMode = false,
            bShowFps = true;

        #region - internal variables - 

        SpriteFont versionFont;
        Texture2D pixelTexture_ScanLines;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public int
            gameState = 0,
            frameRate,
            frameCounter,
            virtualScreenWidth = 1920,
            virtualScreenHeight = 1080,
            virtualScreenUltraWidth = 2560,
            virtualScreenUltraHeight = 1080,
            scanLineSpacing = 2,
            scanLineSize = 1;
        
        Color scanLineColor = new
            Color(0.1f, 0.1f, 0.1f, 0.005f);

        float
            scaleX,
            scaleY;

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

                int virtX = virtualScreenWidth,
                    virtY = virtualScreenHeight;

                if (graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight >= 2f)
                {
                    bUltraWideMode = true;
                    virtX = virtualScreenUltraWidth;
                    virtY = virtualScreenUltraHeight;
                }
                
                if (graphics.PreferredBackBufferWidth >= 3840)
                {
                    b4KMode = true;
                }

                scaleX = (float) graphics.PreferredBackBufferWidth / virtX;
                scaleY = (float) graphics.PreferredBackBufferHeight / virtY;                

                base.Initialize();
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }

        public void PrepareStates()
        {
            lstGameStates = new List<GameState>
                {
                    new GameState_ShowLogo(this),
                    new GameState_ShowFrontendStart(this),
                    new GameState_ShowMainGame(this)
                };

            foreach (var item in lstGameStates)
                item.LoadContent(Content, GraphicsDevice);
        }

        protected override void LoadContent()
        {
            try
            {
                pixelTexture_ScanLines = new Texture2D(GraphicsDevice, 1, 1);
                pixelTexture_ScanLines.SetData(new[] { Color.White });
                spriteBatch = new SpriteBatch(GraphicsDevice);
                PrepareStates();
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

                switch (gameState)
                {
                    case GAME_STATE_RESET:
                        PrepareStates();
                        gameState = GAME_STATE_SHOW_FRONTEND_START;
                        break;
                }

                base.Update(gameTime);
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }

            if (bShowFps)
            {
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    frameRate = frameCounter;
                    frameCounter = 0;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                GraphicsDevice.Viewport = 
                    new Viewport(0, 0, 
                    graphics.PreferredBackBufferWidth, 
                    graphics.PreferredBackBufferHeight);

                spriteBatch.Begin(
                    transformMatrix: Matrix.CreateScale(scaleX, scaleY, 1f),
                    samplerState: SamplerState.LinearClamp);

                DrawGameCode();
                spriteBatch.End();
                DrawScanLines();
                base.Draw(gameTime);

                frameCounter++;
            }
            catch (Exception ex)
            {
                ShutdownWithError(ex);
            }
        }
        
        public void DrawGameCode()
        {
            lstGameStates[gameState].Draw(spriteBatch);

            if (bShowFps)
            {
                spriteBatch.DrawString(
                    versionFont, "v0.1.25 / Fps: " + frameRate, 
                    new Vector2(0, 0), Color.Yellow);
            }
        }

        public void DrawScanLines()
        {
            spriteBatch.Begin();

            for (int y = 0; y < graphics.PreferredBackBufferHeight; y += scanLineSpacing)
                spriteBatch.Draw(pixelTexture_ScanLines,
                    new Rectangle(0, y, graphics.PreferredBackBufferWidth, scanLineSize), scanLineColor);

            spriteBatch.End();
        }
    }
}
