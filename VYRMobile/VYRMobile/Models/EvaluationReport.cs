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
        string img;
        public string Img
        {
            get => img;
            set => SetProperty(ref img, value);
        }
        string videos;
        public string Videos
        {
            get => videos;
            set => SetProperty(ref videos, value);
        }
        string audios;
        public string Audios
        {
            get => audios;
            set => SetProperty(ref audios, value);
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
    }
}
