using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VYRMobile.Models
{
    public class Report : ObservableObject
    {
        private static Report _instance;

        public static Report Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Report();

                return _instance;
            }
        }
        string userId;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
        string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        string typeIcon;
        public string TypeIcon
        {
            get => typeIcon;
            set => SetProperty(ref typeIcon, value);
        }

        string address;
        public string Address
        { 
            get => address;
            set => SetProperty(ref address, value); 
        }

        ReportTypes reportType;
        public ReportTypes ReportType
        {
            get => reportType;
            set => SetProperty(ref reportType, value);
        }

        public Color Color { get; set; }

        public enum ReportTypes
        {
            Default = 0,
            Alarma = 1,
            Robo = 2,
            Asistencia = 3,
            Daño = 4
        }

        ReportStatuses reportStatus;
        public ReportStatuses ReportStatus
        {
            get => reportStatus;
            set => SetProperty(ref reportStatus, value);
        }

        public enum ReportStatuses
        {
            Default = 0,
            Completo = 1,
            Incompleto = 2,
            PorAsignar = 3
        }
        public List<ReportStatuses> GetStatuses()
        {
            var statuses = new List<ReportStatuses>()
            {
                
            };

            /*foreach (var status in statuses)
            {
                statuses.Add(status);
            }*/

            return statuses;
        }
        string description;
        public string Description { 
            get => description; 
            set => SetProperty(ref description, value); 
        }
        Uri img;
        public Uri Img { 
            get => img;
            set => SetProperty(ref img, value); 
        }
        
        DateTimeOffset created;
        public DateTimeOffset Created { 
            get => created; 
            set => SetProperty(ref created, value); 
        }
        DateTimeOffset resolveDate;
        public DateTimeOffset ResolveDate { 
            get => resolveDate; 
            set => SetProperty(ref resolveDate, value); 
        }
    }
}
