using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using VYRMobile.ViewModels;


namespace VYRMobile.Models
{
    public class ApplicationUser : ObservableObject
    {
        //
        string userId;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
        //
        string email;
        public string Email { 
            get => email;
            set => SetProperty(ref email, value); 
        }
        //
        private string password;
        public string Password { 
            get => password; 
            set => SetProperty(ref password, value);
        }
        private string supervisorUserId;
        public string SupervisorUserId
        {
            get => supervisorUserId;
            set => SetProperty(ref supervisorUserId, value);
        }
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
    }
}
