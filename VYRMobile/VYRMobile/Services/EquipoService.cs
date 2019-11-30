using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Services
{
    public class EquipoService
    {
        private static EquipoService _instance;

        public static EquipoService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EquipoService();

                return _instance;
            }
        }

        public List<Models.Equipo> GetEquipos()
        {
            // NOTE: In this sample the focus is on the UI. This is a Fake service.
            return new List<Models.Equipo>
            {
                new Models.Equipo { Icon = "label.png", Name = "Radio", Toggle = true},
                new Models.Equipo { Icon = "label.png", Name = "Uniforme", Toggle = true},
                new Models.Equipo { Icon = "label.png", Name = "Taser", Toggle = true},
                new Models.Equipo { Icon = "label.png", Name = "Pistola", Toggle = true},
                new Models.Equipo { Icon = "label.png", Name = "Gorra", Toggle = true},
                new Models.Equipo { Icon = "label.png", Name = "Linterna", Toggle = true},
                new Models.Equipo { Icon = "label.png", Name = "Botas", Toggle = true},
                new Models.Equipo { Icon = "label.png", Name = "Esposas", Toggle = true}
            };
        }
    }
}
