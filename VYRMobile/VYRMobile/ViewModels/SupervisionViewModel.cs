using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Data;
using VYRMobile.Models;

namespace VYRMobile.ViewModels
{

    public class SupervisionViewModel : ObservableObject
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
        private ObservableCollection<ApplicationUser> _users2;
        public ObservableCollection<ApplicationUser> Users2
        {
            get { return _users2; }
            set
            {
                _users2 = value;
                OnPropertyChanged();
            }
        }
        public SupervisionViewModel()
        {
            Users = new ObservableCollection<ApplicationUser>() 
            {
                new ApplicationUser{FullName = "Jorge Perez", HasAttended="attended.png"},
                new ApplicationUser{FullName = "Carlos Martinez", HasAttended="undefined.png"},
                new ApplicationUser{FullName = "Santiago Cabrera", HasAttended="undefined.png"},
                new ApplicationUser{FullName = "Ramon Castillo", HasAttended="undefined.png"},
                new ApplicationUser{FullName = "Alberto Jaquez", HasAttended="undefined.png"},
                new ApplicationUser{FullName = "Roberto Quezada", HasAttended="undefined.png"},
                new ApplicationUser{FullName = "Javier Rodriguez", HasAttended="undefined.png"},
            };

            Users2 = new ObservableCollection<ApplicationUser>()
            {
                new ApplicationUser{FullName = "Andres Tejada", HasAttended="undefined.png"},
                new ApplicationUser{FullName = "Adrian Feliz", HasAttended="undefined.png"}
            };

            //LoadUsers();
        }
        public SupervisionViewModel(ApplicationUser applicationUser)
        {
            Users = new ObservableCollection<ApplicationUser>();

            LoadPickerUsers(applicationUser);
        }
        public SupervisionViewModel(ApplicationUser applicationUser, ApplicationUser applicationUser2)
        {
            Users = new ObservableCollection<ApplicationUser>();

            LoadUpdatedUsers(applicationUser, applicationUser2);
        }
        private async void LoadUsers()
        {
            var users = await ReportsStore.Instance.GetUsersAsync();

            Users.Clear();
            foreach (var user in users)
            {
                user.FullName = user.FirstName + " " + user.LastName;
                Users.Add(user);
            }
        }
        private async void LoadPickerUsers(ApplicationUser applicationUser)
        {
            var users = await ReportsStore.Instance.GetUsersAsync();

            Users.Clear();
            foreach (var user in users)
            {
                user.FullName = user.FirstName + " " + user.LastName;

                if(user.FullName != applicationUser.FullName)
                {
                    Users.Add(user);
                }
            }
        }
        private async void LoadUpdatedUsers(ApplicationUser applicationUser, ApplicationUser applicationUser2)
        {
            var users = await ReportsStore.Instance.GetUsersAsync();

            Users.Clear();
            foreach (var user in users)
            {
                user.FullName = user.FirstName + " " + user.LastName;

                if (user.FullName != applicationUser.FullName)
                {
                    Users.Add(user);
                }
                else
                {
                    Users.Add(applicationUser2);
                }
            }
        }
    }
}
