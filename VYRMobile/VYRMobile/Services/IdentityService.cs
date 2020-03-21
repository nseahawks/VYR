using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> LoginAsync(ApplicationUser user)
        {
            if (user.Email == null || user.Password == null || !IsConnected)
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
                string stringJWT = response.Content.ReadAsStringAsync().Result;
                JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);
                await SecureStorage.SetAsync("token", jwt.Token);
                ApiHelper.Token = jwt.Token;
                DeserializeToken(jwt.Token);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }
        public async void DeserializeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var deserializedToken = handler.ReadToken(token) as JwtSecurityToken;

            Dictionary<string, object> dictionary = deserializedToken.Payload;

            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                await SecureStorage.SetAsync(pair.Key.ToString(), pair.Value.ToString());
            }

            App.ApplicationUserId = await SecureStorage.GetAsync("id");
            App.ApplicationUserRole = await SecureStorage.GetAsync("role");

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch(App.ApplicationUserRole)
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
                    case "Master":
                        mergedDictionaries.Add(new GerentialTheme());
                        mergedDictionaries.Add(new Colors());
                        mergedDictionaries.Add(new Fonts());
                        break;
                }
            }
        }
    }
    
}
