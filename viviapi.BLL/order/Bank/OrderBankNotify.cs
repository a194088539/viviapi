using System;
using System.Net;
using System.Text;
using viviapi.IMessaging;
using viviapi.MessagingFactory;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;
using viviLib.Text;
using viviLib.Web;

namespace viviapi.BLL
{
    public class OrderBankNotify
    {
        private static readonly IOrderBankNotify notifyQueue = QueueAccess.OrderBankNotify();

        public void DoNotify(OrderBankInfo order)
        {
            if (order == null)
                return;
            OrderNotifyBase.AsynchronousNotify(1, (object)order);
        }

        public OrderBankInfo ReceiveFromQueue(int timeout)
        {
            return OrderBankNotify.notifyQueue.Receive(timeout);
        }

        public string SynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return string.Empty;
            OrderBank orderBank = new OrderBank();
            OrderBankInfo model = orderBank.GetModel(orderId);
            if (model == null)
                return string.Empty;
            string str1 = SystemApiHelper.Successflag(model.version);
            string callBackUrl = orderBank.GetCallBackUrl(model);
            string str2 = string.Empty;
            try
            {
                if (PageValidate.IsUrl(callBackUrl))
                {
                    str2 = WebClientHelper.GetString(callBackUrl, string.Empty, "GET", Encoding.GetEncoding("GB2312"), 100000);
                    if ((str2.StartsWith(str1) || str2.ToLower().StartsWith(str1)) && model.notifystat != 2)
                    {
                        model.notifystat = 2;
                        model.againNotifyUrl = callBackUrl;
                        model.notifycontext = str2;
                        orderBank.UpdateNotifyInfo(model);
                    }
                }
                else
                    str2 = "返回地址不正确！";
            }
            catch
            {
            }
            return str2;
        }

        public void NotifyCheckStatus(object stateInfo)
        {
            try
            {
                OrderBank orderBank = new OrderBank();
                OrderNotify orderNotify = (OrderNotify)stateInfo;
                string callBackUrl = orderBank.GetCallBackUrl(orderNotify.orderInfo);
                string str1 = SystemApiHelper.Successflag(orderNotify.orderInfo.version);
                if (string.IsNullOrEmpty(callBackUrl))
                {
                    orderNotify.tmr.Dispose();
                    orderNotify.tmr = (System.Threading.Timer)null;
                }
                else
                {
                    bool flag1 = false;
                    ++orderNotify.orderInfo.notifycount;
                    if (orderNotify.tmr != null)
                    {
                        switch (orderNotify.orderInfo.notifycount)
                        {
                            case 1:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromSeconds(20.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 2:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 3:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(2.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 4:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 5:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(10.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 6:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(20.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 7:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(30.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 8:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(60.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 9:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(120.0), TimeSpan.FromSeconds(200.0));
                                break;
                            case 10:
                                flag1 = orderNotify.tmr.Change(TimeSpan.FromMinutes(240.0), TimeSpan.FromSeconds(200.0));
                                break;
                        }
                    }
                    string str2 = string.Empty;
                    string str3;
                    try
                    {
                        str3 = WebClientHelper.GetString(callBackUrl, string.Empty, "GET", Encoding.GetEncoding("GB2312"), 100000);
                    }
                    catch (WebException ex)
                    {
                        str3 = ex.Status.ToString() + ex.Message;
                    }
                    if (orderNotify.orderInfo.notifycount <= 10)
                    {
                        bool flag2 = str3.StartsWith(str1) || str3.ToLower().StartsWith(str1);
                        orderNotify.orderInfo.notifystat = flag2 ? 2 : 4;
                        orderNotify.orderInfo.againNotifyUrl = callBackUrl;
                        orderNotify.orderInfo.notifycontext = str3;
                        orderNotify.orderInfo.notifytime = DateTime.Now;
                        orderBank.UpdateNotifyInfo(orderNotify.orderInfo);
                    }
                    if ((str3.ToLower() == str1 || orderNotify.orderInfo.notifycount >= 10) && orderNotify.tmr != null)
                    {
                        orderNotify.tmr.Dispose();
                        orderNotify.tmr = (System.Threading.Timer)null;
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
