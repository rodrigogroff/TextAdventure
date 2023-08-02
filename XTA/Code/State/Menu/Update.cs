using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace XTA.Code.State
{
    public partial class GameState_ShowFrontendStart : GameState
    {
        public override void Update(GameTime gameTime) 
        {
            myTitle.Update();

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
                    else if (key == Keys.Enter)
                    {
                        ProcessInput();
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

            // --------------
            // cursor            
            // --------------

            cursorElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (cursorElapsed >= cursorBlinkTime)
            {
                cursorVisible = !cursorVisible;
                cursorElapsed -= cursorBlinkTime;
            }
        }
    }
}
