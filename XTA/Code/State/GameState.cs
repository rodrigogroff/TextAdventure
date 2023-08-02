using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using XTA.Code.Infra;

namespace XTA.Code.State
{
    public class GameState : BaseGame
    {
        public GameXTA main { get; set; }
        public int id { get; set; }
        public int nextState { get; set; }
        public bool done { get; set; }

        public virtual void LoadContent(ContentManager Content, GraphicsDevice Device) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

        public string inputText { get; set; }

        public bool cursorVisible { get; set; }

        public char GetCharacterFromKey(Keys key)
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

                case Keys.NumPad0: return '0';
                case Keys.NumPad1: return '1';
                case Keys.NumPad2: return '2';
                case Keys.NumPad3: return '3';
                case Keys.NumPad4: return '4';
                case Keys.NumPad5: return '5';
                case Keys.NumPad6: return '6';
                case Keys.NumPad7: return '7';
                case Keys.NumPad8: return '8';
                case Keys.NumPad9: return '9';
                        
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

        public void DisplayText(SpriteBatch spriteBatch, string text, Vector2 startPosition, SpriteFont textFont, int width = 0, float alpha = -1)
        {
            #region - code - 

            var internalTextPosition = startPosition;

            float w_letter_pad = 0f,
                    h_letter_pad = 0f;

            bool IsBlue = false,
                    IsRed = false,
                    IsYellow = false;

            var current_color = Color.White;

            foreach (var letter in text)
            {
                Vector2 nextPosition = internalTextPosition + new Vector2(w_letter_pad, h_letter_pad);

                if (letter == '\"' || letter == '¨')
                    IsYellow = !IsYellow;
                else if (letter == '^')
                    IsBlue = !IsBlue;
                else if (letter == '~')
                    IsRed = !IsRed;

                if (IsYellow)
                    current_color = Color.Yellow;
                else if (IsBlue)
                    current_color = Color.Cyan;
                else if (IsRed)
                    current_color = Color.Red;
                else
                    current_color = Color.White;

                if (letter != '^' && letter != '~' && letter != '¨')
                {
                    spriteBatch.DrawString(textFont, letter.ToString(), new Vector2(nextPosition.X + 2, nextPosition.Y + 2), Color.Black);
                    spriteBatch.DrawString(textFont, letter.ToString(), nextPosition, current_color * 0.7f * (alpha > 0 ? alpha:1));

                    if (letter == '\n')
                    {
                        w_letter_pad = 0;
                        h_letter_pad += 20;
                    }
                    else
                    {
                        if (width == 0)
                            w_letter_pad += textFont.MeasureString(letter.ToString()).X + 1;
                        else
                            w_letter_pad += width;
                    }
                }
            }

            #endregion
        }

        public void DisplayCursorText(SpriteBatch spriteBatch, Vector2 textPosition, SpriteFont textFont, float Alpha = -1)
        {
            #region - code - 

            spriteBatch.DrawString(textFont, inputText, textPosition, Color.Green * (Alpha > 0 ? Alpha : 1));

            if (cursorVisible)
            {
                float cursorWidth = textFont.MeasureString(inputText).X;
                Vector2 cursorPosition = textPosition + new Vector2(cursorWidth, 0);
                Color cursorColor = Color.Green;
                spriteBatch.DrawString(textFont, "|", cursorPosition, cursorColor * (Alpha > 0 ? Alpha: 1));
            }

            #endregion
        }
    }
}
