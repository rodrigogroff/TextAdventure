
public partial class TextAdventureGame
{
    void ShowHints()
    {
        Console.WriteLine();

        if (bHintsDisabled || bHardcore)
        {
            Write("¨ Hints disabled.", ConsoleColor.Red);
            return;
        }

        if (!bUnlimitedHints)
            game.hints--;

        if (current_game_Room.hint.Any(y => !string.IsNullOrEmpty(y) && game.hints > 0))
        {
            foreach (var item in current_game_Room.hint)
                Write("¨>> " + item, ConsoleColor.DarkYellow);
        }
        else
            Write("¨ No hints available.", ConsoleColor.DarkYellow);
        
        Console.WriteLine();
    }
}
