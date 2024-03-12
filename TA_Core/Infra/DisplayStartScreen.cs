public partial class TextAdventureGame
{
    public void DisplayLogo()
    {
        bAutomation = true;
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(); 
        Write("¨                                           Text Adventures by\n", ConsoleColor.DarkGray);
        Console.WriteLine();
        Console.WriteLine();
        Write("¨  ▄▄▄▄    ██▓  ▄████     ▄▄▄▄    ▒█████ ▓██   ██▓  ██████      ▄████  ▄▄▄       ███▄ ▄███▒ █████   ██████ \n", ConsoleColor.DarkRed);
        Write("¨ ▓█████▄ ▓██▒ ██▒ ▀█▒   ▓█████▄ ▒██▒  ██▒▒██  ██░▒██    ▒     ██▒ ▀█▒▒████▄    ▓██▒▀█▀▒██▒ █▒▒▒▀ ▒██▒░░ ▒ \n", ConsoleColor.DarkRed);
        Write("¨ ▒██▒ ▄██▒██▒▒██░▄▄▄░   ▒██▒ ▄██▒██░  ██▒ ▒██ ██░░ ▓███▄     ▒██░▄▄▄░▒██░░▀█▄  ▓██   ▒▓██░▒█████ ░ ▓███▄  \n", ConsoleColor.DarkRed);
        Write("¨ ▒█████  ░██░░▓█ ▓██▓   ▒█████  ▒██   ██░ ░ ▐██░░  ▒   ██▒   ░▓█  ██▓░██▄▄▄▄██ ▒██░   ▒██ ▒▓█  ▄   ▒   ██▒\n", ConsoleColor.DarkRed);
        Write("¨ ░██  ▀█▓░██░░▒▓███▀▒   ░██  ▀█▓░ ████▓▒░ ░ ██░░░▒██████▒▒   ░▒▓███▀▒ ▓█ ░░▓██▒▒██▒   ░██▒░▒████▒▒██████▒▒\n", ConsoleColor.DarkRed);
        Write("¨ ░▒▓███▀▒░▓   ░▒ ▓      ░█████▓ ▒▒▒ ▒ ▒ ▒ ▒██░░   ░▒▓▓▓ ▒  ▒▒   ▓▒▓░░ ▒░   ░  ░░░ ▒░ ░▒ ▒▓▒ ▒▓▒   ▒▒▓▓▒▒  \n", ConsoleColor.DarkRed);
        Write("¨ ▒░▒░ ▒░  ▒ ░  ░░ ░     ▒░▒  ▓▒   ░ ▒ ▒░ ▓▓▓ ░▒░ ░ ░▒  ░ ░     ░░░ ░   ▒   ▒▒ ░░  ░      ░ ░ ▒  ░░ ░▒ ░░  \n", ConsoleColor.DarkRed);
        Write("¨  ░░   ░  ▒ ░░ ░   ░     ░    ░ ░     ▒  ▒ ▒ ░░  ░  ░  ░     ░ ░░      ░   ▒                 ░   ░  ░  ░  \n", ConsoleColor.DarkRed);
        Write("¨  ░       ░        ░     ░               ░           ░       ░   ░       ░                 ░  ░       ░   \n", ConsoleColor.DarkRed);
        Console.WriteLine();
        bAutomation = false;
    }
}