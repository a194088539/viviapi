using System;
using System.Text;
using System.Web;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.BLL.Api
{
    public class PayBank20
    {
        public static string Successflag = "opstate=0";

        public static string VersionName
        {
            get
            {
                if (WebInfoFactory.CurrentWebInfo != null)
                    return WebInfoFactory.CurrentWebInfo.apibankname + "[" + WebInfoFactory.CurrentWebInfo.apibankversion + "]";
                return string.Empty;
            }
        }

        public static bool SignVerification(string type, string userid, string cardno, string cardpwd, string value, string orderid, string restrict, string callbackurl, string key, string sign)
        {
            try
            {
                string str = string.Empty;
                return Cryptography.MD5(string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}{8}", (object)type, (object)userid, (object)cardno, (object)cardpwd, (object)value, (object)restrict, (object)orderid, (object)callbackurl, (object)key)).ToLower() == sign;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static string CreateNotifyUrl(OrderBankInfo orderinfo, bool isNotify, string apiKey)
        {
            string str1 = string.Empty;
            if (orderinfo == null || string.IsNullOrEmpty(apiKey))
                return str1;
            string str2 = !isNotify ? orderinfo.returnurl : orderinfo.notifyurl;
            string userorder = orderinfo.userorder;
            string opstate = orderinfo.opstate;
            string paramValue1 = Decimal.Round(orderinfo.realvalue.Value, 2).ToString();
            string paramValue2 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)userorder, (object)opstate, (object)paramValue1, (object)apiKey));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("orderid={0}", (object)PayBank20.UrlEncode(userorder));
            stringBuilder.AppendFormat("&opstate={0}", (object)PayBank20.UrlEncode(opstate));
            stringBuilder.AppendFormat("&ovalue={0}", (object)PayBank20.UrlEncode(paramValue1));
            stringBuilder.AppendFormat("&sysorderid={0}", (object)PayBank20.UrlEncode(orderinfo.orderid));
            stringBuilder.AppendFormat("&systime={0}", (object)PayBank20.UrlEncode(orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss")));
            stringBuilder.AppendFormat("&attach={0}", (object)PayBank20.UrlEncode(orderinfo.attach));
            stringBuilder.AppendFormat("&msg={0}", (object)PayBank20.UrlEncode(orderinfo.msg));
            stringBuilder.AppendFormat("&sign={0}", (object)PayBank20.UrlEncode(paramValue2));
            return str2 + "?" + stringBuilder.ToString();
        }

        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;
            return HttpUtility.UrlEncode(paramValue, Encoding.GetEncoding("gb2312"));
        }
    }
}
