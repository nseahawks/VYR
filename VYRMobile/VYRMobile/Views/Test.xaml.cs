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

namespace VYRMobile.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        MediaFile file;
        FirebaseStorage firebaseStorage = new FirebaseStorage("gs://vyr-x-aec24.appspot.com");
        public Test()
        {
            InitializeComponent();
            //BindingContext = new TestViewModel();

            //btn.Clicked += Btn_Clicked;
        }

        private async void btnPick_Clicked(object sender, EventArgs e)
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
                await StoreImages(file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void btnStore_Clicked(object sender, EventArgs e)
        {
            await StoreImages(file.GetStream());
        }

        public async Task<string> StoreImages(Stream imageStream)
        {
            var storageImage = await firebaseStorage
                .Child("ReportImages")
                .Child("image.jpg")
                .PutAsync(imageStream);
            string imgurl = storageImage;
            return imgurl;
        }
    }
}