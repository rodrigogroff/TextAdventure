using System.Collections.Generic;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void ToogleBag(string subItem)
        {
            bShowBag = !bShowBag;
            cmdText = subItem;

            if (bShowBag)
            {
                currentAlphaMainBag = 0;
                indexAlphaCurveBag = 0;
            }
        }

        public void ProcessUserInput(string cmd)
        {
            if (bDeath)
            {
                this.done = true;
                this.nextState = GameXTA.GAME_STATE_RESET;
                return;
            }

            if (bShowBag)
                if (cmd == "" || cmd == "[ESC]")
                    cmd = "bag";

            cmd = cmd.ToLower();

            inputText = "";
            textIncoming = "";           

            if (cmd == "quit")
            {
                bDeath = true;
                textToDisplay = "";
            }
            else if (cmd == "help")
            {
                textToDisplay = "";
                textDelay = 1;
                text_curIndex = 1;
                bTextDisplayed = false;
                original_text = helpText;
            }
            else if (cmd == "stat")
            {
                bShowStats = !bShowStats;
                if (bShowStats)
                {
                    currentAlphaMainStats = 0;
                    indexAlphaCurveStats = 0;
                }
            }
            else if (cmd == "bag")
            {
                ToogleBag("Bag");
            }
            else if (cmd == "use")
            {
                ToogleBag("Use Item");
            }
            else if (cmd == "give")
            {
                ToogleBag("Give Item");
            }
            else if (cmd == "map")
            {
                ToogleBag("Map");
            }
            else if (cmd == "quest")
            {
                ToogleBag("Quest");
            }
        }
    }
}
