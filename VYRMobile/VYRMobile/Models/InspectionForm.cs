using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class InspectionForm : ObservableObject
    {
        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        ApplicationUser worker;
        public ApplicationUser Worker
        {
            get => worker;
            set => SetProperty(ref worker, value);
        }
        string supervisorId;
        public string SupervisorId
        {
            get => supervisorId;
            set => SetProperty(ref supervisorId, value);
        }
        DateTime created;
        public DateTime Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }
        List<InspectionSection> sections;
        public List<InspectionSection> Sections
        {
            get => sections;
            set => SetProperty(ref sections, value);
        }
    }
}
