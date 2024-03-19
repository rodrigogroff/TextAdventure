using System.Collections;

public partial class TextAdventureGame
{
    void DisplayHelpBox()
    {
        if (bHardcore)
        {
            Console.WriteLine();
            Write("¨  >> Hardcore is meant to play like living in a 80's non-intuitive parser: welcome to hell ;) <<", ConsoleColor.Red);
            Console.WriteLine();
            return;
        }

        Console.WriteLine();
        Console.CursorVisible = false;
        List<string> lines = new List<string>();
        List<ConsoleColor> colors = new List<ConsoleColor>();
        int w1 = 15, w2 = 35;

        colors.Add(ConsoleColor.Yellow);
        lines.Add("¨ --- Game Commands ----                            --- Interface commands ----\n\n");

        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /stat".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" show current attributes".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);
        lines.Add(" /hint".PadRight(w1, ' '));
        colors.Add(ConsoleColor.Blue);
        lines.Add(" discover how to play using hints\n");

        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /bag".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" display player inventory".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);
        lines.Add(" ".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" \n");
        
        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /look".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" investigate npc / scene".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);
        lines.Add("    ".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" \n");

        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /give".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" interact with npc".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);        
        lines.Add(" /skip".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" Bypass a long sequence of events\n");
        
        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /take".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" interact with npc".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);
        lines.Add(" /save".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" record your game to play later\n");

        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /use".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" consume inventory item".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);
        lines.Add(" /die".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" player suicide (in the game)\n");
        
        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /map".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" show the current map".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);
        lines.Add(" /cls".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" clear your screen\n");

        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /quest".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" game quests list".PadRight(w2, ' '));
        colors.Add(ConsoleColor.Green);
        lines.Add(" /quit".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" back to game selection\n");

        colors.Add(ConsoleColor.Green);
        lines.Add("¨ /award".PadRight(w1, ' '));
        colors.Add(ConsoleColor.DarkGray);
        lines.Add(" game goals currently achieved".PadRight(w2, ' '));
                                
        Print(lines, colors, 0);
        Console.WriteLine();
    }
}