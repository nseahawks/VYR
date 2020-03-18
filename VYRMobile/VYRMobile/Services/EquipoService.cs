using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using Xamarin.Essentials;

namespace VYRMobile.Services
{
    public class EquipoService
    {
        private static EquipoService _instance;

        public static EquipoService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EquipoService();

                return _instance;
            }
        }

        public async Task<IEnumerable<Equipo>> GetEquipos()
        {
            string _userId = await SecureStorage.GetAsync("id");
            var equipo = new Equipo {
                EquipmentId = "1",
                Icon  = "label.png",
                Name ="test",
                Toggle = false
                };

            //set test for firebase
            //await CrossCloudFirestore.Current.Instance
            //                             .GetCollection("usersApp")
            //                             .GetDocument(_userId)
            //                             .GetCollection("Items")
            //                             .GetDocument("0")
            //                             .SetDataAsync(equipo);
            //await CrossCloudFirestore.Current.Instance
            //                            .GetCollection("usersApp")
            //                            .GetDocument(_userId)
            //                            .GetCollection("Items")
            //                            .GetDocument("1")
            //                            .SetDataAsync(equipo);
            //await CrossCloudFirestore.Current.Instance
            //                            .GetCollection("usersApp")
            //                            .GetDocument(_userId)
            //                            .GetCollection("Items")
            //                            .GetDocument("2")
            //                            .SetDataAsync(equipo);
            // NOTE: In this sample the focus is on the UI. This is a Fake service.

            var items =  await CrossCloudFirestore.Current.Instance
                                        .GetCollection("usersApp")
                                        .GetDocument(_userId)
                                        .GetCollection("Items")
                                        .GetDocumentsAsync();
           

            IEnumerable<Equipo> myList = items.ToObjects<Equipo>();


            return myList;
                
            //    new List<Models.Equipo>
            //{
            //    new Models.Equipo { Icon = "label.png", Name = "Radio", Toggle = true},
            //    new Models.Equipo { Icon = "label.png", Name = "Uniforme", Toggle = true},
            //    new Models.Equipo { Icon = "label.png", Name = "Taser", Toggle = true},
            //    new Models.Equipo { Icon = "label.png", Name = "Pistola", Toggle = true},
            //    new Models.Equipo { Icon = "label.png", Name = "Gorra", Toggle = true},
            //    new Models.Equipo { Icon = "label.png", Name = "Linterna", Toggle = true},
            //    new Models.Equipo { Icon = "label.png", Name = "Botas", Toggle = true},
            //    new Models.Equipo { Icon = "label.png", Name = "Esposas", Toggle = true}
            //};
        }
    }
}
