using System;
using System.Collections;
using System.Threading;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviLib.ScheduledTask;

namespace viviapi.BLL.Order.Card
{
    public class TaskInterval : IScheduledTaskExecute
    {
        private static int notifyTransactionTimeout = MSMQSetting.NotifyTransactionTimeout_Card;
        private static int notifyqueueTimeout = MSMQSetting.NotifyQueueTimeout_Card;
        private static int notifybatchSize = MSMQSetting.NotifyBatchSize_Card;
        private static int notifythreadCount = MSMQSetting.NotifyThreadCount_Card;

        public void Execute()
        {
            TaskInterval.ProcessNotify();
        }

        private static void ProcessNotify()
        {
            TimeSpan.FromSeconds(Convert.ToDouble(TaskInterval.notifyTransactionTimeout * TaskInterval.notifybatchSize));
            viviapi.BLL.OrderCardNotify orderCardNotify1 = new viviapi.BLL.OrderCardNotify();
            ArrayList arrayList = new ArrayList();
            for (int index = 0; index < TaskInterval.notifybatchSize; ++index)
            {
                try
                {
                    OrderCardInfo orderCardInfo = orderCardNotify1.ReceiveFromQueue(TaskInterval.notifyqueueTimeout);
                    orderCardInfo.notifycount = 0;
                    viviapi.Model.Order.OrderCardNotify orderCardNotify2 = new viviapi.Model.Order.OrderCardNotify();
                    orderCardNotify2.orderInfo = orderCardInfo;
                    Timer timer = new Timer(new TimerCallback(orderCardNotify1.NotifyCheckStatus), (object)orderCardNotify2, 0, 1000);
                    orderCardNotify2.tmr = timer;
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
