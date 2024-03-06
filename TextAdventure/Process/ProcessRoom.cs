
public partial class TextAdventureGame
{
    void DisplayCurrentRoomText()
    {
        Console.WriteLine();

        if (current_game_Room.option?.ToLower() != "death")
        {
            Write(" ▒▓██ ", ConsoleColor.Blue);
            Write(current_game_Room.label, ConsoleColor.White);
            Write(" █████████▓▒\n", ConsoleColor.Blue);
        }

        Thread.Sleep(500);

        game.logs.Add(" -- Entered: " + current_game_Room.label);

        Console.WriteLine();
        Console.WriteLine();

        foreach (var item in current_game_Room.text)
        {
            Thread.Sleep(10);
            if (current_game_Room.id == "0")
                PrintRoomText(item, ConsoleColor.DarkRed, 35);
            else
                PrintRoomText(item, ConsoleColor.Yellow, 35);
        }
        if (current_game_Room.textOptional != null)
        {
            Thread.Sleep(10);
            if (current_game_Room.textOptional.Count > 0)
            {
                foreach (var item in current_game_Room.textOptional)
                {
                    ProcessCommand(item, "procRoom");
                }
            }
        }

        Console.WriteLine();
    }

    void ProcessRoom(string id)
    {
        if (id == "1")
        {
            Write("\n --- Current game awards ----- \n", ConsoleColor.Yellow);
            ShowAward();
            DisplayHelpBox();
        }

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
                        ProcessCommand(current_game_Room.startup, "procRoom");

                    if (current_game_Room.startupProgram.Any())
                        foreach (var _p in current_game_Room.startupProgram)
                            ProcessCommand(_p, "procRoom");

                    if (current_game_Room.option != "death")
                        CheckConstraints();
                }

                DisplayCurrentRoomText();

                while (true)
                {
                    DisplayPlayerInputBox();
                    first = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.CursorVisible = true;
                    while (Console.KeyAvailable) Console.ReadKey(intercept: true);
                    ParseInput(Console.ReadLine().Trim());
                    Console.WriteLine();
                }
            }
        }
    }
}