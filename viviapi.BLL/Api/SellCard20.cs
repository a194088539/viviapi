using System;
using System.Text;
using System.Web;
using viviapi.Model;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.BLL.Api
{
    public class SellCard20
    {
        public static string Successflag = "opstate=0";

        public static string VersionName
        {
            get
            {
                if (WebInfoFactory.CurrentWebInfo != null)
                    return WebInfoFactory.CurrentWebInfo.apicardname + "[" + WebInfoFactory.CurrentWebInfo.apicardversion + "]";
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

        public static string CreateNotifyUrl(OrderCardInfo orderinfo, string apikey)
        {
            string str = string.Empty;
            if (orderinfo == null || string.IsNullOrEmpty(apikey))
                return str;
            string notifyurl = orderinfo.notifyurl;
            Decimal realvalue1 = new Decimal(0);
            Decimal? realvalue2 = orderinfo.realvalue;
            if (realvalue2.HasValue)
            {
                realvalue2 = orderinfo.realvalue;
                realvalue1 = Decimal.Round(realvalue2.Value, 0);
            }
            string paramValue1 = SellCard20.ConvertErrorCode(orderinfo.supplierId, orderinfo.errtype, orderinfo.refervalue, realvalue1);
            string paramValue2 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)orderinfo.userorder, (object)paramValue1, (object)realvalue1, (object)apikey));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("orderid={0}", (object)SellCard20.UrlEncode(orderinfo.userorder));
            stringBuilder.AppendFormat("&opstate={0}", (object)SellCard20.UrlEncode(paramValue1));
            stringBuilder.AppendFormat("&ovalue={0}", (object)SellCard20.UrlEncode(realvalue1.ToString()));
            stringBuilder.AppendFormat("&sysorderid={0}", (object)SellCard20.UrlEncode(orderinfo.orderid));
            stringBuilder.AppendFormat("&systime={0}", (object)SellCard20.UrlEncode(orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss")));
            stringBuilder.AppendFormat("&attach={0}", (object)SellCard20.UrlEncode(orderinfo.attach));
            stringBuilder.AppendFormat("&msg={0}", (object)SellCard20.UrlEncode(orderinfo.userViewMsg));
            stringBuilder.AppendFormat("&sign={0}", (object)SellCard20.UrlEncode(paramValue2));
            return notifyurl + "?" + stringBuilder.ToString();
        }

        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;
            return HttpUtility.UrlEncode(paramValue, Encoding.GetEncoding("gb2312"));
        }

        public static string ConvertErrorCode(int suppId, string errcode, Decimal refervalue, Decimal realvalue)
        {
            SupplierCode supplierCode = (SupplierCode)suppId;
            string str = "99";
            if (supplierCode == SupplierCode.OfCard)
            {
                if (errcode == "2000")
                    str = "0";
                else if (errcode == "2010")
                    str = "10";
                else if (errcode == "2011")
                    str = "11";
                else if (errcode == "2012")
                    str = "12";
                else if (errcode == "2013")
                    str = "13";
                else if (errcode == "2016")
                    str = "16";
                else if (errcode == "2017")
                    str = "17";
                else if (errcode == "2018")
                    str = "18";
                else if (errcode == "2019")
                    str = "19";
            }
            else if (supplierCode == SupplierCode.HuiYuan)
            {
                if (errcode == "0")
                {
                    str = "0";
                    if (realvalue != refervalue)
                        str = "11";
                }
                else if (errcode == "9")
                    str = "16";
                else if (errcode == "10")
                    str = "18";
                else if (errcode == "98")
                    str = "19";
            }
            else if (supplierCode == SupplierCode.Card60866)
                str = !string.IsNullOrEmpty(errcode) && !(errcode == "0") ? (!(errcode == "1001") ? (!(errcode == "1003") ? "10" : "13") : "16") : "0";
            if (supplierCode == SupplierCode.LongBaoPay)
                str = errcode;
            return str;
        }

        public static string ConvertSynchronousErrorCode(SupplierCode supp, string errcode)
        {
            string str = "99";
            if (supp == SupplierCode.OfCard)
            {
                if (errcode == "2001")
                    str = "1";
                else if (errcode == "2002")
                    str = "2";
                else if (errcode == "2005")
                    str = "5";
                else if (errcode == "2009")
                    str = "19";
                else if (errcode == "2010")
                    str = "10";
                else if (errcode == "2012")
                    str = "12";
                else if (errcode == "2016")
                    str = "16";
            }
            else if (supp == SupplierCode.HuiYuan)
            {
                str = "99";
                if (errcode == "0")
                    str = "1";
                else if (errcode == "9")
                    str = "16";
                else if (errcode == "10")
                    str = "18";
                else if (errcode == "98")
                    str = "19";
            }
            else if (supp == SupplierCode.Card60866)
            {
                if (errcode == "1")
                    str = "1";
            }
            else if (supp == SupplierCode.LongBaoPay)
                str = errcode;
            return str;
        }

        public static string ConvertSynchronousErrorCodeForV1(SupplierCode supp, string errcode)
        {
            string str = "-1";
            if (supp == SupplierCode.OfCard)
                str = !(errcode == "2001") ? "-1" : "0";
            else if (supp == SupplierCode.HuiYuan)
            {
                str = "-1";
                if (errcode == "0")
                    str = "0";
            }
            else if (supp == SupplierCode.Card60866)
            {
                if (errcode == "1")
                    str = "0";
            }
            else if (supp == SupplierCode.LongBaoPay)
                str = !(str == "1") ? "-1" : "0";
            return str;
        }
    }
}
