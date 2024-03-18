
public partial class TextAdventureGame
{
    void GiveRoom()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            DisplayCurrentSceneTitle();

            Console.WriteLine();
            Console.WriteLine();

            Write("¨ ▒▓██ Give Item\n", ConsoleColor.DarkGray);

            Console.WriteLine();

            if (current_game_Room.npc != true)
            {
                Write("¨ No one is here to receive....\n", ConsoleColor.DarkYellow);
                Console.WriteLine();
                EnterToContinue();
                return;
            }

            if (current_game_Room.give.Count() == 0)
            {
                Write("¨ Not interested!\n", ConsoleColor.DarkYellow);
                Console.WriteLine();
                EnterToContinue();
                break;
            }

            if (game.player.inventory.Count == 0)
            {
                Write("¨ You have nothing to give!\n", ConsoleColor.DarkYellow);
                Console.WriteLine();
                EnterToContinue();
                break;
            }

            int index = 1;
            foreach (var item in game.player.inventory)
            {
                var gameItem = game.itens.FirstOrDefault(y => y.name == item.name);

                Write("¨ -- ", ConsoleColor.DarkGray);
                Write((index++).ToString(), ConsoleColor.White);
                Write(" ", ConsoleColor.DarkGray);
                Write(item.name.Replace("_"," ").PadRight(30,' '), ConsoleColor.Yellow);
                Write(" [ ", ConsoleColor.DarkGray);
                Write(item.quantity.ToString().PadLeft(3, ' '), ConsoleColor.Yellow);
                Write(" ] ", ConsoleColor.DarkGray);
                Write(gameItem.description + "\n", ConsoleColor.Red);
            }
            Console.WriteLine();
            Write("¨ [Select item to give:]", ConsoleColor.DarkGray);
            Write(" 'Enter'", ConsoleColor.White);
            Write(" to continue ", ConsoleColor.DarkGray);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" >> ");
            while (Console.KeyAvailable) Console.ReadKey(intercept: true);
            string option = ConsoleReadLine().Trim();
            if (option == "")
                break;

            try
            {
                if (Convert.ToInt32(option) > game.player.inventory.Count || Convert.ToInt32(option) < 1)
                {
                    Write("¨ >> Invalid option!\n", ConsoleColor.Red);
                    Console.WriteLine();
                    EnterToContinue();
                    continue;
                }
            }
            catch
            {
                Write("¨ >> Invalid option!\n", ConsoleColor.Red);
                Console.WriteLine();
                EnterToContinue();
                continue;
            }

            var current_give_item = game.player.inventory[Convert.ToInt32(option) - 1];
            var match = false;

            foreach (var i in current_game_Room.give)
            {
                var item = i.Split('|')[0];
                var item_program = i.Split('|')[1];
                var item_name = item.Split(' ')[0];
                var item_qtty = Convert.ToInt32(item.Split(' ')[1]);

                if (current_give_item.name == item_name)
                {
                    match = true;
                    if (current_give_item.quantity >= item_qtty)
                    {
                        Console.WriteLine();
                        Write("¨ Accepted!\n", ConsoleColor.DarkYellow);
                        Console.WriteLine();
                        current_give_item.quantity -= item_qtty;

                        Write("¨ (-) ", ConsoleColor.DarkGray);
                        Write(item_name, ConsoleColor.Yellow);
                        Write(" loss (", ConsoleColor.DarkGray);
                        Write(item_qtty.ToString(), ConsoleColor.Yellow);
                        Write(") = ", ConsoleColor.DarkGray);
                        Write(current_give_item.quantity.ToString() + "\n", ConsoleColor.Yellow);
                        
                        ProcessCommand(item_program);

                        if (current_give_item.quantity == 0)
                            if (current_give_item.persistInventory != true)
                                game.player.inventory.Remove(current_give_item);

                        Console.WriteLine();
                        EnterToContinue();
                    }
                    else
                    {
                        Console.WriteLine();
                        Write("¨ Not enough!\n", ConsoleColor.DarkYellow);
                        Console.WriteLine();
                        EnterToContinue();
                    }
                    break;
                }
            }

            if (!match)
            {
                Console.WriteLine();
                Write("¨ Not interested in '" + current_give_item.name.Replace("_", " ") + "'\n", ConsoleColor.DarkYellow);
                Console.WriteLine();
                EnterToContinue();
            }
        }
    }
}
