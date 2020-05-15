using System.Collections.ObjectModel;
using VYRMobile.Services;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class EquipoViewModel : BindableObject
    {
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

            if(Equipos.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
        }
    }
}
