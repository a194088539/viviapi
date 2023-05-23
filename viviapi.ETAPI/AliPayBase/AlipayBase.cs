using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Logging;

namespace viviapi.ETAPI
{
    public class AliPayBase : ETAPIBase
    {
        private static int suppId = 10019;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/AliPayBase.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/AliPayBase.aspx";
            }
        }

        public AliPayBase()
          : base(AliPayBase.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode)
        {
            string str1 = this.postBankUrl;
            if (string.IsNullOrEmpty(str1))
                str1 = "https://shenghuo.alipay.com/send/payment/fill.htm";
            string suppUserName = this.suppUserName;
            string suppKey = this.suppKey;
            Decimal num = orderAmt;
            string str2 = orderid;
            string str3 = string.Concat(new object[4]
            {
        (object) ("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"optEmail\" value=\"" + suppUserName + "\" />"),
        (object) "<input type=\"hidden\" name=\"payAmount\" value=\"",
        (object) num,
        (object) "\" />"
            }) + "<input type=\"hidden\" name=\"title\" value=\"" + str2 + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            LogHelper.Write(str3);
            return str3;
        }

        public void Notify()
        {
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            string supplierOrderId = HttpContext.Current.Request["tradeNo"];
            string s = HttpContext.Current.Request["Money"];
            string orderId = HttpContext.Current.Request["title"];
            string str1 = HttpContext.Current.Request["memo"];
            string str2 = HttpContext.Current.Request["Sign"];
            string str3 = "0";
            string str4 = AliPayBase.MD5(suppAccount + suppKey + supplierOrderId + s + orderId + str1).ToUpper();
            try
            {
                if (str2 == str4)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (str3 == "0")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderBank().DoBankComplete(AliPayBase.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), false, true);
                    HttpContext.Current.Response.Write("Success");
                }
                else
                    HttpContext.Current.Response.Write("Fail");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public static string GetQueryString(string QueryString, string defaultValue)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request.QueryString[QueryString] == null || request.QueryString[QueryString].Length == 0)
                return defaultValue;
            else
                return request.QueryString[QueryString].Trim();
        }

        public static int GetQueryString(string QueryString, int defaultValue)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request.QueryString[QueryString] == null || request.QueryString[QueryString].Length == 0)
                return defaultValue;
            string strVal = request.QueryString[QueryString].Trim();
            if (AliPayBase.IsInt(strVal))
                return Convert.ToInt32(strVal);
            else
                return defaultValue;
        }

        public static bool IsInt(string strVal)
        {
            return Regex.IsMatch(strVal, "^[+-]?\\d*$");
        }

        public static string MD5(string input)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");
        }
    }
}
