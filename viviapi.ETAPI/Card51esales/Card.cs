using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI.Card51esales
{
    public class Card : ETAPIBase
    {
        private static int suppId = 51;

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/Card51esales_Card_Notify.aspx";
            }
        }

        public Card()
          : base(Card.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            errmsg = string.Empty;
            supporderid = string.Empty;
            string url = this.postCardUrl + "?";
            if (string.IsNullOrEmpty(this.postCardUrl))
                url = "http://it.51esales.net/gateway/zfgateway.asp?";
            string cardType = this.GetCardType(_typeId, cardvalue);
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = Cryptography.MD5("customerid=" + this.suppAccount + "&sdcustomno=" + _orderid + "&noticeurl=" + this.notify_url + "&mark=" + str2 + "&key=" + this.suppKey, "GB2312").ToUpper();
            string str4 = "-1";
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&customerid={0}", (object)this.suppAccount);
                stringBuilder.AppendFormat("&sdcustomno={0}", (object)_orderid);
                stringBuilder.AppendFormat("&ordermoney={0}", (object)cardvalue);
                stringBuilder.AppendFormat("&cardno={0}", (object)_cardno);
                stringBuilder.AppendFormat("&faceno={0}", (object)cardType);
                stringBuilder.AppendFormat("&cardnum={0}", (object)_cardno);
                stringBuilder.AppendFormat("&cardpass={0}", (object)_cardpwd);
                stringBuilder.AppendFormat("&noticeurl={0}", (object)this.notify_url);
                stringBuilder.AppendFormat("&remarks={0}", (object)str1);
                stringBuilder.AppendFormat("&mark={0}", (object)str2);
                stringBuilder.AppendFormat("&remoteip={0}", (object)ServerVariables.TrueIP);
                stringBuilder.AppendFormat("&sign={0}", (object)str3);
                string @string = WebClientHelper.GetString(url, stringBuilder.ToString(), "GET", Encoding.GetEncoding("GB2312"), 10000);
                errmsg = this.RetXmsValue("errmsg", @string);
                if (this.RetXmsValue("state", @string) == "1")
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
            string str1 = request["state"].Trim();
            string str2 = request["customerid"].Trim();
            string supplierOrderId = request["sd51no"].Trim();
            string orderId = request["sdcustomno"].Trim();
            string s = request["ordermoney"].Trim();
            string str3 = request["mark"].Trim();
            string str4 = request["sign"].Trim();
            string str5 = request["des"].Trim();
            string str6 = "customerid=" + str2 + "&sd51no=" + supplierOrderId + "&sdcustomno=" + orderId + "&mark=" + str3 + "&key=" + this.suppKey;
            string str7 = Cryptography.MD5(str6);
            try
            {
                if (str7 == str4)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (str1 == "1")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderCard().ReceiveSuppResult(Card.suppId, orderId, supplierOrderId, status, opstate, str5, Decimal.Parse(s), new Decimal(0), str5);
                    HttpContext.Current.Response.Write("<result>1</result>");
                }
                else
                    HttpContext.Current.Response.Write(str6);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public string Getfaceno(int paytype)
        {
            string str = paytype.ToString();
            if (paytype == 103)
                str = "szx";
            else if (paytype == 104)
                str = "sdk";
            else if (paytype == 105)
                str = "ztk";
            else if (paytype == 106)
                str = "jwk";
            else if (paytype == 107)
                str = "qqb";
            else if (paytype == 108)
                str = "ltk";
            else if (paytype == 109)
                str = "jyk";
            else if (paytype == 110)
                str = "wyk";
            else if (paytype == 111)
                str = "wmk";
            else if (paytype == 112)
                str = "shk";
            else if (paytype == 113)
                str = "dxk";
            return str;
        }

        public string GetCardType(int paytype, int money)
        {
            string str = paytype.ToString();
            if (paytype == 103)
            {
                switch (money)
                {
                    case 300:
                        str = "szx300";
                        break;
                    case 500:
                        str = "szx500";
                        break;
                    case 50:
                        str = "szx050";
                        break;
                    case 100:
                        str = "szx100";
                        break;
                    case 10:
                        str = "szx010";
                        break;
                    case 20:
                        str = "szx020";
                        break;
                    case 30:
                        str = "szx030";
                        break;
                }
            }
            else if (paytype == 104)
            {
                switch (money)
                {
                    case 300:
                        str = "sdk300";
                        break;
                    case 350:
                        str = "sdk350";
                        break;
                    case 1000:
                        str = "sdk000";
                        break;
                    case 45:
                        str = "sdk045";
                        break;
                    case 50:
                        str = "sdk050";
                        break;
                    case 100:
                        str = "sdk100";
                        break;
                    case 25:
                        str = "sdk025";
                        break;
                    case 30:
                        str = "sdk030";
                        break;
                    case 35:
                        str = "sdk035";
                        break;
                    case 5:
                        str = "sdk005";
                        break;
                    case 10:
                        str = "sdk010";
                        break;
                    case 15:
                        str = "sdk015";
                        break;
                }
            }
            else if (paytype == 105)
            {
                switch (money)
                {
                    case 100:
                        str = "ztk100";
                        break;
                    case 300:
                        str = "ztk300";
                        break;
                    case 468:
                        str = "ztk468";
                        break;
                    case 50:
                        str = "ztk050";
                        break;
                    case 60:
                        str = "ztk060";
                        break;
                    case 20:
                        str = "ztk020";
                        break;
                    case 25:
                        str = "ztk025";
                        break;
                    case 30:
                        str = "ztk030";
                        break;
                    case 10:
                        str = "ztk010";
                        break;
                    case 15:
                        str = "ztk015";
                        break;
                }
            }
            else if (paytype == 106)
            {
                switch (money)
                {
                    case 30:
                        str = "jwk030";
                        break;
                    case 50:
                        str = "jwk050";
                        break;
                    case 100:
                        str = "jwk100";
                        break;
                    case 5:
                        str = "jwk005";
                        break;
                    case 6:
                        str = "jwk006";
                        break;
                    case 10:
                        str = "jwk010";
                        break;
                    case 15:
                        str = "jwk015";
                        break;
                }
            }
            else if (paytype == 107)
            {
                switch (money)
                {
                    case 30:
                        str = "qqb030";
                        break;
                    case 60:
                        str = "qqb060";
                        break;
                    case 100:
                        str = "qqb100";
                        break;
                    case 5:
                        str = "qqb005";
                        break;
                    case 10:
                        str = "qqb010";
                        break;
                    case 15:
                        str = "qqb015";
                        break;
                }
            }
            else if (paytype == 108)
            {
                switch (money)
                {
                    case 100:
                        str = "ltk100";
                        break;
                    case 300:
                        str = "ltk300";
                        break;
                    case 500:
                        str = "ltk500";
                        break;
                    case 20:
                        str = "ltk020";
                        break;
                    case 30:
                        str = "ltk030";
                        break;
                    case 50:
                        str = "ltk050";
                        break;
                }
            }
            else if (paytype == 109)
            {
                switch (money)
                {
                    case 50:
                        str = "jyk050";
                        break;
                    case 100:
                        str = "jyk100";
                        break;
                    case 25:
                        str = "jyk025";
                        break;
                    case 30:
                        str = "jyk030";
                        break;
                    case 15:
                        str = "jyk020";
                        break;
                    case 20:
                        str = "jyk015";
                        break;
                    case 5:
                        str = "jyk005";
                        break;
                    case 10:
                        str = "jyk010";
                        break;
                }
            }
            else if (paytype == 110)
            {
                switch (money)
                {
                    case 20:
                        str = "wyk020";
                        break;
                    case 30:
                        str = "wyk030";
                        break;
                    case 50:
                        str = "wyk050";
                        break;
                    case 5:
                        str = "wyk005";
                        break;
                    case 10:
                        str = "wyk010";
                        break;
                    case 15:
                        str = "wyk015";
                        break;
                }
            }
            else if (paytype == 111)
            {
                switch (money)
                {
                    case 50:
                        str = "wmk050";
                        break;
                    case 100:
                        str = "wmk100";
                        break;
                    case 15:
                        str = "wmk015";
                        break;
                    case 30:
                        str = "wmk030";
                        break;
                }
            }
            else if (paytype == 112)
            {
                switch (money)
                {
                    case 30:
                        str = "shk030";
                        break;
                    case 40:
                        str = "shk040";
                        break;
                    case 100:
                        str = "shk100";
                        break;
                    case 5:
                        str = "shk005";
                        break;
                    case 10:
                        str = "shk010";
                        break;
                    case 15:
                        str = "shk015";
                        break;
                }
            }
            else if (paytype == 113)
            {
                switch (money)
                {
                    case 50:
                        str = "dxk050";
                        break;
                    case 100:
                        str = "dxk100";
                        break;
                }
            }
            else if (paytype == 114)
            {
                switch (money)
                {
                    case 5:
                        str = "sxk005";
                        break;
                    case 10:
                        str = "sxk010";
                        break;
                    case 15:
                        str = "sxk015";
                        break;
                }
            }
            else if (paytype == 117)
            {
                switch (money)
                {
                    case 30:
                        str = "zyk030";
                        break;
                    case 50:
                        str = "zyk050";
                        break;
                    case 100:
                        str = "zyk100";
                        break;
                    case 10:
                        str = "zyk010";
                        break;
                    case 15:
                        str = "zyk015";
                        break;
                }
            }
            return str;
        }

        public string RetXmsValue(string nodename, string value)
        {
            string str = string.Empty;
            try
            {
                value = value.Replace("<fill version=\"1.0\">", "");
                value = value.Replace("</fill>", "");
                Stream inStream = (Stream)new MemoryStream(Encoding.Default.GetBytes(value));
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(inStream);
                XmlNodeList childNodes = xmlDocument.SelectSingleNode("items").ChildNodes;
                if (childNodes.Count != 0)
                {
                    foreach (XmlNode xmlNode in childNodes)
                    {
                        if (xmlNode.Attributes["name"].InnerText == nodename)
                            str = xmlNode.Attributes["value"].InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str;
        }
    }
}
