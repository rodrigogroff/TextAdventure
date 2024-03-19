﻿
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
                Write(" Missing img_" + current_game_Room.id + "_" + current_game_Room.version + "  ", ConsoleColor.Red );

            if (bAutomation)
            {
                Write("\n", ConsoleColor.DarkGray);
                Write("¨ -- Automation: ", ConsoleColor.DarkGray);
                Write(currentAutomation, ConsoleColor.Green);
            }
            
            Write(" -- Press ", ConsoleColor.DarkGray);
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
