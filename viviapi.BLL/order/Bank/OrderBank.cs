namespace viviapi.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Transactions;
    using System.Web;
    using viviapi.BLL.Payment;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.DALFactory;
    using viviapi.IBLLStrategy;
    using viviapi.MessagingFactory;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.Model.Order;
    using viviapi.Model.User;
    using viviapi.SysConfig;
    using viviLib.Data;
    using viviLib.ExceptionHandling;
    public class OrderBank
    {
        private static readonly viviapi.IDAL.IOrderBank dal = DataAccess.CreateOrderBank();
        private static readonly IOrderBankStrategy orderInsertStrategy = LoadInsertStrategy();
        private static readonly viviapi.IMessaging.IOrderBank orderQueue = QueueAccess.CreateBankOrder();

        public void BankOrderReturn(OrderBankInfo orderinfo)
        {
            string s = SystemApiHelper.NewBankNoticeUrl(orderinfo, false);
            if (orderinfo.version == "vyb1.00")
            {
                HttpContext.Current.Response.Write(s);
            }
            else
            {
                HttpContext.Current.Response.Redirect(s, false);
            }
        }

        public void Complete(OrderBankInfo order)
        {
            orderInsertStrategy.Complete(order);
        }

        public bool Deduct(string orderId)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(orderId))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        flag = dal.Deduct(orderId);
                        scope.Complete();
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    flag = false;
                }
            }
            return flag;
        }

        public bool DoBankComplete(int supplierId, string orderId, string supplierOrderId, int status, string opstate, string msg, decimal tranAMT, out string returnUrl)
        {
            try
            {
                returnUrl = string.Empty;
                bool flag = false;
                OrderBankInfo order = WebCache.GetCacheService().RetrieveObject(orderId) as OrderBankInfo;
                if (order == null)
                {
                    order = this.GetModel(orderId);
                    flag = true;
                }
                else
                {
                    flag = order.status == 1;
                }
                if ((order != null) && flag)
                {
                    order.orderid = orderId;
                    order.status = status;
                    if (SysConfig.isOpenDeduct && (status == 2))
                    {
                        UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(order.userid);
                        if ((cacheUserBaseInfo != null) && (new Random(Guid.NewGuid().GetHashCode()).Next(1, 0x3e8) <= cacheUserBaseInfo.CPSDrate))
                        {
                            order.status = 8;
                        }
                    }
                    order.realvalue = new decimal?(tranAMT);
                    order.supplierId = supplierId;
                    order.completetime = new DateTime?(DateTime.Now);
                    if (order.payRate <= 0M)
                    {
                        order.payRate = PayRateFactory.GetUserPayRate(order.userid, order.typeId);
                    }
                    order.payAmt = order.payRate * tranAMT;
                    if (order.agentId >= 0)
                    {
                        order.promRate = PayRateFactory.GetUserPayRate(order.agentId, order.typeId);
                        order.promAmt = (order.promRate - order.payRate) * tranAMT;
                        if (order.promAmt < 0M)
                        {
                            order.promAmt = 0M;
                        }
                    }
                    if (order.supplierRate <= 0M)
                    {
                        ChannelInfo info3 = viviapi.BLL.Channel.Channel.GetModel(order.paymodeId, order.userid, false);
                        if (info3 != null)
                        {
                            order.supplierRate = info3.supprate;
                        }
                    }
                    order.commission = 0;
                    if (order.manageId > 0)
                    {
                        Manage model = ManageFactory.GetModel(order.manageId.Value);
                        if ((model != null) && (model.commissiontype == 2))
                        {
                            decimal num2 = tranAMT;
                            order.commission = num2 * model.commission;
                        }
                        if (order.commission < 0M)
                        {
                            order.commission = 0;
                        }
                    }
                    order.supplierAmt = order.supplierRate * tranAMT;
                    order.profits = ((order.supplierAmt - order.payAmt) - order.promAmt) - order.commission.Value;
                    order.opstate = opstate;
                    order.msg = msg;
                    if (!string.IsNullOrEmpty(supplierOrderId))
                    {
                        order.supplierOrder = supplierOrderId;
                    }
                    this.Complete(order);
                    if (order.ordertype == 1)
                    {
                        new OrderBankNotify().DoNotify(order);
                    }
                    WebCache.GetCacheService().RemoveObject(orderId);
                }
                if (!string.IsNullOrEmpty(order.returnurl))
                {
                    returnUrl = SystemApiHelper.NewBankNoticeUrl(order, false);
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("o={0}", order.orderid);
                    builder.AppendFormat("&uo={0}", order.userorder);
                    builder.AppendFormat("&t={0}", order.typeId);
                    builder.AppendFormat("&c={0}", order.paymodeId);
                    builder.AppendFormat("&s={0}", order.status);
                    builder.AppendFormat("&v={0:f2}", tranAMT);
                    builder.AppendFormat("&e={0}", msg);
                    builder.AppendFormat("&u={0}", order.userid);
                    returnUrl = RuntimeSetting.SiteDomain + "/PayResult.aspx?" + builder.ToString();
                }
                return true;
            }
            catch (ThreadAbortException)
            {
                returnUrl = string.Empty;
                return true;
            }
            catch (Exception exception2)
            {
                returnUrl = string.Empty;
                ExceptionHandler.HandleException(exception2);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="orderId"></param>
        /// <param name="supplierOrderId"></param>
        /// <param name="status"></param>
        /// <param name="opstate"></param>
        /// <param name="msg"></param>
        /// <param name="tranAMT">提交价</param>
        /// <param name="suppAmt">结算价</param>
        /// <param name="removecache"></param>
        /// <param name="isreturn"></param>
        /// <returns></returns>
        public bool DoBankComplete(int supplierId, string orderId, string supplierOrderId, int status, string opstate, string msg, decimal tranAMT, decimal suppAmt, bool removecache, bool isreturn)
        {
            return this.DoBankComplete(supplierId, orderId, supplierOrderId, status, opstate, msg, tranAMT, suppAmt, removecache, isreturn, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="orderId">订单号</param>
        /// <param name="supplierOrderId">接口供应商订单号</param>
        /// <param name="status">status=2代表成功</param>
        /// <param name="opstate"></param>
        /// <param name="msg"></param>
        /// <param name="tranAMT">提交价</param>
        /// <param name="suppAmt">结算价，提交0根据系统设置计算，否则根据提交值计算</param>
        /// <param name="removecache"></param>
        /// <param name="isreturn"></param>
        /// <param name="isDeduct">扣量</param>
        /// <returns></returns>
        public bool DoBankComplete(int supplierId, string orderId, string supplierOrderId, int status, string opstate, string msg, decimal tranAMT, decimal suppAmt, bool removecache, bool isreturn, bool isDeduct)
        {
            try
            {
                bool flag = false;
                OrderBankInfo order = WebCache.GetCacheService().RetrieveObject(orderId) as OrderBankInfo;
                if (order == null)
                {
                    order = this.GetModel(orderId);

                    flag = (order.status == 1);
                }
                else//有缓存
                {
                    flag = (order.status == 1);
                }

                if ((order != null) && flag && !isreturn)//只处理订单状态为初始化状态的订单,只在异步调用时处理订单
                {
                    order.orderid = orderId;
                    order.status = status;
                    order.opstate = opstate;
                    order.msg = msg;

                    //根据设置选择是否标记为扣量
                    if ((isDeduct && SysConfig.isOpenDeduct) && (status == 2))
                    {
                        UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(order.userid);
                        if ((cacheUserBaseInfo != null) && (new Random(Guid.NewGuid().GetHashCode()).Next(1, 0x3e8) <= cacheUserBaseInfo.CPSDrate))
                        {
                            order.status = 8;
                        }
                    }
                    if (status == 2)
                    {
                        order.realvalue = new decimal?(tranAMT);
                    }
                    order.supplierId = supplierId;
                    order.completetime = new DateTime?(DateTime.Now);
                    if (status == 2)
                    {
                        //先计算商家所得
                        if (order.payRate <= 0M)
                        {
                            order.payRate = PayRateFactory.GetUserPayRate(order.userid, order.typeId);
                        }

                        order.payAmt = order.payRate * tranAMT;

                        //平台的结算价
                        if ((suppAmt > 0M) && (order.refervalue > 0M))//根据返回值计算费率
                        {
                            order.supplierRate = suppAmt / order.refervalue;
                            order.supplierAmt = suppAmt;
                        }

                        else//根据系统设置费率计算
                        {
                            if (order.supplierRate <= 0M)
                            {
                                decimal rate = SupplierPayRateFactory.GetRate(supplierId, order.typeId);//供应商费率
                                order.supplierRate = rate;
                                order.supplierAmt = rate * tranAMT;
                            }
                            order.supplierAmt = order.supplierRate * tranAMT;
                        }

                        //有代理计算代理分润
                        if (order.agentId > 0)
                        {
                            order.promRate = PayRateFactory.GetUserPayRate(order.agentId, order.typeId);
                            order.promAmt = (order.promRate - order.payRate) * tranAMT;
                            if (order.promAmt < 0M)
                            {
                                order.promAmt = 0M;
                            }
                        }

                        order.commission = 0;
                        order.supplierAmt = order.supplierRate * tranAMT;
                        //平台利润=平台结算价-（商家所得+代理所得+业务员提成）
                        order.profits = ((order.supplierAmt - order.payAmt) - order.promAmt) - order.commission.Value;

                        //计算所属业务员提成
                        if (order.manageId > 0)
                        {
                            decimal num3 = order.profits;
                            Manage model = ManageFactory.GetModel(order.manageId.Value);
                            if ((model != null) && (model.commissiontype == 2))
                            {

                                order.commission = (num3 * model.commission.Value);
                            }

                            if (order.commission.Value < 0M)
                            {
                                order.commission = 0m;
                            }
                            order.profits = num3 - order.commission.Value;
                        }

                    }

                    if (!string.IsNullOrEmpty(supplierOrderId))
                    {
                        order.supplierOrder = supplierOrderId;
                    }
                    this.Complete(order);
                    if (order.ordertype == 1 && status == 2)
                    {
                        new OrderBankNotify().DoNotify(order);
                    }
                    if (removecache)
                    {
                        WebCache.GetCacheService().RemoveObject(orderId);
                    }
                }

                if (isreturn && (RuntimeSetting.Paycompletpage == "0"))
                {
                    order.opstate = opstate;
                    if (!string.IsNullOrEmpty(order.returnurl))
                    {
                        this.BankOrderReturn(order);
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendFormat("o={0}", order.orderid);
                        builder.AppendFormat("&uo={0}", order.userorder);
                        builder.AppendFormat("&c={0}", order.paymodeId);
                        builder.AppendFormat("&t={0}", order.typeId);
                        builder.AppendFormat("&v={0:f2}", tranAMT);
                        builder.AppendFormat("&e={0}", msg);
                        builder.AppendFormat("&u={0}", order.userid);
                        builder.AppendFormat("&s={0}", order.status);
                        HttpContext.Current.Response.Redirect(RuntimeSetting.SiteDomain + "/PayResult.aspx?" + builder.ToString(), false);
                    }
                }
                return true;
            }
            catch (ThreadAbortException)
            {
                return true;
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleException(exception2);
                return false;
            }
        }

        public string GenerateUniqueOrderId(int typeId)
        {
            string str = typeId.ToString().Substring(1);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string objId = DateTime.Now.ToString("yyMMddHHmmssff") + str + random.Next(0x3e8).ToString("0000");
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                return this.GenerateUniqueOrderId(typeId);
            }
            WebCache.GetCacheService().AddObject(objId, objId);
            return objId;
        }

        public string GetCallBackUrl(OrderBankInfo orderinfo)
        {
            return SystemApiHelper.GetBankBackUrl(orderinfo, true);
        }

        public OrderBankInfo GetModel(long id)
        {
            if (id <= 0L)
            {
                return null;
            }
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderBankInfo GetModel(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return null;
            }
            try
            {
                return dal.GetModel(orderId);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderBankInfo GetModel(long id, int userid)
        {
            if (id <= 0L)
            {
                return null;
            }
            try
            {
                return dal.GetModel(id, userid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
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
            }
            return "";
        }

        public void Insert(OrderBankInfo order)
        {
            orderInsertStrategy.Insert(order);
        }

        private static IOrderBankStrategy LoadInsertStrategy()
        {
            string orderStrategyAssembly = RuntimeSetting.OrderStrategyAssembly;
            string orderStrategyClass = RuntimeSetting.OrderStrategyClass;
            return (IOrderBankStrategy)Assembly.Load(orderStrategyAssembly).CreateInstance(orderStrategyClass);
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            return dal.PageSearch(searchParams, pageSize, page, orderby);
        }

        public OrderBankInfo ReceiveFromQueue(int timeout)
        {
            return orderQueue.Receive(timeout);
        }

        public bool ReDeduct(string orderId)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(orderId))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        flag = dal.ReDeduct(orderId);
                        scope.Complete();
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    flag = false;
                }
            }
            return flag;
        }

        public bool UpdateNotifyInfo(OrderBankInfo order)
        {
            try
            {
                if (order == null)
                {
                    return false;
                }
                return dal.Notify(order);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
    }
}

