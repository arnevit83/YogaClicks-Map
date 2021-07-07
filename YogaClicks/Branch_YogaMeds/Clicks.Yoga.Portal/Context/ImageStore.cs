using System;
using System.Configuration;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Clicks.Yoga.Portal.Context
{
    public class ImageStore : IImageStore
    {
        public void SaveImage(Image image)
        {
            if (image == null) throw new ArgumentNullException("image");
            if (image.Source == null) throw new ArgumentException("Image must have a source stream.");

            // TODO: Temporary fix for double save bug.
            if (image.Source.Position > 0) return;

            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.Uri);
            blob.UploadFromStream(image.Source);
        }


        public CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.ImageStore.AzureStorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("yogaimages");
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            return blobContainer;
        }
    }
}