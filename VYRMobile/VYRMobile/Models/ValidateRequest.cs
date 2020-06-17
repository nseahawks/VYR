using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class ValidateRequest : ObservableObject
    {
        string userId;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
        private string supervisorId;
        public string SupervisorId
        {
            get => supervisorId;
            set => SetProperty(ref supervisorId, value);
        }
        private DateTime dateTime;
        public DateTime DateTime
        {
            get => dateTime;
            set => SetProperty(ref dateTime, value);

        }
        private bool isAssist;
        public bool IsAssist
        {
            get => isAssist;
            set => SetProperty(ref isAssist, value);
        }
        private string picture;
        public string Picture
        {
            get => picture;
            set => SetProperty(ref picture, value);
        }
    }
}
