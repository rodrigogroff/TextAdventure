using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
            main = _main;
            id = GameXTA.GAME_STATE_SHOW_MAIN_GAME;         
            done = false;
        }

        float[] 
            curve = new GameFunctions().GenerateLogarithmicArray(FADE_FRAMES);

        bool 
            bShowStats = false,
            bShowBag = false,
            bDeath = false,
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
            titleFont,
            arialFont,
            textStatsFont,
            lucidaBigFont,
            textFont;

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
            MainTitle = "Soul Selection",
            textToDisplay = "",
            textIncoming = "";

        double 
            cursorBlinkTime = 0.3,
            cursorElapsed = 0;

        List<string> original_text = new List<string>
        {
            "\"I'm out for groceries!\" Sarah said to her husband Paul, while searching for the car keys in her",
            "big pockets full of lollipops. She was still wearing her blue jacket with the name ^Dr. Sarah^",
            "^Allis Smith^ embroided in blue letters.",
            "",
            "Paul got out of the couch and fast he went to the ~kitchen~.",
            "",
            "\"We are out of.. pizza; and all the usual things...\" answered Paul, looking sad at the almost",
            "empty fridge at a friday night. \"Let me see if we are missing more..\" said the man in his forties,",
            "with a beer lite in his other hand.",
            "",
            "Sarah was exausted by coming home after a long shift at the clinic. Winter was coming, and lots ",
            "of little persons with their 'big' problems were coming too -- some came with a fever, but most",
            "consults, unfortunally, were just of scared parents in post COVID times who couldnt handle a",
            "simple cold, or a sore throat from their children without going mental.",
            "",
            "There was nothing wrong about being scared. She was a mom aswell, and got a pretty damn chill",
            "in her spine when Danny had his first seizure. Of course epilepsy is treatable and we are not in ",
            "the dark ages anymore; so, no evil possession or demons from hell, and simple medicine from the",
            "pharmacy solve (most of it). But everytime Danny looked directly in Sarah's eyes, she would get ",
            "anxious, and her mind played again the first time she saw the rolling up eye and the shaking: Paul",
            "had to act immediately, pushing her aside and screaming for her to help -- all she did was stare ",
            "and do nothing, while her husband rolled him to the side and stuck two fingers in the child's mouth."
        };
    }
}
