using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    class QR : ObservableObject
    {
        string userId;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }

        string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        
        string hash;
        public string Hash
        {
            get => hash;
            set => SetProperty(ref hash, value
              /*  $"userId: {userId},message: {message}"*/);

        }

    }
}
