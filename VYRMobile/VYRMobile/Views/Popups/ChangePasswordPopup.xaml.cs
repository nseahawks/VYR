using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ChangePasswordService _service { get; set; }
        public ChangePasswordPopup()
        {
            InitializeComponent();

            _service = new ChangePasswordService();

            btnNext.Clicked += BtnNext_Clicked;
            btnBack.Clicked += BtnBack_Clicked;
            OldPasswordOffClicked();
            OldPasswordOnClicked();
            NewPasswordOffClicked();
            NewPasswordOnClicked();
            ConfirmationPasswordOffClicked();
            ConfirmationPasswordOnClicked();
        }
        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            AnimateLayoutToRight(newPassLayout);
            FadeButton(btnBack);
            btnNext.Text = "Siguiente";
            step1.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
            step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.NotStarted;
            AnimateLayoutToRight(oldPassLayout);
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
        protected override bool OnBackgroundClicked()
        {
            return true;
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            if(step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress || step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.NotStarted)
            {
                AnimateLayoutToLeft(oldPassLayout);
                FadeButton(btnBack);
                step1.Status = Syncfusion.XForms.ProgressBar.StepStatus.Completed;
                step2.Status = Syncfusion.XForms.ProgressBar.StepStatus.InProgress;
                AnimateLayoutToLeft(newPassLayout);
                btnNext.Text = "Enviar";
            }
            else if(step1.Status == Syncfusion.XForms.ProgressBar.StepStatus.Completed || step2.Status == Syncfusion.XForms.ProgressBar.StepStatus.InProgress)
            {
                if (newPass.Text != confirmationPass.Text)
                {
                    await DisplayAlert("Error", "Las contraseñas no coinciden", "Aceptar");
                }
                else if (oldPass.Text == null || oldPass.Text == "" || newPass.Text == null || newPass.Text == "")
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Uno o más campos están vacíos", "Aceptar");
                }
                else if (!IsConnected)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Problemas al conectarse a internet", "Aceptar");
                }
                else if (newPass.Text.Length < 8)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "La contraseña es muy corta", "Aceptar");
                }
                else
                {
                    TryChange();
                }
            }
        }

        private void OldPasswordOffClicked()
        {
            oldPasswordOff.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img3.IsVisible = false;
                    img3.IsEnabled = false;
                    img4.IsVisible = true;
                    img4.IsEnabled = true;
                    oldPass.IsPassword = false;
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void OldPasswordOnClicked()
        {
            oldPasswordOn.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img4.IsVisible = false;
                    img4.IsEnabled = false;
                    img3.IsVisible = true;
                    img3.IsEnabled = true;
                    oldPass.IsPassword = true;
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void NewPasswordOffClicked()
        {
            newPasswordOff.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img1.IsVisible = false;
                    img1.IsEnabled = false;
                    img2.IsVisible = true;
                    img2.IsEnabled = true;
                    newPass.IsPassword = false;
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void NewPasswordOnClicked()
        {
            newPasswordOn.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img2.IsVisible = false;
                    img2.IsEnabled = false;
                    img1.IsVisible = true;
                    img1.IsEnabled = true;
                    newPass.IsPassword = true;
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void ConfirmationPasswordOffClicked()
        {
            confirmationPasswordOff.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img5.IsVisible = false;
                    img5.IsEnabled = false;
                    img6.IsVisible = true;
                    img6.IsEnabled = true;
                    confirmationPass.IsPassword = false;
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void ConfirmationPasswordOnClicked()
        {
            confirmationPasswordOn.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img6.IsVisible = false;
                    img6.IsEnabled = false;
                    img5.IsVisible = true;
                    img5.IsEnabled = true;
                    confirmationPass.IsPassword = true;
                }),
                NumberOfTapsRequired = 1
            });
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
        private void FadeButton(SfButton button)
        {
            uint duration = 250;

            var animation = new Animation();

            if (button.Opacity == 1)
            {
                button.IsEnabled = false;

                animation.WithConcurrent((f) => button.Opacity = f, 1, 0, Easing.Linear);

                button.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
            else if (button.Opacity == 0)
            {
                button.IsEnabled = true;

                animation.WithConcurrent((f) => button.Opacity = f, 0, 1, Easing.Linear);

                button.Animate("FadeOut", animation, 16, Convert.ToUInt32(duration));
            }
        }

        private async void TryChange()
        {
            uint duration = 50;


            var animation = new Animation();

            animation.WithConcurrent((f) => newPassLayout.Opacity = f, 1, 0, Easing.Linear);

            newPassLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));

            var animation2 = new Animation();

            animation2.WithConcurrent((f) => loadingLayout.Opacity = f, 0, 1, Easing.Linear);

            loadingLayout.Animate("FadeIn", animation2, 16, Convert.ToUInt32(duration));

            indicator.IsRunning = true;


            ChangePassword password = new ChangePassword()
            {
                OldPassword = oldPass.Text,
                NewPassword = newPass.Text
            };

            bool IsSuccess = await _service.ChangePasswordAsync(password);


            indicator.IsRunning = false;
            if (IsSuccess == true)
            {
                DependencyService.Get<IToast>().LongToast("Contraseña actualizada exitosamente");
                await Navigation.PopPopupAsync();
            }
            else
            {
                animation.WithConcurrent((f) => loadingLayout.Opacity = f, 1, 0, Easing.Linear);

                loadingLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));

                await App.Current.MainPage.DisplayAlert("Error", "Contraseña invalida, verifique e intente de nuevo", "Aceptar");


                animation2.WithConcurrent((f) => newPassLayout.Opacity = f, 0, 1, Easing.Linear);

                newPassLayout.Animate("FadeIn", animation2, 16, Convert.ToUInt32(duration));
            }
        }
    }
}