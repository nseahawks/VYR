using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using Entry = Microcharts.Entry;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Estadisticas : ContentPage
    {
        public Estadisticas()
        {
            BindingContext = new ViewModels.ChartViewModel();
            InitializeComponent();

            

            //var chart = new BarChart() { Entries = entries };
        }
    }
}