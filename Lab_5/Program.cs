using System;
using System.Collections.Generic;
using Azure;
using Azure.AI.TextAnalytics;

namespace Lab5_Console
{
    class Program
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("KEY");
        private static readonly Uri endpoint = new Uri("URL");

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            var client = new TextAnalyticsClient(endpoint, credentials);

            Console.WriteLine("=== Лабораторна робота №5: Entity Linking ===");
            Console.WriteLine("Введіть текст для аналізу:");

            string inputText = Console.ReadLine();

            if (!string.IsNullOrEmpty(inputText))
            {
                AnalyzeEntities(client, inputText);
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        static void AnalyzeEntities(TextAnalyticsClient client, string text)
        {
            var response = client.RecognizeLinkedEntities(text);

            Console.WriteLine("\nЗнайдені сутності та посилання на базу знань:");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Назва сутності",-25} | {"Посилання на Wikipedia"}");
            Console.WriteLine(new string('-', 80));

            foreach (var entity in response.Value)
            {
                Console.WriteLine($"{entity.Name,-25} | {entity.Url}");
            }
        }
    }
}
