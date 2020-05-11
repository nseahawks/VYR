﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Newtonsoft.Json;

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
            BaseAddress = new Uri("https://vyr-x-270115.appspot.com");
            //BaseAddress = new Uri("https://10.0.2.2:5001");
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }
    }
}
