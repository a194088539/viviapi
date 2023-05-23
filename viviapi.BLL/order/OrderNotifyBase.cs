using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using viviapi.BLL.Order.Notify;
using viviapi.IMessaging;
using viviapi.MessagingFactory;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;

namespace viviapi.BLL
{
    public class OrderNotifyBase
    {
        private static readonly IOrderBankNotify banknotifyQueue = QueueAccess.OrderBankNotify();
        private static readonly IOrderCardNotify cardnotifyQueue = QueueAccess.OrderCardNotify();
        private static readonly IOrderSmsNotify smsnotifyQueue = QueueAccess.OrderSmsNotify();
        private const int BUFFER_SIZE = 1024;
        private const int DefaultTimeout = 120000;

        private static void UpdatetoDB(int oclass, object obj, string agurl, int times, string callbacktext, bool success, string errcode)
        {
            if (oclass == 1)
            {
                OrderBankInfo orderBankInfo = obj as OrderBankInfo;
                if (orderBankInfo == null)
                    return;
                OrderBank orderBank = new OrderBank();
                bool flag = SystemApiHelper.CheckCallBackIsSuccess(orderBankInfo.version, callbacktext);
                orderBankInfo.notifystat = flag ? 2 : 4;
                orderBankInfo.againNotifyUrl = agurl;
                orderBankInfo.notifycontext = callbacktext;
                orderBankInfo.notifycount = times;
                orderBankInfo.notifytime = DateTime.Now;
                orderBank.UpdateNotifyInfo(orderBankInfo);
                if (orderBankInfo.notifystat != 2)
                    OrderNotifyBase.banknotifyQueue.Send(orderBankInfo);
            }
            else if (oclass == 2)
            {
                OrderCardInfo orderCardInfo = obj as OrderCardInfo;
                if (orderCardInfo == null)
                    return;
                OrderCard orderCard = new OrderCard();
                bool flag = SystemApiHelper.CheckCallBackIsSuccess(orderCardInfo.version, callbacktext);
                orderCardInfo.notifystat = flag ? 2 : 4;
                orderCardInfo.againNotifyUrl = agurl;
                orderCardInfo.notifycontext = callbacktext;
                orderCardInfo.notifycount = times;
                orderCardInfo.notifytime = DateTime.Now;
                orderCard.UpdateNotifyInfo(orderCardInfo);
                if (orderCardInfo.notifystat != 2)
                    OrderNotifyBase.cardnotifyQueue.Send(orderCardInfo);
            }
            else
            {
                if (oclass != 3)
                    return;
                OrderSmsInfo orderSmsInfo = obj as OrderSmsInfo;
                if (orderSmsInfo != null)
                {
                    OrderSms orderSms = new OrderSms();
                    orderSmsInfo.notifystat = success ? 2 : 4;
                    orderSmsInfo.issucc = success;
                    orderSmsInfo.errcode = errcode;
                    orderSmsInfo.againNotifyUrl = agurl;
                    orderSmsInfo.notifycontext = callbacktext;
                    orderSmsInfo.notifycount = times;
                    orderSms.UpdateNotifyInfo(orderSmsInfo);
                    if (orderSmsInfo.notifystat != 2)
                        OrderNotifyBase.smsnotifyQueue.Send(orderSmsInfo);
                }
            }
        }

        public static string GetNotifyUrl(int oclass, object obj)
        {
            string str = string.Empty;
            if (oclass == 1)
            {
                OrderBankInfo orderinfo = obj as OrderBankInfo;
                if (orderinfo == null)
                    return string.Empty;
                str = new OrderBank().GetCallBackUrl(orderinfo);
            }
            else if (oclass == 2)
            {
                OrderCardInfo orderinfo = obj as OrderCardInfo;
                if (orderinfo == null)
                    return string.Empty;
                str = new OrderCard().GetCallBackUrl(orderinfo);
            }
            else if (oclass == 3)
            {
                OrderSmsInfo orderinfo = obj as OrderSmsInfo;
                if (orderinfo == null)
                    return string.Empty;
                str = new OrderSms().GetCallBackUrl(orderinfo);
            }
            return str;
        }

        public static void AsynchronousNotify(int oclass, object obj)
        {
            string notifyUrl = OrderNotifyBase.GetNotifyUrl(oclass, obj);
            if (string.IsNullOrEmpty(notifyUrl))
                return;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(notifyUrl);
                RequestState requestState = new RequestState();
                requestState.orderclass = oclass;
                requestState.order = obj;
                requestState.url = notifyUrl;
                requestState.request = httpWebRequest;
                ThreadPool.RegisterWaitForSingleObject(httpWebRequest.BeginGetResponse(new AsyncCallback(OrderNotifyBase.RespCallback), (object)requestState).AsyncWaitHandle, new WaitOrTimerCallback(OrderNotifyBase.TimeoutCallback), (object)requestState, 120000, true);
            }
            catch (WebException ex)
            {
                string str = ex.Status.ToString() + ex.Message;
                OrderNotifyBase.UpdatetoDB(oclass, obj, notifyUrl, 1, str, false, str);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        private static void TimeoutCallback(object state, bool timedOut)
        {
            if (!timedOut)
                return;
            RequestState requestState = state as RequestState;
            if (requestState != null)
            {
                string str = "访问超时";
                OrderNotifyBase.UpdatetoDB(requestState.orderclass, requestState.order, requestState.url, 1, str, false, str);
            }
        }

        private static void RespCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                RequestState requestState = (RequestState)asynchronousResult.AsyncState;
                HttpWebRequest httpWebRequest = requestState.request;
                requestState.response = (HttpWebResponse)httpWebRequest.EndGetResponse(asynchronousResult);
                Stream responseStream = requestState.response.GetResponseStream();
                requestState.streamResponse = responseStream;
                responseStream.BeginRead(requestState.BufferRead, 0, 1024, new AsyncCallback(OrderNotifyBase.ReadCallBack), (object)requestState);
            }
            catch (WebException ex)
            {
                RequestState requestState = (RequestState)asynchronousResult.AsyncState;
                if (requestState == null)
                    return;
                string str = ex.Status.ToString() + ex.Message;
                OrderNotifyBase.UpdatetoDB(requestState.orderclass, requestState.order, requestState.url, 1, str, false, str);
            }
        }

        private static void ReadCallBack(IAsyncResult asyncResult)
        {
            try
            {
                RequestState requestState = (RequestState)asyncResult.AsyncState;
                Stream stream = requestState.streamResponse;
                int count = stream.EndRead(asyncResult);
                if (count > 0)
                {
                    requestState.requestData.Append(Encoding.GetEncoding("GB2312").GetString(requestState.BufferRead, 0, count));
                    stream.BeginRead(requestState.BufferRead, 0, 1024, new AsyncCallback(OrderNotifyBase.ReadCallBack), (object)requestState);
                }
                else
                {
                    string str = string.Empty;
                    if (requestState.requestData.Length > 1)
                        str = requestState.requestData.ToString();
                    stream.Close();
                    OrderNotifyBase.UpdatetoDB(requestState.orderclass, requestState.order, requestState.url, 1, str, true, str);
                }
            }
            catch (WebException ex)
            {
                RequestState requestState = (RequestState)asyncResult.AsyncState;
                if (requestState != null)
                {
                    string str = ex.Status.ToString() + ex.Message;
                    OrderNotifyBase.UpdatetoDB(requestState.orderclass, requestState.order, requestState.url, 1, str, false, str);
                }
            }
        }
    }
}
