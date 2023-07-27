
using System.Collections;

public partial class TextAdventureGame
{
    void UpdateInventory(GameItem item)
    {
        if (!game.player.inventory.Any( y=> y.name == item.name))
        {
            if (game.player.inventory.Count >= game.maxInventory)
            {
                Console.WriteLine();
                Write(" >> Maximum inventory reached!", ConsoleColor.Red);
                Write(" >> Itens may be dropped on the ground <<\n", ConsoleColor.Red);
                Console.WriteLine();

                game.world_itens.Add(new GameSceneItem
                {
                    guid = Guid.NewGuid(),
                    scene_id = current_game_Room.id,
                    item = new GameItem
                    {
                        name = item.name,
                        quantity = item.quantity,
                    }
                });
            }
            else
            {
                game.player.inventory.Add(item);
                Write(" (+) Acquired: ", ConsoleColor.Blue);
                Write(item.name, ConsoleColor.White);
                Write(" [", ConsoleColor.DarkGray);
                Write(item.quantity.ToString(), ConsoleColor.Yellow);
                Write("] \n", ConsoleColor.DarkGray);
                string msg = "(+) Acquired: " + item.name + " [" + item.quantity + "]";
                game.logs.Add(msg);
            }
        }
        else
        {
            var myItem = game.player.inventory.FirstOrDefault(y => y.name == item.name);
            string msg = "";
            if (item.quantity > 0)
            {
                msg = "(+) Acquired: " + item.name + " +[" + item.quantity + "] = Total: " + (myItem.quantity + item.quantity);
                myItem.quantity += item.quantity;

                Write(" (+) Acquired: ", ConsoleColor.Blue);
                Write(item.name, ConsoleColor.White);

                Write(" +[ ", ConsoleColor.DarkGray);
                Write(item.quantity.ToString(), ConsoleColor.Yellow);
                Write(" ] = Total ", ConsoleColor.DarkGray);
                Write((myItem.quantity + item.quantity).ToString() + "\n", ConsoleColor.Yellow);
            }
            else
            {
                var rem = item.quantity + myItem.quantity;
                if (rem < 0)
                    rem = 0;
                msg = "(-) Lost: " + item.name + " [" + myItem.quantity + item.quantity + "] = Remaining: " + (myItem.quantity + item.quantity);

                Write(" (-) Lost: ", ConsoleColor.Red);
                Write(item.name, ConsoleColor.White);
                Write(" [", ConsoleColor.DarkGray);
                Write(" " + (myItem.quantity + item.quantity) + " ", ConsoleColor.Yellow);
                Write(" ] = Remaining ", ConsoleColor.DarkGray);
                Write(rem + "\n", ConsoleColor.Yellow);

                myItem.quantity = rem;
            }
            
            if (myItem.quantity == 0)
                if (myItem.persistInventory != true)
                    game.player.inventory.Remove(myItem);
            game.logs.Add(msg);
        }
    }
}
