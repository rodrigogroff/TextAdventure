using System.Collections;
using System.Collections.Immutable;

public partial class TextAdventureGame
{
    Game game = new Game();
    Context current_game_Room = new Context();
    Random random = new Random();
    
    string currentFile = "";

    bool bFastMode = false;
    bool bAbortOp = false;
    bool bUnlimitedHints = false;
    bool bHintsDisabled = false;
    bool bHardcore = false;

    string gameDifficulty = "";

    Hashtable hshNumbers = new Hashtable();

    int GetRandomNumber(int start, int finish)
    {
        var tag = game.currentRoom + "_" + start + "_" + finish;

        if (hshNumbers[tag] == null)
            hshNumbers[tag] = new List<int>();

        var c_numbers_list = hshNumbers[tag] as List<int>;

        if (c_numbers_list.Count == finish)
            c_numbers_list.Clear();

        int resp = 0;

        while (true)
        {
            resp = random.Next(start, finish + 1);

            if (!c_numbers_list.Contains(resp))
            {
                c_numbers_list.Add(resp);
                break;
            }
        }

        return resp;
    }

    void EnterToContinue()
    {
        Write(" [Enter to continue]", ConsoleColor.DarkGray);
        Write(" [> ", ConsoleColor.Green);
        Console.ForegroundColor = ConsoleColor.White;
        Console.CursorVisible = true;
        while (Console.KeyAvailable) Console.ReadKey(intercept: true);
        Console.ReadLine();
    }
}