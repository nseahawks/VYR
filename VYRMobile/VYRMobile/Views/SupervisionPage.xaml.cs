using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VYRMobile.Data;
using VYRMobile.Helper;
using VYRMobile.Models;
using VYRMobile.Services;
using VYRMobile.Views.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupervisionPage : ContentPage
    {
        private ReportsStore _store { get; set; }
        FirebaseHelper _firebase = new FirebaseHelper();
        PermissionsHelper _permissions = new PermissionsHelper();
        public SupervisionPage()
        {
            InitializeComponent();

            //BindingContext = new SupervisionViewModel();

            _store = new ReportsStore();

            saveChangesButton.IsEnabled = false;
            TakePhoto();
            GetWorkers();

            btnNext.Clicked += BtnNext_Clicked;
            btnBack.Clicked += BtnBack_Clicked;
            sustituteButton.Clicked += SustituteButton_Clicked;
            validateButton.Clicked += ValidateButton_Clicked;
            //captionLabelClicked();
        }

        private async void SustituteButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new PickerPopup());
        }

        private void ValidateButton_Clicked(object sender, EventArgs e)
        {
            workerInfo.IsVisible = true;
            workerInfo.IsEnabled = true;
            workerInfo.Opacity = 0;

            OnWorkerInfoDisappearing();
            OnValidationViewAppearing();
        }
        private async void saveChangesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopup("Guardando..."));

            try
            {
                await ExchangeWorker();
            }
            catch
            {
                await DisplayAlert("Error", "Ocurrió un problema al procesar la información", "Aceptar");
            }

            await Navigation.PopPopupAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        IEnumerable<ApplicationUser> GetWorkers(string searchText = null)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                workersList.IsVisible = false;
                workersList.IsEnabled = false;
            }
            return App.Workers.Where(p => p.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (workersList.IsVisible == false)
            {
                workersList.IsVisible = true;
                workersList.IsEnabled = true;
                workersList.ItemsSource = GetWorkers(e.NewTextValue);
            }
            else
            {
                workersList.ItemsSource = GetWorkers(e.NewTextValue);
            }
        }

        private void exchangeCheckbox_CheckedChanged(object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            saveChangesButton.IsEnabled = true;
            notSavedLabel.IsVisible = true;
        }
        private void captionLabelClicked()
        {
            captionLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushPopupAsync(new CaptionPopup());
                }),
                NumberOfTapsRequired = 1
            });
        }
        private async void workersList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var sel = e.Item as ApplicationUser;
            App.WorkerOnReview = sel;

            workersList.IsVisible = false;
            workersList.IsEnabled = false;
            isEmptyLabel.IsVisible = false;
            saveChangesButton.IsEnabled = false;
            notSavedLabel.IsVisible = false;

            activityIndicator.IsVisible = true;

            if (workerInfo.Opacity == 1)
            {
                workerInfo.IsVisible = false;
                workerInfo.IsEnabled = false;
                workerInfo.Opacity = 0;
            }

            await Task.Delay(1000);

            activityIndicator.IsVisible = false;
            workerNameLabel.Text = sel.FullName;
            dateLabel.Text = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy");
            workerIdLabel.Text = sel.Id;
            workerScheduleLabel.Text = "Turno:" + sel.Schedule;

            if (sel.IsAssist == true)
            {
                isValidatedLabel.Text = "(validado)";
                isValidatedLabel.TextColor = Color.FromHex("#06C17C");
                validateButton.IsEnabled = false;
            }
            else
            {
                isValidatedLabel.Text = "(por validar)";
                isValidatedLabel.TextColor = Color.FromHex("#DD0808");
                validateButton.IsEnabled = true;
            }

            if (sel.Capacitated == true)
            {
                capacitatedImg.IsVisible = true;
            }
            else
            {
                capacitatedImg.IsVisible = false;
            }

            if (sel.Exchange == true)
            {
                exchangeCheckbox.IsChecked = true;
            }
            else
            {
                exchangeCheckbox.IsChecked = false;
            }

            OnWorkerInfoAppearing();

        }
        private void OnWorkerInfoAppearing()
        {
            uint duration = 250;

            var animation = new Animation();

            workerInfo.IsVisible = true;
            workerInfo.IsEnabled = true;

            animation.WithConcurrent((f) => workerInfo.Opacity = f, 0, 1, Easing.Linear);

            workerInfo.Animate("FadeOut", animation, 16, Convert.ToUInt32(duration));
        }
        private void OnWorkerInfoDisappearing()
        {
            uint duration = 250;

            var animation = new Animation();

            animation.WithConcurrent((f) => workerInfo.Opacity = f, 1, 0, Easing.Linear);

            workerInfo.IsVisible = false;
            workerInfo.IsEnabled = false;

            workerInfo.Animate("FadeOut", animation, 16, Convert.ToUInt32(duration));
        }
        private void OnValidationViewAppearing()
        {
            uint duration = 250;

            var animation = new Animation();

            validationView.IsVisible = true;
            validationView.IsEnabled = true;

            animation.WithConcurrent((f) => validationView.Opacity = f, 0, 1, Easing.Linear);

            validationView.Animate("FadeOut", animation, 16, Convert.ToUInt32(duration));
        }
        private void OnValidationViewDisappearing()
        {
            uint duration = 250;

            var animation = new Animation();

            animation.WithConcurrent((f) => validationView.Opacity = f, 1, 0, Easing.Linear);

            validationView.IsVisible = false;
            validationView.IsEnabled = false;

            validationView.Animate("FadeOut", animation, 16, Convert.ToUInt32(duration));
        }
        private async void GetWorkers()
        {
            try
            {
                var workers = await ReportsStore.Instance.GetUsersAsync();

                App.Workers.Clear();
                App.ExchangeableWorkers.Clear();

                foreach (var worker in workers)
                {
                    var lastEvaluatedDate = await SecureStorage.GetAsync("lastEvaluatedDate");
                    worker.FullName = worker.FirstName + " " + worker.LastName;

                    if (App.ExchangeableWorkers.Contains(worker) == false && worker.Exchange == true)
                    {
                        App.ExchangeableWorkers.Add(worker);
                    }

                    if (lastEvaluatedDate != DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        worker.IsAssist = false;
                        App.Workers.Add(worker);
                    }
                    else
                    {
                        App.Workers.Add(worker);
                    }
                }

                await SecureStorage.SetAsync("lastEvaluatedDate", DateTime.Now.ToString("dd/MM/yyyy"));
            }
            catch
            {
                await DisplayAlert("Error", "No se puede procesar la informacion en este momento", "Aceptar");
            }
            
        }
        private void BtnNext_Clicked(object sender, EventArgs e)
        {
            if (step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                AnimateLayoutToLeft(codeLayout);
                step1.Status = Syncfusion.XForms.ProgressBar.StepStatus.Completed;
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
                AnimateLayoutToLeft(imageLayout);
                btnNext.Text = "Terminar";
            }
            else if (step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                ConfirmValidation();
            }
        }
        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            if (step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.NotStarted)
            {
                OnValidationViewDisappearing();
                OnWorkerInfoAppearing();
            }
            else if (step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                AnimateLayoutToRight(imageLayout);
                step1.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.NotStarted;
                btnNext.Text = "Siguiente";
                AnimateLayoutToRight(codeLayout);
            }
            else if (step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.Completed)
            {
                AnimateLayoutToRight(imageLayout);
                btnNext.Text = "Siguiente";
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
            }
        }

        private void AnimateLayoutToRight(StackLayout stackLayout)
        {
            uint duration = 500;

            var animation = new Animation();

            if (step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 1, 0, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 0, 300,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
            else if (step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 0, 1, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 0, 0,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
        }
        private void AnimateLayoutToLeft(StackLayout stackLayout)
        {

            uint duration = 500;

            var animation = new Animation();

            if (step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 1, 0, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 0, -300,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
            else if (step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 0, 1, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 300, 0,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
        }
        private async void ConfirmValidation()
        {
            uint duration = 50;


            var animation = new Animation();

            animation.WithConcurrent((f) => imageLayout.Opacity = f, 1, 0, Easing.Linear);

            imageLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));

            var animation2 = new Animation();

            animation2.WithConcurrent((f) => loadingLayout.Opacity = f, 0, 1, Easing.Linear);

            loadingLayout.Animate("FadeIn", animation2, 16, Convert.ToUInt32(duration));

            indicator.IsRunning = true;

            try
            {
                var photoLink = await UploadPhoto();

                if (string.IsNullOrEmpty(photoLink) == false)
                {
                    await Validate(photoLink);
                }
            }
            catch
            {
                await DisplayAlert("Error", "Ocurrió un problema al procesar la información", "Aceptar");
            }

            indicator.IsRunning = false;

            var animation3 = new Animation();

            animation3.WithConcurrent((f) => loadingLayout.Opacity = f, 1, 0, Easing.Linear);

            loadingLayout.Animate("FadeIn", animation3, 16, Convert.ToUInt32(duration));
            AnimateLayoutToRight(imageLayout);
            AnimateLayoutToRight(codeLayout);
            ///codeLayout.Opacity = 1;
            codeEntry.Text = "";
            pic.Source = ImageSource.FromFile("camera2.png");
            //pic.Aspect = Aspect.AspectFit;

            OnValidationViewDisappearing();

            step1.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
            step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.NotStarted;
            btnNext.Text = "Siguiente";
            OnWorkerInfoAppearing();
        }
        private void TakePhoto()
        {
            photoFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    bool isCameraPermited = await _permissions.CheckCameraPermissionsStatus();

                    if(!isCameraPermited) 
                    {
                        await DisplayAlert("Denegado", "Otorga los permisos de la camara para continuar", "Aceptar");
                    }
                    else
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

                        App.ImagesNames.Clear();
                        App.ImagesStreams.Clear();

                        App.ImagesNames.Add(Path.GetFileName(file.Path));
                        App.ImagesStreams.Add(file.GetStream());

                        pic.Aspect = Aspect.AspectFill;
                        pic.Source = ImageSource.FromStream(() => file.GetStream());
                    }
                }),
                NumberOfTapsRequired = 1
            });
        }
        private async Task<string> UploadPhoto()
        {
            var UserId = await SecureStorage.GetAsync("id");

            if (App.ImagesStreams.Count != 0 && App.ImagesNames.Count != 0)
            {
                await _firebase.RunList(App.ImagesStreams, App.ImagesNames, UserId, DateTime.UtcNow);
                var link = await _firebase.GetLink(App.ImagesNames, UserId, DateTime.UtcNow);
                return link;
            }
            else
            {
                await DisplayAlert("Incompleto", "Falta imagen de comprobación", "OK");
                return null;
            }
        }
        private async Task<bool> Validate(string photoLink)
        {
            string workerCode = codeEntry.Text;
            string workerId = workerIdLabel.Text;
            var IsValidated = await _store.ValidateWorkerAsync(workerId, workerCode, photoLink);

            if (IsValidated == true)
            {
                isValidatedLabel.Text = "(validado)";
                isValidatedLabel.TextColor = Color.FromHex("#06C17C");
                validateButton.IsEnabled = false;

                var oldUser = App.Workers.Find(p => p.Id.Equals(workerIdLabel.Text));
                var newUser = oldUser;
                newUser.IsAssist = true;

                App.Workers[App.Workers.FindIndex(ind => ind.Equals(oldUser))] = newUser;
                workersList.ItemsSource = App.Workers;


                return IsValidated;
            }
            else
            {
                return IsValidated;
            }
        }
        private async Task ExchangeWorker()
        {
            bool IsExchanged = await _store.ExchangeWorkerAsync(App.WorkerOnReview.Id, exchangeCheckbox.IsChecked);

            if (IsExchanged == true)
            {
                var oldUser = App.Workers.Find(p => p.Id.Equals(App.WorkerOnReview.Id));
                var newUser = oldUser;
                newUser.Exchange = exchangeCheckbox.IsChecked;

                App.Workers[App.Workers.FindIndex(ind => ind.Equals(oldUser))] = newUser;

                workersList.ItemsSource = App.Workers;


                if (App.ExchangeableWorkers.Contains(oldUser) || newUser.Exchange == false)
                {
                    App.ExchangeableWorkers.Remove(oldUser);
                }
                else if (App.ExchangeableWorkers.Contains(oldUser) == false || newUser.Exchange == true)
                {
                    App.ExchangeableWorkers.Add(newUser);
                }

                App.ExchangeableWorkers = new System.Collections.ObjectModel.ObservableCollection<ApplicationUser>(App.ExchangeableWorkers.OrderByDescending(workers => workers.FullName));


                DependencyService.Get<IToast>().LongToast("Hecho");

                saveChangesButton.IsEnabled = false;
                notSavedLabel.IsVisible = false;
            }
            else
            {
                DependencyService.Get<IToast>().LongToast("Fallido");
            }
        }
    }
}