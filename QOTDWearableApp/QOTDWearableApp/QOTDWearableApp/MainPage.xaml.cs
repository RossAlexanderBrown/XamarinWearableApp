using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QOTDWearableApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace QOTDWearableApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        readonly HttpClient client = new HttpClient();
        //const string Url = "https://api.quotable.io/random";
        private ObservableCollection<Quote> quote;
        public MainPage()
        {
            InitializeComponent();
            QOTDLabel.Text = "QOTD";
            AuthorLabel.Text = "Author";
            GetQuote();
        }

        async private void GetQuote()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Token {Environment.GetEnvironmentVariable("QOTD_API_KEY")}");
            string response = await client.GetStringAsync("http://api.paperquotes.com/apiv1/qod");
            List <Quote> quotes = JsonConvert.DeserializeObject<List<Quote>>(response);
            quote = new ObservableCollection<Quote>(quotes);
            QOTDLabel.Text = quote.First().content;
            AuthorLabel.Text = quote.First().author;
        }
    }
}