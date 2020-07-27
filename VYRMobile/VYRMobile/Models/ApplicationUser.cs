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
        string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
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
        private string job;
        public string Job
        {
            get => job;
            set => SetProperty(ref job, value);
        }

        private string code;
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }
        private string address;
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        private string city;
        public string City
        {
            get => city;
            set => SetProperty(ref city, value);
        }
        private string supervisor;
        public string Supervisor
        {
            get => supervisor;
            set => SetProperty(ref supervisor, value);
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
        private string profilePicture;
        public string ProfilePicture
        {
            get => profilePicture;
            set => SetProperty(ref profilePicture, value);
        }
        private string schedule;
        public string Schedule
        {
            get => schedule;
            set => SetProperty(ref schedule, value);
        }
        private bool isAssist;
        public bool IsAssist
        {
            get => isAssist;
            set => SetProperty(ref isAssist, value);
        }
        private bool exchange;
        public bool Exchange
        {
            get => exchange;
            set => SetProperty(ref exchange, value);
        }
        private bool capacitated;
        public bool Capacitated
        {
            get => capacitated;
            set => SetProperty(ref capacitated, value);
        }
    }
}
