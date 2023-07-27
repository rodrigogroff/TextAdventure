
using System.Collections;

public partial class TextAdventureGame
{
    void UpdateWorldVariable(GameVariable item)
    {
        if (!game.world.Any(y => y.variable == item.variable))
            game.world.Add(item);
        else
        {
            var i = game.world.FirstOrDefault(y => y.variable == item.variable);
            i.content = item.content;
        }
    }
}
