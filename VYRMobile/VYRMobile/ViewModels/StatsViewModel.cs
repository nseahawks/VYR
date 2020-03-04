using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        public ObservableCollection<Stat> Data { get; set; }
        public ObservableCollection<Fault> Faults { get; set; }
        public StatsViewModel()
        {
            Users = new ObservableCollection<ApplicationUser>();

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
        }
        private async void LoadUsers()
        {
            var users = await UsersService.Instance.GetUsers();

            Users.Clear();
            foreach(var user in users)
            {
                Users.Add(user);
            }
        }
    }
}
