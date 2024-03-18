using Newtonsoft.Json;

public partial class TextAdventureGame
{
    void LoadGame()
    {
        var file = game.gameJsonFile;

        game = JsonConvert.DeserializeObject<Game>(crypt.DecryptFile(game.gameJsonFile));
        game.gameJsonFile = file;

        if (game.gameBigTitle.Count() == 0)
        {
            var TitlefromFile = File.ReadAllText(currentFile.Replace(".game.jsonx", ".title.txt"));
            foreach (var item in TitlefromFile.Split('\n'))
                game.gameBigTitle.Add(item);
        }

        foreach (var currentStage in game.stages)
        {
            foreach (var takeItem in currentStage.take)
            {
                var _item = takeItem.Split('|')[0];
                var _item_formula = takeItem.Contains("|") ? takeItem.Split('|')[1] : "";

                var name = _item.Split(' ')[0];
                var qtty = Convert.ToInt32(_item.Split(' ')[1].Split(':')[0]);
                var g_item = game.itens.FirstOrDefault(y => y.name == name);
                    
                game.world_itens.Add(new GameSceneItem
                {
                    guid = Guid.NewGuid(),
                    scene_id = currentStage.id,
                    scene_version = currentStage.version,

                    item = new GameItem
                    {
                        name = name,
                        quantity = qtty,
                        persistInventory = g_item.persistInventory == true,
                        formula = _item_formula
                    }
                });
            }
            currentStage.take.Clear();            
        }
        game.hintsMAX = game.hints;
    }
}
