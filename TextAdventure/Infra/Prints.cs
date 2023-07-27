﻿
public partial class TextAdventureGame
{
    void PrintGameBig()
    {
        var max_len = 0;
        foreach (var item in game.gameBigTitle)
        {
            Thread.Sleep(50);
            if (item.Length > max_len)
                max_len = item.Length;
            Print(item, ConsoleColor.DarkRed);
        }
        Print(("v" + game.gameVersion).PadLeft(max_len, ' '), ConsoleColor.White);
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
            {
                if (";,-.!?—'\"".Contains(c))
                {
                    Thread.Sleep(300);
                }
                else
                {
                    Thread.Sleep(ms);
                }
            }
        }
        Console.WriteLine();
    }

    public bool quote = false;
    public bool red_quote = false;
    public bool blue_quote = false;
    public ConsoleColor lastColor = ConsoleColor.Yellow;

    public string itemOriginating = "";
    public string nextLocation = "";

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
            if (!bFastMode)
                if (c == ' ')
                    Thread.Sleep(100);

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
                Thread.Sleep(5);
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
