using MvvmHelpers;

namespace VYRMobile.Models
{
    public class Tarea : ObservableObject
    {
        string taskId;
        public string TaskId
        {
            get => taskId;
            set => SetProperty(ref taskId, value);
        }


        string taskName;
        public string TaskName
        {
            get => taskName;
            set => SetProperty(ref taskName, value);
        }

        string taskDate;
        public string TaskDate
        {
            get => taskDate;
            set => SetProperty(ref taskDate, value);
        }

    }
}
