using Newtonsoft.Json;
using System.Diagnostics;

public partial class TextAdventureGame
{
    public void StartGame()
    {
        try
        {
            Console.CursorVisible = false;
            Console.Clear();
            Thread.Sleep(500);
            Console.WriteLine();
            Write(" DOS/4GW Professional Protected Mode Run-Time Versiom 2.1c\n", ConsoleColor.White);
            Write(" Copyright (C) United TA Systems, Inc. 1976\n", ConsoleColor.DarkGray);
            Write(" Engine Version:", ConsoleColor.DarkGray); Write(" 0.1.1\n", ConsoleColor.Red);
            Console.WriteLine();
            Write(" -- Use ALT+ENTER for fullscreen\n", ConsoleColor.DarkGray);
            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(2000);
            Write(" To play this game, please support the patreon page (for the month password): \n", ConsoleColor.White);
            Write(" https://www.patreon.com/bigboysgames \n", ConsoleColor.Blue);
            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(2000);
            Console.CursorVisible = true;

            string email = "";

            if (File.Exists("email.txt"))
            {
                email = File.ReadAllText("email.txt");
                Write(" [Email: " + email + " ]\n", ConsoleColor.Yellow);
            }
            else
            {
                while (!email.Contains("@"))
                {
                    Write(" [Email:                              ]\n", ConsoleColor.White);
                    Write(" [> ", ConsoleColor.Green);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.CursorVisible = true;
                    email = Console.ReadLine().Trim();
                    Console.WriteLine();
                }
                File.WriteAllText("email.txt", email);
            }

            Write(" [Password:] ", ConsoleColor.Green);
            Console.ForegroundColor = ConsoleColor.Green;
            string password = "";
            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.Write("\b \b"); // Remove the asterisk from the console
                    }
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }

            int mode = 1;

            while (true)
            {
                
                Console.CursorVisible = false;

                switch (mode)
                {
                    case 1:
                        DisplayStartScreen();
                        Console.WriteLine();
                        Write(" 1 - ", ConsoleColor.DarkGray);
                        Write("Local games\n", ConsoleColor.White);
                        Write(" 2 - ", ConsoleColor.DarkGray);
                        Write("Online Server\n", ConsoleColor.White);
                        Write(" 3 - ", ConsoleColor.DarkGray);
                        Write("Open README file\n", ConsoleColor.White);
                        Console.WriteLine();
                        Write("[> ", ConsoleColor.Green);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorVisible = true;
                        string optionMaster = Console.ReadLine().Trim();

                        if (optionMaster == "3")
                        {
                            Process.Start("notepad.exe", "Readme.txt");
                        }
                        else if (optionMaster == "2")
                        {
                            Write(" Server OFFLINE\n", ConsoleColor.White);
                        }
                        else if (optionMaster == "1")
                        {
                            mode = 2;
                        }

                        break;

                    case 2:
                        DisplayStartScreen();
                        Console.WriteLine();
                        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Games", "*.game.json");
                        Write("[" + files.Count() + "] Files found:\n", ConsoleColor.Blue);
                        Console.WriteLine();
                        
                        for (int i = 0; i < files.Length; i++)
                        {
                            Write(" " + (i + 1) + " - ", ConsoleColor.DarkGray);
                            Write(Path.GetFileNameWithoutExtension(files[i]).Replace("_", " ").Replace(".game", "") + "\n", ConsoleColor.White);
                        }
                        Console.WriteLine();
                        Write("[Select your game:] -- use ", ConsoleColor.DarkGray);
                        Write("/q ", ConsoleColor.White);
                        Write("to quit, ", ConsoleColor.DarkGray);
                        Write("/s", ConsoleColor.White);
                        Write(" to fast text \n", ConsoleColor.DarkGray);
                        Write("[> ", ConsoleColor.Green);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorVisible = true;
                        string option = Console.ReadLine().Trim();

                        if (option.ToLower() == "/q")
                        {
                            mode = 1;
                            break;
                        }

                        if (option.EndsWith("/s"))
                            bFastMode = true;

                        option = option.Replace("/s", "");

                        Console.CursorVisible = false;

                        if (int.TryParse(option, out int selectedIndex) &&
                            selectedIndex > 0 && selectedIndex <= files.Length)
                        {
                            currentFile = files[selectedIndex - 1];
                            Console.WriteLine();
                            game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(currentFile));

                            // process all stages inventory
                            foreach (var currentStage in game.stages)
                            {
                                if (currentStage.take.Count > 0)
                                {
                                    foreach (var takeItem in currentStage.take)
                                    {
                                        var name = takeItem.Split(' ')[0];
                                        var qtty = Convert.ToInt32(takeItem.Split(' ')[1].Split(':')[0]);
                                        var g_item = game.itens.FirstOrDefault(y => y.name == name);

                                        game.world_itens.Add(new GameSceneItem
                                        {
                                            guid = Guid.NewGuid(),
                                            scene_id = currentStage.id,
                                            item = new GameItem
                                            {
                                                name = name,
                                                quantity = qtty,
                                                persistInventory = g_item.persistInventory == true,
                                            }
                                        });
                                    }

                                    currentStage.take.Clear();
                                }
                            }

                            game.hintsMAX = game.hints;

                            game.player.gear = new PlayerGear
                            {
                                body = null,
                                feet = null,
                                hands = null,
                                head = null,
                                rings = new List<GameItem>(),
                                amulets = new List<GameItem>()
                            };

                            mode = 3;
                        }
                        else
                            mode = 1;

                        break;
                        

                    case 3:

                        DisplayStartScreen();
                        Console.WriteLine();
                        Write(" 1 - ", ConsoleColor.DarkGray);
                        Write("Easy       ", ConsoleColor.Yellow);
                        Write("-- Unlimited hints\n", ConsoleColor.DarkGray);
                        Write(" 2 - ", ConsoleColor.DarkGray);
                        Write("Normal     ", ConsoleColor.DarkYellow);
                        Write("-- Counted hints\n", ConsoleColor.DarkGray);
                        Write(" 3 - ", ConsoleColor.DarkGray);
                        Write("Old School ", ConsoleColor.Red);
                        Write("-- Alone in the dark\n", ConsoleColor.DarkGray);
                        Console.WriteLine();
                        Write(" [> ", ConsoleColor.Green);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorVisible = true;

                        var diff = Console.ReadLine().Trim();

                        if (diff == "1")
                        {
                            bUnlimitedHints  = true;
                        }
                        else if(diff == "2")
                        {
                            bUnlimitedHints = false;
                            bHintsDisabled = false;
                        }
                        else if (diff == "3")
                        {                            
                            bHintsDisabled = true;
                            bFastMode = true;
                        }
                        else if (diff == "4")
                        {
                            bHardcore = true;
                            bFastMode = true;
                            
                            Write("\n  --- [HARDCORE MODE UNLOCKED!] ---\n", ConsoleColor.White);
                            Thread.Sleep(2000);
                        }

                        mode = 4;
                        break;

                    case 4:

                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine();
                        PrintRoomText("^Big Boys Games^ proudly presents ...", ConsoleColor.White, 10);
                        Console.WriteLine();
                        PrintGameBig();
                        string option_load = "";

                        if (File.Exists(currentFile + ".save"))
                        {
                            Console.WriteLine();
                            Write(" 1 - ", ConsoleColor.DarkGray);
                            Write("Continue from saved game\n", ConsoleColor.White);
                            Write(" 2 - ", ConsoleColor.DarkGray);
                            Write("New game (lose all awards!)\n", ConsoleColor.DarkGray);
                            Console.WriteLine();
                            Write(" [> ", ConsoleColor.Green);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.CursorVisible = true;
                            option_load = Console.ReadLine().Trim();
                            if (option_load == "1")
                            {
                                var savegame = JsonConvert.DeserializeObject<SaveGameFile>(File.ReadAllText(currentFile + ".save"));
                                game.player = savegame.player;
                                game.world = savegame.world;
                                game.world_itens = savegame.world_itens;
                                game.currentRoom = savegame.currentRoom;
                                game.logs = savegame.logs;
                                game.hints = savegame.hints;
                                Thread.Sleep(1000);
                            }
                            else
                                File.Delete(currentFile + ".save");
                        }
                        else
                        {
                            DisplayTips();
                        }

                        if (game.currentRoom == null)
                            game.currentRoom = "1";

                        Console.WriteLine();

                        Console.CursorVisible = true;
                        ProcessRoom(game.currentRoom);

                        break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }
}
