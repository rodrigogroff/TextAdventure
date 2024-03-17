using System.Collections;

public partial class TextAdventureGame
{
    void UpdateAttributes(GameItem item)
    {
        if (!game.player.attributes.Any(y => y.name == item.name))
        {
            game.player.attributes.Add(item);
            if (!item.name.EndsWith("MAX"))
            {
                Write("¨ (+) Acquired Stat: " + item.name + " +[" + item.quantity + "]\n", ConsoleColor.Blue);
            }
        }
        else
        {
            var i = game.player.attributes.FirstOrDefault(y => y.name == item.name);
            List<string> lines = new List<string>();
            List<ConsoleColor> colors = new List<ConsoleColor>();
            if (item.quantity < 0)
            {
                var rem = item.quantity + i.quantity;
                if (rem < 0)
                    rem = 0;
                var max_st = game.player.attributes.FirstOrDefault(y => y.name == item.name + "MAX");
                if (max_st != null)
                {
                    Write("¨ (-) Lost Stat: ", ConsoleColor.DarkGray);
                    Write(item.name, ConsoleColor.Yellow);
                    Write(" [", ConsoleColor.DarkGray);
                    Write(item.quantity.ToString(), ConsoleColor.White);
                    Write(" ] = Remaining ", ConsoleColor.DarkGray);
                    Write(rem.ToString(), ConsoleColor.Yellow);
                    Write("/", ConsoleColor.DarkGray);
                    Write(max_st.quantity + "\n", ConsoleColor.Yellow);
                }
                else
                {
                    Write("¨ (-) Lost Stat: ", ConsoleColor.DarkGray);
                    Write(item.name, ConsoleColor.Yellow);
                    Write(" [", ConsoleColor.DarkGray);
                    Write((i.quantity + item.quantity).ToString(), ConsoleColor.White);
                    Write(" ] = Remaining ", ConsoleColor.DarkGray);
                    Write(rem.ToString() + "\n", ConsoleColor.Yellow);
                }

                i.quantity = rem;
            }
            else
            {
                var max_st = game.player.attributes.FirstOrDefault(y => y.name == item.name + "MAX");
                if (max_st != null)
                {
                    var add_mx = item.quantity + i.quantity;
                    if (add_mx > max_st.quantity)
                        add_mx = max_st.quantity;
                    i.quantity += add_mx;
                }
                else
                {
                    i.quantity += item.quantity;
                    Write("¨ (+) Acquired Stat: " + item.name + " [+" + item.quantity + "] = Total: " + i.quantity + "\n", ConsoleColor.DarkGray);
                }
            }
        }
    }
}
