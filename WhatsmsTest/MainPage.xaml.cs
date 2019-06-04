using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;

namespace WhatsmsTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var number = BoxNumber.Text ?? "";
            if (string.IsNullOrEmpty(number))
            {
                await DisplayAlert("Info", "El numero no es válido", "Aceptar");
                return;
            }

            if (number.Length <= 7 || number.Length >= 15)
            {
                await DisplayAlert("Info", "El numero no es válido", "Aceptar");
                return;
            }

            var code = new Random().Next(100000,999999);
            HttpClient client = new HttpClient();
            // Get your ApiKey From https:/whatsmsapi.com
            client.DefaultRequestHeaders.Add("x-api-key", "YourApiKey");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync("https://whatsmsapi.com/api/send", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("phone", "525532182680"),
                    new KeyValuePair<string,string>("text", "Your Code Is: " + code)
                }));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (response != null)
            {
                var strjson = await response.Content.ReadAsStringAsync();
                Console.WriteLine(strjson);
            }
        }
    }
}
