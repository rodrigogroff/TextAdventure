using System.Collections;

public partial class TextAdventureGame
{
    void MapRoom()
    {
        if (string.IsNullOrEmpty(current_game_Room.map))
        {
            Console.WriteLine();
            Write(" No map available to this area!", ConsoleColor.DarkYellow);
            Console.WriteLine();
            return;
        }

        var current_map = game.maps.FirstOrDefault(y => y.name == current_game_Room.map);

        if (current_map != null)
        {
            while (true)
            {
                var cur_letter = "";

                foreach (var item in current_map.places)
                {
                    var letter = item.Split('|')[0];
                    var letter_id = item.Split('|')[1];

                    if (letter_id == current_game_Room.id)
                    {
                        cur_letter = letter;
                        break;
                    }
                }

                Console.WriteLine();
                foreach (var item in current_map.graphics)
                {
                    Write(" ", ConsoleColor.DarkGray);
                    foreach (var c in item)
                    {
                        if (c.ToString() == cur_letter)
                            Write(c.ToString(), ConsoleColor.Blue);
                        else
                            Write(c.ToString(), ConsoleColor.DarkGray);
                    }
                    Write("\n", ConsoleColor.DarkGray);
                }

                Console.WriteLine();
                var hashLetterId = new Hashtable();
                foreach (var item in current_map.places)
                {
                    var letter = item.Split('|')[0];
                    var letter_id = item.Split('|')[1];
                    var letter_name = item.Split('|')[3];
                    hashLetterId[letter.ToLower()] = letter_id;
                    List<string> lines = new List<string>();
                    List<ConsoleColor> colors = new List<ConsoleColor>();
                    colors.Add(ConsoleColor.Yellow);
                    lines.Add(" " + letter.PadRight(5, ' '));
                    colors.Add(ConsoleColor.DarkGray);
                    lines.Add(letter_name.PadRight(25, ' '));
                    if (letter_id == current_game_Room.id)
                    {
                        colors.Add(ConsoleColor.Blue);
                        lines.Add("[Player]");
                    }
                    colors.Add(ConsoleColor.DarkGray);
                    lines.Add("\n");
                    Print(lines, colors);
                }

                Console.WriteLine();
                Write(" [Select destination:]\n", ConsoleColor.DarkGray);
                Write(" [> ", ConsoleColor.Green);
                string option = Console.ReadLine().ToLower().Trim();
                if (option == "")
                    break;
                foreach (var item in current_map.places)
                {
                    var letter = item.Split('|')[0];
                    var letter_id = item.Split('|')[1];
                    var letter_dest = item.Split('|')[2];
                    var letter_name = item.Split('|')[3];

                    if (letter_id == current_game_Room.id)
                    {
                        foreach (var it in letter_dest.Split(','))
                        {
                            if (option == it.ToLower())
                            {
                                nextLocation = hashLetterId[option] as string;

                                if (current_game_Room.program.Any())
                                {
                                    foreach (var prg in current_game_Room.program)
                                    {
                                        ProcessCommand(prg, "MapRoom");
                                    }
                                }

                                CheckConstraints();

                                Console.WriteLine();
                                Print("You walk towards your destination...", ConsoleColor.DarkYellow);
                                Console.WriteLine();

                                EnterToContinue();
                                ProcessRoom(hashLetterId[it.ToLower()] as string);
                            }
                        }
                        break;
                    }
                }
                Console.WriteLine();
                if (hashLetterId[option] as string == current_game_Room.id)
                    Write(" You are already there!", ConsoleColor.DarkYellow);
                else
                    Write(" Thats too far away...", ConsoleColor.DarkYellow);
                Console.WriteLine();
                break;
            }
        }
    }

}