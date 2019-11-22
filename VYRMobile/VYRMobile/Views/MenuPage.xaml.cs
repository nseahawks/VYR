using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace VYRMobile.Views
{
    public partial class MenuPage : TabbedPage
    {
        public MenuPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            this.Children.Add(new Historial());
            this.Children.Add(new Home());
            this.Children.Add(new Mapa2());
            this.Children.Add(new Reportes());
            this.Children.Add(new Usuario());
        }
    }
}