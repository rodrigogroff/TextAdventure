using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;

public partial class TextAdventureGame
{
    public void StartGame()
    {
        try
        {
            Console.CursorVisible = false;

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

            var summary = JsonConvert.DeserializeObject<List<GameSummary>>(File.ReadAllText(Directory.GetCurrentDirectory() + summary_file));
            
            if (!File.Exists(setup_file))
            {
                var setupCfg_temp = new GameSetup
                {
                    emptySpace = this.emptySpace
                };

                File.WriteAllText(Directory.GetCurrentDirectory() + setup_file, JsonConvert.SerializeObject(setupCfg_temp));
            }

            var setupCfg = JsonConvert.DeserializeObject<GameSetup>(File.ReadAllText(Directory.GetCurrentDirectory() + setup_file));

            var aboutText = JsonConvert.DeserializeObject<List<GameAboutDetail>>(File.ReadAllText(Directory.GetCurrentDirectory() + about_file));
            var patreonText = JsonConvert.DeserializeObject<List<GameAboutDetail>>(File.ReadAllText(Directory.GetCurrentDirectory() + patreon_file));

            emptySpace = setupCfg.emptySpace;

            int mode = 2;

            while (true)
            {
                switch (mode)
                {
                    case 1:
                        break;

                    case 2:

                        int indexSelected = 0, totSeconds = 0, totDeaths = 0;
                        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Games", "*.game.jsonx");

                        #region - setup summary - 

                        var hshGameInfo = new Hashtable();

                        for (int i = 0; i < files.Length; i++)
                        {
                            int seconds = 0, deaths = 0;
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
                            if (seconds > 0)
                            {
                                totSeconds += seconds;
                                totDeaths += deaths;
                                hshGameInfo[g_name] =
                                    " -- awards|" + awards +
                                    "| -- time: |" + FormatTimeSpan(TimeSpan.FromSeconds(seconds)) +
                                    "| -- deaths: |" + deaths;
                            }
                        }

                        #endregion

                        bool bEnterPressed = false;

                        List<string> mainMenu = new() {
                            "     Games    ",
                            "    Screen    ",
                            "     About    ",
                            "    Patreon   ",
                            "     Exit     ",
                        };

                        while (true)
                        {
                            while (true)
                            {
                                DisplayLogo();
                                Console.WriteLine();

                                for (int i = 0; i < mainMenu.Count; i++)
                                {
                                    Write("¨", ConsoleColor.Black);
                                    Write(" ".PadLeft(48, ' '), ConsoleColor.Black);

                                    if (i == indexSelected)
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Write(mainMenu[i], ConsoleColor.White);
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Write(mainMenu[i], ConsoleColor.DarkGray);
                                    }

                                    Write("\n", ConsoleColor.Black);
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }

                                Thread.Sleep(200);

                                while (true)
                                {
                                    if (Console.KeyAvailable)
                                    {
                                        ConsoleKeyInfo key = Console.ReadKey(true);
                                        if (key.Key == ConsoleKey.UpArrow && indexSelected > 0)
                                        {
                                            indexSelected--;
                                            break;
                                        }
                                        if (key.Key == ConsoleKey.DownArrow && indexSelected < mainMenu.Count - 1)
                                        {
                                            indexSelected++;
                                            break;
                                        }
                                        if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                                        {
                                            bEnterPressed = true;
                                            break;
                                        }
                                    }
                                }

                                if (bEnterPressed)
                                {
                                    break;
                                }
                            }

                            bEnterPressed = false;

                            if (indexSelected == 0)
                            {
                                #region - games - 

                                bool bShowDetails = false,
                                        escapePressed = false;

                                while (true)
                                {
                                    DisplayLogo();
                                    Write("¨                         [Use the ", ConsoleColor.DarkGray);
                                    Write("arrows", ConsoleColor.Green);
                                    Write(" to select, ", ConsoleColor.DarkGray);
                                    Write("Space", ConsoleColor.White);
                                    Write(" for details, ", ConsoleColor.DarkGray);
                                    Write("Enter", ConsoleColor.Green);
                                    Write(" to start]\n", ConsoleColor.DarkGray);
                                    Console.WriteLine();

                                    Write("¨ ░█▀█░█▀▄░█░█░█▀▀░█▀█░▀█▀░█░█░█▀█░█▀▀░█▀▀\n", ConsoleColor.DarkGray);
                                    Write("¨ ░█▀█░█░█░▀▄▀░█▀▀░█░█░░█░░█░█░█▀▄░█▀▀░▀▀█\n", ConsoleColor.DarkGray);
                                    Write("¨ ░▀░▀░▀▀░░░▀░░▀▀▀░▀░▀░░▀░░▀▀▀░▀░▀░▀▀▀░▀▀▀\n", ConsoleColor.DarkGray);

                                    Console.WriteLine();
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        string fileName = Path.GetFileNameWithoutExtension(files[i]);
                                        var g_name = fileName.Replace("_", " ").Replace(".game", "");

                                        if (i == indexSelected)
                                        {
                                            Write("¨", ConsoleColor.Black);
                                            Console.BackgroundColor = ConsoleColor.DarkRed;
                                            Write("  ", ConsoleColor.Black);
                                            Write(g_name.PadRight(30, ' '), ConsoleColor.White);
                                            Write("   ", ConsoleColor.Black);
                                            Console.BackgroundColor = ConsoleColor.Black;
                                        }
                                        else
                                        {
                                            Write("¨  " + g_name.PadRight(33, ' '), ConsoleColor.DarkGray);
                                        }

                                        var hsh_res = (hshGameInfo[g_name] as string)?.Split('|');

                                        if (hsh_res != null)
                                        {
                                            if (hsh_res.Length > 0)
                                            {
                                                Write(" " + hsh_res[0], ConsoleColor.Gray);
                                                Write(" " + hsh_res[1], ConsoleColor.Yellow);
                                                Write(" " + hsh_res[2], ConsoleColor.Gray);
                                                Write(" " + hsh_res[3], ConsoleColor.Green);
                                                Write(" " + hsh_res[4], ConsoleColor.Gray);
                                                Write(" " + hsh_res[5] + "  ", ConsoleColor.Red);

                                                if (i == indexSelected)
                                                {
                                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                                    Write("        ", ConsoleColor.Black);
                                                    Console.BackgroundColor = ConsoleColor.Black;
                                                }
                                            }
                                        }
                                        else
                                            Write("  -- not played ", ConsoleColor.DarkGray);

                                        Write("\n", ConsoleColor.Green);

                                        var pTip2 = summary.FirstOrDefault(y => y.game_name == g_name);

                                        if (i == indexSelected && bShowDetails)
                                        {
                                            if (pTip2 != null)
                                            {
                                                Write("¨\n", ConsoleColor.DarkGray);
                                                bFastMode = false;
                                                foreach (var t in pTip2.game_tip)
                                                    PrintRoomText(t, ConsoleColor.Yellow, 5);

                                                Write("¨\n", ConsoleColor.DarkGray);
                                            }
                                        }
                                    }

                                    List<string> msg = new List<string>
                                {
                                    "Total time wasted on eighties nostalgia: ",
                                    "Total time doing actual work for non-real entities: ",
                                    "Total time wasted on fantasy chores: ",
                                };

                                    Console.WriteLine();
                                    Console.WriteLine();

                                    Write("¨  " + msg[GetRandomNumber(0, msg.Count - 1)], ConsoleColor.Yellow);
                                    Write(" " + FormatTimeSpan(TimeSpan.FromSeconds(totSeconds)), ConsoleColor.Green);
                                    Write(" -- Total deaths: ", ConsoleColor.DarkGray);
                                    Write(totDeaths + "\n", ConsoleColor.Red);

                                    while (true)
                                    {
                                        if (Console.KeyAvailable)
                                        {
                                            ConsoleKeyInfo key = Console.ReadKey(true);
                                            if (key.Key == ConsoleKey.UpArrow && indexSelected > 0)
                                            {
                                                indexSelected--;
                                                Thread.Sleep(100);
                                                bShowDetails = false;
                                                break;
                                            }
                                            if (key.Key == ConsoleKey.DownArrow && indexSelected < files.Length - 1)
                                            {
                                                indexSelected++;
                                                Thread.Sleep(100);
                                                bShowDetails = false;
                                                break;
                                            }
                                            if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.LeftArrow)
                                            {
                                                bShowDetails = !bShowDetails;
                                                Thread.Sleep(100);
                                                break;
                                            }
                                            if (key.Key == ConsoleKey.Enter)
                                            {
                                                bEnterPressed = true;
                                                Thread.Sleep(100);
                                                break;
                                            }
                                            if (key.Key == ConsoleKey.End)
                                            {
                                                bFastMode = true;
                                                bAutomation = true;
                                                bEnterPressed = true;
                                                Thread.Sleep(100);
                                                break;
                                            }
                                            if (key.Key == ConsoleKey.Escape)
                                            {
                                                escapePressed = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (bEnterPressed || escapePressed)
                                        break;
                                }

                                if (escapePressed)
                                    break;

                                currentFile = files[indexSelected];
                                Console.WriteLine();
                                game.gameJsonFile = currentFile;
                                LoadGame();

                                if (bAutomation)
                                    automationFile = File.ReadAllText(currentFile.Replace(".game.jsonx", ".QA.txt")).Split("\r\n").ToList();

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

                                #endregion
                            }
                            else if (indexSelected == 1)
                            {
                                #region - setup - 

                                while (true)
                                {
                                    DisplayLogo();
                                    Console.WriteLine();
                                    Write("¨ ░█▀▀░█▀▀░▀█▀░█░█░█▀█\n", ConsoleColor.DarkGray);
                                    Write("¨ ░▀▀█░█▀▀░░█░░█░█░█▀▀\n", ConsoleColor.DarkGray);
                                    Write("¨ ░▀▀▀░▀▀▀░░▀░░▀▀▀░▀░░\n", ConsoleColor.DarkGray);
                                    Console.WriteLine();
                                    Write("¨ You can configure the ", ConsoleColor.DarkGray);
                                    Write("background image", ConsoleColor.Yellow);
                                    Write(" by your windwos terminal settings.\n", ConsoleColor.DarkGray);
                                    Write("¨ In this release, we provide some ", ConsoleColor.DarkGray);
                                    Write("wallpapers", ConsoleColor.Yellow);
                                    Write(" for you to use in TA.\n", ConsoleColor.DarkGray);
                                    Console.WriteLine();
                                    Write("¨ Default values for the ", ConsoleColor.DarkGray);
                                    Write("font centering", ConsoleColor.DarkGray);
                                    Write(" are used for the standard console font, 'Cascadia Mono', at 12pt.\n", ConsoleColor.DarkGray);
                                    Console.WriteLine();
                                    Write("¨ Adjust the number: ", ConsoleColor.DarkGray);
                                    Write(setupCfg.emptySpace.ToString(), ConsoleColor.Green);
                                    Write(" to suit your personal preference, or press 'Enter' to finish.", ConsoleColor.DarkGray);
                                    Console.WriteLine();
                                    Console.CursorVisible = true;
                                    Write("¨ > ", ConsoleColor.Green);
                                    var newSPace = ConsoleReadLine().Trim();
                                    if (newSPace == "")
                                    {
                                        Console.CursorVisible = false;
                                        break;
                                    }
                                    try
                                    {
                                        emptySpace = Convert.ToInt32(newSPace);
                                        setupCfg.emptySpace = emptySpace;
                                        File.Delete(Directory.GetCurrentDirectory() + setup_file);
                                        File.WriteAllText(Directory.GetCurrentDirectory() + setup_file, JsonConvert.SerializeObject(setupCfg));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine();
                                        Write("¨ You are a funny human being!\n", ConsoleColor.Red);
                                        Console.WriteLine();
                                        EnterToContinue();
                                    }
                                }

                                #endregion
                            }
                            else if (indexSelected == 2)
                            {
                                #region - about - 

                                while (true)
                                {
                                    DisplayLogo();
                                    Console.WriteLine();
                                    Write("¨ ░█▀█░█▀█░█▀█░█░█░▀█▀\n", ConsoleColor.DarkGray);
                                    Write("¨ ░█▀█░█▀█░█░█░█░█░░█░\n", ConsoleColor.DarkGray);
                                    Write("¨ ░▀░▀░▀▀░░▀▀▀░▀▀▀░░▀░\n", ConsoleColor.DarkGray);
                                    Console.WriteLine();
                                    int enters = 0, _page = 1;
                                    foreach (var itemDet in aboutText.FirstOrDefault(y => y.lang == "ENG").info)
                                    {
                                        Console.CursorVisible = false;
                                        Console.WriteLine();
                                        foreach (var line in itemDet.text)
                                            PrintRoomText(line, ConsoleColor.Yellow, 5);
                                        Thread.Sleep(500);
                                        Console.CursorVisible = true;
                                        EnterToContinue();
                                        Console.CursorVisible = false;
                                        Console.WriteLine();
                                        bFastMode = false;

                                        if (++enters == 2)
                                        {
                                            enters = 0;
                                            _page++;
                                            DisplayLogo();
                                            Console.WriteLine();
                                            Write("¨ ░█▀█░█▀█░█▀█░█░█░▀█▀\n", ConsoleColor.DarkGray);
                                            Write("¨ ░█▀█░█▀█░█░█░█░█░░█░\n", ConsoleColor.DarkGray);
                                            Write("¨ ░▀░▀░▀▀░░▀▀▀░▀▀▀░░▀░\n", ConsoleColor.DarkGray);
                                            Console.WriteLine();
                                        }
                                    }
                                    break;
                                }

                                #endregion
                            }
                            else if (indexSelected == 3)
                            {
                                #region - patreon - 

                                while (true)
                                {
                                    DisplayLogo();
                                    Console.WriteLine();
                                    Write("¨ ░█▀█░█▀█░▀█▀░█▀█░█▀▀░█▀█░█▀█\n", ConsoleColor.DarkGray);
                                    Write("¨ ░█▀▀░█▀█░░█░░█▀▄░█▀▀░█░█░█░█\n", ConsoleColor.DarkGray);
                                    Write("¨ ░▀░░░▀░▀░░▀░░▀░▀░▀▀▀░▀▀▀░▀░▀░\n", ConsoleColor.DarkGray);

                                    Console.WriteLine();
                                    int enters = 0, _page = 1;
                                    foreach (var itemDet in patreonText.FirstOrDefault(y => y.lang == "ENG").info)
                                    {
                                        Console.CursorVisible = false;
                                        Console.WriteLine();
                                        foreach (var line in itemDet.text)
                                            PrintRoomText(line, ConsoleColor.Yellow, 5);
                                        Console.WriteLine();
                                        Thread.Sleep(500);
                                        Console.CursorVisible = true;
                                        EnterToContinue();
                                        Console.CursorVisible = false;
                                        Console.WriteLine();
                                        bFastMode = false;

                                        if (++enters == 2)
                                        {
                                            enters = 0;
                                            _page++;
                                            DisplayLogo();
                                            Console.WriteLine();
                                            Write("¨ ░█▀█░█▀█░▀█▀░█▀█░█▀▀░█▀█░█▀█\n", ConsoleColor.DarkGray);
                                            Write("¨ ░█▀▀░█▀█░░█░░█▀▄░█▀▀░█░█░█░█\n", ConsoleColor.DarkGray);
                                            Write("¨ ░▀░░░▀░▀░░▀░░▀░▀░▀▀▀░▀▀▀░▀░▀░\n", ConsoleColor.DarkGray);
                                            Console.WriteLine();
                                        }
                                    }

                                    var url = "https://www.patreon.com/bigboysbooks";

                                    Process.Start(new ProcessStartInfo(url)
                                    {
                                        UseShellExecute = true
                                    });

                                    break;
                                }

                                #endregion
                            }
                            else if (indexSelected == 4)
                            {
                                // exit
                                return;
                            }
                        }

                        break;


                    case 3:
                        Console.CursorVisible = false;

                        DisplayLogo();
                        Console.WriteLine();
                        Write("¨ " + game.gameName + "\n", ConsoleColor.Blue);
                        Console.WriteLine();

                        var pTip = summary.FirstOrDefault(y => y.game_name == game.gameName);

                        if (pTip != null)
                        {
                            foreach (var t in pTip.game_tip)
                                Write("¨    " + t + "\n", ConsoleColor.Yellow);
                            Write("¨\n", ConsoleColor.DarkGray);
                        }

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
                        Console.CursorVisible = false;
                        bHardcore = false;
                        bFastMode = false;

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
                            Console.WriteLine();
                            Write("¨ --- [HARDCORE MODE UNLOCKED!] ---\n", ConsoleColor.White);
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
                        bFastMode = false;
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
