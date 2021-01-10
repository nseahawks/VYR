using Rg.Plugins.Popup.Extensions;
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
    public partial class CommentaryPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public CommentaryPopup()
        {
            InitializeComponent();
        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}