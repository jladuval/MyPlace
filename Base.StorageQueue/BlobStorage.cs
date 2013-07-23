

namespace Base.AzureStorage
{
    using System;
    using System.IO;
    using System.Web;

    using Base.DDD.Domain.Annotations;

    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    [DomainService]
    public class BlobStorage : IBlobStorage
    {
        private readonly CloudBlobClient _blobStorage;

        private readonly string _blobStorageImagesName;

        private const string LocalPath = "C:/Temp";

        public BlobStorage()
        {
            if (RoleEnvironment.IsAvailable)
            {
                var storageAccount =
                    CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

                _blobStorageImagesName = CloudConfigurationManager.GetSetting("BlobStorage.ImagesName");

                _blobStorage = storageAccount.CreateCloudBlobClient();
            }
        }

        public string SaveImage(HttpPostedFileBase image, string folderName)
        {
            if (RoleEnvironment.IsAvailable)
            {
                var container = _blobStorage.GetContainerReference(_blobStorageImagesName);

                var uniqueBlobName = string.Format(
                    "{0}/{1}/{2}", _blobStorageImagesName, folderName, image.FileName);

                var blob = container.GetBlockBlobReference(uniqueBlobName);

                blob.Properties.ContentType = image.ContentType;
                blob.UploadFromStream(image.InputStream);

                return blob.Uri.ToString();
            }

            var pic = Path.GetFileName(image.FileName) ?? Guid.NewGuid().ToString();
            var path = Path.Combine(LocalPath, pic);
            image.SaveAs(path);

            return path;
        }
    }
}
