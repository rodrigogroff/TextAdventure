using System;
using System.Collections;

public partial class TextAdventureGame
{
    void UseRoom()
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

            Write("¨ ▒▓██ Use Item ██▓▒\n", ConsoleColor.DarkGray);

            var currentRoom = game.stages.FirstOrDefault(y => y.id == game.currentRoom);
            Console.WriteLine();

            if (game.player.inventory.Count == 0)
            {
                Console.WriteLine();
                Write("¨ Nothing to use!\n", ConsoleColor.DarkYellow);
                Console.WriteLine();
                return;
            }
                    
            int indexItem = 1;
            var hash = new Hashtable();
            foreach (var item in game.player.inventory)
            {
                var gameItem = game.itens.FirstOrDefault(y => y.name == item.name);
                hash[indexItem] = gameItem;

                Write("¨ -- ", ConsoleColor.DarkGray);
                Write((indexItem++).ToString(), ConsoleColor.White);
                Write(" ", ConsoleColor.DarkGray);
                Write(item.name.Replace("_", " ").PadRight(30, ' '), ConsoleColor.Yellow);
                Write(" [ ", ConsoleColor.DarkGray);
                Write(item.quantity.ToString().PadLeft(3, ' '), ConsoleColor.Yellow);
                Write(" ] ", ConsoleColor.DarkGray);
                Write(gameItem.description + "\n", ConsoleColor.Red);
            }

            Console.WriteLine();
            Write("¨ [Select item to use:]", ConsoleColor.DarkGray);
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

            var current_give_item = hash[Convert.ToInt32(option)] as Item;

            if (current_give_item.usable == true)
            {
                UpdateInventory(new GameItem
                {
                    name = current_give_item.name,
                    quantity = -1
                });
                ProcessCommand(current_give_item.formula);
                Console.WriteLine();
                CheckConstraints();
                Console.WriteLine();
                EnterToContinue();                
                break;
            }
            else
            {
                Console.WriteLine();
                var _f = false;
                foreach (var x in current_game_Room.use)
                {
                    if (x.item == current_give_item.name)
                    {
                        _f = true;
                        foreach (var y in x.formula)
                            ProcessCommand(y);
                        Console.WriteLine();
                        EnterToContinue();
                        break;
                    }
                }
                if (!_f)
                {
                    Write("¨ >> Nothing happened!\n", ConsoleColor.DarkYellow);
                    Console.WriteLine();
                    EnterToContinue();
                }
            }
        }
    }
}
