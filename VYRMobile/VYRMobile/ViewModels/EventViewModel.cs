using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class EventViewModel : BindableObject
    {
        internal static string localPath;
        string recordItems = "record.json";
        //List<Record> Events = new List<Record>();
        private static EventViewModel _instance;
        public static EventViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventViewModel();

                return _instance;
            }
        }
        public EventViewModel() 
        {
            localPath = Path.Combine(FileSystem.AppDataDirectory, recordItems);
            //LoadRecords();
            //LoadData();
        }

        private async void LoadRecords()
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("record.json");
            StreamReader reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            //Events = JsonConvert.DeserializeObject<List<Record>>(json);
        }

        /*private void LoadData()
        {
            var events = EventService.Instance.GetEvents();

            Events.Clear();
            foreach (var evento in events)
            {
                Events.Add(evento);
            }
        }*/
    }
}
