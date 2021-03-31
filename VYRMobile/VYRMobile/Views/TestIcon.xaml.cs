using Rg.Plugins.Popup.Extensions;
using VYRMobile.ViewModels;
using VYRMobile.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestIcon : ContentPage
    {
        private static TestIcon _instance;
        public static TestIcon Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TestIcon("");

                return _instance;
            }
        }
        public TestIcon()
        {
            InitializeComponent();
        }
        public TestIcon(string workerId)
        {
            InitializeComponent();

            BindingContext = new EquipmentViewModel(workerId);
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new NewPasswordPopup());
        }
    }
}