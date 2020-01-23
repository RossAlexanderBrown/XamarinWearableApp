using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Wearable.Activity;
using Java.Interop;
using Android.Views.Animations;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;
using QOTDAndroidWearableApp.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

namespace QOTDAndroidWearableApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        TextView textView;
        TextView authorView;
        readonly HttpClient client = new HttpClient();
        //const string Url = "https://api.quotable.io/random";
        private ObservableCollection<Quote> quote;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            textView = FindViewById<TextView>(Resource.Id.text);
            authorView = FindViewById<TextView>(Resource.Id.author);
            SetAmbientEnabled();
            GetQuote();
        }
        async private void GetQuote()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string response = await client.GetStringAsync("https://quotes.rest/qod");
            var quotes = JsonConvert.DeserializeObject<APIResponse>(response, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            quote = new ObservableCollection<Quote>(quotes.contents.quotes);
            textView.Text = quote.First().quote;
            authorView.Text = quote.First().author;
        }
    }
}


