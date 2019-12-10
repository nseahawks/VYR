using System;
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
            get => Preferences.Get(nameof(Token), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhc2FzaGJ5QGhvdG1haWwuY29tIiwianRpIjoiMmM0NDNlN2UtNzlhOS00YzFkLTk1ODEtYzY3ZDAzNmY4ZWZkIiwiZW1haWwiOiJhc2FzaGJ5QGhvdG1haWwuY29tIiwiaWQiOiI3ZDU2ZTM2YS03YjI0LTQzYTgtYWRjOS05MDVjNDQ4YmJiNDQiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU3NTkxNjgyOSwiZXhwIjoxNTc1OTkyNDI5LCJpYXQiOjE1NzU5MTY4Mjl9.GolquAFMC3a6S-hn9ebmaKz86M9Py5llgjvmwjgLQ10");
            set => Preferences.Set(nameof(Token), value);
        }

        public ApiHelper ()
        {
           
           BaseAddress = new Uri("https://vyrapi.azurewebsites.net");
           DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer",
                    Token
                    
                    );
            //if (string.IsNullOrEmpty(Token))
            //{
                
            //}
        }
    }
}
