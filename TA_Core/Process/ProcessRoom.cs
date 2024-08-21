
public partial class TextAdventureGame
{
    void ProcessRoom(string id)
    {
        if (id == "1")
        {
            var pl = game.player;
            LoadGame();
            game.player = pl;
            ShowAward();
            EnterToContinue();
            Console.Clear();            
        }
        else
            Console.WriteLine();

        int version = 1;

        if (id.Contains(":"))
        {
            version = Convert.ToInt32(id.Split(':')[1]);
            id = id.Split(':')[0];
        }

        current_game_Room = game.stages.FirstOrDefault(y => y.id == id && y.version == version);

        if (current_game_Room != null)
        {
            game.currentRoom = current_game_Room.id;

            bool first = true;
            while (true)
            {
                if (current_game_Room.option != "death")
                {
                    if (current_game_Room.version == 1 && current_game_Room.id == "1"){} else Console.Clear();
                }
                else if (!first)
                    return;

                Console.CursorVisible = false;
                Console.WriteLine();

                if (first)
                {
                    if (!string.IsNullOrEmpty(current_game_Room.startup))
                        ProcessCommand(current_game_Room.startup);

                    if (current_game_Room.startupProgram.Any())
                        foreach (var _p in current_game_Room.startupProgram)
                            ProcessCommand(_p);

                    if (current_game_Room.option != "death")
                        CheckConstraints();
                }

                Console.WriteLine();
                DisplayCurrentRoomText();

                while (true)
                {
                    DisplayPlayerInputBox();
                    first = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    while (Console.KeyAvailable) Console.ReadKey(intercept: true);
                    ParseInput(ConsoleReadLine().Trim());
                    Console.WriteLine();
                }
            }
        }
    }
}
