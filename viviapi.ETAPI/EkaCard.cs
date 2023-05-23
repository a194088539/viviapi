using System;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI
{
    public class EkaCard : ETAPIBase
    {
        private static int suppId = 365;

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/Eka_Card_Notify.aspx";
            }
        }

        public EkaCard()
          : base(EkaCard.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            string suppKey = this.suppKey;
            string url = this.postCardUrl + "?";
            if (string.IsNullOrEmpty(this.postCardUrl))
                url = "http://gateway.wxk8.com/cardReceive.aspx?";
            string str1 = this.GetCardType(_typeId).ToString("0000") + cardvalue.ToString();
            string str2 = "0";
            string strToEncrypt = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}", (object)str1, (object)this.suppAccount, (object)_cardno, (object)_cardpwd, (object)cardvalue, (object)str2, (object)_orderid, (object)this.notify_url);
            string str3 = Cryptography.MD5(strToEncrypt);
            string str4 = "-1";
            try
            {
                StringBuilder stringBuilder = new StringBuilder(strToEncrypt);
                stringBuilder.AppendFormat("&sign={0}", (object)str3);
                string @string = WebClientHelper.GetString(url, stringBuilder.ToString(), "GET", Encoding.GetEncoding("GB2312"), 10000);
                errmsg = @string;
                if (@string.ToUpper() == "opstate=0")
                    str4 = "0";
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str4;
        }

        public string GetMsgInfo(string cardstatus)
        {
            switch (cardstatus)
            {
                case "ERROR001":
                    return "商户编号不能为空";
                case "ERROR002":
                    return "无效的用户名或用户名没有被启用";
                case "ERROR003":
                    return "商户验证KEY不能为空";
                case "ERROR004":
                    return "MD5验证失败";
                case "ERROR005":
                    return "商户订单号不能为空";
                case "ERROR006":
                    return "充值卡类型不能为空";
                case "ERROR007":
                    return "充值卡号不能为空";
                case "1009":
                    return "其他游戏专用卡";
                default:
                    return cardstatus;
            }
        }

        public void CardNotify()
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
                string str5 = "-1";
                int status = 4;
                if (str1.ToLower() == "0")
                {
                    str5 = "0";
                    status = 2;
                }
                new OrderCard().ReceiveSuppResult(EkaCard.suppId, orderId, supplierOrderId, status, str5, string.Empty, Decimal.Parse(s), new Decimal(0), str5);
                HttpContext.Current.Response.Write("opstate=0");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public int GetCardType(int paytype)
        {
            int num = paytype;
            if (paytype == 103)
                num = 13;
            else if (paytype == 104)
                num = 2;
            else if (paytype == 105)
                num = 7;
            else if (paytype == 106)
                num = 3;
            else if (paytype == 107)
                num = 1;
            else if (paytype == 108)
                num = 14;
            else if (paytype == 109)
                num = 8;
            else if (paytype == 110)
                num = 9;
            else if (paytype == 111)
                num = 5;
            else if (paytype == 112)
                num = 6;
            else if (paytype == 113)
                num = 12;
            return num;
        }
    }
}
