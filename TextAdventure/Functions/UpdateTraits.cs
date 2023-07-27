﻿
using System.Collections;

public partial class TextAdventureGame
{
    void UpdateTraits(GameTrait item)
    {
        if (!game.player.traits.Any(y => y.name == item.name))
        {
            game.player.traits.Add(item);
            string msg = "(+) Acquired Trait: " + item.name + " > " + item.description;
            Write(" (+) Acquired Trait: ", ConsoleColor.Blue);
            Write(item.name + "\n", ConsoleColor.White);
            Write("  [  " + item.description + " ]\n", ConsoleColor.Red);
            game.logs.Add(msg);
        }
    }
}
