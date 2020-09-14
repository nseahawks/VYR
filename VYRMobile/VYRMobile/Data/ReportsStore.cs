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
using System.IO;

namespace VYRMobile.Data
{
    public class ReportsStore : IReportsStore<Report>
    {
        private HttpClient _client;
        FirebaseHelper _firebase = new FirebaseHelper();
        private List<string> ImagesNames = new List<string>();
        private List<Stream> ImagesStreams = new List<Stream>();
        string Image;
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

            if (report.Location == null || string.IsNullOrEmpty(report.Description))
            {
                await App.Current.MainPage.DisplayAlert("Inválido", "Rellene todos los campos para continuar correctamente", "Aceptar");
                return false;
            }

            DateTime date = DateTime.UtcNow;
            ImagesStreams = App.ImagesStreams;
            ImagesNames = App.ImagesNames;

            if (ImagesStreams != null & ImagesNames != null)
            {
                await _firebase.RunList(ImagesStreams, ImagesNames, App.ApplicationUserId, date);
                report.Img = await _firebase.GetLink(ImagesNames, App.ApplicationUserId, date);
            }

            var responseReport = new Report()
            {
                Title = report.Location.LocationName.ToString(),
                ReportType = report.ReportType,
                Created = date,
                Description = report.Description,
                Address = "",
                ReportStatus = report.ReportStatus,
                Img = report.Img
            };

            string serializedData = JsonConvert.SerializeObject(responseReport);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/reports", contentData);
            DependencyService.Get<IToast>().LongToast(response.StatusCode.ToString());

            App.ImagesStreams.Clear();
            App.ImagesNames.Clear();

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddTemporaryReportAsync(CompanyLocation antena, string imageName, Stream imageStream)
        {
            if (antena == null || !IsConnected)
                return false;

            if (string.IsNullOrEmpty(antena.LocationName))
            {
                await App.Current.MainPage.DisplayAlert("Inválido", "Seleccione una localidad para continuar", "Aceptar");
                return false;
            }

            DateTime date = DateTime.UtcNow;

            if (ImagesStreams != null & ImagesNames != null)
            {
                try
                {
                    await _firebase.Upload(imageStream, App.ApplicationUserId, imageName, date);
                    Image = await _firebase.GetFile(imageName, App.ApplicationUserId, date);
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo procesar la informacion correctamente", "Aceptar");
                    return false;
                }
            }

            var requestReport = new Report()
            {
                Title = antena.LocationName.ToString(),
                Created = date,
                Img = this.Image
            };

            string serializedData = JsonConvert.SerializeObject(requestReport);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/reports", contentData);
            DependencyService.Get<IToast>().LongToast(response.StatusCode.ToString());

            App.ImagesStreams.Clear();
            App.ImagesNames.Clear();

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> SendEventualityReportAsync(Report report)
        {
            if (!IsConnected)
                return false;
            try
            {
                string serializedData = JsonConvert.SerializeObject(report);
                var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("/api/v1/reports", contentData);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public Task<bool> DeleteReportAsync(string id)
        {
            throw new NotImplementedException();
        }
        public Task<Report> GetReportAsync(string id)
        {
            throw new System.NotImplementedException();
        }
        public async Task<IEnumerable<Report>> GetReportsAsync(bool forceRefresh = false)
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/reports?userId=" + App.ApplicationUserId);
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                List<Report> reports = JsonConvert.DeserializeObject<List<Report>>(jsonReports);
                return reports;
            }
            return null;
        }
        public async Task<List<CompanyLocation>> GetAntenasAsync()
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/locations?userId=" + App.ApplicationUserId);
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                List<CompanyLocation> antennas = JsonConvert.DeserializeObject<List<CompanyLocation>>(jsonReports);
                return antennas;
            }
            return null;
        }
        public async Task<ApplicationUser> GetUserAsync()
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/users/" + App.ApplicationUserId);
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                ApplicationUser user = JsonConvert.DeserializeObject<ApplicationUser>(jsonReports);
                return user;
            }

            return null;
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/users");
                var jsonReports = response.Content.ReadAsStringAsync().Result;
                List<ApplicationUser> users = JsonConvert.DeserializeObject<List<ApplicationUser>>(jsonReports);
                return users;
            }
            return null;
        }
        public async Task<ApplicationUser> GetUsersByUserIdAsync()
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var response = await _client.GetAsync("/api/v1/users/supervisor=" + App.ApplicationUserId);
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
        public async Task<bool> AddEvaluationReportAsync(EvaluationReport evaluationReport, List<Calculation> calculations)
        {
            if (evaluationReport == null || !IsConnected)
                return false;

            DateTime date = DateTime.UtcNow;

            ImagesStreams = App.ImagesStreams;
            ImagesNames = App.ImagesNames;

            if (ImagesStreams != null & ImagesNames != null)
            {
                await _firebase.RunList(ImagesStreams, ImagesNames, App.ApplicationUserId, date);
                evaluationReport.Files = await _firebase.GetLink(ImagesNames, App.ApplicationUserId, date);
            }

            List<Fault> faults = new List<Fault>();

            foreach (var fault in App.Faults) 
            {
                faults.Add(new Fault(fault.Text)
                {
                    Name = fault.Text
                });
            }

            var responseEvaluationReport = new EvaluationReport()
            {
                ReviewedUser = App.ReviewedUserId,
                Created = date,
                EventDate = evaluationReport.EventDate,
                Files = evaluationReport.Files,
                Calculations = calculations,
                Faults = faults
            };

            string serializedData = JsonConvert.SerializeObject(responseEvaluationReport);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/evalReports", contentData);
            DependencyService.Get<IToast>().LongToast(response.StatusCode.ToString());

            App.ReviewedUserId = null;
            App.ImagesStreams.Clear();
            App.ImagesNames.Clear();
            App.Calculations.Clear();
            App.Faults.Clear();

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ValidateWorkerAsync(string id, string userCode, string photoLink)
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var request = new ValidateRequest()
                {
                    UserId = id,
                    Code = userCode,
                    SupervisorId = App.ApplicationUserId,
                    DateTime = DateTime.Now,
                    IsAssist = true,
                    Picture = photoLink
                };

                string serializedData = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
                var apiResponse = await _client.PostAsync("/api/v1/supervisors", contentData);
                if (apiResponse.IsSuccessStatusCode == true)
                {
                    DependencyService.Get<IToast>().LongToast("Trabajador validado");
                    return true;
                }
                else
                {
                    DependencyService.Get<IToast>().LongToast("Codigo invalido");
                    return false;
                }
            }
            return false;
        }
        public async Task<bool> SetWorkerScheduleAsync(ApplicationUser worker, string schedule)
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var request = new SustituteRequest()
                {
                    Schedule = schedule
                };

                string serializedData = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
                var apiResponse = await _client.PutAsync("/api/v1/users/" + worker.Id, contentData);
                if (apiResponse.IsSuccessStatusCode == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public async Task<bool> ExchangeWorkerAsync(string workerId, bool exchange)
        {
            if (App.IsUserLoggedIn && IsConnected)
            {
                var request = new ExchangeRequest()
                {
                    Exchange = exchange
                };

                string serializedData = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
                var apiResponse = await _client.PutAsync("/api/v1/users/" + workerId, contentData);
                if (apiResponse.IsSuccessStatusCode == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}