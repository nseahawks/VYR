using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class EventViewModel : BindableObject
    {
        private ObservableCollection<Models.Event> _events;
        
        public EventViewModel() 
        {
            Events = new ObservableCollection<Models.Event>();
            LoadData();
        }
        public ObservableCollection<Models.Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged();
            }
        }

        private void LoadData()
        {
            var events = EventService.Instance.GetEvents();

            Events.Clear();
            foreach (var evento in events)
            {
                Events.Add(evento);
            }
        }
    }
}
