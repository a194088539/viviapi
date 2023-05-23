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
    public class OrderSmsNotify
    {
        private static readonly IOrderSmsNotify notifyQueue = QueueAccess.OrderSmsNotify();

        public void DoNotify(OrderSmsInfo order)
        {
            OrderNotifyBase.AsynchronousNotify(3, (object)order);
        }

        public OrderSmsInfo ReceiveFromQueue(int timeout)
        {
            return OrderSmsNotify.notifyQueue.Receive(timeout);
        }

        public string SynchronousNotify(OrderSmsInfo orderInfo)
        {
            if (orderInfo == null)
                return string.Empty;
            OrderSms orderSms = new OrderSms();
            string callBackUrl = orderSms.GetCallBackUrl(orderInfo);
            string str = string.Empty;
            bool flag;
            try
            {
                flag = true;
                str = WebClientHelper.GetString(callBackUrl, string.Empty, "GET", Encoding.GetEncoding("GB2312"), 100000);
            }
            catch (WebException ex)
            {
                str = ex.Status.ToString() + ex.Message;
                flag = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                flag = false;
            }
            if (!string.IsNullOrEmpty(str) && orderInfo.notifystat != 2)
            {
                orderInfo.notifystat = flag ? 2 : 4;
                orderInfo.againNotifyUrl = callBackUrl;
                orderInfo.notifycontext = str;
                orderInfo.issucc = false;
                orderInfo.errcode = str;
                orderSms.UpdateNotifyInfo(orderInfo);
            }
            return str;
        }

        public string SynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return string.Empty;
            OrderSmsInfo model = new OrderSms().GetModel(orderId);
            if (model == null)
                return string.Empty;
            return this.SynchronousNotify(model);
        }

        public void NotifyCheckStatus(object stateInfo)
        {
            try
            {
                OrderSms orderSms = new OrderSms();
                viviapi.Model.Order.OrderSmsNotify orderSmsNotify = (viviapi.Model.Order.OrderSmsNotify)stateInfo;
                string callBackUrl = orderSms.GetCallBackUrl(orderSmsNotify.orderInfo);
                if (string.IsNullOrEmpty(callBackUrl))
                {
                    orderSmsNotify.tmr.Dispose();
                    orderSmsNotify.tmr = (System.Threading.Timer)null;
                }
                else
                {
                    string str1 = string.Empty;
                    bool flag;
                    string str2;
                    try
                    {
                        flag = true;
                        str2 = WebClientHelper.GetString(callBackUrl, string.Empty, "GET", Encoding.GetEncoding("GB2312"), 100000);
                    }
                    catch (WebException ex)
                    {
                        flag = false;
                        str2 = ex.Status.ToString() + ex.Message;
                    }
                    ++orderSmsNotify.orderInfo.notifycount;
                    if (orderSmsNotify.tmr != null)
                    {
                        if (orderSmsNotify.orderInfo.notifycount == 1)
                            orderSmsNotify.tmr.Change(TimeSpan.FromSeconds(20.0), TimeSpan.FromSeconds(200.0));
                        if (orderSmsNotify.orderInfo.notifycount == 2)
                            orderSmsNotify.tmr.Change(TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(200.0));
                        if (orderSmsNotify.orderInfo.notifycount == 3)
                            orderSmsNotify.tmr.Change(TimeSpan.FromSeconds(1200.0), TimeSpan.FromSeconds(200.0));
                        if (orderSmsNotify.orderInfo.notifycount == 4)
                            orderSmsNotify.tmr.Change(TimeSpan.FromSeconds(3600.0), TimeSpan.FromSeconds(200.0));
                        if (orderSmsNotify.orderInfo.notifycount == 5)
                            orderSmsNotify.tmr.Change(TimeSpan.FromSeconds(7200.0), TimeSpan.FromSeconds(200.0));
                    }
                    if (orderSmsNotify.orderInfo.notifycount == 5 || !string.IsNullOrEmpty(str2.ToLower()))
                    {
                        orderSmsNotify.orderInfo.notifystat = string.IsNullOrEmpty(str2.ToLower()) || !flag ? 4 : 2;
                        orderSmsNotify.orderInfo.userorder = str2;
                        orderSmsNotify.orderInfo.againNotifyUrl = callBackUrl;
                        orderSmsNotify.orderInfo.notifycontext = str2;
                        orderSmsNotify.orderInfo.issucc = flag;
                        orderSmsNotify.orderInfo.errcode = str2;
                        orderSms.UpdateNotifyInfo(orderSmsNotify.orderInfo);
                    }
                    if ((!string.IsNullOrEmpty(str2.ToLower()) || orderSmsNotify.orderInfo.notifycount >= 5) && orderSmsNotify.tmr != null)
                    {
                        orderSmsNotify.tmr.Dispose();
                        orderSmsNotify.tmr = (System.Threading.Timer)null;
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
