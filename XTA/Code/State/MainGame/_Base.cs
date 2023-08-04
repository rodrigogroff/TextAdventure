using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using XTA.Code.Infra;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        const int 
            MAIN_START_FADEOUT_WALLPAPER = 0,
            MAIN_START_FADEOUT_COMPLETE = 1,
            MAIN_START_FADEIN_MAINDIALOG = 2,
            MAIN_COMPLETE = 3,
            FADE_FRAMES = 60;
        
        public GameState_ShowMainGame(GameXTA _main)
        {
            id = GameXTA.GAME_STATE_SHOW_MAIN_GAME;
            main = _main;            
            done = false;
        }

        float[] 
            curve = new GameFunctions().GenerateLogarithmicArray(FADE_FRAMES);

        bool 
            bShowStats = false,
            bShowBag = false,
            bDeath = false,
            bStarted = false,
            bTextDisplayed = false;

        int 
            internalState = 0,
            textDelayTimeStd = 1,
            textDelay = 1,
            text_curIndex = 1,
            indexAlphaMainDialog = 60,
            indexAlphaCurveStats = 60,
            indexAlphaCurveBag = 60,
            indexAlphaCurveDeath = 0,
            delayStartFadeout = 90;

        float 
            currentAlphaWallpaper = 1,
            currentAlphaMainDialog = 0,
            currentAlphaDeath = 0,
            currentAlphaMainStats = 0,
            currentAlphaMainBag = 0;

        KeyboardState prevKeyboardState;

        SpriteFont 
            titleAndCursorFont,
            roomTextFont,
            statsFont,
            cursorHelpFont;

        Texture2D 
            pngTexture_wallpaper,
            pngTexture_dialog,
            pngTexture_bag,
            pngTexture_deathPage,
            pixelTexture_ScanLines,
            pngTexture_stats;

        Vector2 
            mainDialogPos,
            mainStatsPos,
            mainBagPos,
            wallpaperPosition,
            deathPos;

        string 
            text_to_mainTitle = "",
            cmdText = "",
            textToDisplay = "",
            textIncoming = "";

        double 
            cursorBlinkTime = 0.3,
            cursorElapsed = 0;

        List<string> helpText = new List<string>
                {
                    "   Help ¨-- game commands --¨",
                    "",
                    "   ^stat^ = show current player attibutes",
                    "   ^bag^ = display player current inventory",
                    "   ^use^ = consume item in inventory",
                    "   ^give^ = give item in inventory to npc",
                    "   ^map^ = give item in inventory to npc",
                    "   ^quest^ = give item in inventory to npc",
                    "   ^quit^ = end current game",
                };

        List<string> text_to_display;

        Context currentRoom;

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

            currentRoom = main.master.stages.FirstOrDefault(x => x.id == "1" && x.version == 1);

            text_to_mainTitle = currentRoom.label;
            text_to_display = currentRoom.text;
        }
    }
}
