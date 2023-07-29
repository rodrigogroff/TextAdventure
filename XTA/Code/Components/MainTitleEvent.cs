﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XTA.Code.Infra;

namespace XTA.Code.Components
{
    public class MainTitleEvent : GameEvent
    {
        public const int DELAY_TO_PRESENT = 0;
        public const int FADEIN_PRESENT = 1;
        public const int PRESENT = 2;
        
        public int myState = DELAY_TO_PRESENT;

        public int framesStart = 1;

        public float currentAlpha = 0.0f;
        
        Texture2D pngTexture;
        public int indexer = 0;
        public float[] curve;

        public override void LoadContent(ContentManager Content)
        {
            pngTexture = Content.Load<Texture2D>("Hellfire");
            curve = new GameFunctions().GenerateLogarithmicArray(300);
            position = new Vector2(1920 / 2 - pngTexture.Width / 2, 200);
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
                    break;                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (myState)
            {
                case FADEIN_PRESENT:
                case PRESENT:
                    spriteBatch.Draw(pngTexture, position, Color.White * currentAlpha);
                    break;
            }
        }
    }
}
