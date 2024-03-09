using TextAdventure.Infra;

public class Program
{
    public static async Task Main(string[] args)
    {
        var crypt = new FileEncryptorDecryptor();
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Games", "*.game.json"))
            crypt.EncryptFile(item, item + "x");
    }
}
