using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Views.Templates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportItemViewCell : ViewCell
    {
        public ReportItemViewCell()
        {
            InitializeComponent();
        }
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            uint duration = 750;

            var animation = new Animation();

            animation.WithConcurrent((f) => ReportItemTemplate.Opacity = f, 0, 1, Easing.CubicOut);

            animation.WithConcurrent(
              (f) => ReportItemTemplate.TranslationY = f,
              ReportItemTemplate.TranslationY + 50, 0,
              Easing.CubicOut, 0, 1);

            ReportItemTemplate.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
        }
    }
}