using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class InspectionItem : ObservableObject
    {
        string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        bool isDone;
        public bool IsDone
        {
            get => isDone;
            set => SetProperty(ref isDone, value);
        }
        bool hasCommentary;
        public bool HasCommentary
        {
            get => hasCommentary;
            set => SetProperty(ref hasCommentary, value);
        }
        string observations;
        public string Observations
        {
            get => observations;
            set => SetProperty(ref observations, value);
        }
    }
}
