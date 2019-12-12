using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VYRMobile.Helper;
using VYRMobile.Models;
using Xamarin.Essentials;

namespace VYRMobile.Data
{
    class ReportsStore : IReportsStore<Report>
    {
        private HttpClient _client;
        public ReportsStore()
        {
            _client = new ApiHelper();
            
        }

        private static ReportsStore _instance;

        public static ReportsStore Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ReportsStore();

                return _instance;
            }
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> AddReportAsync(Report report)
        {
            if (report == null || !IsConnected)
                return false;

            var responseReport = new Report()
            {
                Title = report.Title,
                Address = "",
                Description = report.Description,
                Created = DateTimeOffset.Now,
                ReportStatus = report.ReportStatus,
                ReportType = report.ReportType,
                ResolveDate = DateTimeOffset.Now
            };

            string serializedData = JsonConvert.SerializeObject(responseReport);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/reports", contentData);

            return response.IsSuccessStatusCode;
        }

        public Task<bool> DeleteReportAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Report> GetReportAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Report>> GetReportsAsync(bool forceRefresh = false)
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/reports");
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                List<Report> reports = JsonConvert.DeserializeObject<List<Report>>(jsonReports);
                return reports;
            }

            return null;
        }

       
        public Task<bool> UpdateReportAsync(Report report)
        {
            throw new System.NotImplementedException();
        }
    }
}
