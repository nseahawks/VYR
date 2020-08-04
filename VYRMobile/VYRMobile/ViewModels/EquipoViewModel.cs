using System.Collections.Generic;
using System.Collections.ObjectModel;
using VYRMobile.Models;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class EquipoViewModel : BindableObject
    {
        private readonly static EquipoViewModel _instance = new EquipoViewModel();
        public static EquipoViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private ObservableCollection<Models.Equipo> _equipos;
        public ObservableCollection<Models.Equipo> Equipos
        {
            get { return _equipos; }
            set
            {
                _equipos = value;
                OnPropertyChanged();
            }
        }
        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
        private static bool isTrueForAll(Equipo equipo)
        {
            return (equipo.Toggle == true);
        }
        public EquipoViewModel()
        {
            Equipos = new ObservableCollection<Models.Equipo>();
            LoadData();
        }
        private async void LoadData()
        {
            var equipos = await EquipoService.Instance.GetEquipos();
            Equipos.Clear();
            foreach (var equipo in equipos)
            {
                Equipos.Add(equipo);
            }

            if (Equipos.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
        }
        public bool getEquipos()
        {
            List<Equipo> items = new List<Equipo>();
            foreach(var equipo in Equipos)
            {
                items.Add(equipo);
            }
            var isEquipmentReady = items.TrueForAll(isTrueForAll);

            if(isEquipmentReady)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Equipo> getMissingEquipment()
        {
            List<Equipo> items = new List<Equipo>();

            foreach (var equipo in Equipos)
            {
                if(equipo.Toggle == false)
                {
                    items.Add(equipo);
                }
            }

            return items;
        }
    }
}
