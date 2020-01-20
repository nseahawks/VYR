using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public CallPopup()
        {
            InitializeComponent();

            call1.Clicked += call1_Clicked;
            call2.Clicked += call2_Clicked;
        }

        private void call1_Clicked(object sender, EventArgs e)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("911", "Emergencias");
            }
        }
        private void call2_Clicked(object sender, EventArgs e)
        {
            CrossMessaging.Current.PhoneDialer.MakePhoneCall("+18097966316", "Francisco Rojas");
        }
    }
}