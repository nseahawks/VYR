using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Maps;
using VYRMobile.Views;

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : Xamarin.Forms.TabbedPage
    {
        public Dashboard()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            

            var historial = new Historial();
            var home = new Home();
            var mapa = new Mapa();
            var reportes = new Reportes();
            var usuario = new Usuario();

            this.Children.Add(historial);
            this.Children.Add(home);
            this.Children.Add(mapa);
            this.Children.Add(reportes);
            this.Children.Add(usuario);
        }

        
    }
}