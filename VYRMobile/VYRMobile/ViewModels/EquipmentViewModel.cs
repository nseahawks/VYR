using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class EquipmentViewModel : BindableObject
    {
        public Command SetEquipmentCommand { get; set; }
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
        private List<EquipmentItem> _equipmentList;
        public List<EquipmentItem> EquipmentList
        {
            get { return _equipmentList; }
            set
            {
                _equipmentList = value;
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
        private bool needsJustification;
        public bool NeedsJustification
        {
            get { return needsJustification; }
            set
            {
                needsJustification = value;
                OnPropertyChanged(nameof(NeedsJustification));
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
            EquipmentList = new List<EquipmentItem>();
            LoadData("");

            ToggleColor = Color.FromHex("#01BD00");
        }
        public EquipmentViewModel(string _userId)
        {
            Equipment = new ObservableCollection<EquipmentItem>();
            //LoadData(_userId);
            ToggleColor = Color.FromHex("#01BD00");
        }
        public async void LoadData(string _workerId)
        { 
            string id;
            if (string.IsNullOrEmpty(_workerId))
            {
                id = _workerId;
            }
            else
            {
                id = App.ApplicationUserId;
            }
                //var equipos = await EquipmentService.Instance.GetEquipment(id);

            List<EquipmentItem> equipos = new List<EquipmentItem>()
            {
                new EquipmentItem()
                {
                    EquipmentId = "0",
                    Icon = "label.png",
                    Name = "Arma",
                    Toggle = true
                },
                new EquipmentItem()
                {
                    EquipmentId = "1",
                    Icon = "label.png",
                    Name = "Gorra",
                    Toggle = true
                },
                new EquipmentItem()
                {
                    EquipmentId = "2",
                    Icon = "label.png",
                    Name = "Camisa",
                    Toggle = true
                },
                new EquipmentItem()
                {
                    EquipmentId = "3",
                    Icon = "label.png",
                    Name = "Pantalones",
                    Toggle = true
                },
                new EquipmentItem()
                {
                    EquipmentId = "4",
                    Icon = "label.png",
                    Name = "Zapatos",
                    Toggle = true
                },
                new EquipmentItem()
                {
                    EquipmentId = "5",
                    Icon = "label.png",
                    Name = "Radio",
                    Toggle = true
                },
                new EquipmentItem()
                {
                    EquipmentId = "6",
                    Icon = "label.png",
                    Name = "Flota",
                    Toggle = true
                }
            };
            Equipment.Clear();
            EquipmentList.Clear();
            foreach (var equipo in equipos)
            {
                Equipment.Add(equipo);
                EquipmentList.Add(equipo);
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
        public bool getEquipment()
        {
            var isEquipmentReady = EquipmentList.TrueForAll(isTrueForAll);

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
        public async Task<bool> UpdateEquipment(string userId)
        {
            var isSuccess = await EquipmentService.Instance.SetEquipment(Equipment, userId);

            return isSuccess;
        }
        public void EnableOrDisableCommentaryBox()
        {
            var isEquipmentReady = EquipmentList.TrueForAll(isTrueForAll);

            if (isEquipmentReady)
            {
                NeedsJustification = false;
            }
            else
            {
                NeedsJustification = true;
            }
        }
    }
}
