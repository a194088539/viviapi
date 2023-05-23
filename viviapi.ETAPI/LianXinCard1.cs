using System;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Logging;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI
{
    public class LianXinCard1 : ETAPIBase
    {
        private static int suppId = 901;

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/LianXin_Card_Notify1.aspx";
            }
        }

        public LianXinCard1()
          : base(LianXinCard1.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            string suppAccount = this.suppAccount;
            string str1 = _orderid;
            string suppKey = this.suppKey;
            string url = this.postCardUrl + "?";
            if (string.IsNullOrEmpty(this.postCardUrl))
                url = "http://superapi.kltong.me:9180/busias/PayRequest?";
            string cardType = this.GetCardType(_typeId, cardvalue);
            string str2 = _cardno;
            string str3 = _cardpwd;
            string str4 = cardvalue.ToString();
            string str5 = string.Empty;
            string str6 = string.Empty;
            string str7 = string.Empty;
            string notifyUrl = this.notify_url;
            string str8 = string.Empty;
            string str9 = Cryptography.MD5(suppAccount + str1 + cardType + str2 + str3 + notifyUrl + str4 + suppKey);
            string str10 = "-1";
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&MerchantID={0}", (object)suppAccount);
                stringBuilder.AppendFormat("&MerOrderNo={0}", (object)str1);
                stringBuilder.AppendFormat("&CardType={0}", (object)cardType);
                stringBuilder.AppendFormat("&CardNo={0}", (object)str2);
                stringBuilder.AppendFormat("&CardPassword={0}", (object)str3);
                stringBuilder.AppendFormat("&Money={0}", (object)str4);
                stringBuilder.AppendFormat("&CustomizeA={0}", (object)str5);
                stringBuilder.AppendFormat("&CustomizeB={0}", (object)str6);
                stringBuilder.AppendFormat("&CustomizeC={0}", (object)str7);
                stringBuilder.AppendFormat("&NoticeURL={0}", (object)notifyUrl);
                stringBuilder.AppendFormat("&IsEncrypt={0}", (object)str8);
                stringBuilder.AppendFormat("&sign={0}", (object)str9);
                string @string = WebClientHelper.GetString(url, stringBuilder.ToString(), "POST", Encoding.GetEncoding("GB2312"), 10000);
                errmsg = @string;
                if (@string == "OK," + _orderid)
                    str10 = "0";
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str10;
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
            string str1 = request["PayOrderNo"].Trim();
            string str2 = request["MerchantID"].Trim();
            string orderId = request["MerOrderNo"].Trim();
            string supplierOrderId = request["CardNo"].Trim();
            string str3 = request["CardType"].Trim();
            string s = request["FactMoney"].Trim();
            string str4 = request["PayResult"].Trim();
            string str5 = request["CustomizeA"].Trim();
            string str6 = request["CustomizeB"].Trim();
            string str7 = request["CustomizeC"].Trim();
            string str8 = HttpUtility.UrlDecode(request["PayTime"].Trim());
            string str9 = HttpUtility.UrlDecode(request["ErrorMsg"].Trim());
            string str10 = request["sign"].Trim().ToLower();
            string str11 = str1 + str2 + orderId + supplierOrderId + str3 + s + str4 + str5 + str6 + str7 + str8 + this.suppKey;
            LogHelper.Write(str11);
            string str12 = Cryptography.MD5(str11).ToLower();
            LogHelper.Write(str12);
            LogHelper.Write(str10);
            try
            {
                if (!(str12 == str10))
                    return;
                string opstate = "-1";
                int status = 4;
                if (str4.ToLower() == "true")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderCard().ReceiveSuppResult(LianXinCard1.suppId, orderId, supplierOrderId, status, opstate, str9, Decimal.Parse(s), new Decimal(0), str9);
                HttpContext.Current.Response.Write("OK");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public string GetCardmoney(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 103:
                    return "10,20,30,50,100,300,500";
                case 104:
                    return "5,10,15,25,30,35,45,50,100,350";
                case 105:
                    return "10,20,30,50,60,100,300,500";
                case 106:
                    return "4,5,6,10,15,20,30,50,100,200";
                case 107:
                    return "5,10,15,30,60,100,200";
                case 108:
                    return "20,30,50,100,300,500";
                case 109:
                    return "5,10,15,20,25,30,50,100";
                case 110:
                    return "10,15,20,30,50";
                case 111:
                    return "15,30,50,100";
                case 112:
                    return "5,15,30,40,100";
                case 113:
                    return "10,30,50,100";
                default:
                    return str;
            }
        }

        public string GetCardType(int paytype, int money)
        {
            string str = paytype.ToString();
            if (paytype == 103)
            {
                switch (money)
                {
                    case 300:
                        str = "0105";
                        break;
                    case 500:
                        str = "0107";
                        break;
                    case 50:
                        str = "0101";
                        break;
                    case 100:
                        str = "0102";
                        break;
                    case 10:
                        str = "0103";
                        break;
                    case 20:
                        str = "0106";
                        break;
                    case 30:
                        str = "0104";
                        break;
                }
            }
            else if (paytype == 104)
            {
                switch (money)
                {
                    case 300:
                        str = "0211";
                        break;
                    case 350:
                        str = "0212";
                        break;
                    case 1000:
                        str = "0207";
                        break;
                    case 45:
                        str = "0204";
                        break;
                    case 50:
                        str = "0205";
                        break;
                    case 100:
                        str = "0206";
                        break;
                    case 25:
                        str = "0209";
                        break;
                    case 30:
                        str = "0203";
                        break;
                    case 35:
                        str = "0208";
                        break;
                    case 5:
                        str = "0201";
                        break;
                    case 10:
                        str = "0202";
                        break;
                    case 15:
                        str = "0210";
                        break;
                }
            }
            else if (paytype == 105)
            {
                switch (money)
                {
                    case 100:
                        str = "0606";
                        break;
                    case 300:
                        str = "0607";
                        break;
                    case 468:
                        str = "0610";
                        break;
                    case 50:
                        str = "0604";
                        break;
                    case 60:
                        str = "0605";
                        break;
                    case 20:
                        str = "0602";
                        break;
                    case 25:
                        str = "0609";
                        break;
                    case 30:
                        str = "0603";
                        break;
                    case 10:
                        str = "0601";
                        break;
                    case 15:
                        str = "0608";
                        break;
                }
            }
            else if (paytype == 106)
            {
                switch (money)
                {
                    case 30:
                        str = "0306";
                        break;
                    case 50:
                        str = "0304";
                        break;
                    case 100:
                        str = "0305";
                        break;
                    case 5:
                        str = "0301";
                        break;
                    case 6:
                        str = "0308";
                        break;
                    case 10:
                        str = "0302";
                        break;
                    case 15:
                        str = "0303";
                        break;
                }
            }
            else if (paytype == 107)
            {
                switch (money)
                {
                    case 30:
                        str = "0504";
                        break;
                    case 60:
                        str = "0505";
                        break;
                    case 100:
                        str = "0506";
                        break;
                    case 5:
                        str = "0501";
                        break;
                    case 10:
                        str = "0502";
                        break;
                    case 15:
                        str = "0503";
                        break;
                }
            }
            else if (paytype == 108)
            {
                switch (money)
                {
                    case 100:
                        str = "0404";
                        break;
                    case 300:
                        str = "0405";
                        break;
                    case 500:
                        str = "0406";
                        break;
                    case 20:
                        str = "0401";
                        break;
                    case 30:
                        str = "0402";
                        break;
                    case 50:
                        str = "0403";
                        break;
                }
            }
            else if (paytype == 109)
            {
                switch (money)
                {
                    case 50:
                        str = "0705";
                        break;
                    case 100:
                        str = "0708";
                        break;
                    case 25:
                        str = "0706";
                        break;
                    case 30:
                        str = "0704";
                        break;
                    case 15:
                        str = "0703";
                        break;
                    case 20:
                        str = "0707";
                        break;
                    case 5:
                        str = "0701";
                        break;
                    case 10:
                        str = "0702";
                        break;
                }
            }
            else if (paytype == 110)
            {
                switch (money)
                {
                    case 20:
                        str = "0905";
                        break;
                    case 30:
                        str = "0902";
                        break;
                    case 50:
                        str = "0906";
                        break;
                    case 5:
                        str = "0903";
                        break;
                    case 10:
                        str = "0904";
                        break;
                    case 15:
                        str = "0901";
                        break;
                }
            }
            else if (paytype == 111)
            {
                switch (money)
                {
                    case 50:
                        str = "0803";
                        break;
                    case 100:
                        str = "0804";
                        break;
                    case 15:
                        str = "0801";
                        break;
                    case 30:
                        str = "0802";
                        break;
                }
            }
            else if (paytype == 112)
            {
                switch (money)
                {
                    case 30:
                        str = "1103";
                        break;
                    case 40:
                        str = "1104";
                        break;
                    case 100:
                        str = "1106";
                        break;
                    case 5:
                        str = "1101";
                        break;
                    case 10:
                        str = "1105";
                        break;
                    case 15:
                        str = "1102";
                        break;
                }
            }
            else if (paytype == 113)
            {
                switch (money)
                {
                    case 50:
                        str = "1201";
                        break;
                    case 100:
                        str = "1202";
                        break;
                }
            }
            return str;
        }
    }
}
