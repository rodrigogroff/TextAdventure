using Newtonsoft.Json.Linq;
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

    static bool IsExecutableInstalled(string executableName)
    {
        // Get the directories listed in the PATH environment variable
        string[] pathDirectories = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator);

        // Search each directory for the executable
        foreach (string directory in pathDirectories)
        {
            string executablePath = Path.Combine(directory, executableName);
            if (File.Exists(executablePath))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsWT_Installed()
    {
        return IsExecutableInstalled("wt.exe");
    }

    static void ModifyTerminalSettings(string wallpaperPath)
    {
        string settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages\\Microsoft.WindowsTerminal_8wekyb3d8bbwe\\LocalState\\settings.json");

        // Read the settings file
        string settingsJson = File.ReadAllText(settingsFilePath);

        // Parse the JSON
        JObject settings = JObject.Parse(settingsJson);

        // Modify the background image setting
        settings["profiles"]["defaults"]["backgroundImage"] = wallpaperPath;

        // Write the modified settings back to the file
        File.WriteAllText(settingsFilePath, settings.ToString());
    }

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Checking for new version...");
            string fileUrl = "https://drive.google.com/uc?id=1h-Dpc0WFC9yGHKvuH5VhH4pPRn0ie8Ud";
            string outputPath = "ta_upgrade.zip";
            await DownloadFile(fileUrl, outputPath);
            new TA_Update.GameUpdater().Update();
            File.Delete(outputPath);

            Console.WriteLine("> abrindo arquivo");
            Thread.Sleep(1000);

            // Path to the wallpaper image
            string wallpaperPath = Directory.GetCurrentDirectory() + "\\wallpaper_1_2560.jpg";

            // Set the wallpaper
            ModifyTerminalSettings(wallpaperPath);

            var batFile = "start.bat";
            if (File.Exists(batFile)) File.Delete(batFile);

            if (IsWT_Installed())
                File.WriteAllText(batFile, "wt -F \"" + Directory.GetCurrentDirectory() + "\\TextAdventure.exe\" start");
            else
                File.WriteAllText(batFile, "\"" + Directory.GetCurrentDirectory() + "\\TextAdventure.exe\" start");

            Console.WriteLine("Starting game...");
            Thread.Sleep(2000);

            Process process = new();
            process.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\" + batFile;
            process.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadLine();
        }
    }
}
