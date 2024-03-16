
public partial class TextAdventureGame
{
    private void ParseInput(string command)
    {
        switch (command.ToLower().Trim())
        {
            case "/help":
                Console.Clear(); 
                Console.WriteLine();
                DisplayHelpBox();
                break;

            case "/give":
                Console.Clear();
                Console.WriteLine();
                GiveRoom();
                break;

            case "/cls":
                Console.Clear();
                Console.WriteLine();
                DisplayCurrentRoomText();
                break;

            case "/take":
                Console.Clear();
                Console.WriteLine();
                TakeRoom();
                break;

            case "/award":
                Console.Clear();
                Console.WriteLine();
                ShowAward();
                break;

            case "/quest":
                Console.Clear();
                Console.WriteLine();
                QuestRoom();
                break;

            case "/use":
                Console.Clear();
                Console.WriteLine();
                UseRoom();
                break;

            case "/s":
            case "/speed":
                bFastMode = !bFastMode;
                Console.WriteLine();
                Print("Text speed: " + (bFastMode ? "[FAST]" : "[SLOW]"), ConsoleColor.White, 15);
                Console.WriteLine();
                break;

            case "/save":
                SaveGame();
                break;

            case "/map":
                Console.Clear();
                Console.WriteLine();
                MapRoom();
                break;

            case "/look":
                LookRoom();
                break;

            case "/stat":
                Console.Clear();
                Console.WriteLine();
                ShowStat();
                break;

            case "/gear":
                Console.Clear();
                Console.WriteLine();
                ShowGear();
                break;

            case "/hint":
                ShowHints();
                break;

            case "/bag":
                Console.Clear();
                Console.WriteLine();
                BagRoom();
                break;

            case "/automap":
                bAutomap = !bAutomap;
                Console.WriteLine();
                Print("¨ Automap: " + (bAutomap ? "[ON]" : "[OFF]"), ConsoleColor.White, 15);
                Console.WriteLine();
                break;

            case "/log":
                Console.Clear();
                ShowLog();
                break;

            case "/die":
                var awards = game.player.awards;
                game.player = new Player();
                game.player.awards = awards;
                ProcessRoom("1");
                break;

            case "/quit":
            case "/q":
                game.player = null;
                StartGame();
                break;

            default:

                switch (current_game_Room.option.ToLower())
                {
                    case "startup":
                        Console.WriteLine();
                        command = command.Trim();
                        if (current_game_Room.program.Any())
                        {
                            foreach (var prg in current_game_Room.program)
                                ProcessCommand(prg, "procCommand");

                            Console.WriteLine();
                            EnterToContinue();
                        }

                        if (!string.IsNullOrEmpty(command))
                        {
                            if (!command.StartsWith("/"))
                            {
                                game.player.name = command.PadRight(20, ' ').Substring(0, 20).Trim();
                                game.logs.Add("Player entered name: " + command);
                                Console.WriteLine();
                                Write("¨ (+) Acquired player name: ", ConsoleColor.Blue);
                                Write(game.player.name + "\n", ConsoleColor.Yellow);
                                Console.WriteLine();
                                EnterToContinue();
                                
                                ProcessRoom(current_game_Room.nextStep[0]);
                            }
                        }
                        else
                        {
                            ProcessRoom(current_game_Room.nextStep[1]);
                        }
                        break;

                    case "death":
                        ResetGame();
                        game.currentRoom = "1";
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine();
                        PrintGameBig();
                        ProcessRoom(current_game_Room.nextStep[0]);
                        break;

                    case "choice":

                        if (!string.IsNullOrEmpty(command))
                        {
                            try
                            {
                                Console.WriteLine();
                                string opt_command = current_game_Room.program[Convert.ToInt32(command) - 1];
                                ProcessCommand(opt_command, "procCommand");
                                Console.WriteLine();
                                EnterToContinue();
                                ProcessRoom(current_game_Room.nextStep[0]);
                            }
                            catch
                            {
                                Console.WriteLine();
                                Write("¨ >> Invalid choice option! << " + command, ConsoleColor.Red);
                                Console.WriteLine();
                            }
                        }
                        else if (current_game_Room.nextStep != null)
                        {
                            if (current_game_Room.nextStep.Count > 1)
                                ProcessRoom(current_game_Room.nextStep[1]);
                        }

                        break;

                    default:

                        if (command == "")
                        {
                            if (current_game_Room.program != null)
                                if (current_game_Room.program.Count > 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    foreach (var i_cmd in current_game_Room.program)
                                        ProcessCommand(i_cmd, "procCommand");
                                    Console.WriteLine();
                                    EnterToContinue();
                                }

                            if (current_game_Room.nextStep.Count > 0)
                                ProcessRoom(current_game_Room.nextStep[0]);
                        }
                        else
                        {
                            Console.WriteLine();
                            DisplayHelpBox();                            
                        }

                        break;
                }
                break;
        }
    }
}