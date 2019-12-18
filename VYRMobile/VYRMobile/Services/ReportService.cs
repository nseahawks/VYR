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
                new Models.Report { Title = "Central Guaricano M1026",  Created = DateTime.Now, TypeIcon = "asistencia.png"},
                new Models.Report { Title = "Central Villa Mella M1014",  Created = DateTime.Now, TypeIcon = "dano.png"},
                new Models.Report { Title = "El polvorin M1067", Created = DateTime.Now, TypeIcon = "robo.png"},
                new Models.Report { Title = "Colgate marañon", Created = DateTime.Now, TypeIcon = "alerta.png"},
                new Models.Report { Title = "PCS Sabana Perdida", Created = DateTime.Now, TypeIcon = "asistencia.png"},
                new Models.Report { Title = "Sabana Perdida 11 M1071", Created = DateTime.Now, TypeIcon = "dano.png"}
            };
        }
    }
}
