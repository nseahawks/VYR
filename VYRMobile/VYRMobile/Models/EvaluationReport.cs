using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class EvaluationReport : ObservableObject
    {
        string reviewedUserId;
        public string ReviewedUserId
        {
            get => reviewedUserId;
            set => SetProperty(ref reviewedUserId, value);
        }
        DateTime created;
        public DateTime Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }
        DateTime eventDate;
        public DateTime EventDate
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
        List<Calculation> getCalculations;
        public List<Calculation> GetCalculations
        {
            get => getCalculations;
            set => SetProperty(ref getCalculations, value);
        }
        List<Fault> getFaults;
        public List<Fault> GetFaults
        {
            get => getFaults;
            set => SetProperty(ref getFaults, value);
        }

        public EvaluationReport()
        {
            EventDate = DateTime.Today;
        }
    }
}
