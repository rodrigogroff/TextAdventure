using System.Collections;

public partial class TextAdventureGame
{
    void MapRoom()
    {        
        if (string.IsNullOrEmpty(current_game_Room.map))
        {
            Console.WriteLine();
            Write("¨ No map available to this area!", ConsoleColor.DarkYellow);
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

                    if (letter_id.Split(',').Contains(current_game_Room.id))
                    {
                        cur_letter = letter;
                        break;
                    }
                }

                var hashLetterId = new Hashtable();

                foreach (var item in current_map.places)
                {
                    var letter = item.Split('|')[0];
                    var letter_id = item.Split('|')[1];
                    var letter_name = item.Split('|')[3];
                    hashLetterId[letter.ToLower()] = letter_id;
                }

                Console.WriteLine();

                int idx_line = 0;
                foreach (var item in current_map.graphics)
                {
                    Write("¨ ", ConsoleColor.DarkGray);

                    if (idx_line++ < 1)
                    {
                        Write(item, ConsoleColor.DarkGray);
                    }
                    else
                    {
                        foreach (var c in item)
                        {
                            if (c.ToString() == cur_letter)
                                Write(c.ToString(), ConsoleColor.Blue);
                            else
                            {
                                if (Char.IsLetter(c) && idx_line > 0)
                                {
                                    var letterStageId = hashLetterId[c.ToString().ToLower()] as string;

                                    var c_map_item = game.stages.FirstOrDefault(y => y.id == letterStageId);

                                    if (c_map_item != null)
                                    {
                                        if (c_map_item.npc == true)
                                            Write(c.ToString(), ConsoleColor.Red);
                                        else
                                            Write(c.ToString(), ConsoleColor.Yellow);
                                    }
                                    else
                                        Write(c.ToString(), ConsoleColor.DarkGray);
                                }
                                else
                                    Write(c.ToString(), ConsoleColor.DarkGray);
                            }
                        }
                    }

                    Write("\n", ConsoleColor.DarkGray);                    
                }

                Console.WriteLine();
                
                foreach (var item in current_map.places)
                {
                    var letter = item.Split('|')[0];
                    var letter_id = item.Split('|')[1];
                    var letter_name = item.Split('|')[3];
                    hashLetterId[letter.ToLower()] = letter_id;

                    var c_map_item = game.stages.FirstOrDefault(y => y.id == letter_id);

                    List<string> lines = new List<string>();
                    List<ConsoleColor> colors = new List<ConsoleColor>();

                    if (letter == cur_letter || cur_letter == "")
                        colors.Add(ConsoleColor.Blue);                    
                    else
                        colors.Add(ConsoleColor.DarkGray);

                    lines.Add("¨ " + letter.PadRight(5, ' '));

                    if (letter == cur_letter || cur_letter == "")
                        colors.Add(ConsoleColor.Blue);
                    else
                        colors.Add(ConsoleColor.DarkGray);

                    lines.Add(letter_name.PadRight(35, ' '));

                    if (c_map_item != null)
                        if (c_map_item.npc == true)
                        {
                            colors.Add(ConsoleColor.Red);
                            lines.Add("[NPC]");
                        }

                    if (letter_id.Split(',').Contains(current_game_Room.id) || cur_letter == "")
                    {
                        colors.Add(ConsoleColor.Blue);
                        lines.Add("[Player]");
                    }

                    colors.Add(ConsoleColor.DarkGray);
                    lines.Add("\n");
                    Print(lines, colors);
                }

                Console.WriteLine();

                if (current_map.places.Count > 1)
                {
                    Write("¨ [Select destination:]\n", ConsoleColor.DarkGray);
                    Write("¨ [> ", ConsoleColor.Green);
                    while (Console.KeyAvailable) Console.ReadKey(intercept: true);
                    string option = ConsoleReadLine().ToLower().Trim();
                    if (option == "")
                        break;
                    foreach (var item in current_map.places)
                    {
                        var letter = item.Split('|')[0];
                        var letter_id = item.Split('|')[1].Split(',');
                        var letter_dest = item.Split('|')[2];
                        var letter_name = item.Split('|')[3];

                        if (letter_id.Contains(current_game_Room.id))
                        {
                            foreach (var it in letter_dest.Split(','))
                            {
                                if (option == it.ToLower())
                                {
                                    nextLocation = hashLetterId[option] as string;
                                    nextLocation = nextLocation.Split(',')[0];

                                    Console.WriteLine();

                                    this.bAbortOp = false;

                                    if (current_game_Room.mapProgram.Any())
                                    {
                                        foreach (var prg in current_game_Room.mapProgram)
                                        {
                                            ProcessCommand(prg, "MapRoom");

                                            if (this.bAbortOp)
                                                break;
                                        }
                                    }

                                    if (!this.bAbortOp)
                                    {
                                        CheckConstraints();
                                        Console.WriteLine();
                                        Print("¨ You walk towards your destination...", ConsoleColor.DarkYellow);
                                        Console.WriteLine();
                                        Console.WriteLine();
                                        EnterToContinue();                                                                                
                                        ProcessRoom((hashLetterId[it.ToLower()] as string).Split(',')[0]);
                                    }
                                                                       
                                    this.bAbortOp = false;
                                }
                            }
                            break;
                        }
                    }
                    Console.WriteLine();
                    if (hashLetterId[option] as string == current_game_Room.id)
                        Write("¨ You are already there!", ConsoleColor.DarkYellow);
                    else
                        Write("¨ Thats too far away...", ConsoleColor.DarkYellow);
                    Console.WriteLine();
                }
                break;
            }
        }
    }

}