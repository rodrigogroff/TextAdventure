using System;
using System.Collections.Generic;
using System.Linq;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void StartUp()
        {
            foreach (var currentStage in main.master.stages)
            {
                if (currentStage.take.Count > 0)
                {
                    foreach (var takeItem in currentStage.take)
                    {
                        var name = takeItem.Split(' ')[0];
                        var qtty = Convert.ToInt32(takeItem.Split(' ')[1].Split(':')[0]);
                        var g_item = main.master.itens.FirstOrDefault(y => y.name == name);

                        main.master.world_itens.Add(new GameSceneItem
                        {
                            guid = Guid.NewGuid(),
                            scene_id = currentStage.id,
                            item = new GameItem
                            {
                                name = name,
                                quantity = qtty,
                                persistInventory = g_item.persistInventory == true,
                            }
                        });
                    }

                    currentStage.take.Clear();
                }
            }

            main.master.hintsMAX = main.master.hints;

            main.master.player.gear = new PlayerGear
            {
                body = null,
                feet = null,
                hands = null,
                head = null,
                rings = new List<GameItem>(),
                amulets = new List<GameItem>()
            };

            ProcessRoom("1");
        }
    }
}
