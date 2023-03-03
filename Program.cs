using ChatGPT;
using Newtonsoft.Json.Linq;
using System.Text.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        string myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(myDocsPath, "conversation.json");

        var client = new OpenAiClient("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");  // generated api key from https://platform.openai.com/account/api-keys

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Ask: ");

            string? message = Console.ReadLine();

            if (string.IsNullOrEmpty(message))
            {
                await Console.Out.WriteLineAsync("Ask message must not null or not empty!");
            }
            else
            {
                try
                {
                    var response = await client.SendRequest(message, "gpt-3.5-turbo-0301");
                    var answer = JObject.Parse(response)?["choices"]?[0]?["message"]?["content"]?.ToString();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Answer:{answer}");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}

