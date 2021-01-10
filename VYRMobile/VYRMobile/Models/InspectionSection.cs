using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class InspectionSection : ObservableObject
    {
        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        List<InspectionItem> items;
        public List<InspectionItem> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }
    }
}
