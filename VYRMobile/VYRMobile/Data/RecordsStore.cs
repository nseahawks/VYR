using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Helper;
using VYRMobile.Models;
using Xamarin.Essentials;

namespace VYRMobile.Data
{
    public class RecordsStore
    {
        private HttpClient _client;
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public RecordsStore()
        {
            //_client = new ApiHelper();
        }
        public async Task<bool> AddRecordAsync(Record record)
        {
            if (record == null || !IsConnected)
                return false;

            string serializedData = JsonConvert.SerializeObject(record);
            var contentData = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/records", contentData);

            return response.IsSuccessStatusCode;
        }
    }
}
