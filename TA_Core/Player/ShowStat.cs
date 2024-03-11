﻿
public partial class TextAdventureGame
{
    void ShowStat()
    {
        Console.WriteLine();
        Write("¨ Name:  ", ConsoleColor.DarkGray);
        Write(game.player.name + "\n", ConsoleColor.Yellow);
        Write("¨ Title: ", ConsoleColor.DarkGray);
        Write(game.player.title + "\n", ConsoleColor.Yellow);
        Write("¨ --- Attributes ----\n", ConsoleColor.Green);

        int text_size = 16;

        foreach (var item in game.player.attributes)
        {
            if (!item.name.EndsWith("MAX"))
            {
                var max_st = game.player.attributes.FirstOrDefault(y => y.name == item.name + "MAX");
                if (max_st != null)
                {
                    Write(("¨ " + item.name).PadRight(text_size, ' '), ConsoleColor.DarkGray);
                    Write(item.quantity.ToString(), ConsoleColor.Yellow);
                    Write(" / ", ConsoleColor.DarkGray);
                    Write(max_st.quantity.ToString() + "\n", ConsoleColor.Yellow);
                }
                else
                {
                    Write(("¨ " + item.name).PadRight(text_size, ' '), ConsoleColor.DarkGray);
                    Write(item.quantity.ToString() + "\n", ConsoleColor.Yellow);
                }
            }
        }
        if (game.player.attributes.Count == 0)
            Write("¨ Empty!", ConsoleColor.DarkGray);
        Write("¨--- Traits ----", ConsoleColor.Green);
        foreach (var item in game.player.traits)
        {
            Write("¨ " + item.name.PadRight(text_size - 1, ' '), ConsoleColor.DarkGray);
            Write(item.description + "\n", ConsoleColor.Red);
        }

        if (game.player.traits.Count == 0)
            Write("¨ Empty!", ConsoleColor.DarkGray);

        Console.WriteLine();
    }
}
