using System.Collections;

public partial class TextAdventureGame
{
    void BagRoom()
    {
        while (true)
        {
            Console.WriteLine();

            int i = 1;
            foreach (var item in game.player.inventory)
            {
                var w_item = game.itens.FirstOrDefault(y => y.name == item.name);
                var desc = w_item != null ? w_item.description : "";

                Write(" -- ", ConsoleColor.DarkGray);
                Write((i++).ToString(), ConsoleColor.White);
                Write(" ", ConsoleColor.DarkGray);
                Write(item.name, ConsoleColor.Yellow);
                Write(" [ ", ConsoleColor.DarkGray);
                Write(item.quantity.ToString(), ConsoleColor.Blue);
                Write(" ] ", ConsoleColor.DarkGray);
                Write(desc, ConsoleColor.Red);
                Console.Write("\n");
            }
            if (game.player.inventory.Count == 0)
            {
                Write(" Empty!\n", ConsoleColor.DarkYellow);
                break;
            }
            Console.WriteLine();
            Write(" [Select item to drop:]", ConsoleColor.DarkGray);
            Write(" 'Enter'", ConsoleColor.White);
            Write(" to continue ", ConsoleColor.DarkGray);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" >> ");

            while (Console.KeyAvailable) Console.ReadKey(intercept: true);

            string option = Console.ReadLine().Trim();
            if (option == "")
                break;

            try
            {
                var index = Convert.ToInt32(option);

                if (index < 1 || index > game.player.inventory.Count)
                    break;

                var game_inv_item = game.player.inventory[index - 1];

                if (game_inv_item.quantity > 0)
                {
                    Console.WriteLine();
                    Write("[Type quantity:]", ConsoleColor.White);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" >> ");
                    while (Console.KeyAvailable) Console.ReadKey(intercept: true);

                    string option_qqy = Console.ReadLine().Trim();
                    if (option_qqy == "")
                        break;
                    try
                    {
                        int i_option_qty = Convert.ToInt32(option_qqy);
                        if (i_option_qty > 0 && i_option_qty <= game_inv_item.quantity)
                        {
                            game.world_itens.Add(new GameSceneItem
                            {
                                guid = Guid.NewGuid(),
                                scene_id = current_game_Room.id,
                                item = new GameItem
                                {
                                    name = game_inv_item.name,
                                    quantity = i_option_qty,
                                }
                            });
                            UpdateInventory(new GameItem
                            {
                                name = game_inv_item.name,
                                quantity = -i_option_qty,
                            });
                            break;
                        }
                        else
                        {
                            Print(" Wrong amount!", ConsoleColor.DarkGray);
                            break;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
        }
    }

}