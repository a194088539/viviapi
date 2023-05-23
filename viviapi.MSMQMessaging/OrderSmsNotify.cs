using System;
using System.Messaging;
using viviapi.IMessaging;
using viviapi.Model.Order;
using viviapi.SysConfig;

namespace viviapi.MSMQMessaging
{
    public class OrderSmsNotify : BaseQueue, IOrderSmsNotify
    {
        private static readonly string queuePath = MSMQSetting.SmsNotifyPath;
        private static int queueTimeout = 20;

        public OrderSmsNotify()
          : base(OrderSmsNotify.queuePath, OrderSmsNotify.queueTimeout)
        {
            this.queue.Formatter = (IMessageFormatter)new BinaryMessageFormatter();
        }

        public OrderSmsInfo Receive()
        {
            this.transactionType = MessageQueueTransactionType.Automatic;
            return (OrderSmsInfo)((Message)base.Receive()).Body;
        }

        public OrderSmsInfo Receive(int timeout)
        {
            this.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return this.Receive();
        }

        public void Send(OrderSmsInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }
    }
}
