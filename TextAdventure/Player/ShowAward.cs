﻿using System.Collections;

public partial class TextAdventureGame
{
    void ShowAward()
    {
        Console.WriteLine();
        int x = 1;
        foreach (var item in game.awards)
        {
            var i = game.player.awards.FirstOrDefault(y => y.id == item.id);
            Write(" - " + (x++).ToString().PadLeft(3, '0') + " - ", ConsoleColor.DarkGray);
            if (i != null)
                Write("[OK] " + item.text + "\n", ConsoleColor.Yellow);
            else
                Write("[  ] " + item.text + "\n", ConsoleColor.Green);
            Thread.Sleep(100);
        }
        Console.WriteLine();
    }
}