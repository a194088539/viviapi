using System;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.ETAPI
{
    public class EkaBank : ETAPIBase
    {
        private static int suppId = 365;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/Eka_Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Eka_Bank_Notify.aspx";
            }
        }

        public EkaBank()
          : base(EkaBank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string BankID)
        {
            string str1 = this.postBankUrl;
            if (string.IsNullOrEmpty(str1))
                str1 = "http://gateway.wxk8.com/chargebank.aspx";
            orderAmt = Decimal.Round(orderAmt, 2);
            string str2 = Cryptography.MD5(string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", (object)this.suppAccount, (object)BankID, (object)orderAmt, (object)orderid, (object)this.notifyUrl), "GB2312");
            return string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}", (object)str1, (object)this.suppAccount, (object)BankID, (object)orderAmt, (object)orderid, (object)this.notifyUrl, (object)this.returnurl, (object)str2);
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
                new OrderBank().DoBankComplete(EkaBank.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), false, true);
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
                new OrderBank().DoBankComplete(EkaBank.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
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
                    str2 = "CMB";
                    break;
                case "967":
                    str2 = "ICBC";
                    break;
                case "964":
                    str2 = "ABC";
                    break;
                case "965":
                    str2 = "CCB";
                    break;
                case "963":
                    str2 = "BOC";
                    break;
                case "977":
                    str2 = "SPDB";
                    break;
                case "981":
                    str2 = "COMM";
                    break;
                case "980":
                    str2 = "CMBC";
                    break;
                case "974":
                    str2 = "SDB";
                    break;
                case "985":
                    str2 = "GDB";
                    break;
                case "962":
                    str2 = "CITIC";
                    break;
                case "982":
                    str2 = "HXB";
                    break;
                case "972":
                    str2 = "CIB";
                    break;
                case "984":
                    str2 = "GNXS";
                    break;
                case "1015":
                    str2 = "GZCB";
                    break;
                case "976":
                    str2 = "SHRCB";
                    break;
                case "989":
                    str2 = "BCCB";
                    break;
                case "988":
                    str2 = "CBHB";
                    break;
                case "990":
                    str2 = "BJRCB";
                    break;
                case "979":
                    str2 = "NJCB";
                    break;
                case "986":
                    str2 = "CEB";
                    break;
                case "987":
                    str2 = "HKBEA";
                    break;
                case "1025":
                    str2 = "NBCB";
                    break;
                case "983":
                    str2 = "HCCB";
                    break;
                case "978":
                    str2 = "SZPAB";
                    break;
                case "975":
                    str2 = "BOS";
                    break;
                case "971":
                    str2 = "PSBC";
                    break;
                default:
                    str2 = "ICBC";
                    break;
            }
            return str2;
        }
    }
}
