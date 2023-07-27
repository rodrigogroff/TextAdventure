using System.Collections;

public partial class TextAdventureGame
{
    void ResetGame()
    {
        var awards = game.player.awards;

        game.player = new Player();
        game.player.awards = awards;

        game.world = new List<GameVariable>();
    }

}