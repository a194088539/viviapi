using System;
using System.Messaging;
using viviapi.IMessaging;
using viviapi.Model.Order;
using viviapi.SysConfig;

namespace viviapi.MSMQMessaging
{
    public class OrderCard : BaseQueue, IOrderCard
    {
        private static readonly string queuePath = MSMQSetting.CardOrderPath;
        private static int queueTimeout = 20;

        public OrderCard()
          : base(OrderCard.queuePath, OrderCard.queueTimeout)
        {
            this.queue.Formatter = (IMessageFormatter)new BinaryMessageFormatter();
        }

        public new object Receive()
        {
            this.transactionType = MessageQueueTransactionType.Automatic;
            return ((Message)base.Receive()).Body;
        }

        public object Receive(int timeout)
        {
            this.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return this.Receive();
        }

        public void Send(OrderCardInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }

        public void SendItem(CardItemInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }

        public void Complete(OrderCardInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }

        public void ItemComplete(CardItemInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }
    }
}
