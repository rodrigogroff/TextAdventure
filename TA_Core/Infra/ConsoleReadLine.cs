
public partial class TextAdventureGame
{
    List<string> automationFile;
    List<string> automationFile_Out = new List<string>();

    int automationIndex = 0;

    string currentAutomation;

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
                    currentAutomation = line.Substring(5);
                }
                else if (line.StartsWith("check"))
                {
                    if (game.player.awards.FirstOrDefault(y=> y.id == line.Split(':')[1]).done == true)
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
                    return line.Substring("input:".Length);
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
