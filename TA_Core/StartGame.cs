﻿using Newtonsoft.Json;

public partial class TextAdventureGame
{
    public void StartGame()
    {
        InitScreen();

        if (File.Exists(monitor_file))
            monitor = JsonConvert.DeserializeObject<GameMonitoring>(crypt.DecryptFile(monitor_file));
        else
        {
            monitor = new GameMonitoring
            {
                games = new List<GameMonitor>()
            };
        }

        try
        {
            int mode = 2;

            while (true)
            {                
                switch (mode)
                {
                    case 1:
                        break;

                    case 2:
                        while (true)
                        {
                            Console.Clear();
                            DisplayStartScreen();
                            Console.WriteLine();
                            Write("¨ [Games available:]\n", ConsoleColor.Yellow);
                            Console.WriteLine();
                            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Games", "*.game.jsonx");

                            for (int i = 0; i < files.Length; i++)
                            {
                                int seconds = 0;
                                int deaths = 0;

                                string fileName = Path.GetFileNameWithoutExtension(files[i]);

                                var g_name = fileName.Replace("_", " ").Replace(".game", "");

                                var monit_game = monitor.games.FirstOrDefault(y => y.game_name == g_name);

                                if (monit_game != null)
                                {
                                    foreach (var item in monit_game.plays)
                                    {
                                        if (item.dtEnd != null)
                                        {
                                            seconds += (int)Convert.ToDateTime(item.dtEnd).Subtract(item.dtStart).TotalSeconds;
                                            if (item.death == true)
                                                deaths++;
                                        }
                                    }
                                }

                                string awards = "";

                                if (File.Exists(files[i] + ".save"))
                                {
                                    var contents = crypt.DecryptFile(files[i] + ".save");
                                    var savegame = JsonConvert.DeserializeObject<SaveGameFile>(contents);
                                    var _gam = JsonConvert.DeserializeObject<Game>(crypt.DecryptFile(files[i]));

                                    awards = savegame.player.awards.Count() + "/" + _gam.awards.Count();
                                }

                                Write("¨ " + (i + 1) + " - ", ConsoleColor.DarkGray);
                                Write(g_name.PadRight(35, ' '), ConsoleColor.White);

                                if (awards != "")
                                {
                                    Write(" -- awards: ", ConsoleColor.DarkGray);
                                    Write(awards + " ", ConsoleColor.Green);
                                }

                                if (seconds > 0)
                                {
                                    Write(" -- time: ", ConsoleColor.DarkGray);
                                    Write(FormatTimeSpan(TimeSpan.FromSeconds(seconds)), ConsoleColor.Green);
                                    Write(" -- deaths: ", ConsoleColor.DarkGray);
                                    Write(deaths.ToString(), ConsoleColor.Red);
                                }
                                Write("\n", ConsoleColor.Green);
                            }
                            Console.WriteLine();
                            Write("¨ [Select your game:]\n", ConsoleColor.DarkGray);
                            Write("¨ [> ", ConsoleColor.Green);
                            Console.ForegroundColor = ConsoleColor.Green;
                            while (Console.KeyAvailable) Console.ReadKey(intercept: true);
                            
                            Console.CursorVisible = true;
                            var option = ConsoleReadLine().Trim().ToLower().Split(' ');

                            #if DEBUG
                            if (option.Contains("/qa"))
                            {
                                bFastMode = true;
                                bAutomation = true;
                                Write("¨ [> AUTOMATION\n", ConsoleColor.DarkGray);
                                EnterToContinue();                                
                            }
                            #endif

                            if (int.TryParse(option[0], out int selectedIndex) &&
                                selectedIndex > 0 && selectedIndex <= files.Length)
                            {
                                currentFile = files[selectedIndex - 1];
                                Console.WriteLine();
                                                                
                                game.gameJsonFile = currentFile;

                                LoadGame();

                                if (bAutomation)
                                {
                                    automationFile = File.ReadAllText(currentFile.Replace(".game.jsonx", ".QA.txt")).Split("\r\n").ToList();
                                }
                                
                                if (monitor.games == null)
                                    monitor.games = new List<GameMonitor>();

                                gamePlay = new GameMonitorPlays
                                {
                                    death = false,
                                    dtStart = DateTime.Now,
                                    dtEnd = null
                                };

                                gameMonitor = monitor.games.FirstOrDefault(y => y.game_name == game.gameName);

                                if (gameMonitor == null)
                                {
                                    gameMonitor = new GameMonitor
                                    {
                                        game_name = game.gameName,
                                        plays = new List<GameMonitorPlays>()
                                    };

                                    monitor.games.Add(gameMonitor);
                                }

                                if (gameMonitor.plays == null)
                                    gameMonitor.plays = new List<GameMonitorPlays>();

                                gameMonitor.plays.Add(gamePlay);                               

                                mode = 3;
                                break;
                            }
                            else
                                continue;
                        }
                        break;

                    case 3:
                        DisplayStartScreen();
                        Console.WriteLine();
                        Write("¨ Game [", ConsoleColor.Yellow);
                        Write( game.gameName, ConsoleColor.White);
                        Write("]\n", ConsoleColor.Yellow);
                        Console.WriteLine();
                        Write("¨ 1 - ", ConsoleColor.DarkGray);
                        Write("Easy       ", ConsoleColor.Yellow);
                        Write("-- Unlimited hints\n", ConsoleColor.DarkGray);
                        Write("¨ 2 - ", ConsoleColor.DarkGray);
                        Write("Normal     ", ConsoleColor.DarkYellow);
                        Write("-- Counted hints\n", ConsoleColor.DarkGray);
                        Write("¨ 3 - ", ConsoleColor.DarkGray);
                        Write("Old School ", ConsoleColor.Red);
                        Write("-- Alone in the dark\n", ConsoleColor.DarkGray);
                        Console.WriteLine();
                        Write("¨ [Select your game difficulty:]\n", ConsoleColor.DarkGray);
                        Write("¨ [> ", ConsoleColor.Green);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorVisible = true;
                        while (Console.KeyAvailable) Console.ReadKey(intercept: true);
                        var diff = ConsoleReadLine().Trim();

                        bHardcore = false;

                        gameDifficulty = diff;

                        if (diff == "2")
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

                            Write("\n¨  --- [HARDCORE MODE UNLOCKED!] ---\n", ConsoleColor.White);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            bUnlimitedHints = true;
                        }

                        mode = 4;
                        break;

                    case 4:

                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine();
                        Write("¨   Big Boys Games ", ConsoleColor.Blue);
                        Write(" proudly presents ...\n", ConsoleColor.White);
                        Console.WriteLine();
                        PrintGameBig();
                        string option_load = "";

                        if (File.Exists(currentFile + ".save"))
                        {
                            while (true)
                            {
                                Console.WriteLine();
                                Write("¨ 1 - ", ConsoleColor.DarkGray);
                                Write("Continue from saved game\n", ConsoleColor.White);
                                Write("¨ 2 - ", ConsoleColor.DarkGray);
                                Write("New game (lose all awards!)\n", ConsoleColor.DarkGray);
                                Console.WriteLine();
                                Write("¨ [> ", ConsoleColor.Green);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.CursorVisible = true;
                                while (Console.KeyAvailable) Console.ReadKey(intercept: true);
                                option_load = ConsoleReadLine().Trim();

                                if (option_load == "")
                                    option_load = "1";                                    

                                if (option_load == "1")
                                {
                                    var savegame = JsonConvert.DeserializeObject<SaveGameFile>(crypt.DecryptFile(currentFile + ".save"));
                                    game.player = savegame.player;
                                    game.world = savegame.world;
                                    game.world_itens = savegame.world_itens;
                                    game.currentRoom = savegame.currentRoom;
                                    game.logs = savegame.logs;
                                    game.hints = savegame.hints;
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else
                                {
                                    File.Delete(currentFile + ".save");
                                    break;
                                }
                            }
                        }

                        Console.WriteLine();
                        EnterToContinue();
                        if (game.currentRoom == null)
                            game.currentRoom = "1";
                        Console.Clear();
                        ProcessRoom(game.currentRoom);
                        break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            ConsoleReadLine();
        }
    }
}
