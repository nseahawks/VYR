﻿using MvvmHelpers;

namespace VYRMobile.Models
{
    class TrackMessage : ObservableObject
    {
        string user;
        public string User {
            
            get => user;
            set => SetProperty(ref user, value);
        }
        
        string message;
        public string Message  { 
            get => message; 
            set => SetProperty(ref message, value);
        }
    }
}
