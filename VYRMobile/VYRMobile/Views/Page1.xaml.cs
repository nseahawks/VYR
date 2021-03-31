using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using VYRMobile.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1(string userName)
        {
            InitializeComponent();
            BindingContext = new TestViewModel();
            nameLbl.Text = userName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void example_Clicked(object sender, EventArgs e)
        {
            AnimateLayoutToLeft(list1);
            progressBar.Progress = 50;
            section1.Text = "Armas y equipos";
            currentSectionLbl.Text = "2";
            AnimateLayoutToLeft(list2);
        }

        private async void inspectionView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushPopupAsync(new CommentaryPopup());
        }
        private void AnimateLayoutToLeft(StackLayout stackLayout)
        {
            uint duration = 500;

            var animation = new Animation();

            if (progressBar.Progress == 25)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 1, 0, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 0, -300,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
            else if (progressBar.Progress == 50)
            {
                animation.WithConcurrent((f) => stackLayout.Opacity = f, 0, 1, Easing.Linear);

                animation.WithConcurrent(
                  (f) => stackLayout.TranslationX = f,
                  stackLayout.TranslationX + 300, 0,
                  Easing.Linear, 0, 1);

                stackLayout.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            }
        }
    }
}