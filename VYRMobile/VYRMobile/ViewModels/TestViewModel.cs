using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using VYRMobile.Models;
using VYRMobile.Services;

namespace VYRMobile.ViewModels
{
    public class TestViewModel : BindableObject
    {
        private ObservableCollection<Models.Image> _posts;
        private Models.Image _currentPost;
        private string imgTotal;
        
        public TestViewModel()
        {
            //LoadPosts();
        }
        public ObservableCollection<Models.Image> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
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

        /*private void LoadPosts()
        {
            var posts = MockImageService.Instance.GetCommunityPosts();
            Posts = new ObservableCollection<Models.Image>(posts);
            CurrentPost = Posts[0];
            ImgTotal = posts.Count.ToString();
        }*/
    }
}
