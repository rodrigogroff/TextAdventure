
public partial class TextAdventureGame
{
    void CheckConstraints()
    {
        foreach (var item in game.constraints)
            ProcessCommand(item);
    }
}