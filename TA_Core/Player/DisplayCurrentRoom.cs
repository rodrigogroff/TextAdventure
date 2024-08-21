
public partial class TextAdventureGame
{
    void DisplayCurrentSceneTitle()
    {
        Write("¨ ▒▓██ ", ConsoleColor.Blue);
        Write(current_game_Room.label, ConsoleColor.White);
    }

    void DisplayCurrentRoomText()
    {
        var wallpaper = Directory.GetCurrentDirectory() + "\\Images\\" + game.gameName + "\\img_" + current_game_Room.id + "_" + current_game_Room.version + "_" + screenWidth + ".jpg";

        var foundIMg = false;

        if (File.Exists(wallpaper))
        {
            foundIMg = true;
            ChangeWallpaper(wallpaper);
        }

        if (current_game_Room.option?.ToLower() != "death")
        {
            Console.WriteLine();
            Console.WriteLine();

            DisplayCurrentSceneTitle();

            if (!foundIMg)
                Write(" Missing img_" + current_game_Room.id + "_" + current_game_Room.version + "  ", ConsoleColor.Red);

            if (bAutomation)
            {
                Write("\n", ConsoleColor.DarkGray);
                Write("¨ -- Automation: ", ConsoleColor.DarkGray);
                Write(currentAutomation, ConsoleColor.Green);
            }

            Write("\n", ConsoleColor.DarkGray);
            Write("¨ -- Press ", ConsoleColor.DarkGray);
            Write("Esc", ConsoleColor.Green);
            Write(" to skip text dialogue", ConsoleColor.DarkGray);
        }
        else
        {
            gamePlay.death = true;
            FlushMonitorFile();
        }

        if (!bAutomation)
            Thread.Sleep(500);

        Console.WriteLine();
        Console.WriteLine();

        foreach (var item in current_game_Room.text)
        {
            if (!bAutomation)
            {
                Thread.Sleep(10);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        //?    
                    }
                }
            }

            if (current_game_Room.id == "0")
                PrintRoomText(item, ConsoleColor.DarkRed, 35);
            else
                PrintRoomText(item, ConsoleColor.Yellow, 35);
        }

        if (current_game_Room.textOptional != null)
        {
            if (!bAutomation)
                Thread.Sleep(10);

            if (current_game_Room.textOptional.Count() > 0)
                foreach (var item in current_game_Room.textOptional)
                    ProcessCommand(item);
        }

        if (current_game_Room.skip.Any())
        {
            Console.WriteLine();
            Write("¨ (Use ", ConsoleColor.Magenta);
            Write("/skip", ConsoleColor.White);
            Write(" to pass this scene)", ConsoleColor.Magenta);
            Console.WriteLine();
        }

        if (!bAutomation)
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

        Console.WriteLine();
    }
}
