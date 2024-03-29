﻿using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Data;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationReportPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        CompanyLocation location;
        string imageName;
        Stream imageStream;
        public LocationReportPopup()
        {
            InitializeComponent();

            BindingContext = HomeViewModel.Instance;

            TakePhoto();
            sendBtn.Clicked += SendBtn_Clicked;
        }

        private async void SendBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopup("Cargando..."));

            try
            {
                bool isSuccess = await ReportsStore.Instance.AddTemporaryReportAsync(location, imageName, imageStream);
                if (isSuccess)
                {
                    App.AntennaId = location.Id.ToString();
                    HomePage.Instance.LocationCheckingCommand.Execute(null);
                }
            }
            catch
            {
                await DisplayAlert("Error", "Ocurrió un problema al procesar la información", "Aceptar");
            }

            await Navigation.PopAllPopupAsync();
        }

        private void TakePhoto()
        {
            photoFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Sin cámara", "No hay cámara disponible", "OK");
                        return;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions { });

                    if (file == null)
                        return;

                    imageName = Path.GetFileName(file.Path);
                    imageStream = file.GetStream();

                    pic.Source = ImageSource.FromStream(() => file.GetStream());
                    pic.Aspect = Aspect.AspectFill;

                }),
                NumberOfTapsRequired = 1
            });
        }

        private void locationsComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            location = e.Value as CompanyLocation;
        }
    }
}