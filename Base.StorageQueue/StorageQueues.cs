using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Base.DDD.Domain.Annotations;

namespace Base.StorageQueue
{
    [DomainService]
    public class StorageQueues : IStorageQueues
    {
        public void InitializeAllQueues()
        {
            if (RoleEnvironment.IsAvailable)
            {
                var storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                var mailQueueName = CloudConfigurationManager.GetSetting("MailQueue.Name");

                var queueClient = storageAccount.CreateCloudQueueClient();

                var queue = queueClient.GetQueueReference(mailQueueName);

                queue.CreateIfNotExists();
            }
        }
    }
}
