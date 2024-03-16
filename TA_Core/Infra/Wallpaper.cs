
using Newtonsoft.Json.Linq;

public partial class TextAdventureGame
{
    public void ChangeWallpaper(string wallpaperPath)
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
}