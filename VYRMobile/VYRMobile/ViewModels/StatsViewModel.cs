using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using VYRMobile.Data;
using VYRMobile.Models;
using VYRMobile.Services;

namespace VYRMobile.ViewModels
{
    public class StatsViewModel : ObservableObject
    {
        private ObservableCollection<ApplicationUser> _users;
        public ObservableCollection<ApplicationUser> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Antena> _locations;
        public ObservableCollection<Antena> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Stat> Data { get; set; }
        public ObservableCollection<Fault> Faults { get; set; }
        public StatsViewModel()
        {
            Users = new ObservableCollection<ApplicationUser>();
            Locations = new ObservableCollection<Antena>();

            Data = new ObservableCollection<Stat>()
            {
                new Stat("Educacion", 50),
                new Stat("Respeto", 70),
                new Stat("Puntualidad", 65),
                new Stat("Comportamiento", 57),
                new Stat("Incidentes", 48),
            };
            Faults = new ObservableCollection<Fault>()
            {
                new Fault("Uso inadecuado de las armas"),
                new Fault("No portar carnet de empleado"),
                new Fault("Uso inadecuado de la flota"),
                new Fault("Uso inadecuado del vehículo"),
                new Fault("Uso inadecuado de la radio"),
                new Fault("Abandono de servicio"),
                new Fault("Tiro zafado"),
                new Fault("Dormir en horas de trabajo")
            };

            LoadUsers();
            LoadLocations();
        }
        private async void LoadUsers()
        {
            var users = await ReportsStore.Instance.GetUsersAsync();

            Users.Clear();
            foreach(var user in users)
            {
                user.FullName = user.FirstName + " " + user.LastName;
                Users.Add(user);
            }
        }
        private async void LoadLocations()
        {
            var locations = await ReportsStore.Instance.GetAntenasAsync();

            Locations.Clear();
            foreach (var location in locations)
            {
                Locations.Add(location);
            }
        }
        public new event PropertyChangedEventHandler PropertyChanged;
        protected new virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            {
                if (changed != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
