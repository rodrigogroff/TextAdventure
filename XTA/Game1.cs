using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace XTA
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont lucidaConsoleFont;
        SpriteFont calibriConsoleFont;

        float currentAlpha = 1.0f; 
        float fadeSpeed = 0.5f; 

        KeyboardState prevKeyboardState;

        string inputText = "";
        bool cursorVisible = true;
        double cursorBlinkTime = 0.5; 
        double cursorElapsed = 0;

        Texture2D pngTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        private char GetCharacterFromKey(Keys key)
        {
            #region - code -

            bool shift = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
                         Keyboard.GetState().IsKeyDown(Keys.RightShift);

            switch (key)
            {
                case Keys.A: return shift ? 'A' : 'a';
                case Keys.B: return shift ? 'B' : 'b';
                case Keys.C: return shift ? 'C' : 'c';
                case Keys.D: return shift ? 'D' : 'd';
                case Keys.E: return shift ? 'E' : 'e';
                case Keys.F: return shift ? 'F' : 'f';
                case Keys.G: return shift ? 'G' : 'g';
                case Keys.H: return shift ? 'H' : 'h';
                case Keys.I: return shift ? 'I' : 'i';
                case Keys.J: return shift ? 'J' : 'j';
                case Keys.K: return shift ? 'K' : 'k';
                case Keys.L: return shift ? 'L' : 'l';
                case Keys.M: return shift ? 'M' : 'm';
                case Keys.N: return shift ? 'N' : 'n';
                case Keys.O: return shift ? 'O' : 'o';
                case Keys.P: return shift ? 'P' : 'p';
                case Keys.Q: return shift ? 'Q' : 'q';
                case Keys.R: return shift ? 'R' : 'r';
                case Keys.S: return shift ? 'S' : 's';
                case Keys.T: return shift ? 'T' : 't';
                case Keys.U: return shift ? 'U' : 'u';
                case Keys.V: return shift ? 'V' : 'v';
                case Keys.W: return shift ? 'W' : 'w';
                case Keys.X: return shift ? 'X' : 'x';
                case Keys.Y: return shift ? 'Y' : 'y';
                case Keys.Z: return shift ? 'Z' : 'z';               

                case Keys.D0: return shift ? '0' : '0';
                case Keys.D1: return shift ? '1' : '1';
                case Keys.D2: return shift ? '2' : '2';
                case Keys.D3: return shift ? '3' : '3';
                case Keys.D4: return shift ? '4' : '4';
                case Keys.D5: return shift ? '5' : '5';
                case Keys.D6: return shift ? '6' : '6';
                case Keys.D7: return shift ? '7' : '7';
                case Keys.D8: return shift ? '8' : '8';
                case Keys.D9: return shift ? '9' : '9';

                default: return ' ';
            }

            #endregion
        }

        protected override void Initialize()
        {
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            lucidaConsoleFont = Content.Load<SpriteFont>("File");
            calibriConsoleFont = Content.Load<SpriteFont>("File2");

            pngTexture = Content.Load<Texture2D>("logo_footer");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            currentAlpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentAlpha = Math.Max(currentAlpha, 0);

            KeyboardState keyboardState = Keyboard.GetState();

            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (prevKeyboardState.IsKeyUp(key))
                {
                    if (key == Keys.Back && inputText.Length > 0)
                    {
                        inputText = inputText.Substring(0, inputText.Length - 1);
                    }
                    else if (key == Keys.Space)
                    {
                        inputText += " ";
                    }
                    else
                    {
                        char inputChar = GetCharacterFromKey(key);
                        if (Char.IsLetterOrDigit(inputChar))
                        {
                            inputText += inputChar;
                        }
                    }
                }
            }

            prevKeyboardState = keyboardState;

            cursorElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (cursorElapsed >= cursorBlinkTime)
            {
                cursorVisible = !cursorVisible;
                cursorElapsed -= cursorBlinkTime;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp);

            {
                string message = "Fading Text Example";
                Vector2 position = new Vector2(100, 100);
                Color textColor = Color.White * currentAlpha; // Multiply the color with the alpha value
                spriteBatch.DrawString(lucidaConsoleFont, message, position, textColor);
            }

            {
                string message = "Text Example\nWith a new line";
                Vector2 position = new Vector2(100, 200);
                Color textColor = Color.White; // Multiply the color with the alpha value
                spriteBatch.DrawString(calibriConsoleFont, message, position, textColor);
            }

            {
                Vector2 textPosition = new Vector2(100, 300);
                Color textColor = Color.White;
                spriteBatch.DrawString(lucidaConsoleFont, inputText, textPosition, textColor);
                if (cursorVisible)
                {
                    float cursorWidth = lucidaConsoleFont.MeasureString(inputText).X; // Get the width of the text
                    Vector2 cursorPosition = textPosition + new Vector2(cursorWidth, 0);
                    Color cursorColor = Color.White; // Use a different color for the cursor
                    spriteBatch.DrawString(lucidaConsoleFont, "|", cursorPosition, cursorColor);
                }
            }

            {
                Color textColor = Color.White * currentAlpha; // Multiply the color with the alpha value
                Vector2 position = new Vector2(500, 100);
                spriteBatch.Draw(pngTexture, position, textColor);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
