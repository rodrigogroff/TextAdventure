
public partial class TextAdventureGame
{
    void ProcessCommand(string cmd_line, string from)
    {
        foreach (var cmd in cmd_line.Split(';'))
        {
            if (cmd.StartsWith("/deathItemAward"))
            {
                if (currentItem == cmd.Split(' ')[1])
                {
                    var aw = game.awards.FirstOrDefault(y => y.id == cmd.Split(' ')[2]);

                    if (aw != null)
                    {
                        if (!game.player.awards.Any(y => y.id == aw.id))
                        {
                            game.player.awards.Add(new GameAward
                            {
                                id = aw.id,
                                text = aw.text,
                                done = true
                            });

                            Console.WriteLine();
                            Write(" (+) New Award: ", ConsoleColor.Green);
                            Write(aw.text + "\n", ConsoleColor.White);
                            game.logs.Add(" (+) New Award: " + aw.text);
                            Console.WriteLine();
                        }
                    }
                }                
            }
            else if (cmd.StartsWith("/nextLocationTitle"))
            {
                var _cmd_ = cmd.Substring("/nextLocationTitle".Length + 1).Split('|');

                var subC_entry = _cmd_[0];
                var subC_cmd_ok = _cmd_[1];
                var subC_cmd_false = _cmd_[2];

                var title = _cmd_[0].Split(':')[0];
                var _location = subC_entry.Split(':')[1];

                if (nextLocation != _location)
                    continue;

                if (title.Replace("_", " ") == game.player.title)
                {
                    ProcessCommand(subC_cmd_false.Replace("?", ";"), from);
                    this.bAbortOp = true;
                }
            }
            else if (cmd.StartsWith("/nextLocationI"))
            {
                var _cmd_ = cmd.Substring("/nextLocationI".Length + 1).Split('|');

                var subC_entry = _cmd_[0];
                var subC_cmd_ok = _cmd_[1];
                var subC_cmd_false = _cmd_[2];

                var _okay = true;

                var _location = subC_entry.Split(':')[1];

                if (nextLocation != _location)
                    continue;

                foreach (var __item_req in subC_entry.Split(':')[0].Split('?'))
                {
                    if (__item_req == "")
                        continue;

                    var _it_name = __item_req.Split(',')[0];
                    var _it_qtd = Convert.ToInt32(__item_req.Split(',')[1]);

                    var _item = game.player.inventory.FirstOrDefault(y => y.name == _it_name);

                    if (_item == null)
                    {
                        _okay = false;
                        break;
                    }

                    if (_item.quantity < _it_qtd)
                    {
                        _okay = false;
                        break;
                    }
                }

                if (_okay)
                {
                    foreach (var __item_req in subC_entry.Split(':')[0].Split('?'))
                    {
                        if (__item_req == "")
                            continue;

                        var _it_name = __item_req.Split(',')[0];
                        var _it_qtd = Convert.ToInt32(__item_req.Split(',')[1]);

                        var _item = game.player.inventory.FirstOrDefault(y => y.name == _it_name);
                        _item.quantity -= _it_qtd;

                        if (_item.quantity == 0)
                            game.player.inventory.Remove(_item);
                    }

                    ProcessCommand(subC_cmd_ok.Replace("?", ";"), from);
                }
                else
                {
                    ProcessCommand(subC_cmd_false.Replace("?", ";"), from);
                    this.bAbortOp = true;
                }
            }
            else if (cmd.StartsWith("/nextLocation")) // get attribute equals
            {
                if (nextLocation == cmd.Split(' ')[1].Split('|')[0])
                {
                    var cmd_to_do = cmd.Split('|')[1];

                    ProcessCommand(cmd_to_do, from);
                }
            }
            else if (cmd.StartsWith("/getAE")) // get attribute equals
            {
                var w_attr_name = cmd.Split(' ')[1];
                var w_attr_value = Convert.ToInt32(cmd.Split(' ')[2].Split(':')[0]);
                var attr = game.player.attributes.FirstOrDefault(y => y.name == w_attr_name);
                if (attr != null)
                {
                    if (attr.quantity == w_attr_value)
                    {
                        var cmd_to_do = cmd.Split(':')[1].Replace(",", ";");
                        ProcessCommand(cmd_to_do, "proc");
                    }
                }
            }
            else if (cmd.StartsWith("/getAL")) // get attribute lower
            {
                var w_attr_name = cmd.Split(' ')[1];
                var w_attr_value = Convert.ToInt32(cmd.Split(' ')[2].Split(':')[0]);
                var attr = game.player.attributes.FirstOrDefault(y => y.name == w_attr_name);
                if (attr != null)
                {
                    if (attr.quantity < w_attr_value)
                    {
                        var cmd_to_do = cmd.Split(':')[1];
                        ProcessCommand(cmd_to_do, "proc");
                    }
                }
            }
            else if (cmd.StartsWith("/getAM"))
            {
                var w_attr_name = cmd.Split(' ')[1];
                var w_attr_value = Convert.ToInt32(cmd.Split(' ')[2].Split(':')[0]);
                var attr = game.player.attributes.FirstOrDefault(y => y.name == w_attr_name);
                if (attr != null)
                {
                    if (attr.quantity > w_attr_value)
                    {
                        var cmd_to_do = cmd.Split(':')[1];
                        ProcessCommand(cmd_to_do, "formula");
                    }
                }
            }
            else if (cmd.StartsWith("/award"))
            {
                var aw = game.awards.FirstOrDefault(y => y.id == cmd.Split(' ')[1]);

                if (aw != null)
                {
                    if (!game.player.awards.Any(y => y.id == aw.id))
                    {
                        game.player.awards.Add(new GameAward
                        {
                            id = aw.id,
                            text = aw.text,
                            done = true
                        });

                        Console.WriteLine();
                        Write(" (+) New Award: ", ConsoleColor.Green);
                        Write(aw.text + "\n", ConsoleColor.White);
                        game.logs.Add(" (+) New Award: " + aw.text);
                        Console.WriteLine();
                    }
                }
            }
            else if (cmd.StartsWith("/getPlayerTitle"))
            {
                var w_title_name = cmd.Split(' ')[1].Split(':')[0];
                if (game.player.title == w_title_name.Replace("_", " "))
                {
                    var cmd_to_do = cmd.Split(':')[1];
                    ProcessCommand(cmd_to_do, "formula");
                }
            }
            else if (cmd.StartsWith("/getT"))
            {
                var w_trait_name = cmd.Split(' ')[1].Split(':')[0];
                var trait = game.player.traits.FirstOrDefault(y => y.name == w_trait_name);
                if (trait != null)
                {
                    var cmd_to_do = cmd.Split(':')[1];
                    ProcessCommand(cmd_to_do, "formula");
                }
            }
            else if (cmd.StartsWith("/worldGet"))
            {
                var w_variable = cmd.Split(' ')[1];
                var w_content_expected = cmd.Split(' ')[2].Split(':')[0];
                var cmd_to_do = cmd.Split(':')[1];
                var w_item = game.world.FirstOrDefault(y => y.variable == w_variable);
                if (w_item != null)
                    if (w_item.content == w_content_expected)
                        ProcessCommand(cmd_to_do, "formula");
            }
            else if (cmd.StartsWith("/worldSet"))
            {
                var w_variable = cmd.Split(' ')[1];
                var w_content = cmd.Split(' ')[2];
                UpdateWorldVariable(new GameVariable
                {
                    variable = w_variable,
                    content = w_content,
                });
            }
            else if (cmd.StartsWith("/giveQ"))
            {
                var quest_id = cmd.Split(' ')[1];

                if (!game.player.quests.Any(y => y.id == quest_id))
                {
                    var quest = game.quests.FirstOrDefault(y => y.id == quest_id);

                    game.player.quests.Add(new Quest
                    {
                        id = quest_id,
                        title = quest.title,
                        subtitle = quest.subtitle,
                        requirements = quest.requirements,
                        active = true,
                        completed = false,
                        description = quest.description,
                        dt_start = DateTime.Now,
                    });

                    Console.WriteLine();
                    Write(" (+) New Quest: ", ConsoleColor.Red);
                    Write(quest.title + "\n", ConsoleColor.White);
                    Console.WriteLine();
                }
            }
            else if (cmd.StartsWith("/giveA"))
            {
                var item = cmd.Split(' ')[1];
                var qqty = cmd.Split(' ')[2];
                UpdateAttributes(new GameItem
                {
                    name = item,
                    quantity = Convert.ToInt32(qqty)
                });
            }
            else if (cmd.StartsWith("/giveT"))
            {
                var item = cmd.Split(' ')[1];
                var originalTrait = game.traits.FirstOrDefault(y => y.name == item);
                UpdateTraits(new GameTrait
                {
                    name = originalTrait.name,
                    description = originalTrait.description,
                    formula = originalTrait.formula,
                    trigger = originalTrait.trigger
                });
            }            
            else if (cmd.StartsWith("/giveI_CTitle"))
            {
                var _cmd = cmd.Split('|')[0];
                var _msg = cmd.Split('|')[1];

                PrintRoomText(_msg.Trim(), ConsoleColor.DarkYellow, 15);
                Thread.Sleep(400);

                var _title = _cmd.Split(' ')[1];
                var item = _cmd.Split(' ')[2];
                var qqty = Convert.ToInt32(_cmd.Split(' ')[3]);

                if (game.player.title == _title.Replace("_", " "))
                {
                    foreach (var tr in game.player.traits)
                    {
                        int idx_trig = 0;

                        foreach (var trig in tr.trigger)
                        {
                            if (trig == item)
                            {
                                Write(" (+) Triggered ", ConsoleColor.Blue);
                                Write(tr.name + "\n", ConsoleColor.White);

                                var vr_to_add = 0;
                                var form = tr.formula[idx_trig];

                                if (form.StartsWith("/randNeg"))
                                {
                                    var range = GetRandomNumber(1, Convert.ToInt32(form.Split(' ')[1]));
                                    vr_to_add = -(((qqty * range) / 100) + 1);
                                    Write("    (-) Lost ", ConsoleColor.Red);
                                    Write(vr_to_add.ToString().Substring(1), ConsoleColor.White);
                                    Write("    " + item + "\n", ConsoleColor.Yellow);
                                    qqty += vr_to_add;
                                }
                                else if (form.StartsWith("/rand"))
                                {
                                    var range = GetRandomNumber(1, Convert.ToInt32(form.Split(' ')[1]));
                                    vr_to_add = (qqty * range) / 100;
                                    Write("    (+) Found ", ConsoleColor.Blue);
                                    Write(vr_to_add.ToString(), ConsoleColor.White);
                                    Write("    " + item + "\n", ConsoleColor.Yellow);
                                    qqty += vr_to_add;
                                }
                            }

                            idx_trig++;
                        }
                    }
                    UpdateInventory(new GameItem
                    {
                        name = item,
                        quantity = Convert.ToInt32(qqty)
                    });
                }
            }
            else if (cmd.StartsWith("/giveI"))
            {
                var item = cmd.Split(' ')[1];
                var qqty = Convert.ToInt32(cmd.Split(' ')[2]);
                foreach (var tr in game.player.traits)
                {
                    int idx_trig = 0;

                    foreach (var trig in tr.trigger)
                    {
                        if (trig == item)
                        {
                            Write(" (+) Triggered ", ConsoleColor.Blue);
                            Write(tr.name + "\n", ConsoleColor.White);

                            var vr_to_add = 0;
                            var form = tr.formula[idx_trig];

                            if (form.StartsWith("/randNeg"))
                            {
                                var range = GetRandomNumber(1, Convert.ToInt32(form.Split(' ')[1]));
                                vr_to_add = -(((qqty * range) / 100) + 1);
                                Write("    (-) Lost ", ConsoleColor.Red);
                                Write(vr_to_add.ToString().Substring(1), ConsoleColor.White);
                                Write("    " + item + "\n", ConsoleColor.Yellow);
                                qqty += vr_to_add;
                            }
                            else if (form.StartsWith("/rand"))
                            {
                                var range = GetRandomNumber(1, Convert.ToInt32(form.Split(' ')[1]));
                                vr_to_add = (qqty * range) / 100;
                                Write("    (+) Found ", ConsoleColor.Blue);
                                Write(vr_to_add.ToString(), ConsoleColor.White);
                                Write("    " + item + "\n", ConsoleColor.Yellow);
                                qqty += vr_to_add;
                            }
                        }

                        idx_trig++;
                    }
                }
                UpdateInventory(new GameItem
                {
                    name = item,
                    quantity = Convert.ToInt32(qqty)
                });
            }
            else if (cmd.StartsWith("/title"))
            {
                game.player.title = cmd.Split(" ")[1].Replace("_", " ");
                game.logs.Add("Player received Title: " + game.player.title);
                Write(" (+) Acquired Title: ", ConsoleColor.Blue);
                Write(game.player.title + "\n", ConsoleColor.White);
                Thread.Sleep(400);
            }
            else if (cmd.StartsWith("/msgRnd"))
            {
                var msg = cmd.Substring(7).Split('%');
                var rnd_item = msg[GetRandomNumber(1, msg.Count()) - 1];
                Console.WriteLine();
                PrintRoomText(rnd_item.Trim(), ConsoleColor.DarkYellow, 15);
                Thread.Sleep(400);
            }
            else if (cmd.StartsWith("/msgO"))
            {
                var msg = cmd.Substring(5).Trim();
                PrintRoomText(msg.Trim(), ConsoleColor.Yellow, 15);
                Thread.Sleep(400);
            }
            else if (cmd.StartsWith("/msgN"))
            {
                var msg = cmd.Substring(5).Trim();
                PrintRoomText(msg.Trim(), ConsoleColor.DarkYellow, 15);
                Thread.Sleep(400);
            }
            else if (cmd.StartsWith("/msgR"))
            {
                var msg = cmd.Substring(5).Trim();
                PrintRoomText(msg.Trim(), ConsoleColor.Red, 15);
                Thread.Sleep(400);
            }
            else if (cmd.StartsWith("/gotoRnd"))
            {
                var stage_rooms = cmd.Split(" ")[1].Split(',');
                var stage = stage_rooms[GetRandomNumber(1, stage_rooms.Count()) - 1];
                if (!string.IsNullOrEmpty(game.player.name))
                {
                    Console.WriteLine();
                    EnterToContinue();
                }
                ProcessRoom(stage);
            }
            else if (cmd.StartsWith("/goto"))
            {
                var room = cmd.Split(" ")[1];

                if (room.StartsWith("0"))
                {
                    game.playerDead = true;

                    foreach (var item in game.deathTriggers)
                        ProcessCommand(item, "death");
                }

                if (!string.IsNullOrEmpty(game.player.name))
                {
                    Console.WriteLine();
                    EnterToContinue();
                }

                ProcessRoom(cmd.Split(" ")[1]);
            }
        }
    }
}

/*
 * 
 
{
        int ST = 1;
        int DX = 2;
        string formula = "ST + 10 * DX / 2";

        // Replace placeholders with actual values in the formula string
        string formulaWithValue = formula.Replace("ST", ST.ToString()).Replace("DX", DX.ToString());

        // Create a temporary DataTable to evaluate the expression
        DataTable dt = new DataTable();

        try
        {
            // Evaluate the expression
            var result = dt.Compute(formulaWithValue, "");

            // The result will be of type object, so you may need to cast it to the appropriate type
            double finalResult = Convert.ToDouble(result);

            Console.WriteLine("Result: " + finalResult);
        }
        catch (Exception ex)
        {
            // Handle any evaluation errors
            Console.WriteLine("Error: " + ex.Message);
        }
    }
 
 * */
