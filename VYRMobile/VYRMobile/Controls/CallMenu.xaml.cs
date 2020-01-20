﻿using Plugin.Messaging;
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
    public partial class CallMenu : ContentView
    {
        public static readonly BindableProperty SelectedCommandProperty =
           BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(CallMenu), null);

        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        //public event EventHandler ItemTapped;
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
            HandleOptionClicked(Seahawks, "+18097966316");
        }

        private void HandleOptionClicked(Image image, string value)
        {
            image.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                   await CloseMenu();

                    if (SelectedCommand?.CanExecute(value) ?? false)
                    {
                        SelectedCommand?.Execute(value);
                    }
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
                await Outer.ScaleTo(1, 150, Easing.CubicInOut);

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
                        await Outer.ScaleTo(3.3, 450, Easing.CubicInOut);
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
        }

        private async Task ShowButtons()
        {
            var speed = 25U;
            await Seahawks.FadeTo(1, speed);
        }
       
    }
}