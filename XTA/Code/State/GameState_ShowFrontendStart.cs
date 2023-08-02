using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using XTA.Code.Components;

namespace XTA.Code.State
{
    public class GameState_ShowFrontendStart : GameState
    {
        const int
            START_GAME = 0,
            DIFFICULTY = 1;

        public GameState_ShowFrontendStart(GameXTA _main)
        {
            main = _main;
            id = GameXTA.GAME_STATE_SHOW_FRONTEND_START;
            done = false;
            nextState = GameXTA.GAME_STATE_SHOW_MAIN_GAME;
        }
        
        int 
            internalState = START_GAME,
            xStartText = 900,
            yStartText = 525;
        
        string 
            inputText = "",
            internalText = "",
            selectedOption = "";

        bool 
            cursorVisible = true;

        double 
            cursorBlinkTime = 0.5,
            cursorElapsed = 0;

        MainTitleEvent myTitle;
        SpriteFont menuFont;
        Vector2 internalTextPosition;
        KeyboardState prevKeyboardState;

        public override void LoadContent(ContentManager Content, GraphicsDevice Device)
        {
            myTitle = new MainTitleEvent();
            myTitle.LoadContent(Content, main);            
            menuFont = Content.Load<SpriteFont>("Merriweather");     
        }

        public override void Update(GameTime gameTime) 
        {
            myTitle.Update();

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

        public void ProcessInput()
        {
            switch (internalState)
            {
                case START_GAME:

                    if (inputText == "1")
                    {
                        inputText = "";
                        selectedOption = "New Game!";

                        internalState++; // novo jogo
                    }
                    else if (inputText == "2")
                    {
                        selectedOption = "Continue!";
                        inputText = "";
                    }
                    else if (inputText == "3")
                    {
                        Environment.Exit(0); // sair
                    }

                    break;

                case DIFFICULTY:

                    this.done = true;

                    break;
            }
        }
        
        public void StartText(Vector2 startPosition)
        {
            internalText = "";
            internalTextPosition = startPosition;
        }

        public void AddText(SpriteBatch spriteBatch, string text, Color color)
        {
            if (internalText == "")
            {
                internalText += text;
                spriteBatch.DrawString(menuFont, text, internalTextPosition, color);
            }
            else
            {
                var w = menuFont.MeasureString(internalText).X;
                Vector2 nextPosition = internalTextPosition + new Vector2(w, 0);
                spriteBatch.DrawString(menuFont, text, nextPosition, color);
                internalText += text;
            }
        }

        public void DrawCurrentInputText(SpriteBatch spriteBatch, Vector2 textPosition)
        {
            spriteBatch.DrawString(menuFont, inputText, textPosition, Color.Green);

            if (cursorVisible)
            {
                float cursorWidth = menuFont.MeasureString(inputText).X;
                Vector2 cursorPosition = textPosition + new Vector2(cursorWidth, 0);
                Color cursorColor = Color.Green;
                spriteBatch.DrawString(menuFont, "|", cursorPosition, cursorColor);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            myTitle.Draw(spriteBatch);
            
            switch (internalState)
            {
                case START_GAME:

                    if (main.bUltraWideMode)
                    {
                        xStartText = (main.virtualScreenUltraWidth / 2) - 80; yStartText = 600;
                    }
                    else
                    {
                        xStartText = (main.virtualScreenWidth / 2) - 80; yStartText = 600;
                    }

                    StartText(new Vector2(xStartText, yStartText));
                    AddText(spriteBatch, "1 - ", Color.Gray * myTitle.currentAlpha);
                    AddText(spriteBatch, "Start Game", Color.LightGray * myTitle.currentAlpha);

                    StartText(new Vector2(xStartText, yStartText + 30));
                    AddText(spriteBatch, "2 - ", Color.Gray * myTitle.currentAlpha);
                    AddText(spriteBatch, "Continue", Color.LightGray * myTitle.currentAlpha);

                    StartText(new Vector2(xStartText, yStartText + 60));
                    AddText(spriteBatch, "3 - ", Color.Gray * myTitle.currentAlpha);
                    AddText(spriteBatch, "Quit", Color.LightGray * myTitle.currentAlpha);

                    if (myTitle.currentAlpha >= 1)
                    {
                        StartText(new Vector2(xStartText, yStartText + 110));
                        DrawCurrentInputText(spriteBatch, new Vector2(xStartText, yStartText + 110));
                    }

                    break;

                case DIFFICULTY:

                    if (main.bUltraWideMode)
                    {
                        xStartText = (main.virtualScreenUltraWidth / 2) - 200; yStartText = 600;
                    }
                    else
                    {
                        xStartText = (main.virtualScreenWidth / 2) - 200; yStartText = 600;
                    }

                    StartText(new Vector2(xStartText + 145, yStartText - 10));
                    AddText(spriteBatch, selectedOption, Color.White);

                    StartText(new Vector2(xStartText, yStartText + 30 ));
                    AddText(spriteBatch, "1 - ", Color.Gray);
                    AddText(spriteBatch, "Easy ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, yStartText + 30));
                    AddText(spriteBatch, "-- unlimited hints ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, yStartText + 60));
                    AddText(spriteBatch, "2 - ", Color.Gray);
                    AddText(spriteBatch, "Normal ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, yStartText + 60));
                    AddText(spriteBatch, "-- counted hints ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, yStartText + 90));
                    AddText(spriteBatch, "3 - ", Color.Gray);
                    AddText(spriteBatch, "Old School ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, yStartText + 90));
                    AddText(spriteBatch, "-- alone in the dark ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, yStartText + 140));
                    DrawCurrentInputText(spriteBatch, new Vector2(xStartText, yStartText + 140));

                    break;
            }
        }
    }
}
