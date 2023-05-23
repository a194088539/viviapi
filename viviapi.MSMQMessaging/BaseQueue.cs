using System;
using System.Messaging;

namespace viviapi.MSMQMessaging
{
    public class BaseQueue : IDisposable
    {
        protected MessageQueueTransactionType transactionType = MessageQueueTransactionType.Automatic;
        protected MessageQueue queue;
        protected TimeSpan timeout;

        public BaseQueue(string queuePath, int timeoutSeconds)
        {
            this.queue = new MessageQueue(queuePath);
            this.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeoutSeconds));
            this.queue.DefaultPropertiesToSend.AttachSenderId = false;
            this.queue.DefaultPropertiesToSend.UseAuthentication = false;
            this.queue.DefaultPropertiesToSend.UseEncryption = false;
            this.queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;
            this.queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        public virtual object Receive()
        {
            try
            {
                using (Message message = this.queue.Receive(this.timeout, this.transactionType))
                    return (object)message;
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                    throw new TimeoutException();
                throw;
            }
        }

        public virtual void Send(object msg)
        {
            this.queue.Send(msg, this.transactionType);
        }

        public void Dispose()
        {
            this.queue.Dispose();
        }
    }
}
