using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VYRMobile.Helper;
using VYRMobile.ViewModels;
using Plugin.FilePicker;
using Syncfusion.XForms.Buttons;
using VYRMobile.Services;

namespace VYRMobile.Views
{
    public partial class Test : ContentPage
    {
        public Test()
        {
            InitializeComponent();
            TakePhoto();

            btnNext.Clicked += BtnNext_Clicked;
            btnBack.Clicked += BtnBack_Clicked;
        } 

        private void BtnNext_Clicked(object sender, EventArgs e)
        {
            if(step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                AnimateLayoutToLeft(codeLayout);
                FadeButton(btnBack);
                step1.Status = Syncfusion.XForms.ProgressBar.StepStatus.Completed;
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
                AnimateLayoutToLeft(imageLayout);
                btnNext.Text = "Terminar";
            }
            else if(step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.Completed;
                ConfirmValidation();
            }
        }
        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            if(step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                AnimateLayoutToRight(imageLayout);
                FadeButton(btnBack);
                step1.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.NotStarted;
                AnimateLayoutToRight(codeLayout);
            }
            else if(step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.Completed)
            {
                AnimateLayoutToRight(imageLayout);
                btnNext.Text = "Siguiente";
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void AnimateLayoutToRight(StackLayout stackLayout)
        {
            uint duration = 500;

            var animation = new Animation();

            if(step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 1, 0, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 0, 300,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
            else if(step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
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

            if(step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 1, 0, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 0, -300,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
            else if(step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
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

            await Task.Delay(300);

            indicator.IsRunning = false;

            DependencyService.Get<IToast>().LongToast("Usuario validado");

            await Navigation.PopAsync();
        }
        private void FadeButton(SfButton button)
        {
            uint duration = 250;

            var animation = new Animation();

            if(button.Opacity == 1)
            {
                button.IsEnabled = false;

                animation.WithConcurrent((f) => button.Opacity = f, 1, 0, Easing.Linear);

                button.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
            else if(button.Opacity == 0)
            {
                button.IsEnabled = true;

                animation.WithConcurrent((f) => button.Opacity = f, 0, 1, Easing.Linear);

                button.Animate("FadeOut", animation, 16, Convert.ToUInt32(duration));
            }
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

                    App.ImagesNames.Clear();
                    App.ImagesStreams.Clear();

                    App.ImagesNames.Add(Path.GetFileName(file.Path));
                    App.ImagesStreams.Add(file.GetStream());

                    pic.Aspect = Aspect.AspectFill;
                    pic.Source = ImageSource.FromStream(() => file.GetStream());

                }),
                NumberOfTapsRequired = 1
            });
        }
    }
}