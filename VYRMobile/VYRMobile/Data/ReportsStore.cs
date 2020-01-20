using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Helper;
using VYRMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using VYRMobile.Services;
using VYRMobile.Views;
using System.Reflection;
using System.IO;
using VYRMobile.ViewModels;

namespace VYRMobile.Data
{
    public class ReportsStore : IReportsStore<Report>
    {
        private HttpClient _client;
        FirebaseHelper _firebase = new FirebaseHelper();
        private List<string> ImagesNames = new List<string>();
        private List<Stream> ImagesStreams = new List<Stream>();
        string UserId;
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

            UserId = await SecureStorage.GetAsync("id");
            ImagesStreams = App.ImagesStreams;
            ImagesNames = App.ImagesNames;

            if (ImagesStreams != null & ImagesNames != null)
            {
                await _firebase.RunList(ImagesStreams, ImagesNames, UserId, report.Created);
                report.Image = await _firebase.GetLinks(ImagesNames, UserId, report.Created);
            }

            var responseReport = new Report()
            {
                Title = report.Title,
                ReportType = report.ReportType,
                Created = DateTime.UtcNow,
                Description = report.Description,
                Address = "",
                ReportStatus = report.ReportStatus,
                Image = report.Image
            };

            string serializedData = JsonConvert.SerializeObject(responseReport);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/reports", contentData);
            DependencyService.Get<IToast>().LongToast(response.StatusCode.ToString());
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
            UserId = await SecureStorage.GetAsync("id");
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/reports?userId=" + UserId);
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                List<Report> reports = JsonConvert.DeserializeObject<List<Report>>(jsonReports);
                return reports;
            }

            return null;
        }

        public async Task<IEnumerable<Antena>> GetAntenasAsync(bool forceRefresh = false)
        {
            UserId = await SecureStorage.GetAsync("id");
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/locations?userId=" + UserId);
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                List<Antena> antennas = JsonConvert.DeserializeObject<List<Antena>>(jsonReports);
                return antennas;
            }
            return null;
        }
        public async Task<ApplicationUser> GetUserAsync()
        {
            UserId = await SecureStorage.GetAsync("id");
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/users/" + UserId);
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                ApplicationUser user = JsonConvert.DeserializeObject<ApplicationUser>(jsonReports);
                return user;
            }
            return null;
        }
        public Task<bool> UpdateReportAsync(Report report)
        {
            throw new System.NotImplementedException();
        }
    }
}
