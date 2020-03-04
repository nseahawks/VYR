using Plugin.FilePicker;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Text;
using VYRMobile.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluationPage : ContentPage
    {
        public Command AddFaultsCommand { get; set; }
        private static EvaluationPage _instance;
        public static EvaluationPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EvaluationPage();

                return _instance;
            }
        }
        public EvaluationPage()
        {
            AddFaultsCommand = new Command<List<SfCheckBox>>(AddFaults);
        }
        public EvaluationPage(string User)
        {
            InitializeComponent();
            userLabel.Text = User;
            OkLabelClicked();
            CancelLabelClicked();
        }
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (segmentLayout.IsEnabled == false)
            {
                segmentLayout.IsEnabled = true;
                segmentLayout.Opacity = 1;
            }
            else
            {
                segmentLayout.IsEnabled = false;
                segmentLayout.Opacity = 0.5;
            }
        }

        private async void btnPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sin cámara", "No hay cámara disponible", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Name = "seahawks.png"
            });

            if (file == null)
                return;

            archive.Source = ImageSource.FromStream(() => file.GetStream());
            emptyLabel.IsVisible = false;
            archiveFrame.IsVisible = true;
        }
        private async void btnAttach_Clicked(object sender, EventArgs e)
        {
            try
            {
                var fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; 

                string fileName = fileData.FileName;
                string contents = Encoding.UTF8.GetString(fileData.DataArray);

                Console.WriteLine("File name chosen: " + fileName);
                Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }
        private void btnFaltas_Clicked(object sender, EventArgs e)
        {
            faultsView.IsVisible = true;
            faultsView.IsEnabled = true;
        }
        private async void AddFaults(List<SfCheckBox> checkBoxes)
        {
            foreach (var checkBox in checkBoxes)
            {
                AddFrames(checkBox);
            }
        }
        private void AddFrames(SfCheckBox checkBox)
        {
            Label label = new Label
            {
                Text = checkBox.Text,
                TextColor = Color.Black,
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };

            Image image = new Image
            {
                Source = ImageSource.FromFile("clear.png"),
                HeightRequest = 35,
                WidthRequest = 35,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 10, 0)
            };

            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
               
            };

            Frame frame = new Frame
            {
                BackgroundColor = Color.FromHex("#F9F8F8"),
                HeightRequest = 36,
                WidthRequest = 120,
                CornerRadius = 20,
                IsClippedToBounds = true,
                HasShadow = true,
                Padding = 0,
                Margin = new Thickness(15, 0, 15, 0),
            };

            stack.Children.Add(label);
            stack.Children.Add(image);
            frame.Content = stack;

            faultList.Children.Add(frame);
        }
        private void OkLabelClicked()
        {
            okLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    AddFaults(App.Faults);
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void CancelLabelClicked()
        {
            cancelLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    App.Faults.Clear();
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void SfCheckBox_StateChanged(object sender, StateChangedEventArgs e)
        {
            var checkBox = sender as SfCheckBox;
            Predicate<SfCheckBox> textFinder = (SfCheckBox t) => { return t.Text == checkBox.Text; };

            if (checkBox.IsChecked == true)
            {
                App.Faults.Add(checkBox);
            }
            else if (App.Faults.Exists(textFinder))
            {
                App.Faults.Remove(checkBox);
            }
            else
            {
                //Do nothing
            }
        }
    }
}