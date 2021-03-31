
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VYRMobile.Helper;
using VYRMobile.ViewModels;
using Plugin.FilePicker;
using Syncfusion.XForms.Buttons;
using VYRMobile.Services;
using VYRMobile.Data;
using VYRMobile.Models;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Views.Popups;
using GraphQL.Client.Http;
using GraphQL;
using Newtonsoft.Json.Linq;

namespace VYRMobile.Views
{
    public partial class Test : ContentPage
    {
        private readonly GraphQLHttpClient graphQLClient;
        private IDisposable _result;
        public Test()
        {
            InitializeComponent();
            //BindingContext = new TestViewModel();

            graphQLClient = new GraphQLHttpClient(new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(Constants.AppSyncEndpoint)

            }, new GraphQL.Client.Serializer.Newtonsoft.NewtonsoftJsonSerializer());

            graphQLClient.HttpClient.DefaultRequestHeaders.Add("x-api-key", Constants.AppSyncApiKey);
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("host", "7g2r6a42b5cvxpxtpe5ygsmsbi.appsync-api.us-east-2.amazonaws.com");

            _result = WaitingTimeUpdatedStream().Subscribe(Result_OnReceive);
        }

        private void testing()
        {
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _result = WaitingTimeUpdatedStream().Subscribe(Result_OnReceive);
        }
        public IObservable<GraphQLResponse<JObject>> WaitingTimeUpdatedStream()
        {
            var req = new GraphQLRequest
            {
                Query = @"
                    subscription {
                        onUpdateAlarm{
                           alarm
                           id
                           name
                        }
                    }"
            };

            return graphQLClient.CreateSubscriptionStream<JObject>(req);
        }
        private async void Result_OnReceive(GraphQLResponse<JObject> obj)
        {
            await DisplayAlert("Test", "Funciona", "Ok");
        }
    }
}