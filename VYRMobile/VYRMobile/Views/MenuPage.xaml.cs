using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;
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

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if(App.ApplicationUserRole == "Supervisor" || App.ApplicationUserRole == "Master")
            {
                NavigationPage supervision = new NavigationPage(new SupervisionPage())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "supervision.png",
                    Title = "Supervision"
                };
                NavigationPage home = new NavigationPage(new Home())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "home2.png",
                    Title = "Home"
                };
                NavigationPage map = new NavigationPage(new Mapa2())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "mapa2.png",
                    Title = "Mapa"
                };
                NavigationPage report = new NavigationPage(new Reportes())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "reportes2.png",
                    Title = "Reportes"
                };
                NavigationPage user = new NavigationPage(new Usuario())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "usuario2.png",
                    Title = "Perfil"
                };

                Children.Add(supervision);
                Children.Add(map);
                Children.Add(home);
                Children.Add(report);
                Children.Add(user);


                var pages = Children.GetEnumerator();
                pages.MoveNext();
                pages.MoveNext();
                pages.MoveNext();
                CurrentPage = pages.Current;
            }
            else if(App.ApplicationUserRole == "Patrol")
            {
                NavigationPage home = new NavigationPage(new Home())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "home2.png",
                    Title = "Home"
                };
                NavigationPage map = new NavigationPage(new Mapa2())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "mapa2.png",
                    Title = "Mapa"
                };
                NavigationPage report = new NavigationPage(new Reportes())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "reportes2.png",
                    Title = "Reportes"
                };
                NavigationPage user = new NavigationPage(new Usuario())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "usuario2.png",
                    Title = "Perfil"
                };

                Children.Add(map);
                Children.Add(home);
                Children.Add(report);
                Children.Add(user);


                var pages = Children.GetEnumerator();
                pages.MoveNext();
                pages.MoveNext();
                CurrentPage = pages.Current;
            }
            else if(App.ApplicationUserRole == "Vigilant")
            {
                NavigationPage home = new NavigationPage(new Home())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "home2.png",
                    Title = "Home"
                };
                NavigationPage report = new NavigationPage(new Reportes())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "reportes2.png",
                    Title = "Reportes"
                };
                NavigationPage user = new NavigationPage(new Usuario())
                {
                    BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                    BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
                    IconImageSource = "usuario2.png",
                    Title = "Perfil"
                };

                Children.Add(report);
                Children.Add(home);
                Children.Add(user);


                var pages = Children.GetEnumerator();
                pages.MoveNext();
                pages.MoveNext();
                CurrentPage = pages.Current;
            }
            else
            {
                DisplayAlert("Denegado", "Tu cuenta no tiene permitido ingresar a VYR-X App", "Aceptar");
            }

            ShowMapCommand = new Command(ShowMap);
        }
        private void ShowMap()
        {
            CurrentPage = Children[1];
        }
    }
}