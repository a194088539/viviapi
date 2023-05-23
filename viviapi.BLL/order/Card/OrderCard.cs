using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Caching;
using viviapi.BLL.Channel;
using viviapi.BLL.Payment;
using viviapi.BLL.User;
using viviapi.Cache;
using viviapi.DALFactory;
using viviapi.IBLLStrategy;
using viviapi.MessagingFactory;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.BLL
{
    public class OrderCard
    {
        private static readonly IOrderCardStrategy orderInsertStrategy = OrderCard.LoadInsertStrategy();
        private static readonly viviapi.IMessaging.IOrderCard orderQueue = QueueAccess.CreateCardOrder();
        private static readonly viviapi.IDAL.IOrderCard dal = DataAccess.CreateOrderCard();

        public string GenerateUniqueOrderId(int typeId)
        {
            string str = "00";
            if (typeId.ToString().Length == 3)
                str = typeId.ToString().Substring(1);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string objId = DateTime.Now.ToString("yyMMddHHmmssff") + str + random.Next(1000).ToString("0000");
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                return this.GenerateUniqueOrderId(typeId);
            WebCache.GetCacheService().AddObject(objId, (object)objId);
            return objId;
        }

        public void Insert(OrderCardInfo order)
        {
            OrderCard.orderInsertStrategy.Insert(order);
        }

        public void InsertItem(CardItemInfo order)
        {
            OrderCard.orderInsertStrategy.InsertItem(order);
        }

        public void Complete(OrderCardInfo order)
        {
            OrderCard.orderInsertStrategy.Complete(order);
        }

        public void ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out Decimal ototalvalue)
        {
            OrderCard.orderInsertStrategy.ItemComplete(order, out allCompleted, out opstate, out ovalue, out ototalvalue);
        }

        public OrderCardInfo GetModel(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return (OrderCardInfo)null;
            return OrderCard.dal.GetModel(orderId);
        }

        public CardItemInfo GetItemModel(string orderId, int serial)
        {
            if (string.IsNullOrEmpty(orderId))
                return (CardItemInfo)null;
            return OrderCard.dal.GetItemModel(orderId, serial);
        }

        public DataTable DataItemsByOrderId(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return (DataTable)null;
            return OrderCard.dal.DataItemsByOrderId(orderId);
        }

        public OrderCardInfo GetModel(long id)
        {
            if (id <= 0L)
                return (OrderCardInfo)null;
            try
            {
                return OrderCard.dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (OrderCardInfo)null;
            }
        }

        public OrderCardInfo GetModel(long id, int userid)
        {
            if (id <= 0L)
                return (OrderCardInfo)null;
            try
            {
                return OrderCard.dal.GetModel(id, userid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (OrderCardInfo)null;
            }
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
                    flag = OrderCard.dal.Deduct(orderId);
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
                    flag = OrderCard.dal.ReDeduct(orderId);
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

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            return OrderCard.dal.PageSearch(searchParams, pageSize, page, orderby);
        }

        public bool UpdateNotifyInfo(OrderCardInfo order)
        {
            if (order == null)
                return false;
            return OrderCard.dal.Notify(order);
        }

        private static IOrderCardStrategy LoadInsertStrategy()
        {
            return (IOrderCardStrategy)Assembly.Load(RuntimeSetting.OrderCardStrategyAssembly).CreateInstance(RuntimeSetting.OrderCardStrategyClass);
        }

        public object ReceiveFromQueue(int timeout)
        {
            return OrderCard.orderQueue.Receive(timeout);
        }

        public string BuilderParms(OrderCardInfo orderinfo, string backurl)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (orderinfo == null || string.IsNullOrEmpty(backurl))
                return stringBuilder.ToString();
            string apiKey = UserFactory.GetCacheUserBaseInfo(orderinfo.userid).APIKey;
            if (orderinfo.ismulticard == 0)
            {
                string str = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)orderinfo.userorder, (object)orderinfo.opstate, (object)Decimal.Round(orderinfo.realvalue.Value, 0), (object)apiKey));
                stringBuilder.AppendFormat("orderid={0}", (object)HttpUtility.UrlEncode(orderinfo.userorder));
                stringBuilder.AppendFormat("&opstate={0}", (object)HttpUtility.UrlEncode(orderinfo.opstate));
                stringBuilder.AppendFormat("&ovalue={0}", (object)HttpUtility.UrlEncode(Decimal.Round(orderinfo.realvalue.Value, 0).ToString()));
                stringBuilder.AppendFormat("&ekaorderid={0}", (object)HttpUtility.UrlEncode(orderinfo.orderid));
                stringBuilder.AppendFormat("&ekatime={0}", (object)HttpUtility.UrlEncode(orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss")));
                stringBuilder.AppendFormat("&attach={0}", (object)HttpUtility.UrlEncode(HttpUtility.UrlEncode(orderinfo.attach, Encoding.GetEncoding("GB2312"))));
                stringBuilder.AppendFormat("&msg={0}", (object)HttpUtility.UrlEncode(HttpUtility.UrlEncode(orderinfo.msg, Encoding.GetEncoding("GB2312"))));
                stringBuilder.AppendFormat("&sign={0}", (object)str);
            }
            else if (orderinfo.ismulticard == 1)
            {
                string str = Cryptography.MD5(string.Format("orderid={0}&cardno={1}&opstate={2}&ovalue={3}&ototalvalue={4}&attach={5}&msg={6}{7}", (object)orderinfo.userorder, (object)orderinfo.cardNo, (object)orderinfo.returnopstate, (object)orderinfo.returnovalue, (object)Decimal.Round(orderinfo.realvalue.Value, 0), (object)orderinfo.attach, (object)orderinfo.msg, (object)apiKey));
                stringBuilder.AppendFormat("orderid={0}", (object)HttpUtility.UrlEncode(orderinfo.userorder));
                stringBuilder.AppendFormat("&cardno={0}", (object)HttpUtility.UrlEncode(orderinfo.cardNo));
                stringBuilder.AppendFormat("&opstate={0}", (object)HttpUtility.UrlEncode(orderinfo.returnopstate));
                stringBuilder.AppendFormat("&ovalue={0}", (object)HttpUtility.UrlEncode(orderinfo.returnovalue));
                Decimal d = new Decimal(0);
                if (orderinfo.realvalue.HasValue)
                    d = orderinfo.realvalue.Value;
                stringBuilder.AppendFormat("&ototalvalue={0}", (object)HttpUtility.UrlEncode(Decimal.Round(d, 0).ToString()));
                stringBuilder.AppendFormat("&attach={0}", (object)HttpUtility.UrlEncode(HttpUtility.UrlEncode(orderinfo.attach, Encoding.GetEncoding("GB2312"))));
                stringBuilder.AppendFormat("&msg={0}", (object)HttpUtility.UrlEncode(HttpUtility.UrlEncode(orderinfo.msg, Encoding.GetEncoding("GB2312"))));
                stringBuilder.AppendFormat("&ekaorderid={0}", (object)HttpUtility.UrlEncode(orderinfo.orderid));
                stringBuilder.AppendFormat("&ekatime={0}", (object)HttpUtility.UrlEncode(orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss")));
                stringBuilder.AppendFormat("&sign={0}", (object)str);
            }
            return stringBuilder.ToString();
        }

        public string GetCallBackUrl(OrderCardInfo orderinfo)
        {
            return SystemApiHelper.GetCardBackUrl(orderinfo);
        }

        public bool DoCardComplete(int supplierId, string orderId, string supplierOrderId, int status, string opstate, string msg, string userviewmsg, Decimal tranAMT, Decimal suppAmt, bool isDeduct, string errtype, byte returnmethod)
        {
            try
            {
                OrderCardInfo order = WebCache.GetCacheService().RetrieveObject(orderId) as OrderCardInfo ?? this.GetModel(orderId);
                if (order != null)
                {
                    order.supplierId = supplierId;
                    UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(order.userid);
                    order.method = returnmethod;
                    order.cardversion = cacheUserBaseInfo.cardversion;
                    order.orderid = orderId;
                    order.status = status;
                    if (isDeduct && SysConfig.isOpenDeduct && status == 2 && cacheUserBaseInfo != null && new Random(Guid.NewGuid().GetHashCode()).Next(1, 1000) <= cacheUserBaseInfo.CPSDrate)
                        order.status = 8;
                    order.realvalue = new Decimal?(tranAMT);
                    order.supplierId = supplierId;
                    order.completetime = new DateTime?(DateTime.Now);
                    if (order.payRate <= new Decimal(0))
                    {
                        if (order.ordertype == 8)
                            order.payRate = Channelsupplier.GetPayRate(order.typeId, order.supplierId);
                        if (order.payRate <= new Decimal(0))
                            order.payRate = PayRateFactory.GetUserPayRate(cacheUserBaseInfo, order.userid, order.typeId);
                    }
                    order.payAmt = order.payRate * tranAMT;
                    if (suppAmt > new Decimal(0))
                    {
                        order.supplierRate = suppAmt / order.refervalue;
                        order.supplierAmt = suppAmt;
                    }
                    else
                    {
                        Decimal rate = SupplierPayRateFactory.GetRate(supplierId, order.typeId);
                        order.supplierRate = rate;
                        order.supplierAmt = rate * tranAMT;
                    }
                    if (order.agentId > 0)
                    {
                        order.promRate = PayRateFactory.GetUserPayRate(order.agentId, order.typeId);
                        order.promAmt = (order.promRate - order.payRate) * tranAMT;
                        if (order.promAmt < new Decimal(0))
                            order.promAmt = new Decimal(0);
                    }
                    order.profits = order.supplierAmt - order.payAmt - order.promAmt;
                    order.opstate = opstate;
                    order.msg = msg;
                    if (!string.IsNullOrEmpty(supplierOrderId))
                        order.supplierOrder = supplierOrderId;
                    order.errtype = errtype;
                    this.Complete(order);
                    if (order.ordertype == 1)
                        new OrderCardNotify().DoNotify(order);
                    WebCache.GetCacheService().RemoveObject(orderId);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool DoCardComplete(int supplierId, string orderId, string supplierOrderId, int status, string opstate, string msg, string userviewmsg, Decimal tranAMT, Decimal suppAmt, string errtype, byte returnmethod)
        {
            return this.DoCardComplete(supplierId, orderId, supplierOrderId, status, opstate, msg, userviewmsg, tranAMT, suppAmt, true, errtype, returnmethod);
        }

        public bool DoMultiCardComplete(int supplierId, string orderId, int serial, string supplierOrderId, int status, string opstate, string msg, Decimal tranAMT, Decimal suppAmt, bool isDeduct)
        {
            try
            {
                string objId = orderId + serial.ToString();
                CardItemInfo order1 = WebCache.GetCacheService().RetrieveObject(objId) as CardItemInfo ?? this.GetItemModel(orderId, serial);
                if (order1 != null)
                {
                    order1.status = status;
                    order1.realvalue = tranAMT;
                    order1.suppid = supplierId;
                    order1.opstate = opstate;
                    order1.msg = msg;
                    order1.completetime = new DateTime?(DateTime.Now);
                    if (status == 2)
                    {
                        order1.payrate = new Decimal?(PayRateFactory.GetUserPayRate(order1.userid, order1.cardtype));
                        if (order1.agentId > 0)
                            order1.promrate = PayRateFactory.GetUserPayRate(order1.agentId, order1.cardtype);
                    }
                    bool allCompleted = false;
                    string opstate1 = string.Empty;
                    string ovalue = string.Empty;
                    Decimal ototalvalue = new Decimal(0);
                    this.ItemComplete(order1, out allCompleted, out opstate1, out ovalue, out ototalvalue);
                    if (allCompleted)
                    {
                        OrderCardInfo order2 = WebCache.GetCacheService().RetrieveObject(orderId) as OrderCardInfo ?? this.GetModel(orderId);
                        if (order2 != null)
                        {
                            order2.opstate = opstate1;
                            order2.ovalue = ovalue;
                            order2.realvalue = new Decimal?(ototalvalue);
                            new OrderCardNotify().DoNotify(order2);
                            WebCache.GetCacheService().RemoveObject(orderId);
                        }
                    }
                }
                WebCache.GetCacheService().RemoveObject(objId);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public void ReceiveSuppResult(int suppId, string orderId, string supplierOrderId, int status, string opstate, string msg, Decimal tranAMT, Decimal suppAmt, string errtype)
        {
            string userviewmsg = msg;
            this.ReceiveSuppResult(true, suppId, orderId, supplierOrderId, status, opstate, msg, userviewmsg, tranAMT, suppAmt, errtype, (byte)1);
        }

        public void ReceiveSuppResult(int suppId, string orderId, string supplierOrderId, int status, string opstate, string msg, string userviewmsg, Decimal tranAMT, Decimal suppAmt, string errtype, byte returnmethod)
        {
            this.ReceiveSuppResult(true, suppId, orderId, supplierOrderId, status, opstate, msg, userviewmsg, tranAMT, suppAmt, errtype, returnmethod);
        }

        public void ReceiveSuppResult(bool iscache, int suppId, string orderId, string supplierOrderId, int status, string opstate, string msg, string userviewmsg, Decimal tranAMT, Decimal suppAmt, string errtype, byte returnmethod)
        {
            try
            {
                if (iscache)
                {
                    string key = "ReceiveSuppResult" + orderId;
                    if (HttpRuntime.Cache[key] != null)
                        return;
                    HttpRuntime.Cache.Insert(key, (object)status, (CacheDependency)null, DateTime.Now.AddSeconds(10.0), TimeSpan.Zero);
                }
                this.DoCardComplete(suppId, orderId, supplierOrderId, status, opstate, msg, userviewmsg, tranAMT, suppAmt, errtype, returnmethod);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public bool RepairOrder(int suppId, string orderId, string supplierOrderId, int status, string opstate, string msg, string userviewMsg, Decimal tranAMT, Decimal suppAmt, string errtype, byte method)
        {
            try
            {
                if (orderId.Length <= 20)
                    return this.DoCardComplete(suppId, orderId, supplierOrderId, status, opstate, msg, userviewMsg, tranAMT, suppAmt, errtype, method);
                string orderId1 = orderId.Substring(0, 20);
                int serial = Convert.ToInt32(orderId.Substring(20));
                this.DoMultiCardComplete(suppId, orderId1, serial, supplierOrderId, status, opstate, msg, tranAMT, suppAmt, false);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
    }
}
