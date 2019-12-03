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

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> AddReportAsync(Report report)
        {
            if (report == null || !IsConnected)
                return false;

            var responseReport = new Report()
            {
                UserId = "52a29a1b-33cd-4a8d-b305-cd741536e8b3",
                Title = report.Title,
                Created = DateTime.UtcNow,
                Description = report.Description,
                Address = "",
                Status = false,
                ResolveDate = DateTime.Now
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

        public Task<IEnumerable<Report>> GetReportsAsync(bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateReportAsync(Report report)
        {
            throw new System.NotImplementedException();
        }
    }
}
