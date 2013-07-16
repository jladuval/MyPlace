using System.Web.Script.Serialization;
using Base.DDD.Domain.Annotations;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Base.AzureStorage
{
    [DomainService]
    public class MailQueue : IMailQueue
    {
        public void SendMessage<T>(T message)where T : class
        {
            if (RoleEnvironment.IsAvailable)
            {
                var storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                var mailQueueName = CloudConfigurationManager.GetSetting("MailQueue.Name");

                // Create the queue client.
                var queueClient = storageAccount.CreateCloudQueueClient();

                // Retrieve a reference to a queue.
                var queue = queueClient.GetQueueReference(mailQueueName);

                queue.CreateIfNotExists();
                var messageJson = new JavaScriptSerializer().Serialize(message);
                queue.AddMessage(new CloudQueueMessage(messageJson));
            }
        }
    }
}
