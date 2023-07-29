using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using XTA.Code.Components;
using XTA.Code.Infra;

namespace XTA.Code.State
{
    public class GameState_ShowMainGame : GameState
    {
        public GameState_ShowMainGame() 
        {
            id = GameXTA.GAME_STATE_SHOW_MAIN_GAME;         
            done = false;
        }

        SpriteFont textFont;

        List<GameEvent> pipeline = new List<GameEvent>();

        KeyboardState prevKeyboardState;
        public override void LoadContent(ContentManager Content) 
        {
            var myGameText = new MainGameText();

            myGameText.LoadContent(Content);

            var myGameStat = new MainGameStat();

            myGameStat.LoadContent(Content);

            pipeline.Add(myGameText);
            pipeline.Add(myGameStat);

            textFont = Content.Load<SpriteFont>("File2");
        }

        public override void Update(GameTime gameTime) 
        {
            foreach (GameEvent e in pipeline.Where(y => y.IsActive == true))
                e.Update();

            if (!pipeline.Any(y => y.IsActive == true))
                done = true;

            #region - process keyoard - 

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

            #endregion

            #region - process cursor -

            cursorElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (cursorElapsed >= cursorBlinkTime)
            {
                cursorVisible = !cursorVisible;
                cursorElapsed -= cursorBlinkTime;
            }

            #endregion
        }

        string selectedOption = "";

        Vector2 internalTextPosition;

        string internalText = "";
        string inputText = "";
        public void StartText(Vector2 startPosition)
        {
            internalText = "";
            internalTextPosition = startPosition;
        }

        bool cursorVisible = true;

        double cursorBlinkTime = 0.5,
               cursorElapsed = 0;
        public void AddText(SpriteBatch spriteBatch, string text, Color color)
        {
            if (internalText == "")
            {
                internalText += text;
                spriteBatch.DrawString(textFont, text, internalTextPosition, color);
            }
            else
            {
                var w = textFont.MeasureString(internalText).X;
                Vector2 nextPosition = internalTextPosition + new Vector2(w, 0);
                spriteBatch.DrawString(textFont, text, nextPosition, color);
                internalText += text;
            }
        }

        public void DrawCurrentInputText(SpriteBatch spriteBatch, Vector2 textPosition)
        {
            spriteBatch.DrawString(textFont, inputText, textPosition, Color.Green);

            if (cursorVisible)
            {
                float cursorWidth = textFont.MeasureString(inputText).X;
                Vector2 cursorPosition = textPosition + new Vector2(cursorWidth, 0);
                Color cursorColor = Color.Green;
                spriteBatch.DrawString(textFont, "|", cursorPosition, cursorColor);
            }
        }
        public override void Draw(SpriteBatch spriteBatch) 
        {
            foreach (GameEvent e in pipeline.Where(y => y.IsActive == true))
                e.Draw(spriteBatch);

            int sx = 250, sy = 844;

            StartText(new Vector2(sx, sy));
            AddText(spriteBatch, "[>", Color.Green );
            DrawCurrentInputText(spriteBatch, new Vector2(sx + 22, sy));
        }
    }
}
