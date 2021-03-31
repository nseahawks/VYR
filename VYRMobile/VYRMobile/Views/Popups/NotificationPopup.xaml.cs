using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NotificationPopup()
        {
            InitializeComponent();

            BindingContext = new NotificationViewModel();
        }

        private void notificationsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var sel = e.Item as NotificationItem;

            Navigation.PushPopupAsync(new MessageDetailsPopup(sel.Message));
        }
    }
}