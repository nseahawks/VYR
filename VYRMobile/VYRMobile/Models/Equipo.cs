using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Equipo : ObservableObject
    {
        string equipmentId;
        public string EquipmentId
        {
            get => equipmentId;
            set => SetProperty(ref equipmentId, value);
        }

        string icon;
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        bool toggle;
        public bool Toggle
        {
            get => toggle;
            set => SetProperty(ref toggle, value);
        }
    }
}
