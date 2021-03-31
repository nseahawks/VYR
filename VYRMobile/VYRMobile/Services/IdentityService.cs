using GraphQL;
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

namespace VYRMobile.Services
{
    class IdentityService : IIdentityService<ApplicationUser>
    {
        private HttpClient _client;
        private CognitoIdentityHelper _helper;
        private GraphQLHelper qLHelper;

        public IdentityService()
        {
            _helper = new CognitoIdentityHelper();
            _client = new ApiHelper();
            qLHelper = new GraphQLHelper();
        }
        //bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> LoginAsync(ApplicationUser user)
        {
            /*if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || !App.isUserConnected)
                return false;*/

            var request = new ApplicationUser()
            {
                Email = user.Email.ToLower(),
                Password = user.Password
            };

            await _helper.LoginAuth(user.Email.ToLower(), user.Password);

            var userResponse = await _client.GetAsync("/alpha/api/users/sub/" + App.ApplicationUserId);
            var jsonUserResponse = userResponse.Content.ReadAsStringAsync().Result;
            ApplicationUser userObject = JsonConvert.DeserializeObject<ApplicationUser>(jsonUserResponse);

            var locRequest = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMilliseconds(1000));
            var location = await Geolocation.GetLocationAsync(locRequest);

            var logRequest = new logApi()
            {
                logStatus = "AUTHORIZED",
                user = userObject.Id,
                latitude = location.Latitude,
                longitude = location.Longitude
            };

            string serializedData = JsonConvert.SerializeObject(logRequest);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/alpha/api/attend/log", contentData);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            logApi logResponse = JsonConvert.DeserializeObject<logApi>(jsonResponse);


            var graphQLRequest = new GraphQLRequest()
            {
                Query = @"mutation MyMutation {
                    createLog(input: { apiLog: $apiLogVar, company: $userCompanyVar, date: $dateVar, status: ACCESS_REQUEST, user: { firstname: $firstNameVar, id: $idVar, lastname: $lastNameVar}
                    }) {
                    apiLog
                    }
                }",
                Variables = new
                {
                    apiLogVar = logResponse.id,
                    userCompanyVar = userObject.Company,
                    dateVar = DateTime.Now,
                    firstNameVar = userObject.FirstName,
                    idVar = App.ApplicationUserId,
                    lastNameVar = userObject.LastName
                }
            };

            var graphQLResponse = await qLHelper.graphQLClient.SendMutationAsync<LogGraphQL>(graphQLRequest);

            App.ApplicationUserId = "example";

            App.ApplicationUserRole = "Supervisor";
            /*if(request.Email == "supervisor")
            {
                App.ApplicationUserRole = "Supervisor";
            }
            else if(request.Email == "master")
            {
                App.ApplicationUserRole = "Master";
            }
            else if(request.Email == "patrol")
            {
                App.ApplicationUserRole = "Patrol";
            }
            else if(request.Email == "vigilant")
            {
                App.ApplicationUserRole = "Vigilant";
            }
            else if(request.Email == "vigilantqr")
            {
                App.ApplicationUserRole = "Qr";
            }
            else if(request.Email == "vigilantrounds")
            {
                App.ApplicationUserRole = "User";
            }
            else
            {
                return false;
            }*/

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

            return true;
            /*
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
                    App.IsUserLoggedIn = true;
                    DeserializeToken(jwt.Token);

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
            }*/
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

            await SecureStorage.SetAsync("isLogged", App.IsUserLoggedIn.ToString());

            App.ApplicationUserId = await SecureStorage.GetAsync("id");
            App.ApplicationUserRole = await SecureStorage.GetAsync("role");

            if (App.ApplicationUserRole != "Qr" &&  App.ApplicationUserRole != "User" && App.ApplicationUserRole != "Patrol" && App.ApplicationUserRole != "Vigilant" && App.ApplicationUserRole != "Supervisor" && App.ApplicationUserRole != "Master")
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
