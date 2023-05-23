using System;
using System.Web;
using viviapi.BLL;
using viviLib.Security;

namespace viviapi.ETAPI
{
    public class Bank60866 : ETAPIBase
    {
        private static int suppId = 60866;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/Bank60866_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Bank60866_notify.aspx";
            }
        }

        public Bank60866()
          : base(Bank60866.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode, bool isform)
        {
            string str1 = this.postBankUrl;
            if (string.IsNullOrEmpty(str1))
                str1 = "http://paybank.yzch.net/pay.aspx";
            bankcode = this.GetBankCode(bankcode);
            orderAmt = Decimal.Round(orderAmt, 2);
            string str2 = Cryptography.MD5(string.Format("userid={0}&orderid={1}&bankid={2}&keyvalue={3}", (object)this.suppAccount, (object)orderid, (object)bankcode, (object)this.suppKey).ToLower(), "GB2312");
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5;
            if (!isform)
                str5 = string.Format("{0}?userid={1}&orderid={2}&money={3}&url={4}&aurl={5}&bankid={6}&ext={7}&sign={8}", (object)str1, (object)this.suppAccount, (object)orderid, (object)orderAmt, (object)this.notifyUrl, (object)this.returnurl, (object)bankcode, (object)str3, (object)str2);
            else
                str5 = string.Concat(new object[4]
                {
          (object) ("<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"userid\" value=\"" + this.suppAccount + "\" />" + "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />"),
          (object) "<input type=\"hidden\" name=\"money\" value=\"",
          (object) orderAmt,
          (object) "\" />"
                }) + "<input type=\"hidden\" name=\"url\" value=\"" + this.notifyUrl + "\" />" + "<input type=\"hidden\" name=\"aurl\" value=\"" + this.returnurl + "\" />" + "<input type=\"hidden\" name=\"bankid\" value=\"" + bankcode + "\" />" + "<input type=\"hidden\" name=\"ext\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + str2 + "\" />" + "</form>";
            return str5;
        }

        public void ReturnBank()
        {
            string supplierOrderId = HttpContext.Current.Request.QueryString["returncode"].ToString();
            string str1 = HttpContext.Current.Request.QueryString["userid"].ToString();
            string orderId = HttpContext.Current.Request.QueryString["orderid"].ToString();
            string s = HttpContext.Current.Request.QueryString["money"].ToString();
            string str2 = HttpContext.Current.Request.QueryString["sign"].ToString();
            HttpContext.Current.Request.QueryString["ext"].ToString();
            string str3 = Cryptography.MD5(string.Format("returncode={0}&userid={1}&orderid={2}&keyvalue={3}", (object)supplierOrderId, (object)str1, (object)orderId, (object)this.suppKey), "GB2312");
            if (!(str2 == str3))
                return;
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (supplierOrderId == "1")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            new OrderBank().DoBankComplete(Bank60866.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), false, true);
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
            string supplierOrderId = HttpContext.Current.Request.QueryString["returncode"].ToString();
            string str1 = HttpContext.Current.Request.QueryString["userid"].ToString();
            string orderId = HttpContext.Current.Request.QueryString["orderid"].ToString();
            string s = HttpContext.Current.Request.QueryString["money"].ToString();
            string str2 = HttpContext.Current.Request.QueryString["sign"].ToString();
            HttpContext.Current.Request.QueryString["ext"].ToString();
            string str3 = Cryptography.MD5(string.Format("returncode={0}&userid={1}&orderid={2}&keyvalue={3}", (object)supplierOrderId, (object)str1, (object)orderId, (object)this.suppKey), "GB2312");
            if (!(str2 == str3))
                return;
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (supplierOrderId == "1")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            new OrderBank().DoBankComplete(Bank60866.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), true, false);
            HttpContext.Current.Response.Write("ok");
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "1001";
                    break;
                case "967":
                    str2 = "1002";
                    break;
                case "964":
                    str2 = "1005";
                    break;
                case "965":
                    str2 = "1003";
                    break;
                case "963":
                    str2 = "1052";
                    break;
                case "977":
                    str2 = "1004";
                    break;
                case "981":
                    str2 = "1020";
                    break;
                case "980":
                    str2 = "1006";
                    break;
                case "974":
                    str2 = "1008";
                    break;
                case "985":
                    str2 = "1027";
                    break;
                case "962":
                    str2 = "1021";
                    break;
                case "982":
                    str2 = "1025";
                    break;
                case "972":
                    str2 = "1009";
                    break;
                case "989":
                    str2 = "1032";
                    break;
                case "986":
                    str2 = "1022";
                    break;
                case "978":
                    str2 = "1010";
                    break;
                case "975":
                    str2 = "1024";
                    break;
                case "971":
                    str2 = "1028";
                    break;
                default:
                    str2 = "1002";
                    break;
            }
            return str2;
        }
    }
}
