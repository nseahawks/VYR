using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventItemViewCell : ViewCell
    {
        public EventItemViewCell()
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

            animation.WithConcurrent((f) => EventItemTemplate.Opacity = f, 0, 1, Easing.CubicOut);

            animation.WithConcurrent(
              (f) => EventItemTemplate.TranslationY = f,
              EventItemTemplate.TranslationY + 50, 0,
              Easing.CubicOut, 0, 1);

            EventItemTemplate.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
        }
    }
}