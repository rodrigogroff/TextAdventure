using System;

namespace XTA.Code.State
{
    public partial class GameState_ShowFrontendStart : GameState
    {        
        public void ProcessInput()
        {
            switch (internalState)
            {
                case START_GAME:

                    if (inputText == "1")
                    {
                        inputText = "";
                        selectedOption = "New Game!";

                        internalState++; // novo jogo
                    }
                    else if (inputText == "2")
                    {
                        selectedOption = "Continue!";
                        inputText = "";
                    }
                    else if (inputText == "3")
                    {
                        Environment.Exit(0); // sair
                    }

                    break;

                case DIFFICULTY:

                    this.done = true;

                    break;
            }
        }        
    }
}
