using Plugin.CloudFirestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VYRMobile.Models;

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

        public async Task<IEnumerable<EquipmentItem>> GetEquipos(string _userId)
        {

                var items = await CrossCloudFirestore.Current.Instance
                                            .GetCollection("Items")
                                            .GetDocument(App.ApplicationUserId)
                                            .GetCollection("Item")
                                            .GetDocumentsAsync();
                IEnumerable<EquipmentItem> itemslist = items.ToObjects<EquipmentItem>();
                return itemslist;
        }
    }
}
