using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using XTA.Code.Components;

namespace XTA.Code.State
{
    public partial class GameState_ShowFrontendStart : GameState
    {
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
                    DisplayText(spriteBatch, "1 - ", Color.Gray * myTitle.currentAlpha);
                    DisplayText(spriteBatch, "Start Game", Color.LightGray * myTitle.currentAlpha);

                    StartText(new Vector2(xStartText, yStartText + 30));
                    DisplayText(spriteBatch, "2 - ", Color.Gray * myTitle.currentAlpha);
                    DisplayText(spriteBatch, "Continue", Color.LightGray * myTitle.currentAlpha);

                    StartText(new Vector2(xStartText, yStartText + 60));
                    DisplayText(spriteBatch, "3 - ", Color.Gray * myTitle.currentAlpha);
                    DisplayText(spriteBatch, "Quit", Color.LightGray * myTitle.currentAlpha);

                    if (myTitle.currentAlpha >= 1)
                    {
                        StartText(new Vector2(xStartText, yStartText + 110));
                        DisplayInputText(spriteBatch, new Vector2(xStartText, yStartText + 110));
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
                    DisplayText(spriteBatch, selectedOption, Color.White);

                    StartText(new Vector2(xStartText, yStartText + 30 ));
                    DisplayText(spriteBatch, "1 - ", Color.Gray);
                    DisplayText(spriteBatch, "Easy ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, yStartText + 30));
                    DisplayText(spriteBatch, "-- unlimited hints ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, yStartText + 60));
                    DisplayText(spriteBatch, "2 - ", Color.Gray);
                    DisplayText(spriteBatch, "Normal ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, yStartText + 60));
                    DisplayText(spriteBatch, "-- counted hints ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, yStartText + 90));
                    DisplayText(spriteBatch, "3 - ", Color.Gray);
                    DisplayText(spriteBatch, "Old School ", Color.Yellow * 0.8f);
                    StartText(new Vector2(xStartText + 200, yStartText + 90));
                    DisplayText(spriteBatch, "-- alone in the dark ", Color.DarkGray * 0.5f);

                    StartText(new Vector2(xStartText, yStartText + 140));
                    DisplayInputText(spriteBatch, new Vector2(xStartText, yStartText + 140));

                    break;
            }
        }
    }
}
