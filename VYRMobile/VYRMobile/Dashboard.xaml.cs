using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : MasterDetailPage
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Detail = new NavigationPage(new Detail());
            this.Master = new Menu();
        }
    }
}