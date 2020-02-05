/*using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;

namespace VYRMobile.Helper
{
    public class RecordHelper
    {
        public async Task WriteFile(string content)
        {
            await App.file.WriteAllTextAsync(content);
        }
        public async Task ReadFile()
        {
            var json = await App.file.ReadAllTextAsync();
            if(json != null)
            {
                App.Records = JsonConvert.DeserializeObject<List<Record>>(json);
            }
        }
    }
}*/
