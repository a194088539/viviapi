namespace viviapi.ETAPI.KuaiQian
{
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using viviapi.BLL;
    using viviapi.ETAPI;
    using viviapi.Model.Order;
    using viviapi.SysConfig;
    using viviLib.Web;

    public class Card : ETAPIBase
    {
        public string bgUrl;
        public string cardNumber;
        public string cardPwd;
        public string cardType;
        public string ext1;
        public string ext2;
        public string merchantAcctId;
        public string orderAmount;
        public string orderId;
        public string pageUrl;
        public string payType;
        public string signMsg;
        private static int suppId = 990;

        public Card() : base(suppId)
        {
            this.merchantAcctId = string.Empty;
            this.orderId = string.Empty;
            this.orderAmount = string.Empty;
            this.payType = string.Empty;
            this.pageUrl = RuntimeSetting.SiteDomain + "/PayReturn/KuaiQian_Card_Return.aspx";
            this.bgUrl = RuntimeSetting.SiteDomain + "/PayReturn/KuaiQian_Card_Return.aspx";
            this.cardType = string.Empty;
            this.cardNumber = string.Empty;
            this.cardPwd = string.Empty;
            this.ext1 = string.Empty;
            this.ext2 = "00";
            this.signMsg = string.Empty;
        }

        private string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    string str2 = returnStr;
                    returnStr = str2 + "&" + paramId + "=" + paramValue;
                }
                return returnStr;
            }
            if (paramValue != "")
            {
                returnStr = paramId + "=" + paramValue;
            }
            return returnStr;
        }

        private string appendParam1(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    string str2 = returnStr;
                    returnStr = str2 + "&" + paramId + "=" + this.UrlEncode(paramValue);
                }
                return returnStr;
            }
            if (paramValue != "")
            {
                returnStr = paramId + "=" + this.UrlEncode(paramValue);
            }
            return returnStr;
        }

        public string GetCardmoney(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 0x67:
                    return "10,20,30,50,100,300,500";

                case 0x68:
                    return "5,10,15,25,30,35,45,50,100,300,350,1000";

                case 0x69:
                    return "10,15,25,20,30,50,60,100,300,468";

                case 0x6a:
                    return "4,5,6,10,15,20,30,50,100,200";

                case 0x6b:
                    return "5,10,15,30,60,100,200";

                case 0x6c:
                    return "20,30,50,100,300,500";

                case 0x6d:
                    return "5,10,15,20,25,30,50,100";

                case 110:
                    return "5,10,15,20,30,50";

                case 0x6f:
                    return "15,30,50,100";

                case 0x70:
                    return "5,10,15,30,40,100";

                case 0x71:
                    return "10,30,50,100";
            }
            return str;
        }

        public string GetCardType(int paytype, int money)
        {
            string str = string.Empty;
            int num = paytype;
            switch (num)
            {
                case 0x68:
                    num = money;
                    if (num > 0x23)
                    {
                        if (num <= 100)
                        {
                            switch (num)
                            {
                                case 0x2d:
                                    return "8";

                                case 50:
                                    return "47";

                                case 100:
                                    return "9";
                            }
                            return str;
                        }
                        switch (num)
                        {
                            case 300:
                                return "112";

                            case 350:
                                return "113";

                            case 0x3e8:
                                return "48";
                        }
                        return str;
                    }
                    if (num > 15)
                    {
                        switch (num)
                        {
                            case 0x19:
                                return "110";

                            case 30:
                                return "7";

                            case 0x23:
                                return "111";
                        }
                        return str;
                    }
                    switch (num)
                    {
                        case 5:
                            return "38";

                        case 10:
                            return "6";

                        case 15:
                            return "93";
                    }
                    return str;

                case 0x69:
                    num = money;
                    if (num > 30)
                    {
                        if (num <= 60)
                        {
                            switch (num)
                            {
                                case 50:
                                    return "42";

                                case 60:
                                    return "43";
                            }
                            return str;
                        }
                        switch (num)
                        {
                            case 100:
                                return "44";

                            case 300:
                                return "45";

                            case 0x1d4:
                                return "70";
                        }
                        return str;
                    }
                    if (num > 15)
                    {
                        switch (num)
                        {
                            case 20:
                                return "40";

                            case 0x19:
                                return "122";

                            case 30:
                                return "41";
                        }
                        return str;
                    }
                    switch (num)
                    {
                        case 10:
                            return "39";

                        case 15:
                            return "121";
                    }
                    return str;

                case 0x6a:
                case 0x6b:
                case 0x6c:
                case 0x6d:
                    return str;

                case 110:
                    num = money;
                    if (num > 15)
                    {
                        switch (num)
                        {
                            case 20:
                                return "159";

                            case 30:
                                return "54";

                            case 50:
                                return "160";
                        }
                        return str;
                    }
                    switch (num)
                    {
                        case 5:
                            return "158";

                        case 10:
                            return "146";

                        case 15:
                            return "53";
                    }
                    return str;

                case 0x6f:
                    num = money;
                    if (num > 30)
                    {
                        switch (num)
                        {
                            case 50:
                                return "117";

                            case 100:
                                return "118";
                        }
                        return str;
                    }
                    switch (num)
                    {
                        case 15:
                            return "115";

                        case 30:
                            return "116";
                    }
                    return str;

                case 0x70:
                    num = money;
                    if (num > 15)
                    {
                        switch (num)
                        {
                            case 30:
                                return "58";

                            case 40:
                                return "63";

                            case 100:
                                return "77";
                        }
                        return str;
                    }
                    switch (num)
                    {
                        case 5:
                            return "55";

                        case 10:
                            return "56";

                        case 15:
                            return "57";
                    }
                    return str;
            }
            return str;
        }

        private string GetMD5(string dataStr, string codeType)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(codeType).GetBytes(dataStr));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public string GetPayUrl(OrderCardInfo order)
        {
            this.merchantAcctId = base._suppInfo.puserid5;
            string paramValue = base._suppInfo.puserkey5;
            this.orderId = order.orderid;
            string cardType = this.GetCardType(order.typeId, int.Parse(order.refervalue.ToString("0")));
            this.cardType = cardType;
            this.cardNumber = order.cardNo;
            this.cardPwd = order.cardPwd;
            this.orderAmount = order.refervalue.ToString("0");
            switch (order.typeId)
            {
                case 0x68:
                    this.payType = "C";
                    break;

                case 0x69:
                    this.payType = "D";
                    break;

                case 110:
                    this.payType = "M";
                    break;

                case 0x6f:
                    this.payType = "U";
                    break;

                case 0x70:
                    this.payType = "N";
                    break;

                default:
                    this.payType = "E";
                    break;
            }
            this.ext1 = "00";
            this.signMsg = "";
            string returnStr = "";
            string str4 = string.Empty;
            returnStr = this.appendParam(returnStr, "merchantAcctId", this.merchantAcctId);
            returnStr = this.appendParam(returnStr, "orderId", this.orderId);
            returnStr = this.appendParam(returnStr, "orderAmount", this.orderAmount);
            returnStr = this.appendParam(returnStr, "payType", this.payType);
            returnStr = this.appendParam(returnStr, "pageUrl", this.pageUrl);
            returnStr = this.appendParam(returnStr, "bgUrl", this.bgUrl);
            returnStr = this.appendParam(returnStr, "cardType", this.cardType);
            returnStr = this.appendParam(returnStr, "cardNumber", this.cardNumber);
            returnStr = this.appendParam(returnStr, "cardPwd", this.cardPwd);
            returnStr = this.appendParam(returnStr, "ext1", this.ext1);
            returnStr = this.appendParam(returnStr, "ext2", this.ext2);
            returnStr = this.appendParam(returnStr, "key", paramValue);
            this.signMsg = this.GetMD5(returnStr, "GB2312").ToUpper();
            str4 = this.appendParam1(str4, "merchantAcctId", this.merchantAcctId);
            str4 = this.appendParam1(str4, "orderId", this.orderId);
            str4 = this.appendParam1(str4, "orderAmount", this.orderAmount);
            str4 = this.appendParam1(str4, "payType", this.payType);
            str4 = this.appendParam1(str4, "pageUrl", this.pageUrl);
            str4 = this.appendParam1(str4, "bgUrl", this.bgUrl);
            str4 = this.appendParam1(str4, "cardType", this.cardType);
            str4 = this.appendParam1(str4, "cardNumber", this.cardNumber);
            str4 = this.appendParam1(str4, "cardPwd", this.cardPwd);
            str4 = this.appendParam1(str4, "ext1", this.ext1);
            str4 = this.appendParam1(str4, "ext2", this.ext2);
            StringBuilder builder = new StringBuilder();
            builder.Append("http://222.73.15.116/recvMerchantInfoAction.aspx?");
            builder.Append(str4);
            builder.Append("&signMsg=" + this.UrlEncode(this.signMsg));
            return WebClientHelper.GetString(builder.ToString(), null, "GET", Encoding.GetEncoding("gb2312"));
        }

        private string GetValue(string pName)
        {
            string str = this.Request[pName];
            if (string.IsNullOrEmpty(str))
            {
                str = this.Request.QueryString[pName];
            }
            return str;
        }

        public void Return()
        {
            string paramValue = base._suppInfo.puserkey5;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = string.Empty;
            string str7 = string.Empty;
            string str8 = string.Empty;
            string str9 = string.Empty;
            string str10 = string.Empty;
            string str11 = string.Empty;
            try
            {
                str2 = this.GetValue("payResult");
                str3 = this.GetValue("dealId");
                str4 = this.GetValue("merchantAcctId");
                str5 = this.GetValue("orderId");
                str6 = this.GetValue("payAmount");
                str7 = this.GetValue("cardNumber");
                str8 = this.GetValue("ext1");
                str9 = this.GetValue("ext2");
                str10 = this.GetValue("errInfo");
                str11 = this.GetValue("signMsg");
            }
            catch
            {
            }
            string returnStr = "";
            returnStr = this.appendParam(returnStr, "payResult", str2);
            returnStr = this.appendParam(returnStr, "dealId", str3);
            returnStr = this.appendParam(returnStr, "merchantAcctId", str4);
            returnStr = this.appendParam(returnStr, "orderId", str5);
            returnStr = this.appendParam(returnStr, "payAmount", str6);
            returnStr = this.appendParam(returnStr, "cardNumber", str7);
            returnStr = this.appendParam(returnStr, "ext1", str8);
            returnStr = this.appendParam(returnStr, "ext2", str9);
            returnStr = this.appendParam(returnStr, "key", paramValue);
            string str13 = this.GetMD5(returnStr, "gb2312");
            if (str11.ToUpper() == str13.ToUpper())
            {
                string opstate = "-1";
                if (str2 == "0")
                {
                    opstate = "0";
                }
                int status = (str2 == "0") ? 2 : 4;
                OrderCard card = new OrderCard();
                string msg = str10;
                string userviewmsg = msg;
                card.DoCardComplete(suppId, str5, str3, status, opstate, msg, userviewmsg, decimal.Parse(str2), 0M, str2, 1);
                HttpContext.Current.Response.Write("OK");
            }
            else
            {
                HttpContext.Current.Response.Write("交易签名无效!");
            }
        }

        public string UrlEncode(string value)
        {
            value = HttpUtility.UrlEncode(value, Encoding.GetEncoding("GB2312")).Replace("+", "%20");
            value = new Regex("(%[0-9a-f][0-9a-f])").Replace(value, m => m.Value.ToUpper());
            return value;
        }

        private HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
    }
}

