

namespace Base.AzureStorage
{
    using DDD.Domain.Annotations;
    using System;
    using System.IO;
    using System.Web;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    [DomainService]
    public class BlobStorage : IBlobStorage
    {
        private readonly CloudBlobClient _blobStorage;

        private readonly string _blobStorageImagesName;

        public BlobStorage()
        {
            var storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

            _blobStorageImagesName = CloudConfigurationManager.GetSetting("BlobStorage.ImagesName");

            _blobStorage = storageAccount.CreateCloudBlobClient();
        }

        public string SaveImage(HttpPostedFileBase image)
        {
            var container = _blobStorage.GetContainerReference(_blobStorageImagesName);

            var uniqueBlobName = string.Format("{0}/{1}{2}",
                _blobStorageImagesName,
                Guid.NewGuid(), 
                Path.GetExtension(image.FileName));

            var blob = container.GetBlockBlobReference(uniqueBlobName);

            blob.Properties.ContentType = image.ContentType;
            blob.UploadFromStream(image.InputStream);

            return blob.Uri.ToString();
        }
    }
}
