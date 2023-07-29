using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using XTA.Code.Components;
using XTA.Code.Infra;

namespace XTA.Code.State
{
    public class GameState_ShowMainGame : GameState
    {
        public GameState_ShowMainGame() 
        {
            id = GameXTA.GAME_STATE_SHOW_MAIN_GAME;         
            done = false;
        }

        List<GameEvent> pipeline = new List<GameEvent>();

        public override void LoadContent(ContentManager Content) 
        {
            var myGameText = new MainGameText();

            myGameText.LoadContent(Content);

            var myGameStat = new MainGameStat();

            myGameStat.LoadContent(Content);

            pipeline.Add(myGameText);
            pipeline.Add(myGameStat);
        }

        public override void Update(GameTime gameTime) 
        {
            foreach (GameEvent e in pipeline.Where(y => y.IsActive == true))
                e.Update();

            if (!pipeline.Any(y => y.IsActive == true))
                done = true;
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            foreach (GameEvent e in pipeline.Where(y => y.IsActive == true))
                e.Draw(spriteBatch);
        }
    }
}
