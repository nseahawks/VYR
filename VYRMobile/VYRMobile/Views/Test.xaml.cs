using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VYRMobile.Helper;
using VYRMobile.ViewModels;
using Xamarin.Essentials;
using VYRMobile.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace VYRMobile.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile]
    public partial class Test : ContentPage
    {
        string localPath;
        const string recordItems = "RecordItems";
        public Test()
        {
            InitializeComponent();
            BindingContext = new CallViewModel();

            btnSave.Clicked += SaveRecord;
            btnGet.Clicked += BtnGet_Clicked;

            localPath = Path.Combine(FileSystem.AppDataDirectory, recordItems);
            //btn.Clicked += Btn_Clicked;
        }

        private async void BtnGet_Clicked(object sender, EventArgs e)
        {
            //string path = localPath + "/" + fileName;
            string json = await File.ReadAllTextAsync(localPath);
            //string jsonn = json.ToString();
            Record response = JsonConvert.DeserializeObject<Record>(json);

            lblType.Text = response.RecordType.ToString();
            lblOwner.Text = response.Owner;
            lblDate.Text = response.Date.ToString();
            img.Source = response.Icon;
        }

        private async void SaveRecord(object sender, EventArgs e)
        {
            List<Record> Records = new List<Record>();
            var record = new Record()
            {
                UserId = await SecureStorage.GetAsync("id"),
                RecordType = Record.RecordTypes.Call,
                Icon = "callM.png",
                Owner = "Yo",
                Date = DateTime.Now
            };

            Records.Add(record);
            var serializedRecords = JsonConvert.SerializeObject(Records);
            await File.WriteAllTextAsync(localPath, serializedRecords, Encoding.UTF8);
        }
    }
}