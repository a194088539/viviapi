using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Web;
using viviapi.BLL.User;
using viviapi.Cache;
using viviapi.DALFactory;
using viviapi.IBLLStrategy;
using viviapi.MessagingFactory;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.BLL
{
    public class OrderSms
    {
        private static readonly IOrderSmsStrategy orderInsertStrategy = OrderSms.LoadInsertStrategy();
        private static readonly viviapi.IMessaging.IOrderSms orderQueue = QueueAccess.CreateSmsOrder();
        private static readonly viviapi.IDAL.IOrderSms dal = DataAccess.CreateOrderSms();

        public string GenerateUniqueOrderId()
        {
            string objId = DateTime.Now.ToString("yyMMddHHmmssff") + "99" + new Random(Guid.NewGuid().GetHashCode()).Next(1000).ToString("0000");
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                return this.GenerateUniqueOrderId();
            WebCache.GetCacheService().AddObject(objId, (object)objId);
            return objId;
        }

        public bool Deduct(string orderId)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(orderId))
                return flag;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    flag = OrderSms.dal.Deduct(orderId);
                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                flag = false;
            }
            return flag;
        }

        public bool ReDeduct(string orderId)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(orderId))
                return flag;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    flag = OrderSms.dal.ReDeduct(orderId);
                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                flag = false;
            }
            return flag;
        }

        public void Insert(OrderSmsInfo order)
        {
            OrderSms.orderInsertStrategy.Insert(order);
        }

        public OrderSmsInfo GetModel(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return (OrderSmsInfo)null;
            try
            {
                return OrderSms.dal.GetModel(orderId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (OrderSmsInfo)null;
            }
        }

        public OrderSmsInfo GetModel(int id)
        {
            if ((long)id <= 0L)
                return (OrderSmsInfo)null;
            try
            {
                return OrderSms.dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (OrderSmsInfo)null;
            }
        }

        public OrderSmsInfo GetModel(int id, int userid)
        {
            if ((long)id <= 0L)
                return (OrderSmsInfo)null;
            try
            {
                return OrderSms.dal.GetModel(id, userid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (OrderSmsInfo)null;
            }
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            return OrderSms.dal.PageSearch(searchParams, pageSize, page, orderby);
        }

        public bool UpdateNotifyInfo(OrderSmsInfo order)
        {
            try
            {
                if (order == null)
                    return false;
                return OrderSms.dal.Notify(order);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        private static IOrderSmsStrategy LoadInsertStrategy()
        {
            return (IOrderSmsStrategy)Assembly.Load(RuntimeSetting.OrderSmsStrategyAssembly).CreateInstance(RuntimeSetting.OrderSmsStrategyClass);
        }

        public OrderSmsInfo ReceiveFromQueue(int timeout)
        {
            return OrderSms.orderQueue.Receive(timeout);
        }

        public string BuilderParms(OrderSmsInfo orderinfo, string backurl)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (orderinfo == null || string.IsNullOrEmpty(backurl))
                return stringBuilder.ToString();
            string apiKey = UserFactory.GetBaseModel(orderinfo.userid).APIKey;
            string str = Cryptography.MD5(string.Format("mob={0}&content={1}&opstate={2}&ovalue={3}{4}", (object)orderinfo.mobile, (object)orderinfo.userMsgContenct, (object)orderinfo.opstate, (object)Convert.ToInt32(orderinfo.fee.ToString()), (object)apiKey));
            stringBuilder.AppendFormat("mob={0}", (object)HttpUtility.UrlEncode(orderinfo.mobile));
            stringBuilder.AppendFormat("&content={0}", (object)HttpUtility.UrlEncode(orderinfo.userMsgContenct));
            stringBuilder.AppendFormat("&opstate={0}", (object)HttpUtility.UrlEncode(orderinfo.opstate));
            stringBuilder.AppendFormat("&ovalue={0}", (object)HttpUtility.UrlEncode(orderinfo.fee.ToString("0")));
            stringBuilder.AppendFormat("&ekaorderid={0}", (object)HttpUtility.UrlEncode(orderinfo.orderid));
            stringBuilder.AppendFormat("&ekatime={0}", (object)HttpUtility.UrlEncode(orderinfo.completetime.ToString("yyyy/MM/dd HH:mm:ss")));
            stringBuilder.AppendFormat("&sign={0}", (object)str);
            return stringBuilder.ToString();
        }

        public string GetCallBackUrl(OrderSmsInfo orderinfo)
        {
            if (string.IsNullOrEmpty(orderinfo.notifyurl))
                return string.Empty;
            return orderinfo.notifyurl + "?" + this.BuilderParms(orderinfo, orderinfo.notifyurl);
        }

        public string getStatusView(int status)
        {
            switch (status)
            {
                case 1:
                    return "处理中";
                case 2:
                    return "已完成";
                case 4:
                    return "失败";
                default:
                    return "";
            }
        }
    }
}
