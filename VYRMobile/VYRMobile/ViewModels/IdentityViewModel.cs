using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    class IdentityViewModel : BaseViewModel
    {
        public ApplicationUser User { get; set; }
        public Command LoginCommand { get; }
        public Command TLoginCommand { get; set; }
        private IdentityService _identity { get; }

        //#region fields
        //string email;
        //string password;
        //#endregion

        #region properties
        public string Email
        {
            get { return User.Email; }
            set
            {
                User.Email = value;
                //email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return User.Password; }
            set
            {
                User.Password = value;
                //password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        public IdentityViewModel()
        {
            User = new ApplicationUser();
            _identity = new IdentityService();
            LoginCommand = new Command(async () => await LoginAsync());

        }

        private async Task LoginAsync()
        {
            App.IsUserLoggedIn = await _identity.LoginAsync(User);
            TLoginCommand.Execute(null);
        }
    }
}
