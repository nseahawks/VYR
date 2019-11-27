﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;

namespace VYRMobile.Helper
{
    public class ApiHelper : HttpClient
    {
        //"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkdW1tWUBlbWFpbC5jb20iLCJqdGkiOiI2M2I3ODAwYi0wNmVmLTQ1MDQtYjg5MC0yODcyYWFjNGEwODgiLCJlbWFpbCI6ImR1bW1ZQGVtYWlsLmNvbSIsImlkIjoiNTJhMjlhMWItMzNjZC00YThkLWIzMDUtY2Q3NDE1MzZlOGIzIiwicm9sZSI6IlVzZXIiLCJuYmYiOjE1NzQxOTg0NzAsImV4cCI6MTU3NDI3NDA3MCwiaWF0IjoxNTc0MTk4NDcwfQ.TWASUUyLY-D_VpmcPzkS0qhh2L76J-DtWI6GAeg3vNU"
        public static string Token
        {
            get => Preferences.Get(nameof(Token), "");
            set => Preferences.Set(nameof(Token), value);
        }

        public ApiHelper ()
        {
           
           BaseAddress = new Uri("https://vyrapi.azurewebsites.net");
           DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (string.IsNullOrEmpty(Token))
            {
                DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", /*ApiHelper.Token*/
                        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkdW1tWUBlbWFpbC5jb20iLCJqdGkiOiI4YmE4YTU4Yi04MDViLTQ1ODktOTQzNi03NWI2Y2Y2ZjdhMmEiLCJlbWFpbCI6ImR1bW1ZQGVtYWlsLmNvbSIsImlkIjoiNTJhMjlhMWItMzNjZC00YThkLWIzMDUtY2Q3NDE1MzZlOGIzIiwicm9sZSI6IlVzZXIiLCJuYmYiOjE1NzQ4NzMzNTIsImV4cCI6MTU3NDk0ODk1MiwiaWF0IjoxNTc0ODczMzUyfQ.PSx6m6vnAr59DlPqzWflxwOYVlWqurdQaxc7Tmzspyw"
                    );
            }
        }
    }
}