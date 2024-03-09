using System.Collections;

public partial class TextAdventureGame
{
    void ShowLog()
    {
        Console.WriteLine();
        foreach (var item in game.logs)
            Print(" -- " + item, ConsoleColor.DarkGray);
        if (game.logs.Count == 0)
            Print(" Empty!", ConsoleColor.DarkGray);
    }
}