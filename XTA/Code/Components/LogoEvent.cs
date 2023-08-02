using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XTA.Code.Infra;

namespace XTA.Code.Components
{
    public class LogoEvent : GameEvent
    {
        public const int 
            DELAY_TO_PRESENT = 0,
            FADEIN_PRESENT = 1,
            PRESENT = 2,
            FADEOUT_PRESENT = 3;

        public int 
            myState = DELAY_TO_PRESENT,
            framesStart = 60 * 3,
            indexer = 0,
            framesDuration = 60 * 3;

        float currentAlpha = 0.0f;

        Vector2 position;
        Texture2D pngTexture;

        public float[] curve = new GameFunctions().GenerateLogarithmicArray(600);

        public override void LoadContent(ContentManager Content, GameXTA main)
        {
            pngTexture = Content.Load<Texture2D>("logo_footer");

            if (main.bUltraWideMode)
            {
                position = new Vector2(main.virtualScreenUltraWidth / 2 - pngTexture.Width / 2, main.virtualScreenUltraHeight / 2 - pngTexture.Height / 2); 
            }
            else
            {
                position = new Vector2(main.virtualScreenWidth / 2 - pngTexture.Width / 2, main.virtualScreenHeight / 2 - pngTexture.Height / 2);
            }
        }

        public override void Update()
        {
            switch (myState)
            {
                case DELAY_TO_PRESENT:
                    if (--framesStart == 0)
                        myState++;
                    break;

                case FADEIN_PRESENT:

                    if (currentAlpha < 1)
                        currentAlpha += curve[indexer++];

                    if (currentAlpha >= 1)
                    {
                        myState++;
                    }

                    break;

                case PRESENT:

                    if (--framesDuration == 0)
                        myState++;

                    break;

                case FADEOUT_PRESENT:

                    if (currentAlpha > 0)
                        currentAlpha -= curve[indexer--];

                    if (currentAlpha <= 0)
                    {
                        currentAlpha = 0;
                        IsActive = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (myState)
            {
                case FADEIN_PRESENT:
                case PRESENT:
                case FADEOUT_PRESENT:
                    spriteBatch.Draw(pngTexture, position, Color.White * currentAlpha);
                    break;
            }
        }
    }
}
