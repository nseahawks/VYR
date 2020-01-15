using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Models;

namespace VYRMobile.ViewModels
{
    public class StatsViewModel : ObservableObject
    {
        public ObservableCollection<Stat> Data { get; set; }
        public StatsViewModel()
        {
            Data = new ObservableCollection<Stat>()
        {
            new Stat("Educacion", 50),
            new Stat("Respeto", 70),
            new Stat("Puntualidad", 65),
            new Stat("Comportamiento", 57),
            new Stat("Incidentes", 48),
        };
        }
    }
}
