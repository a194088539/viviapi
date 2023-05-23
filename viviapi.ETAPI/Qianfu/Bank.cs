namespace viviapi.ETAPI.Qianfu
{
    using System;
    using System.Text;
    using System.Web;
    using viviapi.BLL;
    using viviapi.Cache;
    using viviapi.ETAPI;
    using viviapi.Model.Order;
    using viviapi.SysConfig;
    using viviLib.ExceptionHandling;
    using viviLib.Logging;
    using viviLib.Security;

    public class Bank : ETAPIBase
    {
        private static int suppId = 9004;

        public Bank()
            : base(suppId)
        {
        }

        public string GetBankCode(string paymodeId)
        {
            switch (paymodeId)
            {
                case "992":
                    return "992";
                case "1004":
                    return "1004";
                case "1005":
                    return "1005";
                case "1006":
                    return "1006";
                case "1007":
                    return "1007";
                case "1008":
                    return "1008";
                case "1009":
                    return "1009";
                case "2001":
                    return "2001";
                case "2002":
                    return "2002";
                case "2003":
                    return "2003";

                default:
                    return paymodeId;
            }
        }

        public void Notify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string _opstate = request.QueryString["opstate"];
            string s = request.QueryString["ovalue"];
            string sign = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["sysorderid"];
            string str6 = request.QueryString["Systime"];
            string check = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { orderId, _opstate, s, base.suppKey }));
            LogHelper.Write("千付异步通知成功：" + request.QueryString.ToString());
            try
            {
                if (check == sign)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (_opstate.Equals("0") || _opstate.Equals("-3"))
                    {
                        opstate = "0";
                        status = 2;

                        new OrderBank().DoBankComplete(suppId, orderId, supplierOrderId, status, opstate, "支付成功", decimal.Parse(s), 0M, true, false);
                        HttpContext.Current.Response.Write("opstate=0");
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public string PayBank(string orderid, decimal orderAmt, string BankID)
        {
            string postBankUrl = "";
            if (string.IsNullOrEmpty(postBankUrl))
            {
                postBankUrl = "http://pay.qianfupay.com/chargebank.aspx";
            }
            string bankcode = GetBankCode(BankID);
            orderAmt = decimal.Round(orderAmt, 2);
            string str3 = Cryptography.MD5(string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", new object[] { base.suppAccount, bankcode, orderAmt, orderid, this.notifyUrl }) + base.suppKey, "GB2312");
            return string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}", new object[] { postBankUrl, base.suppAccount, bankcode, orderAmt, orderid, this.notifyUrl, this.returnurl, str3 });

        }


        public string PayBankApp(string orderid, decimal orderAmt, string BankID)
        {
            string postBankUrl = "";
            if (string.IsNullOrEmpty(postBankUrl))
            {
                postBankUrl = "http://pay.qianfupay.com/chargebank.aspx";
            }
            orderAmt = decimal.Round(orderAmt, 2);
            string str3 = Cryptography.MD5(string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", new object[] { base.suppAccount, this.GetBankCode(BankID), orderAmt, orderid, this.notifyUrl }) + base.suppKey, "GB2312");
            string url = string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}", new object[] { postBankUrl, base.suppAccount, this.GetBankCode(BankID), orderAmt, orderid, this.notifyUrl, this.returnurl, str3 });
            return url;
        }

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

        public void ReturnBank()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string str2 = request.QueryString["opstate"];
            string s = request.QueryString["ovalue"];
            string str4 = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["sysorderid"];
            string str6 = request.QueryString["Systime"];
            string str8 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { orderId, str2, s, base.suppKey }));
            //LogHelper.WriteS(request.QueryString.ToString());
            try
            {
                var orderModel = WebCache.GetCacheService().RetrieveObject(orderId) as OrderBankInfo;
                if (orderModel == null)
                {
                    orderModel = new OrderBank().GetModel(orderId);
                }
                if (RuntimeSetting.Paycompletpage == "0")
                {
                    if (!string.IsNullOrEmpty(orderModel.returnurl))
                    {
                        this.BankOrderReturn(orderModel);
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendFormat("o={0}", orderModel.orderid);
                        builder.AppendFormat("&uo={0}", orderModel.userorder);
                        builder.AppendFormat("&c={0}", orderModel.paymodeId);
                        builder.AppendFormat("&t={0}", orderModel.typeId);
                        builder.AppendFormat("&v={0:f2}", s);
                        builder.AppendFormat("&e={0}", "支付成功");
                        builder.AppendFormat("&u={0}", orderModel.userid);
                        builder.AppendFormat("&s={0}", "2");
                        HttpContext.Current.Response.Redirect(RuntimeSetting.SiteDomain + "/PayResult.aspx?" + builder.ToString(), false);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/Qianfu/Notify.aspx");
            }
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/notify/Qianfu/Return.aspx");
            }
        }
    }
}


