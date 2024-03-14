using System;
using System.Runtime.InteropServices;

class Program
{
    // Import the necessary Windows API functions
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteConsoleOutput(
        IntPtr hConsoleOutput,
        CharInfo[] lpBuffer,
        Coord dwBufferSize,
        Coord dwBufferCoord,
        ref SmallRect lpWriteRegion
    );

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        [FieldOffset(0)] public char UnicodeChar;
        [FieldOffset(0)] public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)] public CharUnion Char;
        [FieldOffset(2)] public short Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    static void Main()
    {
        Console.Clear(); // Clear the console screen

        AppendTextToConsole("Hello, World!\nThis is a new line.", ConsoleColor.Green); // Example of appending text

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void AppendTextToConsole(string text, ConsoleColor color)
    {
        IntPtr hConsoleOutput = GetStdHandle(-11); // STD_OUTPUT_HANDLE
        if (hConsoleOutput == IntPtr.Zero || hConsoleOutput == new IntPtr(-1))
        {
            Console.WriteLine("Failed to get console handle.");
            return;
        }

        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        Coord bufferSize = new Coord { X = (short)width, Y = (short)height };
        Coord bufferCoord = new Coord { X = 0, Y = 0 };
        SmallRect writeRegion = new SmallRect { Left = 0, Top = 0, Right = (short)(width - 1), Bottom = (short)(height - 1) };

        CharInfo[] buffer = new CharInfo[width * height];
        int bufferIndex = 0;

        foreach (char c in text)
        {
            if (c == '\n')
            {
                bufferCoord.X = 0;
                bufferCoord.Y++;
                continue;
            }

            buffer[bufferIndex].Char.UnicodeChar = c;
            buffer[bufferIndex].Attributes = GetConsoleColorAttribute(color);
            bufferIndex++;

            bufferCoord.X++;
            if (bufferCoord.X >= width)
            {
                bufferCoord.X = 0;
                bufferCoord.Y++;
            }
        }

        // Write to the console buffer
        bool success = WriteConsoleOutput(hConsoleOutput, buffer, bufferSize, new Coord { X = 0, Y = 0 }, ref writeRegion);
        if (!success)
        {
            Console.WriteLine("Failed to write to console.");
        }
    }

    static short GetConsoleColorAttribute(ConsoleColor color)
    {
        switch (color)
        {
            case ConsoleColor.Black:
                return 0;
            case ConsoleColor.DarkBlue:
                return 1;
            case ConsoleColor.DarkGreen:
                return 2;
            case ConsoleColor.DarkCyan:
                return 3;
            case ConsoleColor.DarkRed:
                return 4;
            case ConsoleColor.DarkMagenta:
                return 5;
            case ConsoleColor.DarkYellow:
                return 6;
            case ConsoleColor.Gray:
                return 7;
            case ConsoleColor.DarkGray:
                return 8;
            case ConsoleColor.Blue:
                return 9;
            case ConsoleColor.Green:
                return 10;
            case ConsoleColor.Cyan:
                return 11;
            case ConsoleColor.Red:
                return 12;
            case ConsoleColor.Magenta:
                return 13;
            case ConsoleColor.Yellow:
                return 14;
            case ConsoleColor.White:
                return 15;
            default:
                return 7; // Default to white
        }
    }
}
