using System;
using System.Collections.Generic;

namespace VYRMobile.Services
{
    public class OptionService
    {
        private static OptionService _instance;

        public static OptionService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OptionService();

                return _instance;
            }
        }

        public List<Models.Option> GetOptions()
        {
            // NOTE: In this sample the focus is on the UI. This is a Fake service.
            return new List<Models.Option>
            {
                new Models.Option { OptionName = "CONFIGURACION", OptionIcon= "configuracion.png"},
                new Models.Option { OptionName = "EQUIPAMIENTO", OptionIcon= "equipo.png"},
                new Models.Option { OptionName = "OBJETIVOS", OptionIcon= "estadisticas.png"},
                new Models.Option { OptionName = "FORMACION", OptionIcon= "formacion.png"}
            };
        }
    }
}
