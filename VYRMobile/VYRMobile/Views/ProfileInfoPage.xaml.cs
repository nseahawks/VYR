using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileInfoPage : ContentPage
    {
        public ProfileInfoPage()
        {
            InitializeComponent();

            BindingContext = new ApplicationUserViewModel();
        }
    }
}