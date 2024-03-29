﻿using System;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportDetailsPage : ContentPage
    {
        public ReportDetailsPage(string Title, string Description, string TypeIcon, Report.ReportTypes ReportType, Report.ReportStatuses ReportStatus, DateTime Created, Color StatusColor, string Img)
        {
            InitializeComponent();

            BindingContext = new ReportViewModel();
            //BindingContext = new ReportViewModel(Img);

            titleLabel.Text = Title;
            typeLabel.Text = ReportType.ToString();
            descriptionLabel.Text = Description;
            statusLabel.Text = ReportStatus.ToString();
            colorBoxView.BackgroundColor = StatusColor;
            dateLabel.Text = Created.ToString();
        }
    }
}