using System.Threading;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviLib.ScheduledTask;

namespace viviapi.BLL.Order.Bank
{
    public class TaskInterval : IScheduledTaskExecute
    {
        private static int notifyTransactionTimeout = MSMQSetting.NotifyTransactionTimeout;
        private static int notifyqueueTimeout = MSMQSetting.NotifyQueueTimeout;
        private static int notifybatchSize = MSMQSetting.NotifyBatchSize;
        private static int notifythreadCount = MSMQSetting.NotifyThreadCount;

        public void Execute()
        {
            TaskInterval.ProcessNotify();
        }

        private static void ProcessNotify()
        {
            OrderBankNotify orderBankNotify = new OrderBankNotify();
            for (int index = 0; index < TaskInterval.notifybatchSize; ++index)
            {
                try
                {
                    OrderBankInfo orderBankInfo = orderBankNotify.ReceiveFromQueue(TaskInterval.notifyqueueTimeout);
                    orderBankInfo.notifycount = 0;
                    OrderNotify orderNotify = new OrderNotify();
                    orderNotify.orderInfo = orderBankInfo;
                    Timer timer = new Timer(new TimerCallback(orderBankNotify.NotifyCheckStatus), (object)orderNotify, 0, 1000);
                    orderNotify.tmr = timer;
                }
                catch
                {
                }
            }
        }
    }
}
