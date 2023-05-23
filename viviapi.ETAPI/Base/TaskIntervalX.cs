using Newtonsoft.Json;
using System;
using System.Data;
using viviapi.BLL.Order;
using viviapi.ETAPI.ShenZhouFu;
using viviLib.ScheduledTask;

namespace viviapi.ETAPI.Base
{
    public class TaskIntervalX : IScheduledTaskExecute
    {
        public void Execute()
        {
            TaskIntervalX.ProcessNotify();
        }

        private static void ProcessNotify()
        {
            DataTable failOrders2 = Dal.GetFailOrders2(DateTime.Now.AddHours(-24.0), DateTime.Now.AddSeconds(-2.0));
            if (failOrders2 == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)failOrders2.Rows)
            {
                int num = Convert.ToInt32(dataRow["supplierID"]);
                string str1 = dataRow["orderid"].ToString();
                string str2 = string.Empty;
                if (num == 70)
                {
                    Cared70 cared70 = new Cared70();
                    string callback = cared70.Query(str1);
                    cared70.Finish(str1, callback);
                }
                else if (num == 80)
                {
                    OfCard ofCard = new OfCard();
                    string callback = ofCard.Query(str1);
                    ofCard.Finish(callback);
                }
                else if (num == 85)
                {
                    viviapi.ETAPI.huiyuan.Card card = new viviapi.ETAPI.huiyuan.Card();
                    string callback = card.Query(str1);
                    if (!string.IsNullOrEmpty(callback))
                        card.Finish(callback);
                }
                else if (num == 86)
                {
                    card card = new card();
                    string str3 = card.Query(str1);
                    if (!string.IsNullOrEmpty(str3))
                    {
                        queryresult queryresult = (queryresult)JsonConvert.DeserializeObject(str3, typeof(queryresult));
                        if (queryresult != null && queryresult.queryResult == "000")
                        {
                            foreach (orderitem orderitem in queryresult.orders)
                                card.Finish(orderitem.orderId, orderitem.cardNo, orderitem.payStatus, orderitem.payMoney);
                        }
                    }
                }
                else if (num == 700)
                {
                    viviapi.ETAPI.Longbao.Card card = new viviapi.ETAPI.Longbao.Card();
                    string retText = card.Query(str1);
                    if (!string.IsNullOrEmpty(retText))
                        card.Finish(retText);
                }
                else if (num == 60866)
                {
                    Card60866 card60866 = new Card60866();
                    string callback = card60866.Query(str1);
                    card60866.Finish(str1, callback);
                }
            }
        }
    }
}
