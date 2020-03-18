using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using Plugin.CloudFirestore;

namespace VYRMobile.Helper
{
    public class FirebaseHelper
    {
        FirebaseStorage firebaseStorage = new FirebaseStorage("vyr-x-270115.appspot.com");
        public async Task RunList(List<Stream> Streams, List<string> Names, string UserId, DateTime date)
        { 
            var streamsAndNames = Streams.Zip(Names, (s, n) => new { stream = s, name = n });

            foreach(var sn in streamsAndNames)
            {
                await Upload(sn.stream, UserId, sn.name, date);
            }
        }
        public async Task<string> Upload(Stream imageStream, string UserId, string imageName, DateTime date)
        {
            var imageUrl = await firebaseStorage
                .Child("Report")
                .Child(UserId)
                .Child(date.ToShortTimeString())
                .Child(imageName)
                .PutAsync(imageStream);
            
            return imageUrl;
        }

        public async Task<string> GetLink(List<string> Names, string UserId, DateTime date)
        {
            string Link = null;

            foreach (var name in Names)
            {
                if (Link == null)
                {
                    Link = await GetFile(name, UserId, date);
                }
                else
                {
                    Link = Link + ", " + await GetFile(name, UserId, date);
                }
            }
            return Link;
        }
        public async Task<string> GetFile(string imageName, string UserId, DateTime date)
        {
            var imageURL = await firebaseStorage
            .Child("Report")
            .Child(UserId)
            .Child(date.ToShortTimeString())
            .Child(imageName)
            .GetDownloadUrlAsync();

            return imageURL;
        }
    }
}
