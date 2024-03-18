
public partial class TextAdventureGame
{
    void LookRoom()
    {
        Console.WriteLine();

        if (current_game_Room.look.Count() == 0)
        {
            Write("¨ Nothing to look\n", ConsoleColor.DarkYellow);
            return;
        }
        
        foreach (var item in current_game_Room.look)
        {
            if (!item.Contains("/"))
            {
                if (!bAutomation)
                    Thread.Sleep(200);
                PrintRoomText(item.Trim(), ConsoleColor.DarkYellow, 35);
            }
            else
                ProcessCommand(item);
        }

        current_game_Room.look.RemoveAll(y => y.StartsWith("/"));
    }
}