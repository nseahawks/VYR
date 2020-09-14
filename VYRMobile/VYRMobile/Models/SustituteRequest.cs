using MvvmHelpers;

namespace VYRMobile.Models
{
    public class SustituteRequest : ObservableObject
    {

        private string schedule;
        public string Schedule
        {
            get => schedule;
            set => SetProperty(ref schedule, value);
        }
    }
}
