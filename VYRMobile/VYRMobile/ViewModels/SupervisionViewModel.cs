using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VYRMobile.Data;
using VYRMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class SupervisionViewModel : ObservableObject
    {
        private readonly static SupervisionViewModel _instance = new SupervisionViewModel();
        public static SupervisionViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private ObservableCollection<ApplicationUser> _workers;
        public ObservableCollection<ApplicationUser> Workers
        {
            get { return _workers; }
            set
            {
                _workers = value;
                OnPropertyChanged();
            }
        }
        public SupervisionViewModel()
        {
            Workers = new ObservableCollection<ApplicationUser>();

            GetWorkers();
        }
        private async void GetWorkers()
        {
            try
            {
                //var workers = await ReportsStore.Instance.GetUsersAsync();

                List<ApplicationUser> workers = new List<ApplicationUser>() 
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
                App.Workers.Clear();
                App.ExchangeableWorkers.Clear();

                foreach (var worker in workers)
                {
                    var lastEvaluatedDate = await SecureStorage.GetAsync("lastEvaluatedDate");
                    worker.FullName = worker.FirstName + " " + worker.LastName;

                    if (App.ExchangeableWorkers.Contains(worker) == false && worker.Exchange == true)
                    {
                        App.ExchangeableWorkers.Add(worker);
                    }

                    if (lastEvaluatedDate != DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        worker.IsAssist = false;
                        App.Workers.Add(worker);
                        Workers.Add(worker);
                    }
                    else
                    {
                        App.Workers.Add(worker);
                        Workers.Add(worker);
                    }
                }

                await SecureStorage.SetAsync("lastEvaluatedDate", DateTime.Now.ToString("dd/MM/yyyy"));
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se puede procesar la informacion en este momento", "Aceptar");
            }
        }
    }
}
