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
        private Models.Image _currentPost;
        private string imgTotal;
        
        public TestViewModel()
        {
            InspectionItems = new ObservableCollection<InspectionItem>();

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
                Name = "El arma de fuego funciona correctamente",
                IsDone = true
            };
            InspectionItem two = new InspectionItem()
            {
                Id = "1",
                Name = "Tiene cartuchos/capsulas",
                IsDone = false
            };
            InspectionItem three = new InspectionItem()
            {
                Id = "2",
                Name = "Tiene capa",
                IsDone = true
            };
            InspectionItem four = new InspectionItem()
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

            InspectionItems.Add(one);
            InspectionItems.Add(two);
            InspectionItems.Add(three);
            InspectionItems.Add(four);
            InspectionItems.Add(five);
        }
    }
}
