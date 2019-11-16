using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.Net.Http;
using VYRMobile.ViewModels;
using System.Diagnostics;
using Org.Apache.Http.Protocol;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearReporte : ContentPage
    {
        //HttpClient _client;
        public CrearReporte()
        {
            InitializeComponent();
            btnAtras.Clicked += btnAtras_clicked;
            btnAttach.Clicked += btnAttach_clicked;
            //btnEnviar.Clicked += btnEnviar_clicked;
            BindingContext = new CrearReporteModel();
            BindingContext = new Reportes();
        }
        /*private void Tokenizer(HttpClient client)
        {
            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "bearer", token
                );
        }
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            Tokenizer(client);

            HttpResponseMessage response = await client.GetAsync("/api/v1/locations");
            string json = await response.Content.ReadAsStringAsync();
            List<Location> locationsList = JsonConvert.DeserializeObject<List<Location>>(json);
            //ViewData["locations"] = locationsList;
            var locations = locationsList;
            client.Dispose();
        }

        private async void btnEnviar_clicked(object sender, EventArgs e)
        {
            _client = new HttpClient();
            string url = "https://vyrapi.azurewebsites.net/api/v1/reports";
            
            try
            {


                var response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    ReportViewModel reports = ReportViewModel.FromJson(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        */
        private async void btnAttach_clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available", "OK");
                    return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Name = "seahawks.png"
            });

            if (file == null)
                return;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private void btnAtras_clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Reportes());
        }
        
    }
}