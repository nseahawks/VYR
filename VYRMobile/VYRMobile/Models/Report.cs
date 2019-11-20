 using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    class Report : ObservableObject
    {
        string userId;
        public string UserId
        {

            get => userId;
            set => SetProperty(ref userId, value);
        }
        string title;
        public string Title 
        { 
            get => title;
            set => SetProperty(ref title, value); 
        }
        string address;
        public string Address
        { 
            get => address;
            set => SetProperty(ref address, value); 
        }
        string description;
        public string Description { 
            get => description; 
            set => SetProperty(ref description, value); 
        }
        Uri img;
        public Uri Img { 
            get => img;
            set => SetProperty(ref img, value); 
        }
        bool status;
        public bool Status { 
            get => status; 
            set => SetProperty(ref status, value); 
        }
        DateTime created;
        public DateTime Created { 
            get => created; 
            set => SetProperty(ref created, value); 
        }
        DateTime resolveDate;
        public DateTime ResolveDate { 
            get => resolveDate; 
            set => SetProperty(ref resolveDate, value); 
        }
    }
}
