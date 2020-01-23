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
        private ObservableCollection<Quote> quote;
        public MainPage()
        {
            InitializeComponent();
            GetQuote();
        }

        async private void GetQuote()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string response = await client.GetStringAsync("https://quotes.rest/qod");
            APIResponse quotes = JsonConvert.DeserializeObject<APIResponse>(response);
            quote = new ObservableCollection<Quote>(quotes.contents.quotes);
            QOTDLabel.Text = quote.First().quote;
            AuthorLabel.Text = quote.First().author;
        }
    }
}