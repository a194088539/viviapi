using System.Data;
using viviapi.BLL.Order;
using viviapi.ETAPI.huiyuan;
using viviLib.ScheduledTask;

namespace viviapi.ETAPI.Base
{
    public class TaskInterval : IScheduledTaskExecute
    {
        public void Execute()
        {
            TaskInterval.ProcessNotify();
        }

        private static void ProcessNotify()
        {
            DataTable failOrders = Dal.GetFailOrders();
            if (failOrders == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)failOrders.Rows)
            {
                string orderid = dataRow["orderid"].ToString();
                Card card = new Card();
                string callback = card.Query(orderid);
                if (!string.IsNullOrEmpty(callback))
                    card.Finish(callback);
            }
        }
    }
}
