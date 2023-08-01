using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XTA.Code.State
{
    public class GameState
    {
        public int id { get; set; }
        public int nextState { get; set; }
        public bool done { get; set; }

        public virtual void LoadContent(ContentManager Content) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

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
    }
}
