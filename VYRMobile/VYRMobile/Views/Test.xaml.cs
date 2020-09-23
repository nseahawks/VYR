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
        List<SfButton> QualificationButtons;
        public Test()
        {
            InitializeComponent();
            QualificationButtons = new List<SfButton>();
            QualificationButtons.Add(veryGoodButton);
            QualificationButtons.Add(goodButton);
            QualificationButtons.Add(regularButton);
            QualificationButtons.Add(badButton);
        }
        private void qualificationButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as SfButton;

            button.IsEnabled = false;

            foreach(var qualificatonButton in QualificationButtons)
            {
                if(button == qualificatonButton)
                {
                    qualificatonButton.IsEnabled = false;
                }
                else if(qualificatonButton.IsEnabled == false)
                {
                    qualificatonButton.IsEnabled = true;
                }
            }
        }
    }
}