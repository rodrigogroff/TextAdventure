using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XTA.Code.State
{
    public class GameState
    {
        public int id { get; set; }

        public int nextState { get; set; }

        public bool done { get; set; }

        public virtual void LoadContent(ContentManager Content) { }
        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
