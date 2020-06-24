using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Data;
using VYRMobile.Models;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{

    public class SupervisionViewModel : ObservableObject
    {
        public Command ChangeStateCommand { get; set; }
        private ObservableCollection<Antena> antenas;
        public ObservableCollection<Antena> Antenas
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
            Antenas = new ObservableCollection<Antena>()
            {
                new Antena{PointChecked = false},
                new Antena{PointChecked = true},
                new Antena{PointChecked = false}
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
