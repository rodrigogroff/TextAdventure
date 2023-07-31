using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using XTA.Code.Infra;

namespace XTA.Code.State
{
    public class GameState_ShowMainGame : GameState
    {
        const int MAIN_START_FADEOUT_WALLPAPER = 0;
        const int MAIN_START_FADEOUT_COMPLETE = 1;
        const int MAIN_START_FADEIN_MAINDIALOG = 2;
        const int MAIN_COMPLETE = 3;

        const int FADE_FRAMES = 60;

        public GameState_ShowMainGame() 
        {
            id = GameXTA.GAME_STATE_SHOW_MAIN_GAME;         
            done = false;
        }

        float[] curve = new GameFunctions().GenerateLogarithmicArray(FADE_FRAMES);

        bool bShowStats = false,
            bTextDisplayed = false,
            cursorVisible = true;

        int internalState = 0,
            textDelayTimeStd = 1,
            textDelay = 1,
            text_curIndex = 1,
            indexAlphaMainDialog = 60,
            indexAlphaCurveStats = 60,
            delayStatsMax = 500,
            delayStats = 0,
            delayStartFadout = 90;

        float currentAlphaWallpaper = 1,
                currentAlphaMainDialog = 0,
                currentAlphaMainStats = 0;

        KeyboardState prevKeyboardState;

        SpriteFont titleFont,
                    textFont;

        Texture2D pngTexture_wallpaper,
                    pngTexture_dialog,
                    pngTexture_stats;

        Vector2 mainDialogPos = new Vector2(70, 0),
                mainStatsPos = new Vector2(1050, 60),
                zero = new Vector2(0, 0),
                internalTextPosition;

        string MainTitle = "Soul Selection",
                inputText = "",
                textToDisplay = "",
                textIncoming = "";

        double cursorBlinkTime = 0.3,
               cursorElapsed = 0;

        List<string> original_text = new List<string>
        {
            "\"I'm out for groceries!\" Sarah said to her husband Paul, while searching for the car keys in her",
            "big pockets full of lollipops. She was still wearing her blue jacket with the name ^Dr. Sarah^",
            "^Allis Smith^ embroided in blue letters.",
            "",
            "Paul got out of the couch and fast he went to the ~kitchen~.",
            "",
            "\"We are out of.. pizza; and all the usual things...\" answered Paul, looking sad at the almost",
            "empty fridge at a friday night. \"Let me see if we are missing more..\" said the man in his forties,",
            "with a beer lite in his other hand.",
            "",
            "Sarah was exausted by coming home after a long shift at the clinic. Winter was coming, and lots ",
            "of little persons with their 'big' problems were coming too -- some came with a fever, but most",
            "consults, unfortunally, were just of scared parents in post COVID times who couldnt handle a",
            "simple cold, or a sore throat from their children without going mental.",
            "",
            "There was nothing wrong about being scared. She was a mom aswell, and got a pretty damn chill",
            "in her spine when Danny had his first seizure. Of course epilepsy is treatable and we are not in ",
            "the dark ages anymore; so, no evil possession or demons from hell, and simple medicine from the",
            "pharmacy solve (most of it). But everytime Danny looked directly in Sarah's eyes, she would get ",
            "anxious, and her mind played again the first time she saw the rolling up eye and the shaking: Paul",
            "had to act immediately, pushing her aside and screaming for her to help -- all she did was stare ",
            "and do nothing, while her husband rolled him to the side and stuck two fingers in the child's mouth."
        };

        public override void LoadContent(ContentManager Content) 
        {            
            textFont = Content.Load<SpriteFont>("File");
            titleFont = Content.Load<SpriteFont>("Merriweather");

            pngTexture_wallpaper = Content.Load<Texture2D>("bg18");
            pngTexture_dialog = Content.Load<Texture2D>("MainDialog");
            pngTexture_stats = Content.Load<Texture2D>("MainStats");
        }

        public override void Update(GameTime gameTime) 
        {
            switch (internalState)
            {
                case MAIN_START_FADEOUT_WALLPAPER:

                    if (--delayStartFadout > 0) { } else
                    {
                        if (indexAlphaMainDialog-- > 0)
                            currentAlphaWallpaper -= 0.01f;
                        else
                        {
                            indexAlphaMainDialog = 60;
                            internalState++;
                        }
                    }
                    break;

                case MAIN_START_FADEOUT_COMPLETE:

                    if (indexAlphaMainDialog-- > 0) { } else
                        internalState++;

                    break;

                case MAIN_START_FADEIN_MAINDIALOG:

                    if (++indexAlphaMainDialog >= FADE_FRAMES)
                        internalState++;
                    else
                        currentAlphaMainDialog = curve[indexAlphaMainDialog];

                    break;

                case MAIN_COMPLETE:

                    #region - process keyboard - 

                    KeyboardState keyboardState = Keyboard.GetState();

                    foreach (Keys key in keyboardState.GetPressedKeys())
                    {
                        if (prevKeyboardState.IsKeyUp(key))
                        {
                            if (key == Keys.Back && inputText.Length > 0)
                            {
                                inputText = inputText.Substring(0, inputText.Length - 1);
                            }
                            else if (key == Keys.Enter)
                            {
                                ProcessUserInput(inputText);
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

                    if (bShowStats)
                    {
                        if (++indexAlphaCurveStats < FADE_FRAMES)
                            currentAlphaMainStats = curve[indexAlphaCurveStats];
                        if (--delayStats == 0)
                            indexAlphaCurveStats = FADE_FRAMES - 1;
                    }

                    if (textIncoming == "")
                    {
                        foreach (var item in original_text)
                            textIncoming += item + "\n";
                    }
                    else if (textToDisplay != textIncoming)
                    {
                        #region - text timer - 

                        KeyboardState keyboardStateW = Keyboard.GetState();

                        bool bFast = false,
                            escPressed = false;

                        foreach (Keys key in keyboardStateW.GetPressedKeys())
                        {
                            if (prevKeyboardState.IsKeyDown(key))
                            {
                                if (key == Keys.Space)
                                    bFast = true;

                                if (key == Keys.Escape)
                                    escPressed = true;
                            }
                        }

                        if (escPressed)
                        {
                            textToDisplay = textIncoming;
                            bTextDisplayed = true; 
                        }
                        else if (!bFast)
                        {
                            if (--textDelay == 0)
                            {
                                if (text_curIndex < textIncoming.Length)
                                    textToDisplay = textIncoming.Substring(0, text_curIndex++);
                                else
                                    bTextDisplayed = true;

                                var curC = textToDisplay[textToDisplay.Length - 1];

                                if (curC == '\n')
                                    textDelay += 40;
                                else if (curC == ' ')
                                    textDelay += 5;
                                else if (";,-.!?—'\"".Contains(curC))
                                    textDelay += 18;
                                else
                                    textDelay += textDelayTimeStd;                                
                            }
                        }    
                        else
                        {
                            if (text_curIndex < textIncoming.Length)
                                textToDisplay = textIncoming.Substring(0, text_curIndex);
                            else
                            {
                                textToDisplay = textIncoming;
                                bTextDisplayed = true;
                            }

                            text_curIndex += 20;
                        }

                        #endregion
                    }

                    break;
            }
        }

        public void ProcessUserInput(string cmd)
        {
            cmd = cmd.ToLower();

            if (cmd == "help")
            {
                inputText = "";
                textIncoming = "";
                textToDisplay = "";
                textDelay = 1;
                text_curIndex = 1;
                bTextDisplayed = false;
                original_text = new List<string> 
                {
                    "¨-- Help -- game commands --¨",
                    "",
                    "^help^ = this screen",
                    "^stat^ = game stats and attibutes",
                    "^quit^ = end current game",
                };
            }
            else if (cmd == "stat")
            {
                bShowStats = !bShowStats;

                if (bShowStats)
                {
                    currentAlphaMainStats = 0;
                    indexAlphaCurveStats = 0;
                }

                inputText = "";
                textIncoming = "";
                delayStats = delayStatsMax;
            }
        }        
        public void StartText(Vector2 startPosition)
        {
            internalTextPosition = startPosition;
        }

        public void DisplayText(SpriteBatch spriteBatch, Vector2 startPosition, float currentAlpha, List<string> lstText)
        {
            #region - code - 

            float w_letter_pad = 0f,
                    h_letter_pad = 0f;

            bool IsBlue = false,
                    IsRed = false,
                    IsYellow = false;

            var current_color = Color.White;

            var text = "";

            foreach (var item in lstText)
            {
                text += item + "\n";
            }

            foreach (var letter in text)
            {
                Vector2 nextPosition = startPosition + new Vector2(w_letter_pad, h_letter_pad);

                if (letter == '\"' || letter == '¨')
                {
                    IsYellow = !IsYellow;
                }
                else if (letter == '^')
                {
                    IsBlue = !IsBlue;
                }
                else if (letter == '~')
                {
                    IsRed = !IsRed;
                }

                if (IsYellow)
                {
                    current_color = Color.Yellow;
                }
                else if (IsBlue)
                {
                    current_color = Color.Cyan;
                }
                else if (IsRed)
                {
                    current_color = Color.Red;
                }
                else
                    current_color = Color.White;

                if (letter != '^' && letter != '~' && letter != '¨')
                {
                    spriteBatch.DrawString(textFont, letter.ToString(), new Vector2(nextPosition.X + 2, nextPosition.Y + 2), Color.Black);
                    spriteBatch.DrawString(textFont, letter.ToString(), nextPosition, current_color * currentAlpha);

                    if (letter == '\n')
                    {
                        w_letter_pad = 0;
                        h_letter_pad += 20;
                    }
                    else
                        w_letter_pad += 7;
                }
            }

            #endregion
        }

        public void ProcessRoomText(SpriteBatch spriteBatch, string text)
        {
            #region - code - 

            float w_letter_pad = 0f,
                    h_letter_pad = 0f;

            bool IsBlue = false,
                    IsRed = false,
                    IsYellow = false;

            var current_color = Color.White;

            foreach (var letter in text)
            {
                Vector2 nextPosition = internalTextPosition + new Vector2(w_letter_pad, h_letter_pad);

                if (letter == '\"')
                {
                    IsYellow = !IsYellow;
                }
                else if (letter == '^')
                {
                    IsBlue = !IsBlue;
                }
                else if (letter == '~')
                {
                    IsRed = !IsRed;
                }

                if (IsYellow)
                {
                    current_color = Color.Yellow;
                }
                else if (IsBlue)
                {
                    current_color = Color.Cyan;
                }
                else if (IsRed)
                {
                    current_color = Color.Red;
                }
                else 
                    current_color = Color.White;

                if (letter != '^' && letter != '~' && letter != '¨')
                {
                    spriteBatch.DrawString(textFont, letter.ToString(), new Vector2(nextPosition.X +2, nextPosition.Y + 2), Color.Black);
                    spriteBatch.DrawString(textFont, letter.ToString(), nextPosition, current_color);

                    if (letter == '\n')
                    {
                        w_letter_pad = 0;
                        h_letter_pad += 20;
                    }
                    else
                        w_letter_pad += 7;
                }
            }

            #endregion
        }
        public void DrawCurrentCursorText(SpriteBatch spriteBatch, Vector2 textPosition)
        {
            #region - code - 

            spriteBatch.DrawString(titleFont, inputText, textPosition, Color.Green * currentAlphaMainDialog);

            if (cursorVisible)
            {
                float cursorWidth = titleFont.MeasureString(inputText).X;
                Vector2 cursorPosition = textPosition + new Vector2(cursorWidth, 0);
                Color cursorColor = Color.Green;
                spriteBatch.DrawString(titleFont, "|", cursorPosition, cursorColor * currentAlphaMainDialog);
            }

            #endregion
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            switch (internalState)
            {
                case MAIN_START_FADEOUT_WALLPAPER:
                case MAIN_START_FADEOUT_COMPLETE:
                    spriteBatch.Draw(pngTexture_wallpaper, zero, Color.White * currentAlphaWallpaper);
                    break;

                case MAIN_START_FADEIN_MAINDIALOG:
                    spriteBatch.Draw(pngTexture_wallpaper, zero, Color.White * currentAlphaWallpaper);
                    spriteBatch.Draw(pngTexture_dialog, mainDialogPos, Color.White * currentAlphaMainDialog);
                    break;

                case MAIN_COMPLETE:

                    spriteBatch.Draw(pngTexture_wallpaper, zero, Color.White * currentAlphaWallpaper);
                    spriteBatch.Draw(pngTexture_dialog, mainDialogPos, Color.White * currentAlphaMainDialog);

                    if (bShowStats)
                    {
                        int idx = curve.Length - indexAlphaCurveStats;

                        if (idx >= 0 && idx < curve.Length)
                            mainStatsPos.X = 1050 + 400 * curve[idx];
                        else
                            mainStatsPos.X = 1050;                        

                        spriteBatch.Draw(pngTexture_stats, mainStatsPos, Color.White * currentAlphaMainStats);
                        DisplayText(spriteBatch, new Vector2(mainStatsPos.X + 220, mainStatsPos.Y + 220), currentAlphaMainStats, new List<string>
                        {
                            "¨-- Character Stats --¨",
                            "",                            
                            "^Name^ Marco polo",
                            "^Type^ ????",
                            "",
                            "-- attributes --",
                            "",
                            "-- traits --",
                            "",
                        });
                    }

                    MainTitle = "This is very big text so deal with it";

                    float titleWidth = textFont.MeasureString(MainTitle).X;
                    spriteBatch.DrawString(titleFont, MainTitle, new Vector2(509 - titleWidth/2, 143), Color.Red * currentAlphaMainDialog);

                    int sx = 210, sy = 292;
                                        
                    StartText(new Vector2(sx, sy));
                    ProcessRoomText(spriteBatch, textToDisplay);

                    if (bTextDisplayed)
                    {
                        sx = 270; sy = 912;

                        spriteBatch.DrawString(textFont, "Type 'Help' for available commands", new Vector2(sx, sy), Color.DarkGray * 0.35f);

                        StartText(new Vector2(sx, sy));
                        DrawCurrentCursorText(spriteBatch, new Vector2(sx, sy + 13));
                    }
                    else
                    {
                        sx = 270; sy = 912;

                        spriteBatch.DrawString(textFont, "Press 'Space' key to skip text", new Vector2(sx, sy), Color.DarkGray * 0.65f);
                    }

                    break;
            }
        }
    }
}
