using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using TextAdventure.Infra;

public partial class TextAdventureGame
{
    Game game = new Game();
    Context current_game_Room = new Context();
    Random random = new Random();

    FileEncryptorDecryptor crypt = new FileEncryptorDecryptor();

    string currentFile = "";
    string currentItem = "";

    bool bAutomation = false;
    bool bFastMode = false;
    bool bAbortOp = false;
    bool bUnlimitedHints = false;
    bool bHintsDisabled = false;
    bool bAutomap = false;
    bool bHardcore = false;

    string gameDifficulty = "";

    GameMonitoring monitor;
    GameMonitor gameMonitor;
    GameMonitorPlays gamePlay;

    public string monitor_file = "monitor.txt";

    Hashtable hshNumbers = new Hashtable();

    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    public const int DESKTOPHORZRES = 118;
    public const int DESKTOPVERTRES = 117;

    public int screenWidth = 0, emptySpace = 0;

    public void InitScreen()
    {
        IntPtr hdc = GetDC(IntPtr.Zero);
        screenWidth = GetDeviceCaps(hdc, DESKTOPHORZRES);
        emptySpace = ((screenWidth / 8 - 120) / 2);
        ReleaseDC(IntPtr.Zero, hdc);
    }

    int GetRandomNumber(int start, int finish)
    {
        var tag = game.currentRoom + "_" + start + "_" + finish;

        if (hshNumbers[tag] == null)
            hshNumbers[tag] = new List<int>();

        var c_numbers_list = hshNumbers[tag] as List<int>;

        if (c_numbers_list.Count == finish)
            c_numbers_list.Clear();

        int resp = 0;

        while (true)
        {
            resp = random.Next(start, finish + 1);

            if (!c_numbers_list.Contains(resp))
            {
                c_numbers_list.Add(resp);
                break;
            }
        }

        return resp;
    }

    public void EnterToContinue()
    {
        Write("¨ [Enter to continue]", ConsoleColor.DarkGray);
        Write(" [> ", ConsoleColor.Green);
        Console.ForegroundColor = ConsoleColor.White;
        Console.CursorVisible = true;
        while (Console.KeyAvailable) Console.ReadKey(intercept: true);
        ConsoleReadLine();
    }

    string FormatTimeSpan(TimeSpan timeSpan)
    {
        string formattedTime = "";

        if (timeSpan.TotalSeconds < 60)
        {
            formattedTime += $"{timeSpan.Seconds}s ";
        }
        else
        {
            if (timeSpan.Days > 0)
                formattedTime += $"{timeSpan.Days}Days ";
            if (timeSpan.Hours > 0)
                formattedTime += $"{timeSpan.Hours}h ";
            if (timeSpan.Minutes > 0)
                formattedTime += $"{timeSpan.Minutes}m ";
        }

        return formattedTime;
    }

    public void FlushMonitorFile()
    {
        gamePlay.dtEnd = DateTime.Now;

        crypt.EncryptContent(monitor_file, JsonConvert.SerializeObject(monitor));
        
        gamePlay = new GameMonitorPlays
        {
            dtStart = DateTime.Now,
            dtEnd = null,
            death = false
        };

        gameMonitor.plays.Add(gamePlay);
    }
}
