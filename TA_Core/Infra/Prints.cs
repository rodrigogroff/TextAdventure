﻿
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
            Write("¨", ConsoleColor.White);
            if (!bAutomation)
                Thread.Sleep(50);
            if (item.Length > max_len)
                max_len = item.Length;
            Print(" " + item + "\n", ConsoleColor.DarkRed);
        }
        Console.WriteLine();
        Write("¨ [", ConsoleColor.Gray);
        Write(game.gameName, ConsoleColor.Yellow);
        Write("] - ", ConsoleColor.Gray);
        Write(game.gameVersion + "\n", ConsoleColor.DarkGray);
        Console.WriteLine();
    }

    public void Write(string text, ConsoleColor color)
    {
        if (game.gameName == null)
        {
            if (text.StartsWith("¨"))
            {
                Console.Write(" ".PadRight(emptySpace));
                text = text.Substring(1);
            }
        }
        else
        {
            if (game.player == null)
            {
                if (text.StartsWith("¨"))
                {
                    Console.Write(" ".PadRight(emptySpace));
                    text = text.Substring(1);
                }
            }
            else if (text.StartsWith("¨"))
            {
                if (game.textAlign == "center")
                {
                    Console.Write(" ".PadRight(emptySpace));
                    text = text.Substring(1);
                }
                else if (game.textAlign == "left")
                {
                    if (screenWidth == 2560)
                        Console.Write(" ".PadRight(16, ' '));
                    else
                        Console.Write(" ".PadRight(4, ' '));

                    text = text.Substring(1);
                }
                else if (game.textAlign == "right")
                {
                    if (screenWidth == 2560)
                        Console.Write(" ".PadRight(170, ' '));
                    else
                        Console.Write(" ".PadRight(104, ' '));

                    text = text.Substring(1);
                }
            }
        }

        Console.ForegroundColor = color;
        Console.Write(text);
    }

    public void Print(List<string> lines, List<ConsoleColor> colors, int ms = 50)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            var text = lines[i];
            var color = colors[i];
            Write(text, color);
            if (!bAutomation)
                Thread.Sleep(ms);
        }
    }

    void Print(string text, ConsoleColor color = ConsoleColor.White)
    {
        Write(PreProcessText(text), color);
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
                    if (!bAutomation)
                        Thread.Sleep(20);

            Console.Write(c);

            if (!bFastMode)
                if (";,-.!?—'\"".Contains(c))
                {
                    if (!bAutomation)
                        Thread.Sleep(300);
                }
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

        Write("¨", color);

        char last = '!';
        
        foreach (char c in text)
        {
            var _timer = ms;

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    bFastMode = true;
                }
            }

            if (!bFastMode)
            {
                if (c == ' ' && last != ' ')
                {
                    if (_timer > 0)
                        if (!bAutomation)
                        {
                            Thread.Sleep(120);
                            last = ' ';
                        }
                }
                else
                {
                    last = '!';
                }
            }

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
                    if (!bAutomation)
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
