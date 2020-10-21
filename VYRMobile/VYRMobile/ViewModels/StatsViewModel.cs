using MvvmHelpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using VYRMobile.Data;
using VYRMobile.Models;

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
        private ObservableCollection<CompanyLocation> _locations;
        public ObservableCollection<CompanyLocation> Locations
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
            Locations = new ObservableCollection<CompanyLocation>();

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
            List<ApplicationUser> users = new List<ApplicationUser>()
                {
                    new ApplicationUser()
                    {
                        Id = "1",
                        FirstName = "Jose",
                        LastName = "Calderon",
                        FullName = "Jose Calderon",
                        Code = "0055464",
                        Email = "josecalderon@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "2",
                        FirstName = "Felipe",
                        LastName = "Ramos",
                        FullName = "Felipe Ramos",
                        Code = "0055464",
                        Email = "feliperamos@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "3",
                        FirstName = "Adrian",
                        LastName = "Mejia",
                        FullName = "Adrian Mejia",
                        Code = "0055464",
                        Email = "adrianmejia@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "4",
                        FirstName = "Victor",
                        LastName = "Mercedes",
                        FullName = "Victor Mercedes",
                        Code = "0055464",
                        Email = "josecalderon@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "5",
                        FirstName = "Ramiro",
                        LastName = "Abreu",
                        FullName = "Ramiro Abreu",
                        Code = "0055464",
                        Email = "ramiroabreu@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "6",
                        FirstName = "Lucas",
                        LastName = "Rodriguez",
                        FullName = "Lucas Rodriguez",
                        Code = "0055464",
                        Email = "lucasrodriguez@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "7",
                        FirstName = "Fernando",
                        LastName = "Toribio",
                        FullName = "Fernando Toribio",
                        Code = "0055464",
                        Email = "fernandotoribio@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "8",
                        FirstName = "Diego",
                        LastName = "Martinez",
                        FullName = "Diego Martinez",
                        Code = "0055464",
                        Email = "diegomartinez@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "9",
                        FirstName = "Emilio",
                        LastName = "Sanchez",
                        FullName = "Emilio Sanchez",
                        Code = "0055464",
                        Email = "emiliosanchez@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "10",
                        FirstName = "Billy",
                        LastName = "Alvarez",
                        FullName = "Billy Alvarez",
                        Code = "0055464",
                        Email = "billyalvarez@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "11",
                        FirstName = "Raul",
                        LastName = "De la Cruz",
                        FullName = "Raul De la Cruz",
                        Code = "0055464",
                        Email = "rauldelacruz@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "12",
                        FirstName = "Fausto",
                        LastName = "Hernandez",
                        FullName = "Fausto Hernandez",
                        Code = "0055464",
                        Email = "faustohernandez@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "13",
                        FirstName = "Rodrigo",
                        LastName = "Peña",
                        FullName = "Rodrigo Peña",
                        Code = "0055464",
                        Email = "rodrigopeña@gmail.com",
                        Schedule = "6am - 6pm"
                    },
                    new ApplicationUser()
                    {
                        Id = "14",
                        FirstName = "Tony",
                        LastName = "Santana",
                        FullName = "Tony Santana",
                        Code = "0055464",
                        Email = "tonysantana@gmail.com",
                        Schedule = "6am - 6pm"
                    }
                };

            Users.Clear();
            foreach(var user in users)
            {
                user.FullName = user.FirstName + " " + user.LastName;
                Users.Add(user);
            }
        }
        private async void LoadLocations()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"VYRMobile.Antenas.locations.json");
            string locationsData;
            using (var reader = new System.IO.StreamReader(stream))
            {
                locationsData = reader.ReadToEnd();
            }

            var locations = JsonConvert.DeserializeObject<List<CompanyLocation>>(locationsData);

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
