
public partial class TextAdventureGame
{
    void ShowAward()
    {
        Console.WriteLine();
        int x = 1;

        bool first = true;

        foreach (var item in game.awards)
        {
            var i = game.player.awards.FirstOrDefault(y => y.id == item.id);

            Write(" - " + (x++).ToString().PadLeft(3, '0') + " - ", ConsoleColor.DarkGray);

            if (i != null)
                Write("[OK] " + item.text + "\n", ConsoleColor.Yellow);
            else
            {
                if (first)
                {
                    first = false;
                    Write("[  ] " + item.text + "\n", ConsoleColor.Blue);
                    Thread.Sleep(2000);
                }
                else
                {
                    Write("[  ] " + item.text + "\n", ConsoleColor.Green);
                }                
            }

            if (!bAutomation)
                Thread.Sleep(10);
        }
        Console.WriteLine();
    }
}
