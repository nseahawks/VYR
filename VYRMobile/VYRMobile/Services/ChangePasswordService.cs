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

namespace VYRMobile.Services
{
    class ChangePasswordService : IChangePassword<ChangePassword>
    {
        private HttpClient _client;
        public ChangePasswordService()
        {
            _client = new ApiHelper();
        }
        public async Task<bool> ChangePasswordAsync(ChangePassword password)
        {
            var request = new ChangePassword()
            {
                OldPassword = password.OldPassword,
                NewPassword = password.NewPassword
            };

            string serializedData = JsonConvert.SerializeObject(request);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("/api/v1/identity/changePassword", contentData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
