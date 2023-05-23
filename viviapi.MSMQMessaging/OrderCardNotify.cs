using System;
using System.Messaging;
using viviapi.IMessaging;
using viviapi.Model.Order;
using viviapi.SysConfig;

namespace viviapi.MSMQMessaging
{
    public class OrderCardNotify : BaseQueue, IOrderCardNotify
    {
        private static readonly string queuePath = MSMQSetting.CardNotifyPath;
        private static int queueTimeout = 20;

        public OrderCardNotify()
          : base(OrderCardNotify.queuePath, OrderCardNotify.queueTimeout)
        {
            this.queue.Formatter = (IMessageFormatter)new BinaryMessageFormatter();
        }

        public OrderCardInfo Receive()
        {
            this.transactionType = MessageQueueTransactionType.Automatic;
            return (OrderCardInfo)((Message)base.Receive()).Body;
        }

        public OrderCardInfo Receive(int timeout)
        {
            this.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return this.Receive();
        }

        public void Send(OrderCardInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }
    }
}
