using MvvmHelpers;

namespace VYRMobile.Models
{
    public class Calculation : ObservableObject
    {
        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        string commentary;
        public string Commentary
        {
            get => commentary;
            set => SetProperty(ref commentary, value);
        }
        double percentage;
        public double Percentage
        {
            get => percentage;
            set => SetProperty(ref percentage, value);
        }
    }
}
