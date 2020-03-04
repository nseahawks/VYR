using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace VYRMobile.Views
{
    public partial class MenuPage : TabbedPage
    {
        public Command ShowMapCommand { get; set; }
        private static MenuPage _instance;
        public static MenuPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MenuPage();

                return _instance;
            }
        }
        public MenuPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            NavigationPage historial = new NavigationPage(new Historial())
            {
                IconImageSource = "historial2.png",
                Title = "Historial"
            };
            NavigationPage home = new NavigationPage(new Home())
            {
                IconImageSource = "home2.png",
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
                IconImageSource = "touchWhite.png",
                Title = "Perfil"
            };

            Children.Add(historial);
            Children.Add(map);
            Children.Add(home);
            Children.Add(report);
            Children.Add(user);
            
            var pages = Children.GetEnumerator();
            pages.MoveNext();
            pages.MoveNext();
            pages.MoveNext();
            CurrentPage = pages.Current;

            ShowMapCommand = new Command(ShowMap);
        }
        private void ShowMap()
        {
            CurrentPage = Children[1];
        }
    }
}