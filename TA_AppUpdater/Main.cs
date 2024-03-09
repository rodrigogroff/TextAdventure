using System.Diagnostics;

public class Program
{
    static async Task DownloadFile(string fileUrl, string outputPath)
    {
        using (HttpClient client = new HttpClient())
        {
            using (HttpResponseMessage response = await client.GetAsync(fileUrl))
            {
                using (HttpContent content = response.Content)
                {
                    using (Stream stream = await content.ReadAsStreamAsync())
                    {
                        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
        }
    }

    public static async Task Main(string[] args)
    {
        try
        {
            string fileUrl = "https://drive.google.com/uc?id=1UCyQbt4J1hXcl7DKwCa85tO25cXWmfHS";
            string outputPath = "downloaded_file.txt";
            await DownloadFile(fileUrl, outputPath);
            string fileContent = File.ReadAllText(outputPath);
            await DownloadFile(fileContent.Trim(), outputPath);
            new TA_Update.GameUpdater().Update();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Working offline...");
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "TextAdventure.exe",
            Arguments = "x",
            UseShellExecute = false,
            CreateNoWindow = false
        };

        Process.Start(startInfo);        
    }
}
