using System;
using System.Linq;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void StartText()
        {
            text_to_mainTitle = current_game_Room.label;
            text_to_display = current_game_Room.text;
            textToDisplay = "";
            textDelay = 1;
            text_curIndex = 1;
            bTextDisplayed = false;
        }

        public void ProcessRoom(string id, string cmd = "")
        {
            int version = 1;

            if (id.Contains(":"))
            {
                version = Convert.ToInt32(id.Split(':')[1]);
                id = id.Split(':')[0];
            }

            current_game_Room = main.master.stages.FirstOrDefault(y => y.id == id && y.version == version);

            if (text_to_mainTitle == "")
            {
                StartText();
                return;
            }

            if (bTextDisplayed)
            {
                switch (current_game_Room.option)
                {
                    case "choice":

                        try
                        {                            
                            int choice = Convert.ToInt32(cmd);
                            if (choice < current_game_Room.program.Count())
                            {
                                text_to_mainTitle = "";
                                ProcessProgram(current_game_Room.program[choice - 1]);
                            }                            
                        }
                        catch
                        {

                        }
                        break;

                    case "startup":

                        if (cmd == "")
                        {
                            text_to_mainTitle = "";
                            ProcessRoom(current_game_Room.nextStep[1]);
                        }
                        else
                        {
                            main.master.player.name = cmd;
                            text_to_mainTitle = "";
                            ProcessRoom(current_game_Room.nextStep[0]);
                        }

                        break;

                    default:

                        if (cmd == "")
                        {
                            text_to_mainTitle = "";
                            ProcessRoom(current_game_Room.nextStep[0]);
                        }

                        break;
                }
            }
        }
    }
}
