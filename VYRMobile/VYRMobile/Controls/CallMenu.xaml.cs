using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallMenu : ContentView
    {
        public static readonly BindableProperty SelectedCommandProperty =
           BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(CallMenu), null);

        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }
        public event EventHandler ItemTapped;
        private bool _isAnimating = false;
        private uint _animationDelay = 150;

        public CallMenu()
        {
            InitializeComponent();

            Close.IsVisible = false;
            Call.IsVisible = true;

            HandleMenuCenterClicked();
            HandleCloseClicked();
            HandleOptionsClicked();
        }
        private void HandleOptionsClicked()
        {
            HandleOptionClicked(Seahawks, "Seahawks");
            HandleOptionClicked(Claro, "Claro");
            HandleOptionClicked(Emergencias, "Emergencias");
        }

        private void HandleOptionClicked(Image image, string value)
        {
            image.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command( () =>
                {
                    ItemTapped?.Invoke(this, new SelectedItemChangedEventArgs(value));
                    CloseMenu();

                }),
                NumberOfTapsRequired = 1
            });
        }

        private void HandleCloseClicked()
        {
            Close.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await CloseMenu();
                }),
                NumberOfTapsRequired = 1
            });

        }

        private async Task CloseMenu()
        {
            if (!_isAnimating)
            {

                _isAnimating = true;

                Call.IsVisible = true;
                Close.IsVisible = true;
                await HideButtons();

                await Close.RotateTo(0, _animationDelay);
                await Close.FadeTo(0, _animationDelay);
                await Call.RotateTo(0, _animationDelay);
                await Call.FadeTo(1, _animationDelay);
                await Outer.ScaleTo(1, 100, Easing.Linear);
                Close.IsVisible = false;

                _isAnimating = false;
            }
        }

        private void HandleMenuCenterClicked()
        {
            Call.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    if (!_isAnimating)
                    {
                        _isAnimating = true;

                        Close.IsVisible = true;
                        Call.IsVisible = true;

                        await Call.RotateTo(360, _animationDelay);
                        await Call.FadeTo(0, _animationDelay);
                        await Close.RotateTo(360, _animationDelay);
                        await Close.FadeTo(1, _animationDelay);
                        await Outer.ScaleTo(3.3, 1000, Easing.Linear);
                        await ShowButtons();
                        Call.IsVisible = false;

                        _isAnimating = false;

                    }
                }),
                NumberOfTapsRequired = 1
            });
        }

        private async Task HideButtons()
        {
            var speed = 25U;
            await Seahawks.FadeTo(0, speed);
            await Claro.FadeTo(0, speed);
            await Emergencias.FadeTo(0, speed);
        }

        private async Task ShowButtons()
        {
            var speed = 25U;
            await Seahawks.FadeTo(1, speed);
            await Claro.FadeTo(1, speed);
            await Emergencias.FadeTo(1, speed);
        }
       
    }
}