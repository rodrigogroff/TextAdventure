
public partial class TextAdventureGame
{
    void DisplayCurrentRoomText()
    {
        Console.WriteLine();

        var Old_fast = bFastMode;

        if (current_game_Room.option?.ToLower() != "death")
        {
            Write(" ▒▓██ ", ConsoleColor.Blue);
            Write(current_game_Room.label, ConsoleColor.White);
            Write(" █████████▓▒ ", ConsoleColor.Blue);

            if (bAutomap)
            {
                Write(" -- Automap: ", ConsoleColor.DarkGray);
                Write("ON", ConsoleColor.White);
            }

            if (!bFastMode)
            {
                Write(" -- Press ", ConsoleColor.DarkGray);
                Write("Space", ConsoleColor.Green);
                Write(" for faster text, ", ConsoleColor.DarkGray);
                Write("Esc", ConsoleColor.Green);
                Write(" to instant text, ", ConsoleColor.DarkGray);
            }
        }
        else
        {
            bFastMode = true;
        }

        Thread.Sleep(500);

        game.logs.Add(" -- Entered: " + current_game_Room.label);

        Console.WriteLine();
        Console.WriteLine();

        bool _b_safe = bFastMode;

        foreach (var item in current_game_Room.text)
        {
            Thread.Sleep(10);

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    bFastMode = true;
                }
            }

            if (current_game_Room.id == "0")
                PrintRoomText(item, ConsoleColor.DarkRed, 35);
            else
                PrintRoomText(item, ConsoleColor.Yellow, 35);
        }

        bFastMode = _b_safe;

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

        Thread.Sleep(100);

        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Spacebar)
            {
                while (true)
                {
                    ConsoleKeyInfo _key = Console.ReadKey(true);
                    if (_key.Key != ConsoleKey.Spacebar)
                    {
                        break;
                    }
                }
            }
        }

        bFastMode = Old_fast;
        Console.WriteLine();
    }

    void ProcessRoom(string id)
    {
        if (id == "1")
        {
            Write("\n --- Current game awards ----- \n", ConsoleColor.Yellow);
            ShowAward();
            EnterToContinue();
            Console.Clear();
            DisplayHelpBox();
            Write("\n", ConsoleColor.Yellow);
            EnterToContinue();
            Write("\n", ConsoleColor.Yellow);
            Console.Clear();
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

                bool bAlreadyMapped = false;

                while (true)
                {
                    if (bAutomap && !bAlreadyMapped)
                    {
                        if (!string.IsNullOrEmpty(current_game_Room.map))
                            MapRoom();
                        bAlreadyMapped = true;
                        Console.WriteLine();
                    }

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