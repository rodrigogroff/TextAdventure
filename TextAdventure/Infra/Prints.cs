using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;

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

    void PrintTitle(string text, ConsoleColor color = ConsoleColor.DarkRed, ConsoleColor colorShadow = ConsoleColor.DarkRed, int timeout = 50)
    {
        foreach (var item in text)
        {
            if (item != '▒' && item != '░' && item != '▓')
            {
                Console.ForegroundColor = color;
                Console.Write(item);
            }
            else
            {
                Console.ForegroundColor = colorShadow;
                Console.Write(item);
            }
        }
        Console.Write('\n');
        Thread.Sleep(timeout);
    }

    public void Print(List<string> lines, List<ConsoleColor> colors, int ms = 50)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            var text = lines[i];
            var color = colors[i];
            Console.ForegroundColor = color;
            foreach (var c in text)
            {
                
                Console.Write(c);
            }
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

/*
 * 
 
{
        int ST = 1;
        int DX = 2;
        string formula = "ST + 10 * DX / 2";

        // Replace placeholders with actual values in the formula string
        string formulaWithValue = formula.Replace("ST", ST.ToString()).Replace("DX", DX.ToString());

        // Create a temporary DataTable to evaluate the expression
        DataTable dt = new DataTable();

        try
        {
            // Evaluate the expression
            var result = dt.Compute(formulaWithValue, "");

            // The result will be of type object, so you may need to cast it to the appropriate type
            double finalResult = Convert.ToDouble(result);

            Console.WriteLine("Result: " + finalResult);
        }
        catch (Exception ex)
        {
            // Handle any evaluation errors
            Console.WriteLine("Error: " + ex.Message);
        }
    }
 
 * */
