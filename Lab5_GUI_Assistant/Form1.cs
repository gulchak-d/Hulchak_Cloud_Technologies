using System;
using System.Windows.Forms;
using Azure;
using Azure.AI.TextAnalytics;
using System.Diagnostics;

namespace Lab5_GUI_Assistant
{
    public partial class Form1 : Form
    {
        private static readonly string key = "9tE263Cj5GAz6g9fuv26PrWyL3D5HIc0fEjkejRVEt2s2V3oEiB1JQQJ99CDAC5RqLJXJ3w3AAAaACOGnuAz";
        private static readonly Uri endpoint = new Uri("https://lab5-ai-language-hulchak.cognitiveservices.azure.com/");

        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns[1].CellTemplate = new DataGridViewLinkCell();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var credentials = new AzureKeyCredential(key);
            var client = new TextAnalyticsClient(endpoint, credentials);

            string inputText = richTextBox1.Text;

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                try
                {
                    var response = await client.RecognizeLinkedEntitiesAsync(inputText);

                    dataGridView1.Rows.Clear();

                    foreach (var entity in response.Value)
                    {
                        dataGridView1.Rows.Add(entity.Name, entity.Url);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                string url = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                if (!string.IsNullOrEmpty(url))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
            }
        }
    }
}