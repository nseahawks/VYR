using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace VYRMobile.Services
{
    class ApiHelper
    {
        public HttpClient VRAPI()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:5001");
            return Client;
        }
    }
}
