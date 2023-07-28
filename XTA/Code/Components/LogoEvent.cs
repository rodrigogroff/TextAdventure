using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XTA.Code.Infra;

namespace XTA.Code.Components
{
    public class LogoEvent : GameEvent
    {
        public const int DELAY_TO_PRESENT = 0;
        public const int FADEIN_PRESENT = 1;
        public const int PRESENT = 2;
        public const int FADEOUT_PRESENT = 3;

        public int myState = DELAY_TO_PRESENT;

        public int framesStart = 60 * 3;
        public int framesDuration = 60 * 3;

        float currentAlpha = 0.0f;
        
        Texture2D pngTexture;
        public int indexer = 0;
        public float[] curve;

        public override void LoadContent(ContentManager Content)
        {
            pngTexture = Content.Load<Texture2D>("logo_footer");
            curve = new GameFunctions().GenerateLogarithmicArray(600);
            position = new Vector2(1920 / 2 - pngTexture.Width / 2, 1080 / 2 - pngTexture.Height / 2);
        }

        public override void Dispose()
        {
            pngTexture.Dispose();
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
                        Dispose();
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
