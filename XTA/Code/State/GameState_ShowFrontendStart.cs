﻿using Microsoft.Xna.Framework;
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
    public class GameState_ShowFrontendStart : GameState
    {
        public GameState_ShowFrontendStart() 
        { 
            id = GameXTA.GAME_STATE_SHOW_FRONTEND_START;
            done = false;
            nextState = GameXTA.GAME_STATE_SHOW_FRONTEND_START;
        }

        public const int START_GAME = 0;
        public const int DIFFICULTY = 1;

        public int internalState = START_GAME;

        List<GameEvent> pipeline_game_title = new List<GameEvent>();
        SpriteFont menuFont;
        
        KeyboardState prevKeyboardState;

        string inputText = "";

        bool cursorVisible = true;

        double cursorBlinkTime = 0.5,
                cursorElapsed = 0;

        int xStartText = 900; 
        public override void LoadContent(ContentManager Content) 
        {
            var myTitle = new MainTitleEvent();
            myTitle.LoadContent(Content);
            pipeline_game_title.Add(myTitle);
            menuFont = Content.Load<SpriteFont>("Merriweather");            
        }

        public override void Update(GameTime gameTime) 
        {
            foreach (GameEvent e in pipeline_game_title.Where(y => y.IsActive == true))
                e.Update();

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
            if (inputText == "1")
            {
                inputText = "";
                selectedOption = "New Game!";

                // novo jogo
                internalState++;
            }
            else if (inputText == "2")
            {
                selectedOption = "Continue!";
                inputText = "";
            }
            else if (inputText == "3")
            {
                // sair
                Environment.Exit(0);
            }
        }

        string internalText = "";
        string selectedOption = "";

        Vector2 internalTextPosition;

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
            foreach (GameEvent e in pipeline_game_title.Where(y => y.IsActive == true))
                e.Draw(spriteBatch);
            
            var title = pipeline_game_title[0] as MainTitleEvent;
            
            switch (internalState)
            {
                case START_GAME:

                    xStartText = 900;

                    StartText(new Vector2(xStartText, 500));
                    AddText(spriteBatch, "1 - ", Color.Gray * title.currentAlpha);
                    AddText(spriteBatch, "Start Game", Color.LightGray * title.currentAlpha);

                    StartText(new Vector2(xStartText, 530));
                    AddText(spriteBatch, "2 - ", Color.Gray * title.currentAlpha);
                    AddText(spriteBatch, "Continue", Color.LightGray * title.currentAlpha);

                    StartText(new Vector2(xStartText, 560));
                    AddText(spriteBatch, "3 - ", Color.Gray * title.currentAlpha);
                    AddText(spriteBatch, "Quit", Color.LightGray * title.currentAlpha);

                    break;

                case DIFFICULTY:

                    xStartText = 780;

                    StartText(new Vector2(xStartText, 450));
                    AddText(spriteBatch, selectedOption, Color.White);

                    StartText(new Vector2(xStartText, 500));
                    AddText(spriteBatch, "1 - ", Color.Gray);
                    AddText(spriteBatch, "Easy ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, 500));
                    AddText(spriteBatch, "-- unlimited hints ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, 530));
                    AddText(spriteBatch, "2 - ", Color.Gray);
                    AddText(spriteBatch, "Normal ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, 530));
                    AddText(spriteBatch, "-- counted hints ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, 560));
                    AddText(spriteBatch, "3 - ", Color.Gray);
                    AddText(spriteBatch, "Old School ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, 560));
                    AddText(spriteBatch, "-- alone in the dark ", Color.DarkGray * 0.5f);

                    break;
            }

            StartText(new Vector2(xStartText, 620));
            AddText(spriteBatch, "[>", Color.Green * title.currentAlpha);

            DrawCurrentInputText(spriteBatch, new Vector2(xStartText + 40, 620));
        }
    }
}
