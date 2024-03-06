
public partial class TextAdventureGame
{
    void LookRoom()
    {
        Console.WriteLine();

        if (current_game_Room.look.Count == 0)
        {
            Write(" Nothing to look\n", ConsoleColor.DarkYellow);
            return;
        }

        game.logs.Add("inspected room [" + current_game_Room.label + "]");
        foreach (var item in current_game_Room.look)
        {
            if (!item.Contains("/"))
            {
                Thread.Sleep(200);
                PrintRoomText(item.Trim(), ConsoleColor.DarkYellow, 35);
            }
            else
                ProcessCommand(item, "look");
        }
        current_game_Room.look.RemoveAll(y => y.StartsWith("/"));
    }
}