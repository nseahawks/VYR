using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class signalR : ContentPage
    {
        TrackViewModel vm;
        TrackViewModel VM
        {
            get => vm ?? (vm = (TrackViewModel)BindingContext);
        }
        public signalR()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.ConnectCommand.Execute(null);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VM.DisconnectCommand.Execute(null);
        }
    }
}