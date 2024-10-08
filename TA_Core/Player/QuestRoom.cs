﻿using System.Collections;

public partial class TextAdventureGame
{
    void QuestRoom()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        DisplayCurrentSceneTitle();

        Console.WriteLine();
        Console.WriteLine();

        Write("¨ ▒▓██ Quests\n", ConsoleColor.DarkGray);

        Console.WriteLine();

        if (game.player.quests.Count == 0)
        {
            Console.WriteLine();
            Write("¨ No quests available!", ConsoleColor.DarkYellow);
            Console.WriteLine();
            return;
        }

        int index = 1;
        var hsh = new Hashtable();
        Console.WriteLine();
        foreach (var qu in game.player.quests.OrderByDescending(y => y.dt_start))
        {
            hsh[index] = qu;
            Write("¨ [" + index++ + "] - ", ConsoleColor.White);
            Write(qu.active == true ? "[Active] " : "[Done] ", ConsoleColor.Blue);
            Write(qu.title + "\n", ConsoleColor.Yellow);
        }

        Console.WriteLine();
        Write("¨ [Select quest to view:]", ConsoleColor.DarkGray);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" >> ");
        while (Console.KeyAvailable) Console.ReadKey(intercept: true);
        string option = ConsoleReadLine().Trim();
        if (option == "")
            return;
        var q = hsh[Convert.ToInt32(option)] as Quest;
        Console.WriteLine();
        Write("¨ -- Quest --\n", ConsoleColor.Green);
        Write("¨ " + q.title + "\n", ConsoleColor.Blue);
        Write("¨ " + q.subtitle + "\n", ConsoleColor.DarkYellow);
        Console.WriteLine();
        foreach (var line in q.description)
        {
            if (!bAutomation)
                Thread.Sleep(120);
            PrintRoomText(line, ConsoleColor.Yellow, 30);
        }
        Console.WriteLine();
        Write("¨ -- Requirements: -- \n\n", ConsoleColor.Green);
        foreach (var gui in q.requirements)
        {
            var ms = game.player.inventory.Any(y => y.name == gui.name && y.quantity >= gui.quantity) ? " [OK] \n" : " [Not yet] \n";

            Write("¨  " + gui.name + ": ", ConsoleColor.DarkGray);
            Write(gui.quantity.ToString(), ConsoleColor.Yellow);

            if (ms.Contains("OK"))
                Write(" " + ms, ConsoleColor.Blue);
            else
                Write(" " + ms, ConsoleColor.Red);

            Write("\n", ConsoleColor.Red);
        }
        Console.WriteLine();
        Console.WriteLine();
    }
}