using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XTA.Code.Components;

namespace XTA.Code.State
{
    public partial class GameState_ShowFrontendStart : GameState
    {
        const int
            START_GAME = 0,
            DIFFICULTY = 1;

        public GameState_ShowFrontendStart(GameXTA _main)
        {
            main = _main;
            id = GameXTA.GAME_STATE_SHOW_FRONTEND_START;
            done = false;
            nextState = GameXTA.GAME_STATE_SHOW_MAIN_GAME;
        }
        
        int 
            internalState = START_GAME,
            xStartText = 900,
            yStartText = 525;
        
        string 
            internalText = "",
            selectedOption = "";

        double 
            cursorBlinkTime = 0.5,
            cursorElapsed = 0;

        MainTitleEvent myTitle;
        SpriteFont menuFont;
        Vector2 internalTextPosition;
        KeyboardState prevKeyboardState;
    }
}
