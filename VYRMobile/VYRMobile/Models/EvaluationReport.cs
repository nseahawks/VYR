using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class EvaluationReport : ObservableObject
    {
        string reviewedUser;
        public string ReviewedUser
        {
            get => reviewedUser;
            set => SetProperty(ref reviewedUser, value);
        }
        DateTimeOffset created;
        public DateTimeOffset Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }
        DateTimeOffset eventDate;
        public DateTimeOffset EventDate
        {
            get => eventDate;
            set => SetProperty(ref eventDate, value);
        }
        string files;
        public string Files
        {
            get => files;
            set => SetProperty(ref files, value);
        }
        List<Calculation> calculations;
        public List<Calculation> Calculations
        {
            get => calculations;
            set => SetProperty(ref calculations, value);
        }
        List<Fault> faults;
        public List<Fault> Faults
        {
            get => faults;
            set => SetProperty(ref faults, value);
        }
    }
}
