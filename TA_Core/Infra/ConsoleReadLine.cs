
public partial class TextAdventureGame
{
    List<string> automationFile;
    List<string> automationFile_Out = new List<string>();

    int automationIndex = 0;

    string currentAutomation;

    public string ConsoleReadLine()
    {
        Console.CursorVisible = true;

        if (bAutomation)
        {
            if (automationFile == null)
                return Console.ReadLine();
            else
            {
                while (automationIndex < automationFile.Count())
                {
                    var line = automationFile[automationIndex];

                    automationIndex++;

                    if (line.StartsWith("test:"))
                    {
                        automationFile_Out.Add(line);
                        currentAutomation = line.Substring(5);
                    }
                    else if (line.StartsWith("check"))
                    {
                        if (game.player.awards.FirstOrDefault(y => y.id == line.Split(':')[1]).done == true)
                        {
                            automationFile_Out.Add("OK!");
                        }
                    }
                    else if (line.StartsWith("delay"))
                    {
                        Write("\n", ConsoleColor.Red);
                        Write("¨ ---------- AUTOMATION DELAY ----------\n", ConsoleColor.Red);
                        Write("\n", ConsoleColor.Red);
                        Thread.Sleep(Convert.ToInt32(line.Split(':')[1]));
                    }
                    else if (line.StartsWith("input"))
                    {
                        var m = line.Substring("input:".Length);
                        Write("\n", ConsoleColor.Red);
                        Write("¨ ---------->" + m + "<----------\n", ConsoleColor.Red);
                        Write("\n", ConsoleColor.Red);
                        //if (automationIndex > 62)
                        Thread.Sleep(500);
                        return m;
                    }
                }
            }

            bAutomation = false;
            Console.Write(" > AUTOMATION OVER! > ");

            if (File.Exists("automation.txt"))
                File.Delete("automation.txt");

            File.WriteAllText("automation.txt", string.Join(Environment.NewLine, automationFile_Out));

            var ret = Console.ReadLine();
            Console.CursorVisible = false;
            return ret;
        }
        else
        {
            var ret = Console.ReadLine();
            Console.CursorVisible = false;
            return ret;
        }
    }
}
