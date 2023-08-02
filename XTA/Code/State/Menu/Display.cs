using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public partial class GameState_ShowFrontendStart : GameState
    {
        public void StartText(Vector2 startPosition)
        {
            internalText = "";
            internalTextPosition = startPosition;
        }

        public void DisplayText(SpriteBatch spriteBatch, string text, Color color)
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

        public void DisplayInputText(SpriteBatch spriteBatch, Vector2 textPosition)
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
    }
}
