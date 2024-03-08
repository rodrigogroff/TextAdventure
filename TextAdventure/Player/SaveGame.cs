using Newtonsoft.Json;

public partial class TextAdventureGame
{
    void SaveGame()
    {
        var fileSaveGame = new SaveGameFile
        {
            currentRoom = current_game_Room.id,
            player = game.player,
            world = game.world,
            world_itens = game.world_itens,
            logs = game.logs,
            hints = game.hints,
        };

        File.WriteAllText(currentFile + ".save", JsonConvert.SerializeObject(fileSaveGame));
        Console.WriteLine();
        Write(" The game is saved!", ConsoleColor.DarkYellow);
        Console.WriteLine();

        FlushMonitorFile();
    }
}
