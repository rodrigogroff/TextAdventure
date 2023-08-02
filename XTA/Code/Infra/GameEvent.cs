using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.Infra
{

    public class BaseGame
    {
        public string GetProperName(string name, GameXTA main)
        {
            return name + (main.b4KMode ? "_4k" : "");
        }
    }

    public class GameEvent : BaseGame
    {
        public bool IsActive = true;

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void LoadContent(ContentManager Content, GameXTA main) { }
    }
}
