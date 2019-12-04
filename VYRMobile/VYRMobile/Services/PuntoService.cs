﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Services
{
    public class PuntoService
    {
        private static PuntoService _instance;

        public static PuntoService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PuntoService();

                return _instance;
            }
        }

        public List<Models.Punto> GetPuntos()
        {
            // NOTE: In this sample the focus is on the UI. This is a Fake service.
            return new List<Models.Punto>
            {
                new Models.Punto { PointName = "Central Guaricano M1026", PointChecked = false},
                new Models.Punto { PointName = "Central Villa Mella M1014", PointChecked = false},
                new Models.Punto { PointName = "El Polvorin", PointChecked = false},
                new Models.Punto { PointName = "Colgate Marañon", PointChecked = false},
                new Models.Punto { PointName = "PCS Sabana Perdida", PointChecked = false},
                new Models.Punto { PointName = "Sabana Perdida", PointChecked = false}
            };
        }
    }
}