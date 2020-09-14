using MvvmHelpers;
using System.Collections.ObjectModel;
using VYRMobile.Models;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class SupervisionViewModel : ObservableObject
    {
        public Command ChangeStateCommand { get; set; }
        private ObservableCollection<CompanyLocation> antenas;
        public ObservableCollection<CompanyLocation> Antenas
        {
            get { return antenas; }
            set
            {
                antenas = value;
                OnPropertyChanged();
            }
        }
        public SupervisionViewModel()
        {
            Antenas = new ObservableCollection<CompanyLocation>()
            {
                new CompanyLocation{PointChecked = false},
                new CompanyLocation{PointChecked = true},
                new CompanyLocation{PointChecked = false}
            };

            ChangeStateCommand = new Command(ChangeAntennaState);
        }
        private void ChangeAntennaState()
        {
            foreach(var antena in Antenas)
            {
                if(antena.PointChecked == false)
                {
                    antena.PointChecked = true;
                }
                else
                {
                    antena.PointChecked = false;
                }
            }
        }
    }
}
