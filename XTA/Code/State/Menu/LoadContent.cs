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
        public override void LoadContent(ContentManager Content, GraphicsDevice Device)
        {
            myTitle = new MainTitleEvent();
            myTitle.LoadContent(Content, main);            
            menuFont = Content.Load<SpriteFont>("Merriweather");     
        }
    }
}
