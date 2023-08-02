using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public override void Update(GameTime gameTime) 
        {
            switch (internalState)
            {
                case MAIN_START_FADEOUT_WALLPAPER:

                    if (--delayStartFadeout > 0) { } else
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
                            if (key == Keys.Back && inputText.Length > 0 && !bDeath)
                            {
                                inputText = inputText.Substring(0, inputText.Length - 1);
                            }
                            else if (key == Keys.Enter)
                            {
                                ProcessUserInput(inputText);
                            }
                            else if (key == Keys.Escape)
                            {
                                ProcessUserInput("[ESC]");
                            }
                            else if (!bDeath)
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

                    if (bDeath)
                    {
                        if (++indexAlphaCurveDeath < FADE_FRAMES)
                            currentAlphaDeath = curve[indexAlphaCurveDeath];
                    }
                    else
                    {
                        if (bShowStats)
                        {
                            if (++indexAlphaCurveStats < FADE_FRAMES)
                                currentAlphaMainStats = curve[indexAlphaCurveStats];
                        }

                        if (bShowBag)
                        {
                            if (++indexAlphaCurveBag < FADE_FRAMES)
                                currentAlphaMainBag = curve[indexAlphaCurveBag];
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
                    }
                    break;
            }
        }
    }
}
