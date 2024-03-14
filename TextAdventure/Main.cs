
using System.Diagnostics;

public class Program
{
    static async Task DownloadFile(string fileUrl, string outputPath)
    {
        using (HttpClient client = new HttpClient())
        {
            using (HttpResponseMessage response = await client.GetAsync(fileUrl))
            {
                using (HttpContent content = response.Content)
                {
                    using (Stream stream = await content.ReadAsStreamAsync())
                    {
                        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
        }
    }

    public static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
#if DEBUG
            var batFile = "start.bat";
            Process process = new();
            process.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\" + batFile;
            process.Start();
            return;
#endif

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
                ta.Write(" [Inform current ", ConsoleColor.Green);
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
        catch
        {

        }
    }
}
