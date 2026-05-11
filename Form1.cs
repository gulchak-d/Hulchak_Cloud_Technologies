using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ex_2 
{
    public partial class Form1 : Form
    {
        private const string key = "ключ";
        private const string endpoint = "https://api.cognitive.microsofttranslator.com/";
        private const string location = "global";

        private Dictionary<string, string> languages = new Dictionary<string, string>
        {
            {"Англійська", "en"},
            {"Німецька", "de"},
            {"Французька", "fr"},
            {"Іспанська", "es"},
            {"Італійська", "it"},
            {"Польська", "pl"},
            {"Китайська", "zh-Hans"},
            {"Японська", "ja"},
            {"Турецька", "tr"},
            {"Чеська", "cs"}
        };

        public Form1()
        {
            InitializeComponent();

            Console.OutputEncoding = Encoding.UTF8;

            foreach (var lang in languages.Keys)
            {
                cmbLanguages.Items.Add(lang);
            }
            cmbLanguages.SelectedIndex = 0;

            btnTranslate.Click += btnTranslate_Click;
        }

        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            string textToTranslate = txtInput.Text;

            if (cmbLanguages.SelectedItem == null) return;

            string selectedLanguageName = cmbLanguages.SelectedItem.ToString();
            string targetLanguageCode = languages[selectedLanguageName];

            if (string.IsNullOrWhiteSpace(textToTranslate)) return;

            try
            {
                string result = await TranslateText(textToTranslate, targetLanguageCode);

                dynamic jsonObj = JsonConvert.DeserializeObject(result);
                string translatedText = jsonObj[0].translations[0].text;

                lblResult.Text = translatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        public async Task<string> TranslateText(string textToTranslate, string targetLanguage)
        {
            string route = $"/translate?api-version=3.0&to={targetLanguage}";
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Azure Error: {response.StatusCode}. {errorDetails}");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}