using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VYRMobile.Services
{
    public class TareaService
    {
        private static TareaService _instance;

        public static TareaService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TareaService();

                return _instance;
            }
        }

        public async Task<List<Models.Tarea>> GetTareas()
        {
            // NOTE: In this sample the focus is on the UI. This is a Fake service.
            return new List<Models.Tarea>
            {
                new Models.Tarea { TaskName = "Cristal Roto", TaskDate= "hace 5 dias"},
                new Models.Tarea { TaskName = "Luz Dañada", TaskDate= "hace 1 semana"},
                new Models.Tarea { TaskName = "Pared Agrietada", TaskDate= "hace 2 semanas"},
                new Models.Tarea { TaskName = "Trinchera caida", TaskDate= "hace 1 mes"}
            };
        }
    }
}
