using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using VYRMobile.Models;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class EquipmentViewModel : BindableObject
    {
        private readonly static EquipmentViewModel _instance = new EquipmentViewModel();
        public static EquipmentViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private readonly static EquipmentViewModel _customInstance = new EquipmentViewModel("");
        public static EquipmentViewModel CustomInstance
        {
            get
            {
                return _customInstance;
            }
        }
        private ObservableCollection<EquipmentItem> _equipment;
        public ObservableCollection<EquipmentItem> Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;
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
        private Color toggleColor;
        public Color ToggleColor
        {
            get { return toggleColor; }
            set
            {
                toggleColor = value;
                OnPropertyChanged(nameof(ToggleColor));
            }
        }
        private static bool isTrueForAll(EquipmentItem equipo)
        {
            return (equipo.Toggle == true);
        }
        public EquipmentViewModel()
        {
            Equipment = new ObservableCollection<EquipmentItem>();
            LoadData();

            ToggleColor = Color.FromHex("#01BD00");
        }
        public EquipmentViewModel(string _userId)
        {
            Equipment = new ObservableCollection<EquipmentItem>();
            LoadData(_userId);
            ToggleColor = Color.FromHex("#01BD00");
        }
        public async void LoadData([Optional] string _workerId)
        {
            try
            {
                string id;
                if(_workerId != null)
                {
                    id = _workerId;
                }
                else 
                {
                    id = App.ApplicationUserId;
                }
                var equipos = await EquipmentService.Instance.GetEquipos(id);
                Equipment.Clear();
                foreach (var equipo in equipos)
                {
                    Equipment.Add(equipo);
                }

                if (Equipment.Count == 0)
                {
                    IsEmpty = true;
                }
                else
                {
                    IsEmpty = false;
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "No es posible conectar con el servidor", "Aceptar");
                await App.Current.MainPage.Navigation.PopAsync();
                return;
            }
        }
        public bool getEquipos()
        {
            List<EquipmentItem> items = new List<EquipmentItem>();

            foreach(var item in Equipment)
            {
                items.Add(item);
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

        public List<EquipmentItem> getMissingEquipment()
        {
            List<EquipmentItem> items = new List<EquipmentItem>();

            foreach (var item in Equipment)
            {
                if(item.Toggle == false)
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
