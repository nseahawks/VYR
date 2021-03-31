using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class ChangePassword : ObservableObject
    {
        string oldPassword;
        public string OldPassword
        {
            get => oldPassword;
            set => SetProperty(ref oldPassword, value);
        }
        string newPassword;
        public string NewPassword
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value);
        }
    }
}
