using System;
using System.Messaging;
using viviapi.IMessaging;
using viviapi.Model.Order;
using viviapi.SysConfig;

namespace viviapi.MSMQMessaging
{
    public class OrderBankNotify : BaseQueue, IOrderBankNotify
    {
        private static readonly string queuePath = MSMQSetting.BankNotifyPath;
        private static int queueTimeout = 20;

        public OrderBankNotify()
          : base(OrderBankNotify.queuePath, OrderBankNotify.queueTimeout)
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
    }
}
