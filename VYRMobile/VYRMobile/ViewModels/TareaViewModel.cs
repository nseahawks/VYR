﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class TareaViewModel : BindableObject
    {
        private ObservableCollection<Models.Tarea> _tareas;
        
        public TareaViewModel()
        {
            Tareas = new ObservableCollection<Models.Tarea>();

            LoadData();
        }
        public ObservableCollection<Models.Tarea> Tareas
        {
            get { return _tareas; }
            set
            {
                _tareas = value;
                OnPropertyChanged();
            }
        }
        private void LoadData()
        {
            var tareas = TareaService.Instance.GetTareas();
            Tareas.Clear();
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }
    }
}
