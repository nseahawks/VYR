using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Android.OS;

namespace VYRMobile.Helper
{
    public class ApiHelper : HttpClient
    {
        public static string Token
        {
            get => Preferences.Get(nameof(Token), "");
            set => Preferences.Set(nameof(Token), value);
        }

        public ApiHelper()
        {
            //string fing = Build.Fingerprint;
            //BaseAddress = new Uri("http://10.0.0.13:5000");
            BaseAddress = new Uri("https://vyr-x-270115.appspot.com");
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }
    }
}
