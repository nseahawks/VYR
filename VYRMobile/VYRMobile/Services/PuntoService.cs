﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<List<Models.Punto>> GetPuntos()
        {
            var response = new List<Models.Punto>
            {
                new Models.Punto { PointId = "1", PointName = "Central Seahawks", PointChecked = false},
                new Models.Punto { PointId = "2", PointName = "Central Guaricano M1026", PointChecked = false},
                new Models.Punto { PointId = "3", PointName = "Central Villa Mella M1014", PointChecked = false},
                new Models.Punto { PointId = "4", PointName = "El Polvorin", PointChecked = false},
                new Models.Punto { PointId = "5", PointName = "Colgate Marañon", PointChecked = false},
                new Models.Punto { PointId = "6", PointName = "PCS Sabana Perdida", PointChecked = false},
                new Models.Punto { PointId = "7", PointName = "Sabana Perdida", PointChecked = false}
            };

            return response;
        }
    }
}
