using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VYRMobile.Models
{
    public class Report : ObservableObject
    {
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
        Antena location;
        public Antena Location
        {
            get => location;
            set => SetProperty(ref location, value);
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

        private Color statusColor;
        public Color StatusColor 
        {
            get => statusColor;
            set => SetProperty(ref statusColor, value);
        } 

        public enum ReportTypes
        {
            Alarma,
            Robo,
            Asistencia,
            Daño
        }

        ReportStatuses reportStatus;
        public ReportStatuses ReportStatus
        {
            get => reportStatus;
            set => SetProperty(ref reportStatus, value);
        }
        public enum ReportStatuses
        {
            Abierto,
            Cerrado,
            PorAsignar
        }

        string description;
        public string Description 
        { 
            get => description; 
            set => SetProperty(ref description, value); 
        }
        string img;
        public string Img { 
            get => img;
            set => SetProperty(ref img, value); 
        }
        DateTime created;
        public DateTime Created { 
            get => created; 
            set => SetProperty(ref created, value); 
        }
        DateTime resolveDate;
        public DateTime ResolveDate { 
            get => resolveDate; 
            set => SetProperty(ref resolveDate, value); 
        }
    }
}
