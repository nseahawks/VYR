using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class PuntoViewModel : BindableObject
    {
        private ObservableCollection<Models.Punto> _puntos;

        public PuntoViewModel()
        {
            Puntos = new ObservableCollection<Models.Punto>();

            LoadData();
        }

        public ObservableCollection<Models.Punto> Puntos
        {
            get { return _puntos; }
            set
            {
                _puntos = value;
                OnPropertyChanged();
            }
        }
        private void LoadData()
        {
            var puntos = PuntoService.Instance.GetPuntos();
            Puntos.Clear();
            foreach (var punto in puntos)
            {
                Puntos.Add(punto);
            }
        }
    }
}
