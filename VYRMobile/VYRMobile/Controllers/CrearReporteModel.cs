using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using VYRMobile.Services;
using VYRMobile.ViewModels;

namespace VYRMobile.Controls
{
    public class CrearReporteModel
    {
        ApiHelper _api = new ApiHelper();
        public List<Estatus> EstatusList { get; set; }

        public CrearReporteModel()
        {
            EstatusList = GetEstatuses().OrderBy(t => t.Value).ToList();
        }

        public void Create(string title, string description)
        {
            HttpClient client = _api.VRAPI();

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            ReportViewModel reportModel = new ReportViewModel
            {
                Created = DateTime.UtcNow,
                Description = description,
                Title = title,
                Status = false,
                Address = "test"
            };

            string stringData = JsonConvert.SerializeObject(reportModel);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpResponseMessage response =  client.PostAsync("/api/v1/reports", contentData).Result;
            Console.WriteLine(response.ToString());
            return;
        }
        public List<Estatus> GetEstatuses()
        {
            var estatuses = new List<Estatus>()
            {
                new Estatus(){Key = 1, Value = "Completo"},
                new Estatus(){Key = 2, Value = "Incompleto"}
            };

            return estatuses;
        }
    }

    public class Estatus
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
