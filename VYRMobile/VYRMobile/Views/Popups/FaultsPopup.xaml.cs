using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FaultsPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public FaultsPopup()
        {
            InitializeComponent();
            BindingContext = new StatsViewModel();
            OkLabelClicked();
            CancelLabelClicked();
        }
        private void OkLabelClicked()
        {
            okLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await AddFaults();
                }),
                NumberOfTapsRequired = 1
            });
        }
        private async Task AddFaults()
        {
            EvaluationPage ePage = new EvaluationPage();
            ePage.AddFaultsCommand.Execute(App.Faults);
        }
        private void CancelLabelClicked()
        {
            cancelLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    App.Faults.Clear();
                    await Navigation.PopPopupAsync();
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void SfCheckBox_StateChanged(object sender, StateChangedEventArgs e)
        {
            var checkBox = sender as SfCheckBox;
            Predicate<SfCheckBox> textFinder = (SfCheckBox t) => { return t.Text == checkBox.Text; };

            if (checkBox.IsChecked == true)
            {
                App.Faults.Add(checkBox);
            }
            else if(App.Faults.Exists(textFinder))
            {
                App.Faults.Remove(checkBox);
            }
            else
            {
                //Do nothing
            }
        }
    }
}