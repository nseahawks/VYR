using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;

namespace VYRMobile.Helper
{
    public class FirebaseHelper
    {
        FirebaseStorage firebaseStorage = new FirebaseStorage("vyr-xfirebase.appspot.com");

        public async Task<string> Upload(Stream imageStream, string imageName)
        {
            var imageUrl = await firebaseStorage
                .Child("Report")
                .Child(imageName)
                .PutAsync(imageStream);
            
            return imageUrl;
        }
        public async Task<string> GetFile(string imageName)
        {
            return await firebaseStorage
                .Child("Report")
                .Child(imageName)
                .GetDownloadUrlAsync();
        }
    }
}
