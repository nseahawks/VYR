using Xamarin.Forms;

namespace VYRMobile.Views
{
    public partial class Estadisticas : ContentPage
    {
        public Estadisticas()
        {
            BindingContext = new ViewModels.ChartViewModel();
            InitializeComponent();
        }
    }
}