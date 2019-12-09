using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class OptionViewModel : BindableObject
    {
        private ObservableCollection<Models.Option> _options;

        public OptionViewModel()
        {
            Options = new ObservableCollection<Models.Option>();

            LoadData();
        }
        public ObservableCollection<Models.Option> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged();
            }
        }
        private void LoadData()
        {
            var options = OptionService.Instance.GetOptions();
            Options.Clear();
            foreach (var option in options)
            {
                Options.Add(option);
            }
        }
    }
}
