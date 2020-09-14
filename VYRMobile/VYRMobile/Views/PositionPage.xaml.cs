using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PositionPage : ContentPage
    {
        public PositionPage()
        {
            InitializeComponent();
            BindingContext = new StatsViewModel();
        }
    }
}