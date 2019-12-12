using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Services
{
    public class ReportService
    {
        private static ReportService _instance;

        public static ReportService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ReportService();

                return _instance;
            }
        }

        public List<Models.Report> GetReports()
        {
            // NOTE: In this sample the focus is on the UI. This is a Fake service.
            return new List<Models.Report>
            {
                /*new Models.Report { Title = "Central Guaricano M1026", ReportType = ReportType.1, Status = true, Created = DateTime.Now, TypeIcon = "asistencia.png"},
                new Models.Report { Title = "Central Villa Mella M1014", ReportType = "Daño", Status = true, Created = DateTime.Now, TypeIcon = "dano.png"},
                new Models.Report { Title = "El polvorin M1067", ReportType = "Robo", Status = true, Created = DateTime.Now, TypeIcon = "robo.png"},
                new Models.Report { Title = "Colgate marañon", ReportType = "Alarma", Status = true, Created = DateTime.Now, TypeIcon = "alerta.png"},
                new Models.Report { Title = "PCS Sabana Perdida", ReportType = "Asistencia", Status = true, Created = DateTime.Now, TypeIcon = "asistencia.png"},
                new Models.Report { Title = "Sabana Perdida 11 M1071", ReportType = "Daño", Status = true, Created = DateTime.Now, TypeIcon = "dano.png"}*/
            };
        }
    }
}
