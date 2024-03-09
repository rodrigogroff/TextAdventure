
public partial class TextAdventureGame
{
    List<string> automationFile;
    List<string> automationFile_Out = new List<string>();

    int automationIndex = 0;

    public string ConsoleReadLine()
    {
        if (bAutomation)
        {
            if (automationFile == null)
                return Console.ReadLine();

            while (automationIndex < automationFile.Count())
            {
                var line = automationFile[automationIndex];

                automationIndex++;

                if (line.StartsWith("test:"))
                {
                    automationFile_Out.Add(line);
                }
                else if (line.StartsWith("check"))
                {
                    var award = line.Split(':')[1];

                    if (game.player.awards.FirstOrDefault(y=> y.id == award).done == true)
                    {
                        automationFile_Out.Add("OK!");
                    }
                }
                else if (line.StartsWith("delay"))
                {
                    Thread.Sleep(Convert.ToInt32(line.Split(':')[1]));
                }
                else if (line.StartsWith("input"))
                {
                    var inp = line.Substring("input:".Length);

                    Console.Write("AUTO: >" + inp + "<");
                    Thread.Sleep(50);

                    return inp;
                }
            }

            bAutomation = false;
            Console.Write(" > AUTOOVER! <");

            if (File.Exists("automation.txt"))
                File.Delete("automation.txt");

            File.WriteAllText("automation.txt", string.Join(Environment.NewLine, automationFile_Out));
            return Console.ReadLine();
        }
        else
            return Console.ReadLine();
    }
}
