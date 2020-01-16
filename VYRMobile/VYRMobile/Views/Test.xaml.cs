using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VYRMobile.Helper;
using VYRMobile.ViewModels;

namespace VYRMobile.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        MediaFile file;
        FirebaseHelper _firebase = new FirebaseHelper();
        public Test()
        {
            InitializeComponent();
            BindingContext = new CallViewModel();

            //btn.Clicked += Btn_Clicked;
        }

        /*private async void btnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }*/

        private async void btnStore_Clicked(object sender, EventArgs e)
        {
            await _firebase.Upload(file.GetStream(), Path.GetFileName(file.Path));
        }

        /*private async void btnDownload_Clicked(object sender, EventArgs e)
        {
            string path = await _firebase.GetFile();
            if (path != null)
            {
                lblPath.Text = path;
                await DisplayAlert("Success", path, "OK");
                imgChoosed.Source = path;
            }

        }*/
        /*public async Task<string> StoreImages(Stream imageStream, string imageName)
        {
            var imageUrl = await firebaseStorage
                .Child("ReportImages")
                .Child(imageName)
                .PutAsync(imageStream);
            return imageUrl;
        }*/
    }
}