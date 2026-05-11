using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;

namespace Task2_Health
{
    class Program
    {
        private static readonly string endpoint = "https://language-lab-hulchak.cognitiveservices.azure.com/";
        private static readonly string key = "FsiyUMEzR99yaOFG9f7dCZ0BnmSUIjKLGBCCsd6yQFszC8LDRRMvJQQJ99CDACYeBjFXJ3w3AAAaACOGxWwn";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

            string document = "Patient was diagnosed with Type 2 Diabetes and prescribed 500mg Metformin twice daily. Blood pressure is 130/80.";

            Console.WriteLine("Оригінальний медичний текст:");
            Console.WriteLine(document + "\n");

            Console.WriteLine("Аналіз медичних даних розпочато (це може зайняти кілька секунд)...\n");

            var operation = await client.StartAnalyzeHealthcareEntitiesAsync(new List<string> { document });
            await operation.WaitForCompletionAsync();

            Console.WriteLine("Структурована медична інформація");
            Console.WriteLine($"{"Сутність (Текст)",-20} | {"Категорія",-20} | {"Впевненість"}");
            Console.WriteLine(new string('-', 65));

            await foreach (var page in operation.Value)
            {
                foreach (var docResult in page)
                {
                    foreach (var entity in docResult.Entities)
                    {
                        Console.WriteLine($"{entity.Text,-20} | {entity.Category,-20} | {entity.ConfidenceScore:F2}");
                    }
                }
            }

            Console.WriteLine("\nНатисніть Enter, щоб закрити вікно...");
            Console.ReadLine();
        }
    }
}