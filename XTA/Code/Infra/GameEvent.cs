﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.Infra
{
    public class GameEvent
    {
        public bool IsActive = true;

        public Vector2 position;

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void LoadContent(ContentManager Content) { }

        public virtual void Dispose() { }
    }
}
