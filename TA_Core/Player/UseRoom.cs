using System.Collections;

public partial class TextAdventureGame
{
    void UseRoom()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        Write("¨ ▒▓██ Use Item ██▓▒\n", ConsoleColor.DarkGray);

        var currentRoom = game.stages.FirstOrDefault(y => y.id == game.currentRoom);

        if (game.player.inventory.Count == 0)
        {
            Console.WriteLine();
            Write("¨ Nothing to use!", ConsoleColor.DarkYellow);
            Console.WriteLine();
            return;
        }

        while (true)
        {
            bool foundUsableItens = false;
            foreach (var item in game.player.inventory)
            {
                var i = game.itens.FirstOrDefault(y => y.name == item.name);
                if (i.usable == true)
                    foundUsableItens = true;
            }
            if (!foundUsableItens)
            {
                Console.WriteLine();
                Write("¨ Nothing to use!", ConsoleColor.DarkYellow);
                Console.WriteLine();
                return;
            }
            Console.WriteLine();
            int indexItem = 1;
            var hash = new Hashtable();
            foreach (var item in game.player.inventory)
            {
                var gameItem = game.itens.FirstOrDefault(y => y.name == item.name);
                if (gameItem.usable == true)
                {
                    hash[indexItem] = gameItem;
                    Write(" -- " + indexItem++ + " ", ConsoleColor.DarkGray);
                    Write(item.name, ConsoleColor.White);
                    Console.Write(" ");
                    Write(item.quantity.ToString(), ConsoleColor.Yellow);
                    Console.Write(" ");
                    Write(gameItem.description + "\n", ConsoleColor.Red);
                }
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
                if (Convert.ToInt32(option) > indexItem && Convert.ToInt32(option) < 1)
                {
                    Write("¨ >> Invalid option!", ConsoleColor.Red);
                    continue;
                }
            }
            catch
            {
                Write("¨ >> Invalid option!", ConsoleColor.Red);
                continue;
            }
            var current_give_item = hash[Convert.ToInt32(option)] as Item;
            UpdateInventory(new GameItem
            {
                name = current_give_item.name,
                quantity = -1
            });
            ProcessCommand(current_give_item.formula, "start");
            CheckConstraints();
        }
    }

}