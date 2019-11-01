using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VYRMobile.Controls
{
    public class CrearReporteModel
    {
        public List<Estatus> EstatusList { get; set; }

        public CrearReporteModel()
        {
            EstatusList = GetEstatuses().OrderBy(t => t.Value).ToList();
        }

        public List<Estatus> GetEstatuses()
        {
            var estatuses = new List<Estatus>()
            {
                new Estatus(){Key = 1, Value = "Completo"},
                new Estatus(){Key = 2, Value = "Incompleto"}
            };

            return estatuses;
        }
    }

    public class Estatus
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
