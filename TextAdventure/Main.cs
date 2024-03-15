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
    static async Task DownloadFile(string fileUrl, string outputPath)
    {
        using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(fileUrl))
                using (HttpContent content = response.Content)
                    using (Stream stream = await content.ReadAsStreamAsync())
                        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            await stream.CopyToAsync(fileStream);
    }

    static bool IsExecutableInstalled(string executableName)
    {
        string[] pathDirectories = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator);
        foreach (string directory in pathDirectories)
        {
            string executablePath = Path.Combine(directory, executableName);
            if (File.Exists(executablePath))
                return true;
        }
        return false;
    }

    public static bool IsWT_Installed()
    {
        return IsExecutableInstalled("wt.exe");
    }

    public static async Task Main(string[] args)
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
        
        if (!IsWT_Installed())
        {
            var hwnd = GetConsoleWindow();
            PostMessage(hwnd, WM_KEYDOWN, (IntPtr)VK_F11, IntPtr.Zero);
            PostMessage(hwnd, WM_MOUSEWHEEL, (IntPtr)(MK_CONTROL | WHEEL_DELTA), IntPtr.Zero);
        }

        try
        { 
            var ta = new TextAdventureGame();

            Console.CursorVisible = false;
            Console.Clear();
        
            Thread.Sleep(500);
            Console.WriteLine();
            ta.Write(" DOS/4GW Professional Protected Mode Run-Time Version 2.1c\n", ConsoleColor.White);
            ta.Write(" Copyright (C) United TA Systems, Inc. 1976\n", ConsoleColor.DarkGray);
            ta.Write(" Engine Version: 1.4.3", ConsoleColor.DarkGray);
            ta.Write(" ", ConsoleColor.Yellow);
            Thread.Sleep(2000);

            string savePassword = "";

            if (File.Exists("password.txt"))
                savePassword = File.ReadAllText("password.txt");
        
            string fileUrl = "https://drive.google.com/uc?id=1ceZUuXUPh8anIY0nEi_pF26HECvxJqGa";
            string outputPath = "ta_password.1";
            await DownloadFile(fileUrl, outputPath);
            string currentPatreonPass = File.ReadAllText(outputPath);
            File.Delete(outputPath);

            if (savePassword != "")
            {
                if (savePassword.ToLower().Trim() == currentPatreonPass.ToLower().Trim())
                {
                    ta.StartGame();
                    return;
                }
                else
                {
                    ta.Write(" -- Password ", ConsoleColor.Gray);
                    ta.Write(" epxired!\n\n", ConsoleColor.Red);
                }
            }
                        
            while (true)
            {
                ta.Write("\n [Inform current ", ConsoleColor.Green);
                ta.Write("patreon", ConsoleColor.Yellow);
                ta.Write(" password:] \n", ConsoleColor.Green);
                ta.Write(" [> ", ConsoleColor.Green);
                var pass = ta.ConsoleReadLine();

                if (pass.ToLower().Trim() != currentPatreonPass.ToLower().Trim())
                {
                    ta.Write(" -- Password ", ConsoleColor.Gray);
                    ta.Write(" incorrect!\n\n", ConsoleColor.Red);
                }
                else
                {
                    if (File.Exists("password.txt"))
                        File.Delete("password.txt");
                    File.WriteAllText("password.txt", currentPatreonPass);
                    break;
                }
            }

            ta.StartGame();
        }
        catch (Exception ex)
        {
            Console.WriteLine(" " + ex.ToString());
            Console.ReadLine();
        }
    }
}
