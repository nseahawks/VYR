﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Helper;
using VYRMobile.Models;
using Xamarin.Essentials;

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
    }
}