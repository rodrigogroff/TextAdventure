
public class GameMonitorPlays
{
    public DateTime dtStart { get; set; }

    public DateTime? dtEnd { get; set; }

    public bool death = false;
}

public class GameSetup
{
    public int emptySpace = 0;
}

public class GameMonitor
{
    public string game_name { get; set; }

    public List<GameMonitorPlays> plays { get; set; }
}

public class GameMonitoring
{
    public List<GameMonitor> games { get; set; }
}

public class GameAboutDetail
{
    public string lang { get; set; }
    public GameAboutDetailInfo[] info { get; set; }
}

public class GameAboutDetailInfo
{
    public string[] text { get; set; }
}

public class GameSummary
{
    public string game_name { get; set; }
    public List<string> game_tip = new List<string>();
}

public class Game
{
    public Player player = new Player();
    public string currentRoom { get; set; }
    public string gameName { get; set; }
    public string gameVersion { get; set; }
    public int hints { get; set; }
    public int hintsMAX { get; set; }
    public int maxInventory { get; set; }
    public int maxRings { get; set; }
    public int maxAmulets { get; set; }
    public string textAlign { get; set; }

    public List<string> gameBigTitle = new List<string>();
    public List<string> constraints = new List<string>();
    public List<string> deathTriggers = new List<string>();
    public List<Context> stages = new List<Context>();
    public List<GameTrait> traits = new List<GameTrait>();
    public List<GameVariable> world = new List<GameVariable>();
    public List<GameSceneItem> world_itens = new List<GameSceneItem>();
    public List<Item> itens = new List<Item>();
    public List<GameMap> maps = new List<GameMap>();
    public List<Quest> quests = new List<Quest>();
    public List<GameAward> awards = new List<GameAward>();
    internal string gameJsonFile;
}

public class SaveGameFile
{
    public string currentRoom { get; set; }
    public int currentVersion { get; set; }
    public int hints { get; set; }
    public Player player = new Player();
    public List<GameVariable> world = new List<GameVariable>();
    public List<GameSceneItem> world_itens = new List<GameSceneItem>();
}

public class Quest
{
    public string id { get; set; }
    public string title { get; set; }
    public string subtitle { get; set; }
    public bool active { get; set; }
    public bool completed { get; set; }

    public DateTime? dt_start { get; set; }

    public List<string> description = new List<string>();
    public List<GameItem> requirements = new List<GameItem>();
}

public class GameSceneItem
{
    public string scene_id { get; set; }
    public int scene_version { get; set; }
    public GameItem item { get; set; }
    public Guid guid { get; set; }
}

public class GameAward
{
    public string id { get; set; }
    public string text { get; set; }
    public bool done { get; set; }
}

public class Player
{
    public string name { get; set; }
    public string title { get; set; }

    public PlayerGear gear { get; set; }

    public List<GameItem> attributes = new List<GameItem>();
    public List<GameItem> inventory = new List<GameItem>();
    public List<GameTrait> traits = new List<GameTrait>();
    public List<Quest> quests = new List<Quest>();
    public List<GameAward> awards = new List<GameAward>();
}

public class PlayerGear
{
    public GameItem head { get; set; }
    public GameItem body { get; set; }
    public GameItem hands { get; set; }
    public GameItem feet { get; set; }
    public GameItem weapon { get; set; }
    public GameItem shield { get; set; }

    public List<GameItem> amulets = new List<GameItem>();
    public List<GameItem> rings = new List<GameItem>();
}

public class GameMap
{
    public string name { get; set; }
    public List<string> places = new List<string>();
    public List<string> graphics = new List<string>();
}

public class Context
{
    public string id { get; set; }
    public int version { get; set; }
    public string option { get; set; }
    public string label { get; set; }
    public string map { get; set; }
    public string startup { get; set; }
    public List<string> startupProgram = new List<string>();

    public bool? npc { get; set; }

    public List<string> text = new List<string>();
    public List<string> textOptional = new List<string>();
    public List<string> nextStep = new List<string>();
    public List<string> program = new List<string>();
    public List<string> mapProgram = new List<string>();
    public List<string> skip = new List<string>();
    public List<string> look = new List<string>();
    public List<string> give = new List<string>();
    public List<string> take = new List<string>();
    public List<string> hint = new List<string>();
    public List<ContextUseItem> use = new List<ContextUseItem>();
}

public class ContextUseItem
{
    public string item { get; set; }
    public List<string> formula = new List<string>();
}

public class GameItem
{
    public string name { get; set; }
    public int quantity { get; set; }
    public bool? persistInventory { get; set; }
    public string formula { get; set; }
}

public class Item
{
    public string name { get; set; }
    public string description { get; set; }
    public string formula { get; set; }
    public bool? usable { get; set; }
    public bool? persistInventory { get; set; }
}

public class GameVariable
{
    public string variable { get; set; }
    public string content { get; set; }
}

public class GameTrait
{
    public string name { get; set; }
    public string description { get; set; }
    public List<string> trigger = new List<string>();
    public List<string> formula = new List<string>();
}
