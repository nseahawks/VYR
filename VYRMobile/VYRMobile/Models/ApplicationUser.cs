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
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }
        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }
        private string fullName;
        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }
        private string shiftId;
        public string ShiftId
        {
            get => shiftId;
            set => SetProperty(ref shiftId, value);
        }
        private string hasAttended = "undefined.png";
        public string HasAttended
        {
            get => hasAttended;
            set => SetProperty(ref hasAttended, value);
        }
    }
}
