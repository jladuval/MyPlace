using Base.DDD.Domain.Annotations;
using Base.StorageQueue;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Base.AzureStorage
{
    [DomainService]
    public class AzureStorage : IAzureStorage
    {
        public void InitializeStorage()
        {
            if (RoleEnvironment.IsAvailable)
            {
                var storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                CreateQueues(storageAccount);

                CreateBlobStorage(storageAccount);
            }

        }

        private void CreateQueues(CloudStorageAccount storageAccount)
        {
            var mailQueueName = CloudConfigurationManager.GetSetting("MailQueue.Name");

            var queueClient = storageAccount.CreateCloudQueueClient();

            var queue = queueClient.GetQueueReference(mailQueueName);

            queue.CreateIfNotExists();
        }

        private void CreateBlobStorage(CloudStorageAccount storageAccount)
        {
            var blobStorageImagesName = CloudConfigurationManager.GetSetting("BlobStorage.ImagesName");

            var blobStorage = storageAccount.CreateCloudBlobClient();

            var container = blobStorage.GetContainerReference(blobStorageImagesName);

            if (container.CreateIfNotExists())
            {
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }
        }
    }
}
