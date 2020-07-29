using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VYRMobile.Data;
using VYRMobile.Models;

namespace VYRMobile.ViewModels
{
    public class ApplicationUserViewModel : INotifyPropertyChanged
    {
        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        private string job;
        public string Job
        {
            get { return job; }
            set
            {
                job = value;
                OnPropertyChanged(nameof(Job));
            }
        }
        private string schedule;
        public string Schedule
        {
            get { return schedule; }
            set
            {
                schedule = value;
                OnPropertyChanged(nameof(Schedule));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            {
                if (changed != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ApplicationUserViewModel()
        {
            LoadUser();
        }
        private async void LoadUser()
        {
            var user = await ReportsStore.Instance.GetUserAsync();

            user.FullName = user.FirstName + " " + user.LastName;

            Address = user.Address;
            City = user.City;
            Code = user.Code;
            Email = user.Email;
            FullName = user.FullName;
            Job = user.Job;
            Schedule = user.Schedule;
        }
    }
}
