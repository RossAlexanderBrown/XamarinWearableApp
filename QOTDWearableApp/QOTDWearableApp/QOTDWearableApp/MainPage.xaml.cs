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
        const string Url = "https://api.quotable.io/random";
        private ObservableCollection<Quote> quote;
        public MainPage()
        {
            InitializeComponent();
            QOTDLabel.Text = "QOTD";
            GetQuote();
        }

        async private void GetQuote()
        {
            string response = await client.GetStringAsync(Url);
            List<Quote> quotes = JsonConvert.DeserializeObject<List<Quote>>(response);
            quote = new ObservableCollection<Quote>(quotes);
        }
    }
}