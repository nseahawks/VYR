﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipoItemViewCell : ViewCell
    {
        public EquipoItemViewCell()
        {
            InitializeComponent();
        }
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            uint duration = 750;

            // We are going to create a simple but nice animation. 
            // We will fade in at the same time we translade the cell view from the bottom to the top.
            var animation = new Animation();

            animation.WithConcurrent((f) => EquipoItemTemplate.Opacity = f, 0, 1, Easing.CubicOut);

            animation.WithConcurrent(
              (f) => EquipoItemTemplate.TranslationY = f,
              EquipoItemTemplate.TranslationY + 50, 0,
              Easing.CubicOut, 0, 1);

            EquipoItemTemplate.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
        }
    }
}