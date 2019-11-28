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

            NavigationPage signalR = new NavigationPage(new SignalR())
            {
                IconImageSource = "historial2.png",
                Title = "Historial"
            };
            NavigationPage home = new NavigationPage(new Home())
            {
                IconImageSource = "dashboard2.png",
                Title = "Home"
            }; 
            NavigationPage map = new NavigationPage(new Mapa2())
            {
                IconImageSource = "mapa2.png",
                Title = "Mapa"
            }; 
            NavigationPage report = new NavigationPage(new Reportes())
            {
                IconImageSource = "reportes2.png",
                Title = "Reportes"
            }; 
            NavigationPage user = new NavigationPage(new Usuario())
            {
                IconImageSource = "usuario2.png",
                Title = "Perfil"
            };

            this.Children.Add(home);
            this.Children.Add(signalR);
            this.Children.Add(map);
            this.Children.Add(report);
            this.Children.Add(user);
        }
    }
}