using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportDetailsPage : ContentPage
    {
        public ReportDetailsPage(string Title, string Description, string TypeIcon, Report.ReportTypes ReportType, Report.ReportStatuses ReportStatus, DateTime Created, Color StatusColor)
        {
            InitializeComponent();

            iconImage.Source = TypeIcon;
            titleLabel.Text = Title;
            typeLabel.Text = ReportType.ToString();
            descriptionLabel.Text = Description;
            statusLabel.Text = ReportStatus.ToString();
            colorBoxView.BackgroundColor = StatusColor;
            dateLabel.Text = Created.ToString();
        }
    }
}