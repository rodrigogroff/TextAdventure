
public partial class TextAdventureGame
{
    public 
        bool 
            quote = false,
            red_quote = false,
            blue_quote = false;

    public ConsoleColor lastColor = ConsoleColor.Yellow;

    public string nextLocation = "";

    void PrintGameBig()
    {
        var max_len = 0;
        foreach (var item in game.gameBigTitle)
        {
            Thread.Sleep(50);
            if (item.Length > max_len)
                max_len = item.Length;
            Print(" " + item, ConsoleColor.DarkRed);
        }
        Console.WriteLine();
        Write(" [", ConsoleColor.Gray);
        Write(game.gameName, ConsoleColor.Yellow);
        Write("] - ", ConsoleColor.Gray);
        Write(game.gameVersion + "\n", ConsoleColor.DarkGray);
        Console.WriteLine();
    }

    void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }

    public void Print(List<string> lines, List<ConsoleColor> colors, int ms = 50)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            var text = lines[i];
            var color = colors[i];
            Console.ForegroundColor = color;
            Console.Write(text);
            Thread.Sleep(ms);
        }
    }

    void Print(string text, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(" " + PreProcessText(text));
    }

    void Print(string text, ConsoleColor color, int ms)
    {
        Console.ForegroundColor = color;
        Console.Write(' ');
        text = PreProcessText(text);
        foreach (char c in text)
        {
            if (!bFastMode)
                if (c == ' ')
                    Thread.Sleep(20);

            Console.Write(c);

            if (!bFastMode)
                if (";,-.!?—'\"".Contains(c))
                    Thread.Sleep(300);
                else
                    Thread.Sleep(ms);
        }

        Console.WriteLine();
    }

    void PrintRoomText(string text, ConsoleColor color, int ms)
    {        
        Console.Write(' ');
        text = PreProcessText(text);

        if (quote)
            Console.ForegroundColor = ConsoleColor.White;
        else
            Console.ForegroundColor = color;
        
        foreach (char c in text)
        {
            var _timer = 35;

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    bFastMode = true;
                }
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Spacebar)
                    _timer = 0;                    
            }

            if (!bFastMode)
                if (c == ' ')
                    if (_timer > 0)
                        Thread.Sleep(120);

            if (c == '\"')
            {
                if (quote)
                {
                    Console.Write(c);
                    Console.ForegroundColor = color;
                    lastColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    lastColor = ConsoleColor.White;
                    Console.Write(c);                    
                }
                quote = !quote;
            }
            else if (c == '~')
            {
                if (red_quote)
                    Console.ForegroundColor = lastColor;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                red_quote = !red_quote;
            }
            else if (c == '^')
            {
                if (blue_quote)
                    Console.ForegroundColor = lastColor;
                else
                    Console.ForegroundColor = ConsoleColor.Blue;
                blue_quote = !blue_quote;
            }
            else
                Console.Write(c);
            
            if (!bFastMode)
                if (_timer > 0)
                    Thread.Sleep(_timer);
        }

        Console.WriteLine();
    }

    string PreProcessText(string text)
    {
        if (text.Contains("{name}"))
            text = text.Replace("{name}", game.player.name);

        if (text.Contains("{title}"))
            text = text.Replace("{title}", game.player.title);

        return text;
    }
}
