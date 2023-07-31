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
        public GameState_ShowMainGame() 
        {
            id = GameXTA.GAME_STATE_SHOW_MAIN_GAME;         
            done = false;
        }

        const int MAIN_START_FADEOUT_WALLPAPER = 0;
        const int MAIN_START_FADEOUT_COMPLETE = 1;
        const int MAIN_START_FADEIN_MAINDIALOG = 2;
        const int MAIN_START_FADEIN_MAINDIALOG_TITLE = 3;
        const int MAIN_START_FADEIN_MAINDIALOG_INTERNAL_TEXT = 4;
        const int MAIN_START_FADEIN_MAINDIALOG_CURSOR = 5;
        const int MAIN_COMPLETE = 6;

        const int FADE_FRAMES = 60;

        public float[] curve = new GameFunctions().GenerateLogarithmicArray(FADE_FRAMES);

        int internalState = 0;
        int timeout = 60;
        int delay = 60;

        float currentAlphaWallpaper = 1;
        float currentAlphaMainDialog = 0;

        SpriteFont titleFont;
        SpriteFont textFont;

        Texture2D pngTexture_wallpaper;
        Texture2D pngTexture_dialog;
        Texture2D pngTexture_stats;

        string MainTitle = "Soul Selection";

        List<string> text = new List<string>();

        List<string> original_text = new List<string>
        {
            "\"I'm out for groceries!\" Sarah said to her husband Paul, while searching for the car keys in her",
            "big pockets full of lollipops. She was still wearing her blue jacket with the name ^Dr. Sarah^",
            "^Allis Smith^ embroided in blue letters.",
            "",
            "Paul got out of the couch and fast he went to the kitchen.",
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

        KeyboardState prevKeyboardState;
        Vector2 zero = new Vector2 (0, 0);

        Vector2 mainDialogPos = new Vector2(70, 0);

        Vector2 internalTextPosition;        
        string internalText = "";
        string inputText = "";
        
        bool cursorVisible = true;

        double cursorBlinkTime = 0.3,
               cursorElapsed = 0;

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

                    if (--delay > 0)
                    {

                    }
                    else
                    {
                        if (timeout-- > 0)
                            currentAlphaWallpaper -= 0.01f;
                        else
                        {
                            timeout = 60;
                            internalState++;
                        }
                    }
                    break;

                case MAIN_START_FADEOUT_COMPLETE:

                    if (timeout-- > 0)
                    {
                        // wait
                    }
                    else
                        internalState++;

                    break;

                case MAIN_START_FADEIN_MAINDIALOG:

                    if (++timeout >= FADE_FRAMES)
                    {
                        internalState++;
                    }
                    else
                    {
                        currentAlphaMainDialog = curve[timeout];
                    }

                    break;

                case MAIN_START_FADEIN_MAINDIALOG_TITLE:
                    internalState++;
                    break;

                case MAIN_START_FADEIN_MAINDIALOG_INTERNAL_TEXT:
                    internalState++;
                    break;

                case MAIN_START_FADEIN_MAINDIALOG_CURSOR:
                    internalState++;
                    break;

                case MAIN_COMPLETE:

                    if (internalState == MAIN_COMPLETE)
                    {
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

                    if (text.Count < original_text.Count)
                    {
                        if (--textDelay == 0)
                        {
                            textDelay = textDelayTime;
                            text.Add(original_text[currentOrigIndex++]);
                        }                        
                    }

                    break;
            }
        }

        int textDelayTime = 10;
        int textDelay = 1;
        int currentOrigIndex = 0;

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
            spriteBatch.DrawString(titleFont, inputText, textPosition, Color.Green * currentAlphaMainDialog);

            if (cursorVisible)
            {
                float cursorWidth = titleFont.MeasureString(inputText).X;
                Vector2 cursorPosition = textPosition + new Vector2(cursorWidth, 0);
                Color cursorColor = Color.Green;
                spriteBatch.DrawString(titleFont, "|", cursorPosition, cursorColor * currentAlphaMainDialog);
            }
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

                case MAIN_START_FADEIN_MAINDIALOG_TITLE:
                case MAIN_START_FADEIN_MAINDIALOG_INTERNAL_TEXT:
                case MAIN_START_FADEIN_MAINDIALOG_CURSOR:
                case MAIN_COMPLETE:

                    spriteBatch.Draw(pngTexture_wallpaper, zero, Color.White * currentAlphaWallpaper);
                    spriteBatch.Draw(pngTexture_dialog, mainDialogPos, Color.White * currentAlphaMainDialog);

                    MainTitle = "This is very big text so deal with it";

                    float titleWidth = textFont.MeasureString(MainTitle).X;
                    spriteBatch.DrawString(titleFont, MainTitle, new Vector2(509 - titleWidth/2, 143), Color.Red * currentAlphaMainDialog);

                    int sx = 210, sy = 292;

                    foreach (var item in text)
                    {
                        StartText(new Vector2(sx, sy));
                        AddText(spriteBatch, item, Color.White * currentAlphaMainDialog);
                        sy += 21;
                    }

                    sx = 252; sy = 917;

                    StartText(new Vector2(sx, sy));
                    DrawCurrentInputText(spriteBatch, new Vector2(sx + 22, sy));

                    break;
            }
        }
    }
}
