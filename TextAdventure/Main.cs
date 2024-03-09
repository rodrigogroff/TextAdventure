using System.Runtime.InteropServices;
using static Win32;

public class Win32
{
    public 
        const 
            int 
                VK_F11 = 0x7A,
                SW_MAXIMIZE = 3;

    public 
        const 
            uint
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
        var hwnd = GetConsoleWindow();
        PostMessage(hwnd, WM_KEYDOWN, (IntPtr)VK_F11, IntPtr.Zero);
        PostMessage(hwnd, WM_MOUSEWHEEL, (IntPtr)(MK_CONTROL | WHEEL_DELTA), IntPtr.Zero);

        new TextAdventureGame().StartGame();
    }
}
