using System;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using VLQR.Base.DDD.Domain.Annotations;

namespace VLQR.Base.ServiceBus
{
    [DomainService]
    public class ServiceBus : IServiceBus
    {
        readonly string _connectionString =
              CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        public void Initialize()
        {
            CreateMailQueue();
        }

        private void CreateMailQueue()
        {
            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(_connectionString);

            var queueName =
               CloudConfigurationManager.GetSetting("MailQueue.Name") ?? "MailQueue";

            if (!namespaceManager.QueueExists(queueName))
            {
                var queueDescription = new QueueDescription(queueName)
                {
                    MaxSizeInMegabytes = 1024,
                    DefaultMessageTimeToLive = new TimeSpan(0, 2, 0)
                };
                namespaceManager.CreateQueue(queueDescription);
            }
        }
    }
}
