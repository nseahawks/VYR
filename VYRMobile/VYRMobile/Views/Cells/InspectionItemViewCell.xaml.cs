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
    public partial class InspectionItemViewCell : ViewCell
    {
        public InspectionItemViewCell()
        {
            InitializeComponent();
        }
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            uint duration = 750;

            var animation = new Animation();

            animation.WithConcurrent((f) => InspectionItemTemplate.Opacity = f, 0, 1, Easing.CubicOut);

            animation.WithConcurrent(
              (f) => InspectionItemTemplate.TranslationY = f,
              InspectionItemTemplate.TranslationY + 0, 50,
              Easing.CubicOut, 0, 1);

            InspectionItemTemplate.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
        }
    }
}