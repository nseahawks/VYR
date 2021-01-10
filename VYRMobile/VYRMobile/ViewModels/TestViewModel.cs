using System.Collections.ObjectModel;
using Xamarin.Forms;
using VYRMobile.Models;

namespace VYRMobile.ViewModels
{
    public class TestViewModel : BindableObject
    {
        private ObservableCollection<ApplicationUser> _users;
        private ObservableCollection<InspectionItem> inspectionItems;
        public ObservableCollection<InspectionItem> InspectionItems
        {
            get { return inspectionItems; }
            set
            {
                inspectionItems = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<InspectionItem> inspectionItems2;
        public ObservableCollection<InspectionItem> InspectionItems2
        {
            get { return inspectionItems2; }
            set
            {
                inspectionItems2 = value;
                OnPropertyChanged();
            }
        }
        private Models.Image _currentPost;
        private string imgTotal;
        
        public TestViewModel()
        {
            InspectionItems = new ObservableCollection<InspectionItem>();
            InspectionItems2 = new ObservableCollection<InspectionItem>();

            LoadItems();
            //LoadPosts();
        }
        public ObservableCollection<ApplicationUser> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public Models.Image CurrentPost
        {
            get { return _currentPost; }
            set
            {
                _currentPost = value;
                OnPropertyChanged();
            }
        }
        
        public string ImgTotal
        {
            get { return imgTotal; }
            set 
            { 
                imgTotal = value;
                OnPropertyChanged();
            }
        }

        private void LoadUsers()
        {
            /*var users = UsersService.Instance.GetUsers();
            Users.Clear();

            foreach(var user in users)
            {
                Users.Add(user);
            }*/
        }
        private void LoadItems()
        {
            InspectionItem one = new InspectionItem()
            {
                Id = "0",
                Name = "En buen estado",
                IsDone = true
            };
            InspectionItem two = new InspectionItem()
            {
                Id = "1",
                Name = "Limpios",
                IsDone = false
            };
            InspectionItem three = new InspectionItem()
            {
                Id = "2",
                Name = "Completo ( camisa, pantalon, gorra, botas)",
                IsDone = true
            };
            InspectionItem four = new InspectionItem()
            {
                Id = "3",
                Name = "Carnet de la empresa",
                IsDone = false
            };

            InspectionItems.Add(one);
            InspectionItems.Add(two);
            InspectionItems.Add(three);
            InspectionItems.Add(four);

            InspectionItem onee = new InspectionItem()
            {
                Id = "0",
                Name = "El arma de fuego funciona correctamente",
                IsDone = true
            };
            InspectionItem twoo = new InspectionItem()
            {
                Id = "1",
                Name = "Tiene cartuchos/capsulas",
                IsDone = false
            };
            InspectionItem threee = new InspectionItem()
            {
                Id = "2",
                Name = "Tiene capa",
                IsDone = true
            };
            InspectionItem fourr = new InspectionItem()
            {
                Id = "3",
                Name = "Foco o linterna",
                IsDone = false
            };
            InspectionItem five = new InspectionItem()
            {
                Id = "4",
                Name = "Medio de comunicacion (flota, celular, radio)",
                IsDone = false
            };

            InspectionItems2.Add(one);
            InspectionItems2.Add(two);
            InspectionItems2.Add(three);
            InspectionItems2.Add(four);
            InspectionItems2.Add(five);
        }
    }
}
