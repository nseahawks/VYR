using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VYRMobile.Controls
{
    public static class ColorAnimation
    {
        public static Task<bool> ColorTo(this Frame self, Color actualColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Color firstGreen = new Color();
            Color secondGreen = new Color();
            Color firstRed = new Color();
            Color secondRed = new Color();

            firstGreen = Color.FromHex("#5dea04");
            secondGreen = Color.FromHex("#54d800");
            firstRed = Color.FromHex("#fd0404");
            secondRed = Color.FromHex("#dd0707");

            if(actualColor == firstGreen)
            {
                Func<double, Color> transform = (t) =>
                  Color.FromRgba(actualColor.R + t * (secondGreen.R),
                                 actualColor.G + t * (secondGreen.G),
                                 actualColor.B + t * (secondGreen.B),
                                 actualColor.A + t * (secondGreen.A));

                return AnimateColor(self, "ColorTo", transform, callback, length, easing);
            }
            else if (actualColor == secondGreen)
            {
                Func<double, Color> transform = (t) =>
                  Color.FromRgba(actualColor.R + t * (firstGreen.R),
                                 actualColor.G + t * (firstGreen.G),
                                 actualColor.B + t * (firstGreen.B),
                                 actualColor.A + t * (firstGreen.A));

                return AnimateColor(self, "ColorTo", transform, callback, length, easing);
            }
            else if (actualColor == firstRed)
            {
                Func<double, Color> transform = (t) =>
                  Color.FromRgba(actualColor.R + t * (secondRed.R),
                                 actualColor.G + t * (secondRed.G),
                                 actualColor.B + t * (secondRed.B),
                                 actualColor.A + t * (secondRed.A));

                return AnimateColor(self, "ColorTo", transform, callback, length, easing);
            }
            else
            {
                Func<double, Color> transform = (t) =>
                  Color.FromRgba(actualColor.R + t * (firstRed.R),
                                 actualColor.G + t * (firstRed.G),
                                 actualColor.B + t * (firstRed.B),
                                 actualColor.A + t * (firstRed.A));

                return AnimateColor(self, "ColorTo", transform, callback, length, easing);
            }

        }

        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }

        static Task<bool> AnimateColor(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
    }
}
