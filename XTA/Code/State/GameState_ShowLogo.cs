using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using XTA.Code.Components;
using XTA.Code.Infra;

namespace XTA.Code.State
{
    public class GameState_ShowLogo : GameState
    {
        public GameState_ShowLogo() 
        { 
            id = GameXTA.GAME_STATE_SHOW_LOGO;
            done = false;
            nextState = GameXTA.GAME_STATE_SHOW_FRONTEND_START;
        }

        List<GameEvent> pipeline_logo = new List<GameEvent>();

        public override void LoadContent(ContentManager Content) 
        {
            var myLogo = new LogoEvent();

            myLogo.LoadContent(Content);

            pipeline_logo.Add(myLogo);
        }

        public override void Update() 
        {
            foreach (GameEvent e in pipeline_logo.Where(y => y.IsActive == true))
                e.Update();

            if (!pipeline_logo.Any(y => y.IsActive == true))
                done = true;
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            foreach (GameEvent e in pipeline_logo.Where(y => y.IsActive == true))
                e.Draw(spriteBatch);
        }
    }
}
