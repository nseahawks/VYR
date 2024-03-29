﻿using MvvmHelpers;
using System;

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
        string code;
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
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
