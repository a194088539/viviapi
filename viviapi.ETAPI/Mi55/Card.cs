using System;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI.Mi55
{
    public class Card : ETAPIBase
    {
        private static int suppId = 55;

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/Mi55/Card_Notify.aspx";
            }
        }

        public Card()
          : base(Card.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            string suppAccount = this.suppAccount;
            string str1 = _orderid;
            string str2 = cardvalue.ToString();
            string cardType = this.GetCardType(_typeId);
            string str3 = cardType + cardvalue.ToString();
            string str4 = _cardno;
            string str5 = "0";
            string str6 = _cardpwd;
            string notifyUrl = this.notify_url;
            string str7 = DateTime.Now.ToString("YYYYMMDDHHMMSS");
            string str8 = Cryptography.MD5(string.Format("userid={0}&userorderno={1}&submitmoney={2}&cardid={3}&childcardid={4}&cardno={5}&isencryptpwd={6}&cardpwd={7}&callbackurl={8}&submittime={9}&keyvalue={10}", (object)suppAccount, (object)str1, (object)str2, (object)cardType, (object)str3, (object)str4, (object)str5, (object)str6, (object)notifyUrl, (object)str7, (object)this.suppKey)).ToUpper();
            string str9 = this.postCardUrl + "?";
            if (string.IsNullOrEmpty(this.postCardUrl))
                str9 = "http://card.mi55.com/recoverCard?";
            string url = str9 + string.Format("userid={0}&userorderno={1}&submitmoney={2}&cardid={3}&childcardid={4}&cardno={5}&isencryptpwd={6}&cardpwd={7}&callbackurl={8}&submittime={9}&sign={10}", (object)suppAccount, (object)str1, (object)str2, (object)cardType, (object)str3, (object)str4, (object)str5, (object)str6, (object)notifyUrl, (object)str7, (object)str8);
            string str10 = "-1";
            try
            {
                string @string = WebClientHelper.GetString(url, (string)null, "GET", Encoding.GetEncoding("GB2312"), 10000);
                if (@string.StartsWith("returncode=1"))
                {
                    str10 = "0";
                }
                else
                {
                    string[] strArray = @string.Split('&');
                    if (strArray.Length >= 3)
                        errmsg = strArray[2].Replace("message=", "");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str10;
        }

        public void CardNotify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string errtype = request["returncode"];
            string str1 = request["userid"];
            string supplierOrderId = request["mi55orderno"];
            string orderId = request["userorderno"];
            string str2 = request["submitmoney"];
            string s = request["realmoney"];
            string str3 = request["discountmoney"];
            string str4 = request["cardid"];
            string str5 = request["childcardid"];
            string str6 = request["cardno"];
            string str7 = request["callbacktime"];
            string str8 = request["sign"];
            string msg = request["message"];
            string str9 = Cryptography.MD5(string.Format("returncode={0}&userid={1}&mi55orderno={2}&userorderno={3}&submitmoney={4}&realmoney={5}&discountmoney={6}&cardid={7}&childcardid={8}&cardno={9}&callbacktime={10}&keyvalue={11}", (object)errtype, (object)str1, (object)supplierOrderId, (object)orderId, (object)str2, (object)s, (object)str3, (object)str4, (object)str5, (object)str6, (object)str7, (object)this.suppKey)).ToUpper();
            try
            {
                if (!(str9 == str8))
                    return;
                string opstate = "-1";
                int status = 4;
                if (errtype == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderCard().ReceiveSuppResult(Card.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), errtype);
                HttpContext.Current.Response.Write("result=success");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public string GetCardType(int paytype)
        {
            string str = paytype.ToString();
            if (paytype == 103)
                str = "55SZQGK";
            else if (paytype >= 200 && paytype <= 203)
                str = "55SZDFK";
            if (paytype == 104)
                str = "55SDYKT";
            else if (paytype == 105)
                str = "55ZTYKT";
            else if (paytype == 106)
                str = "55JWYKT";
            else if (paytype == 107)
                str = "55QQCZK";
            else if (paytype == 108)
                str = "55LTQGK";
            else if (paytype == 109)
                str = "55JYYKT";
            else if (paytype == 110)
                str = "55WYYKT";
            else if (paytype == 111)
                str = "55WMYKT";
            else if (paytype == 112)
                str = "55SHYKT";
            else if (paytype == 113)
                str = "55DXCZK";
            else if (paytype == 114)
                str = "55SXK";
            else if (paytype == 115)
                str = "55GYYKT";
            else if (paytype == 117)
                str = "55ZYYKT";
            else if (paytype == 118)
                str = "55TXTYK";
            else if (paytype == 119)
                str = "55THYKT";
            else if (paytype == 209)
                str = "55TXZXK";
            return str;
        }
    }
}
