using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure;
using Azure.AI.TextAnalytics;

namespace Task3_BusinessGUI
{
    public partial class Form1 : Form
    {
        private readonly string endpoint = "https://language-lab-hulchak.cognitiveservices.azure.com/";
        private readonly string key = "FsiyUMEzR99yaOFG9f7dCZ0BnmSUIjKLGBCCsd6yQFszC8LDRRMvJQQJ99CDACYeBjFXJ3w3AAAaACOGxWwn";
        private TextAnalyticsClient client;

        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;

            try
            {
                button1.Enabled = false;
                richTextBox1.Text = "Обробка даних. Зачекайте...\n";

                PiiEntityCollection piiEntities = client.RecognizePiiEntities(textBox1.Text, "en");
                string maskedText = piiEntities.RedactedText;

                var operation = await client.StartAnalyzeHealthcareEntitiesAsync(new List<string> { textBox1.Text });
                await operation.WaitForCompletionAsync();

                string report = "РЕЗУЛЬТАТ ОБРОБКИ\n\n";
                report += "[ЗНЕОСОБЛЕНИЙ ТЕКСТ ДЛЯ ЗБЕРЕЖЕННЯ]:\n" + maskedText + "\n\n";
                report += "[СТРУКТУРОВАНІ МЕДИЧНІ ДАНІ ДЛЯ АНАЛІТИКИ]:\n";
                report += $"{"Сутність",-25} | {"Категорія",-20}\n";
                report += new string('-', 50) + "\n";

                await foreach (var page in operation.Value)
                {
                    foreach (var docResult in page)
                    {
                        foreach (var entity in docResult.Entities)
                        {
                            report += $"{entity.Text,-25} | {entity.Category,-20}\n";
                        }
                    }
                }

                richTextBox1.Text = report;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення до Azure: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }
    }
}