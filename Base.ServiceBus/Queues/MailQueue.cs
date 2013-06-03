using System.Transactions;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using VLQR.Base.DDD.Domain.Annotations;

namespace VLQR.Base.ServiceBus.Queues
{
    [DomainService]
    public class MailQueue : IMailQueue
    {
        readonly string _queueName =
               CloudConfigurationManager.GetSetting("MailQueue.Name") ?? "MailQueue";

        readonly string _connectionString =
              CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

        public void SendMessage<T>(T message) where T : class
        {
            var client = QueueClient.CreateFromConnectionString(_connectionString, _queueName);

            var options = new TransactionOptions() { IsolationLevel = IsolationLevel.Serializable };
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                client.Send(new BrokeredMessage(message));
                scope.Complete();
            }
        }
    }
}
