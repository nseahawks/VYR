using System.Collections.ObjectModel;
using Xamarin.Forms;
using VYRMobile.Models;

namespace VYRMobile.ViewModels
{
    public class TestViewModel : BindableObject
    {
        private ObservableCollection<ApplicationUser> _users;
        private Models.Image _currentPost;
        private string imgTotal;
        
        public TestViewModel()
        {
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
    }
}
