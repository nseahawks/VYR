using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Services
{
    public class EventService
    {
        private static EventService _instance;

        public static EventService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventService();

                return _instance;
            }
        }

        public List<Models.Event> GetEvents()
        {
            // NOTE: In this sample the focus is on the UI. This is a Fake service.
            return new List<Models.Event>
            {
                new Models.Event { EventName = "Alarma", Owner = "Antena 3", Time = "hace 1 minuto", EventIcon = "alarma.png"},
                new Models.Event { EventName = "Fin de base", Owner = "Yo", Time = "hace 5 minutos", EventIcon = "base.png"},
                new Models.Event { EventName = "Inicio de base", Owner = "Yo", Time = "hace 1 hora", EventIcon = "base.png"},
                new Models.Event { EventName = "Ruta terminada", Owner = "Yo", Time = "hace 2 horas", EventIcon = "endRoute.png"},
                new Models.Event { EventName = "Llamada", Owner = "Monitoreo", Time = "hace 3 horas", EventIcon = "callM.png"},
                new Models.Event { EventName = "Ruta iniciada", Owner = "Yo", Time = "hace 3 horas", EventIcon = "startRoute.png"}
            };
        }
    }
}
