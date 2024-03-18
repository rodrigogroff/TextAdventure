public partial class TextAdventureGame
{
    void SkipRoom()
    {
        if (current_game_Room.skip.Any())
            foreach (var item in current_game_Room.skip)
                ProcessCommand(item);
    }
}
