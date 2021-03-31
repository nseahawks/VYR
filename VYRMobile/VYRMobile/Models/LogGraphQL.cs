using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class LogGraphQL : ObservableObject
    {
        string apiLog;
        public string ApiLog
        {
            get => apiLog;
            set => SetProperty(ref apiLog, value);
        }
        string company;
        public string Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }
        DateTime createdAt;
        public DateTime CreatedAt
        {
            get => createdAt;
            set => SetProperty(ref createdAt, value);
        }
        string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
    }
}
