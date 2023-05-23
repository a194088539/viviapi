using System;
using System.Messaging;
using viviapi.IMessaging;
using viviapi.Model.Order;
using viviapi.SysConfig;

namespace viviapi.MSMQMessaging
{
    public class OrderBank : BaseQueue, IOrderBank
    {
        private static readonly string queuePath = MSMQSetting.BankOrderPath;
        private static int queueTimeout = 20;

        public OrderBank()
          : base(OrderBank.queuePath, OrderBank.queueTimeout)
        {
            this.queue.Formatter = (IMessageFormatter)new BinaryMessageFormatter();
        }

        public OrderBankInfo Receive()
        {
            this.transactionType = MessageQueueTransactionType.Automatic;
            return (OrderBankInfo)((Message)base.Receive()).Body;
        }

        public OrderBankInfo Receive(int timeout)
        {
            this.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return this.Receive();
        }

        public void Send(OrderBankInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }

        public void Complete(OrderBankInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }
    }
}
