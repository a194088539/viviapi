using System;
using System.Net;
using System.Text;
using viviapi.IMessaging;
using viviapi.MessagingFactory;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.BLL
{
    public class OrderCardNotify
    {
        private static readonly IOrderCardNotify notifyQueue = QueueAccess.OrderCardNotify();

        public void DoNotify(OrderCardInfo order)
        {
            OrderNotifyBase.AsynchronousNotify(2, (object)order);
        }

        public OrderCardInfo ReceiveFromQueue(int timeout)
        {
            return OrderCardNotify.notifyQueue.Receive(timeout);
        }

        public string SynchronousNotify(string orderId)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(orderId))
                return string.Empty;
            OrderCard orderCard = new OrderCard();
            OrderCardInfo model = orderCard.GetModel(orderId);
            if (model == null)
                return string.Empty;
            string str1 = SystemApiHelper.Successflag(model.version);
            string callBackUrl = orderCard.GetCallBackUrl(model);
            string str2 = string.Empty;
            try
            {
                flag = true;
                if (!string.IsNullOrEmpty(callBackUrl))
                    str2 = WebClientHelper.GetString(callBackUrl, string.Empty, "GET", Encoding.GetEncoding("GB2312"), 100000);
            }
            catch (WebException ex)
            {
                str2 = ex.Status.ToString() + ex.Message;
                flag = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            if ((str2.StartsWith(str1) || str2.ToLower().StartsWith(str1)) && model.notifystat != 2)
            {
                model.notifystat = 2;
                model.againNotifyUrl = callBackUrl;
                model.notifycontext = str2;
                model.notifycount = 1;
                model.notifytime = DateTime.Now;
                orderCard.UpdateNotifyInfo(model);
            }
            return str2;
        }

        public void NotifyCheckStatus(object stateInfo)
        {
            try
            {
                OrderCard orderCard = new OrderCard();
                viviapi.Model.Order.OrderCardNotify orderCardNotify = (viviapi.Model.Order.OrderCardNotify)stateInfo;
                string callBackUrl = orderCard.GetCallBackUrl(orderCardNotify.orderInfo);
                string str1 = SystemApiHelper.Successflag(orderCardNotify.orderInfo.version);
                if (string.IsNullOrEmpty(callBackUrl))
                {
                    orderCardNotify.tmr.Dispose();
                    orderCardNotify.tmr = (System.Threading.Timer)null;
                }
                else
                {
                    bool flag1 = false;
                    string str2 = string.Empty;
                    ++orderCardNotify.orderInfo.notifycount;
                    if (orderCardNotify.tmr != null)
                    {
                        switch (orderCardNotify.orderInfo.notifycount)
                        {
                            case 1:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromSeconds(20.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 2:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 3:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(2.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 4:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 5:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(10.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 6:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(20.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 7:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(30.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 8:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(60.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 9:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(120.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 10:
                                flag1 = orderCardNotify.tmr.Change(TimeSpan.FromMinutes(240.0), TimeSpan.FromSeconds(200.0));
                                break;
                        }
                    }
                    try
                    {
                        str2 = WebClientHelper.GetString(callBackUrl, string.Empty, "GET", Encoding.GetEncoding("GB2312"), 100000);
                    }
                    catch
                    {
                    }
                    if (orderCardNotify.orderInfo.notifycount <= 10)
                    {
                        bool flag2 = str2.StartsWith(str1) || str2.ToLower().StartsWith(str1);
                        orderCardNotify.orderInfo.notifystat = flag2 ? 2 : 4;
                        orderCardNotify.orderInfo.againNotifyUrl = callBackUrl;
                        orderCardNotify.orderInfo.notifycontext = str2;
                        orderCardNotify.orderInfo.notifytime = DateTime.Now;
                        orderCard.UpdateNotifyInfo(orderCardNotify.orderInfo);
                    }
                    if ((str2.ToLower() == str1 || orderCardNotify.orderInfo.notifycount >= 10) && orderCardNotify != null)
                    {
                        orderCardNotify.tmr.Dispose();
                        orderCardNotify.tmr = (System.Threading.Timer)null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
