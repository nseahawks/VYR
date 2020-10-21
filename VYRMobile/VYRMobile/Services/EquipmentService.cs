using Plugin.CloudFirestore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VYRMobile.Models;
using Xamarin.Forms;

namespace VYRMobile.Services
{
    public class EquipmentService
    {
        private static EquipmentService _instance;
        public static EquipmentService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EquipmentService();

                return _instance;
            }
        }

        public async Task<IEnumerable<EquipmentItem>> GetEquipment(string _userId)
        {
                var items = await CrossCloudFirestore.Current.Instance
                                            .GetCollection("Items")
                                            .GetDocument(_userId)
                                            .GetCollection("Item")
                                            .GetDocumentsAsync();
                IEnumerable<EquipmentItem> itemslist = items.ToObjects<EquipmentItem>();
                return itemslist;
        }
        public async Task<bool> SetEquipment(ObservableCollection<EquipmentItem> equipmentCollection, string _userId)
        {
            await Task.Delay(1000);

            return true;
            /*try
            {
                foreach (var equipmentItem in equipmentCollection)
                {
                    await CrossCloudFirestore.Current.Instance
                        .GetCollection("Items")
                        .GetDocument(_userId)
                        .GetCollection("Item")
                        .GetDocument(equipmentItem.Name)
                        .UpdateDataAsync(new { Toggle = equipmentItem.Toggle });
                }

                DependencyService.Get<IToast>().LongToast("Actualizado exitosamente");
                return true;
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo conectar con el servidor", "Aceptar");
                return false;
            }*/
        }
    }
}
