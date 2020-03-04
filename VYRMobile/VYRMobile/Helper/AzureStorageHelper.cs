using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace VYRMobile.Helper
{
    public class AzureStorageHelper
    {
        readonly static CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=vyrstorage;AccountKey=ISSRsUEtrBDlsWSPIGu/K3KYvhy7nr13PCaPqGDqEwlOl+c5pEVcGgu5SqHriwvqys/w9i37nizrgsqYfdz4sQ==;EndpointSuffix=core.windows.net");
        readonly static CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();
        /*public async Task<string> UploadImage(Stream ImageStream)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=vyrstorage;AccountKey=ISSRsUEtrBDlsWSPIGu/K3KYvhy7nr13PCaPqGDqEwlOl+c5pEVcGgu5SqHriwvqys/w9i37nizrgsqYfdz4sQ==;EndpointSuffix=core.windows.net");
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            string uniqueName = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{uniqueName}.jpg");
            await blockBlob.UploadFromStreamAsync(ImageStream);

            string ImageUrl = blockBlob.Uri.OriginalString;

            return ImageUrl;
        }*/
        public async Task<string> SaveBlockBlob(string containerName, byte[] blob, string blobTitle)
        {
            var blobContainer = _blobClient.GetContainerReference(containerName);

            string uniqueName = Guid.NewGuid().ToString();
            var blockBlob = blobContainer.GetBlockBlobReference(uniqueName + blobTitle);
            await blockBlob.UploadFromByteArrayAsync(blob, 0, blob.Length);

            string link = blockBlob.StorageUri.PrimaryUri.ToString();

            return link;
        }
    }
}
