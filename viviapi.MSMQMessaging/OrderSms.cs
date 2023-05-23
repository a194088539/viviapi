using System;
using System.Messaging;
using viviapi.IMessaging;
using viviapi.Model.Order;
using viviapi.SysConfig;

namespace viviapi.MSMQMessaging
{
    public class OrderSms : BaseQueue, IOrderSms
    {
        private static readonly string queuePath = MSMQSetting.SmsOrderPath;
        private static int queueTimeout = 20;

        public OrderSms()
          : base(OrderSms.queuePath, OrderSms.queueTimeout)
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

        public void Complete(OrderSmsInfo orderMessage)
        {
            this.transactionType = MessageQueueTransactionType.Single;
            this.Send((object)orderMessage);
        }
    }
}
