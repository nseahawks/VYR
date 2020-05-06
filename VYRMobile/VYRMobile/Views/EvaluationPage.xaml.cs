using Plugin.FilePicker;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
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
            BindingContext = new ReportViewModel();
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

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions{ });

            if (file == null)
                return;

            string fileName = Path.GetFileName(file.Path);
            Stream fileStream = file.GetStream();
            string fileExtension = fileName.Substring(fileName.Length - 3);

            App.ImagesNames.Add(fileName);
            App.ImagesStreams.Add(fileStream);

            AddFiles(fileName, fileStream, fileExtension);
        }
        private async void btnAttach_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                await DisplayAlert("Error", "Tu dispositivo no soporta seleccionar archivos", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return;

            string fileName = Path.GetFileName(file.Path);
            Stream fileStream = file.GetStream();
            string fileExtension = fileName.Substring(fileName.Length - 3);

            App.ImagesNames.Add(fileName);
            App.ImagesStreams.Add(fileStream);

            AddFiles(fileName, fileStream, fileExtension);
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
                //Style = (Style)Application.Current.Resources["faultLabelStyle"],
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

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });

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

            grid.Children.Add(label, 0, 0);
            grid.Children.Add(image, 1, 0);
            frame.Content = grid;

            faultList.Children.Add(frame);
        }
        private void OkLabelClicked()
        {
            okLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    faultsView.IsVisible = false;
                    faultsView.IsEnabled = false;
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
                    faultsView.IsVisible = false;
                    faultsView.IsEnabled = false;
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
        private void btnEnviar_Clicked(object sender, EventArgs e)
        {
            if (_switch.IsToggled == true)
            {
                App.Calculations.Add(new Models.Calculation
                {
                    Name = "Respeto",
                    Commentary = editor1.Text,
                    Percentage = slider1.Value
                });
                App.Calculations.Add(new Models.Calculation
                {
                    Name = "Educación",
                    Commentary = editor2.Text,
                    Percentage = slider2.Value
                });
            }

            ReportViewModel.Instance.CreateEvaluationReportCommand.Execute(null);
        }
        private async void AddFiles(string fileName, Stream fileStream, string fileExtension)
        {
            Image image = new Image 
            {
                Aspect = Aspect.AspectFill
            };
            Label label = new Label
            {
                TextColor = Color.Black,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            Frame frame = new Frame
            {
                HeightRequest = 100,
                WidthRequest = 200,
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsClippedToBounds = true,
                Padding = 0,
                HasShadow = true,
                Margin = new Thickness(0, 0, 0, 25)
            };

            if(fileExtension == "png" || fileExtension == "jpg" || fileExtension == "peg")
            {
                image.Source = ImageSource.FromStream(() => fileStream);
                label.Text = fileName;
                
            }
            else if(fileExtension == "mp4")
            {
                image.Source = "video.png";
                label.Text = fileName;
            }
            else
            {
                await DisplayAlert("Error", "Formato de archivo no valido, por favor inténtelo de nuevo", "OK");
            }

            frame.Content = image;

            noFilesLayout.IsVisible = false;
            contentStack.Children.Add(label);
            contentStack.Children.Add(frame);
        }
    }
}