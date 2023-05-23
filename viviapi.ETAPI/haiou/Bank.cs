using System;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.ETAPI.haiou
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10039;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/haiou/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/haiou/Bank_Notify.aspx";
            }
        }

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string BankID)
        {
            string postBankUrl = this.postBankUrl;
            if (string.IsNullOrEmpty(postBankUrl))
                return string.Empty;
            orderAmt = Decimal.Round(orderAmt, 2);
            string str = Cryptography.MD5(string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", (object)this.suppAccount, (object)BankID, (object)orderAmt, (object)orderid, (object)this.notifyUrl) + this.suppKey, "GB2312");
            return string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}", (object)postBankUrl, (object)this.suppAccount, (object)BankID, (object)orderAmt, (object)orderid, (object)this.notifyUrl, (object)this.returnurl, (object)str);
        }

        public void ReturnBank()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string str1 = request.QueryString["opstate"];
            string s = request.QueryString["ovalue"];
            string str2 = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["ekaorderid"];
            string str3 = request.QueryString["ekatime"];
            string str4 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)orderId, (object)str1, (object)s, (object)this.suppKey));
            try
            {
                if (!(str4 == str2))
                    return;
                string opstate = "-1";
                int status = 4;
                if (str1.ToLower() == "0")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), false, true);
                HttpContext.Current.Response.Write("opstate=0");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "-1":
                    str = "系统忙";
                    break;
                case "1":
                    str = "商户订单号无效";
                    break;
                case "2":
                    str = "银行编码错误";
                    break;
                case "3":
                    str = "商户不存在";
                    break;
                case "4":
                    str = "验证签名失败";
                    break;
                case "5":
                    str = "商户储值关闭";
                    break;
                case "6":
                    str = "金额超出限额";
                    break;
            }
            return str;
        }

        public void Notify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string str1 = request.QueryString["opstate"];
            string s = request.QueryString["ovalue"];
            string str2 = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["ekaorderid"];
            string str3 = request.QueryString["ekatime"];
            string str4 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)orderId, (object)str1, (object)s, (object)this.suppKey));
            try
            {
                if (!(str4 == str2))
                    return;
                string opstate = "-1";
                int status = 4;
                if (str1.ToLower() == "0")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("opstate=0");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "970";
                    break;
                case "967":
                    str2 = "967";
                    break;
                case "964":
                    str2 = "964";
                    break;
                case "965":
                    str2 = "965";
                    break;
                case "963":
                    str2 = "963";
                    break;
                case "977":
                    str2 = "977";
                    break;
                case "981":
                    str2 = "981";
                    break;
                case "980":
                    str2 = "980";
                    break;
                case "974":
                    str2 = "974";
                    break;
                case "985":
                    str2 = "985";
                    break;
                case "962":
                    str2 = "962";
                    break;
                case "982":
                    str2 = "982";
                    break;
                case "972":
                    str2 = "972";
                    break;
                case "984":
                    str2 = "984";
                    break;
                case "1015":
                    str2 = "1015";
                    break;
                case "976":
                    str2 = "976";
                    break;
                case "989":
                    str2 = "989";
                    break;
                case "988":
                    str2 = "988";
                    break;
                case "990":
                    str2 = "990";
                    break;
                case "979":
                    str2 = "979";
                    break;
                case "986":
                    str2 = "986";
                    break;
                case "987":
                    str2 = "987";
                    break;
                case "1025":
                    str2 = "1025";
                    break;
                case "983":
                    str2 = "983";
                    break;
                case "978":
                    str2 = "978";
                    break;
                case "975":
                    str2 = "975";
                    break;
                case "971":
                    str2 = "971";
                    break;
                case "993":
                    str2 = "993";
                    break;
                case "992":
                    str2 = "992";
                    break;
                case "1004":
                    str2 = "1004";
                    break;
                case "1003":
                    str2 = "1003";
                    break;
                default:
                    str2 = "ICBC";
                    break;
            }
            return str2;
        }
    }
}

