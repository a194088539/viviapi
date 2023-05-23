using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI.Longbao
{
    public class Card : ETAPIBase
    {
        private static int suppId = 700;

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/longbao/Card_Notify.aspx";
            }
        }

        public Card()
          : base(Card.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string supperrorcode, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            supperrorcode = string.Empty;
            string str1 = "-1";
            if (string.IsNullOrEmpty(this.postCardUrl) || string.IsNullOrEmpty(this.suppKey))
                return str1;
            string url = this.postCardUrl + "?";
            string str2 = this.GetCardType(_typeId).ToString();
            string str3 = "0";
            string str4 = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}", (object)str2, (object)this.suppAccount, (object)_cardno, (object)_cardpwd, (object)cardvalue, (object)str3, (object)_orderid, (object)this.notify_url);
            string str5 = Cryptography.MD5(str4 + this.suppKey);
            try
            {
                StringBuilder stringBuilder = new StringBuilder(str4);
                stringBuilder.AppendFormat("&sign={0}", (object)str5);
                string @string = WebClientHelper.GetString(url, stringBuilder.ToString(), "GET", Encoding.GetEncoding("GB2312"), 10000);
                supperrorcode = @string.Replace("opstate=", "");
                errmsg = this.GetLBMsgInfo(@string, string.Empty);
                if (@string == "opstate=1")
                    str1 = "0";
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str1;
        }

        public string GetLBMsgInfo(string retcode, string _ovalue)
        {
            switch (retcode)
            {
                case "opstate=0":
                    return "支付成功";
                case "opstate=1":
                    return "数据接收成功";
                case "opstate=2":
                    return "不支持该类卡或者该面值的卡";
                case "opstate=3":
                    return "签名验证失败";
                case "opstate=4":
                    return "订单内容重复";
                case "opstate=5":
                    return "该卡密已经有被使用的记录";
                case "opstate=6":
                    return "订单号已经存在";
                case "opstate=7":
                    return "数据非法";
                case "opstate=8":
                    return "非法用户";
                case "opstate=9":
                    return "暂时停止该类卡或者该面值的卡交易";
                case "opstate=10":
                    return "充值卡无效";
                case "opstate=11":
                    return string.Format("支付成功,实际面值{0}元", (object)_ovalue);
                case "opstate=12":
                    return "处理失败卡密未使用";
                case "opstate=13":
                    return "系统繁忙";
                case "opstate=14":
                    return "不存在该笔订单";
                case "opstate=15":
                    return "未知请求";
                case "opstate=16":
                    return "密码错误";
                case "opstate=17":
                    return "匹配订单失败";
                case "opstate=18":
                    return "余额不足";
                case "opstate=19":
                    return "运营商维护";
                case "opstate=20":
                    return "提交次数过多";
                case "opstate=99":
                    return "其他错误";
                default:
                    return retcode;
            }
        }

        public void CardNotify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string str1 = request.QueryString["opstate"];
            string str2 = request.QueryString["ovalue"];
            string str3 = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["sysorderid"];
            string str4 = request.QueryString["systime"];
            string str5 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)orderId, (object)str1, (object)str2, (object)this.suppKey));
            try
            {
                if (!(str5 == str3))
                    return;
                string str6 = "-1";
                int status = 4;
                if (str1.ToLower() == "0")
                {
                    str6 = "0";
                    status = 2;
                }
                OrderCard orderCard = new OrderCard();
                string lbMsgInfo = this.GetLBMsgInfo("opstate=" + str6, str2);
                string userviewmsg = lbMsgInfo;
                orderCard.ReceiveSuppResult(Card.suppId, orderId, supplierOrderId, status, str6, lbMsgInfo, userviewmsg, Decimal.Parse(str2), new Decimal(0), str6, (byte)1);
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
            else if (paytype == 200)
                num = 17;
            else if (paytype == 201)
                num = 18;
            else if (paytype == 202)
                num = 19;
            else if (paytype == 203)
                num = 20;
            else if (paytype == 204)
                num = 10;
            else if (paytype == 205)
                num = 11;
            else if (paytype == 210)
                num = 28;
            return num;
        }

        public string Query(string orderid)
        {
            string str1 = string.Empty;
            string queryCardUrl = this._suppInfo.queryCardUrl;
            if (string.IsNullOrEmpty(queryCardUrl))
                return string.Empty;
            orderid = orderid.Trim();
            string str2 = Cryptography.MD5(string.Format("orderid={0}&parter={1}{2}", (object)orderid, (object)this.suppAccount, (object)this.suppKey));
            string url = string.Format("{0}?orderid={1}&parter={2}&sign={3}", (object)queryCardUrl, (object)orderid, (object)this.suppAccount, (object)str2);
            string str3;
            try
            {
                str3 = WebClientHelper.GetString(url, (NameValueCollection)null, "GET", Encoding.Default);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                str3 = ex.Message;
            }
            return str3;
        }

        public bool Finish(string retText)
        {
            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(retText))
                {
                    string[] strArray = retText.Split('&');
                    string orderId = strArray[0].Replace("orderid=", "");
                    string str1 = strArray[1].Replace("opstate=", "");
                    string s = strArray[2].Replace("ovalue=", "");
                    string str2 = strArray[3].Replace("sign=", "");
                    if (Cryptography.MD5(string.Format("orderid={0}&opstate={0}&ovalue={0}", (object)orderId, (object)str1, (object)s) + this.suppKey) == str2)
                    {
                        string str3 = string.Empty;
                        int status = 4;
                        if (str1 != "1")
                        {
                            string opstate;
                            if (str1 == "2")
                            {
                                opstate = "0";
                            }
                            else
                            {
                                status = 4;
                                opstate = str1;
                            }
                            if (!string.IsNullOrEmpty(opstate))
                            {
                                new OrderCard().ReceiveSuppResult(Card.suppId, orderId, string.Empty, status, opstate, str1, Decimal.Parse(s), new Decimal(0), str1);
                                flag = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return flag;
        }
    }
}
