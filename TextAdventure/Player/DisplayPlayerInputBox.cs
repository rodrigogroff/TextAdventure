using System.Collections;

public partial class TextAdventureGame
{
    void DisplayPlayerInputBox()
    {
        if (!string.IsNullOrEmpty(game.player.name) && string.IsNullOrEmpty(game.player.title))
        {
            Write(" [Player Name: ", ConsoleColor.DarkGray);
            Write(game.player.name, ConsoleColor.Blue);
            Write(", Mode: ", ConsoleColor.DarkGray);
            if (bHintsDisabled==true)
            {
                Write("Old School", ConsoleColor.Red);
            }
            else if (bUnlimitedHints==true)
            {
                Write("Easy", ConsoleColor.Yellow);
            }
            else if (!bHardcore)
                Write("Normal", ConsoleColor.DarkYellow);
            else
                Write("HARDCORE", ConsoleColor.Red);

            Write(" ]\n", ConsoleColor.DarkGray);
        }
        else if (!string.IsNullOrEmpty(game.player.title))
        {
            Console.WriteLine();

            Write(" [Player Name: ", ConsoleColor.DarkGray);
            Write(game.player.name, ConsoleColor.Blue);
            Write(", Title: ", ConsoleColor.DarkGray);
            Write(game.player.title, ConsoleColor.Yellow);
            Write(", Inventory ", ConsoleColor.DarkGray);
            Write(game.player.inventory.Count().ToString(), ConsoleColor.Yellow);
            Write("/", ConsoleColor.DarkGray);
            Write(game.maxInventory.ToString(), ConsoleColor.Yellow);

            Write(", Mode: ", ConsoleColor.DarkGray);
            if (bHintsDisabled == true)
            {
                Write("Old School", ConsoleColor.Red);
            }
            else if (bUnlimitedHints == true)
            {
                Write("Easy", ConsoleColor.Yellow);
            }
            else if (!bHardcore)
                Write("Normal", ConsoleColor.DarkYellow);
            else
                Write("HARDCORE", ConsoleColor.Red);

            Write(" ]\n", ConsoleColor.DarkGray);
        }

        if (!bHardcore)
        {
            if (game.player.quests.Where(y => y.active == true).Count() > 0)
            {
                var lastQ =
                    game.player.quests.
                        Where(y => y.active == true && y.completed == false).
                            OrderByDescending(y => y.dt_start).
                                FirstOrDefault();

                var tot = game.player.quests.Count();

                Write(" [Quests(", ConsoleColor.DarkGray);
                Write(tot.ToString(), ConsoleColor.Yellow);
                Write("), Last: ", ConsoleColor.DarkGray);
                Write(lastQ.title, ConsoleColor.Red);
                Write(" ]\n", ConsoleColor.DarkGray);
            }

            Write(" [Type ", ConsoleColor.DarkGray);
            Write("/help", ConsoleColor.White);
            Write(" to see a list of commands]\n", ConsoleColor.DarkGray);
            Write(" [> ", ConsoleColor.Green);
        }
        else
        {
            Write(" [> ", ConsoleColor.Green);
        }        
    }
}