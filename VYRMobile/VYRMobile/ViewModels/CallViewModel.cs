using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using VYRMobile.Services;

namespace VYRMobile.ViewModels
{
    public class CallViewModel : BindableObject
    {
        public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);

      

        private void ItemSelected(string parameter)
        {
            
        }
    }
}
