
public partial class TextAdventureGame
{
    void ShowHints()
    {
        Console.WriteLine();

        if (bHintsDisabled || bHardcore)
        {
            Print("Hints disabled.", ConsoleColor.Red);
            return;
        }

        if (!bUnlimitedHints)
            game.hints--;

        if (current_game_Room.hint.Any(y => !string.IsNullOrEmpty(y) && game.hints > 0))
        {
            foreach (var item in current_game_Room.hint)
                Print(">> " + item, ConsoleColor.DarkYellow);
        }
        else
        {
            Print("No hints available.", ConsoleColor.DarkYellow);
        }

        Console.WriteLine();

        if (!bUnlimitedHints)
        {
            Print("Remaining hints: " + game.hints + " / " + game.hintsMAX, ConsoleColor.Green);
            Console.WriteLine();
        }
    }
}