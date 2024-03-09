using System.Diagnostics;
using System.Runtime.InteropServices;
using static Win32;

public class Win32
{
    public const int 
                VK_F11 = 0x7A,
                SW_MAXIMIZE = 3;

    public const uint
                WM_KEYDOWN = 0x100,
                WM_MOUSEWHEEL = 0x20A,
                WHEEL_DELTA = 120,
                MK_CONTROL = 0x00008 << 16;

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
}

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            #if RELEASE
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "TA_AppUpdater.exe",
                Arguments = "",
                UseShellExecute = false,
                CreateNoWindow = false
            };

            Process.Start(startInfo);
            return;
            #endif
        }

        var hwnd = GetConsoleWindow();
        PostMessage(hwnd, WM_KEYDOWN, (IntPtr)VK_F11, IntPtr.Zero);
        PostMessage(hwnd, WM_MOUSEWHEEL, (IntPtr)(MK_CONTROL | WHEEL_DELTA), IntPtr.Zero);

        var ta = new TextAdventureGame();

        Console.CursorVisible = false;
        Console.Clear();
        Thread.Sleep(500);
        Console.WriteLine();
        ta.Write(" DOS/4GW Professional Protected Mode Run-Time Versiom 2.1c\n", ConsoleColor.White);
        ta.Write(" Copyright (C) United TA Systems, Inc. 1976\n", ConsoleColor.DarkGray);
        ta.Write(" Engine Version: 1.4.3", ConsoleColor.DarkGray);         
        Console.WriteLine();
        Console.WriteLine();
        Thread.Sleep(2000);

        ta.StartGame();
    }
}
