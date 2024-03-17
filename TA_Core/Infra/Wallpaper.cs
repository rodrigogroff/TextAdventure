using Newtonsoft.Json.Linq;

public partial class TextAdventureGame
{
    public void ChangeWallpaper(string wallpaperPath)
    {
        if (wallpaperPath.Length > 0)
        {
            if (!File.Exists(wallpaperPath))
                return;
        }
        
        string settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages\\Microsoft.WindowsTerminal_8wekyb3d8bbwe\\LocalState\\settings.json");
        string settingsJson = File.ReadAllText(settingsFilePath);
        JObject settings = JObject.Parse(settingsJson);
        settings["profiles"]["defaults"]["backgroundImage"] = wallpaperPath;
        File.WriteAllText(settingsFilePath, settings.ToString());
    }
}