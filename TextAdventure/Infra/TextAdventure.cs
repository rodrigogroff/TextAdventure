using System.Collections;

public partial class TextAdventureGame
{
    Game game = new Game();
    Context current_game_Room = new Context();
    Random random = new Random();
    string currentFile = "";
    bool bFastMode = false;
    bool bUnlimitedHints = false;
    bool bHintsDisabled = false;
    bool bHardcore = false;

    int GetRandomNumber(int start, int finish)
    {
        return random.Next(start, finish + 1);
    }

    void EnterToContinue()
    {
        Write(" [> ", ConsoleColor.DarkGray);
        Console.ForegroundColor = ConsoleColor.White;
        Console.CursorVisible = true;
        Console.ReadLine();
    }
}