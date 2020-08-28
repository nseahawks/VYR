using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Data;
using VYRMobile.Helper;
using VYRMobile.Models;
using VYRMobile.Styles;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Content.Res.Resources;

namespace VYRMobile.Services
{
    class IdentityService : IIdentityService<ApplicationUser>
    {
        private HttpClient _client;

        public IdentityService()
        {
            _client = new ApiHelper();
        }
        //bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> LoginAsync(ApplicationUser user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || !App.isUserConnected)
                return false;

            var request = new ApplicationUser()
            {
                Email = user.Email,
                Password = user.Password
            };

            string serializedData = JsonConvert.SerializeObject(request);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("/api/v1/identity/login", contentData);
                
                if(response.IsSuccessStatusCode == true)
                {
                    string stringJWT = response.Content.ReadAsStringAsync().Result;
                    JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);
                    await SecureStorage.SetAsync("token", jwt.Token);
                    ApiHelper.Token = jwt.Token;
                    DeserializeToken(jwt.Token);
                    App.IsUserLoggedIn = true;

                    //await CheckRecentCrash();

                    return response.IsSuccessStatusCode;
                }
                else
                {
                    return response.IsSuccessStatusCode;
                } 
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No es posible conectar con el servidor", "Aceptar");
                return false;
            }
        }

        public Task<bool> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }
        public async void DeserializeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            App.ApplicationUserToken = handler.ReadToken(token) as JwtSecurityToken;

            Dictionary<string, object> dictionary = App.ApplicationUserToken.Payload;

            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                await SecureStorage.SetAsync(pair.Key.ToString(), pair.Value.ToString());
            }

            var tokenSerializedData = JsonConvert.SerializeObject(App.ApplicationUserToken);
            await SecureStorage.SetAsync("token", tokenSerializedData);
            await SecureStorage.SetAsync("isLogged", App.IsUserLoggedIn.ToString());

            App.ApplicationUserId = await SecureStorage.GetAsync("id");
            App.ApplicationUserRole = await SecureStorage.GetAsync("role");

            if (App.ApplicationUserRole != "User" && App.ApplicationUserRole != "Patrol" && App.ApplicationUserRole != "Vigilant" && App.ApplicationUserRole != "Supervisor" && App.ApplicationUserRole != "Master")
            {
                await Application.Current.MainPage.DisplayAlert("Denegado", "La cuenta ingresada no tiene un rol válido para empezar a usar la aplicación", "OK");
                return;
            }


            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (App.ApplicationUserRole)
                {
                    default:
                        mergedDictionaries.Add(new WorkerTheme());
                        mergedDictionaries.Add(new Colors());
                        mergedDictionaries.Add(new Fonts());
                        break;
                    case "Supervisor":
                        mergedDictionaries.Add(new SupervisorTheme());
                        mergedDictionaries.Add(new Colors());
                        mergedDictionaries.Add(new Fonts());
                        break;
                }
            }
        }
        private async Task CheckRecentCrash()
        {
            if(App.HasAppCrashed)
            {
                string crashReportSerializedData = await SecureStorage.GetAsync("crashReportData");
                Report crashReport = JsonConvert.DeserializeObject<Report>(crashReportSerializedData);
                await ReportsStore.Instance.SendEventualityReportAsync(crashReport);
            }
        }
    }
}
