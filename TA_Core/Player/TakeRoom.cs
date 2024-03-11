
public partial class TextAdventureGame
{
    void TakeRoom()
    {
        var currentRoom = game.stages.FirstOrDefault(y => y.id == game.currentRoom);

        while (true)
        {
            Console.WriteLine();

            var world_inventory = game.world_itens.Where(y => y.scene_id == currentRoom.id && y.scene_version == currentRoom.version).ToList();
            var stage_inventory = new List<GameItem>();
            var guid_inventory = new List<Guid>();
            foreach (var gameScene in world_inventory)
            {
                stage_inventory.Add(gameScene.item);
                guid_inventory.Add(gameScene.guid);
            }
            if (stage_inventory.Count == 0)
            {
                if (currentRoom.npc == true)
                    Write("¨ Nothing for you!", ConsoleColor.DarkYellow);
                else
                    Write("¨ There is nothing around here...", ConsoleColor.DarkYellow);
                Console.WriteLine();
                break;
            }
            int index = 1;
            foreach (var takeItem in stage_inventory)
            {
                var g_item = game.itens.FirstOrDefault(y => y.name == takeItem.name);

                Write("¨ -- ", ConsoleColor.DarkGray);
                Write((index++).ToString(), ConsoleColor.White);
                Write(" ", ConsoleColor.DarkGray);
                Write(takeItem.name, ConsoleColor.Yellow);
                Write(" [ ", ConsoleColor.DarkGray);
                Write(takeItem.quantity.ToString(), ConsoleColor.Blue);
                Write(" ] ", ConsoleColor.DarkGray);
                Write(g_item.description + "\n", ConsoleColor.Red);
            }
            Console.WriteLine();
            Write("¨ [Select item to take:]\n", ConsoleColor.DarkGray);
            Write("¨ [> ", ConsoleColor.Green);
            Console.ForegroundColor = ConsoleColor.Green;
            while (Console.KeyAvailable) Console.ReadKey(intercept: true);
            string option = ConsoleReadLine().Trim();
            if (option == "")
                break;
            try
            {
                if (Convert.ToInt32(option) > index && Convert.ToInt32(option) < 1)
                    continue;
            }
            catch
            {
                Console.WriteLine();
                Write("¨ >> Invalid option! <<\n", ConsoleColor.Red);
                Console.WriteLine();
                continue;
            }

            var g_item_take = stage_inventory[Convert.ToInt32(option) - 1];
            var guid_item_world = guid_inventory[Convert.ToInt32(option) - 1];
            var gg_item = game.itens.FirstOrDefault(y => y.name == g_item_take.name);

            if (game.player.inventory.Count >= game.maxInventory)
            {
                Console.WriteLine();
                Write("¨ >> Maximum inventory reached! <<\n", ConsoleColor.Red);
                Write("¨ >> Itens may be dropped on the ground <<\n", ConsoleColor.Red);
                break;
            }

            UpdateInventory(new GameItem
            {
                name = g_item_take.name,
                quantity = Convert.ToInt32(g_item_take.quantity)
            });

            currentItem = g_item_take.name;

            if (!string.IsNullOrEmpty(gg_item.formula))
                ProcessCommand(gg_item.formula, "take");            

            if (!string.IsNullOrEmpty(g_item_take.formula))
                ProcessCommand(g_item_take.formula, "take");

            foreach (var item in game.world_itens.Where(y => y.scene_id == currentRoom.id && y.scene_version == currentRoom.version))
                if (item.guid == guid_item_world)
                {
                    game.world_itens.Remove(item);
                    break;
                }

            CheckConstraints();
        }
    }
}
