namespace viviapi.ETAPI
{
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Security;
    using viviapi.BLL;
    using viviLib.ExceptionHandling;
    using viviLib.Logging;
    using viviLib.Security;
    using viviLib.Web;

    public class Card60866 : ETAPIBase
    {
        private static int suppId = 0xedc2;

        public Card60866()
            : base(suppId)
        {
        }

        public static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        public void CardNotify()
        {
            string str = HttpContext.Current.Request.QueryString["returncode"].ToString().Trim().ToLower();
            string str2 = HttpContext.Current.Request.QueryString["userid"].ToString().Trim().ToLower();
            string orderId = HttpContext.Current.Request.QueryString["orderid"].ToString().Trim().ToLower();
            string str4 = HttpContext.Current.Request.QueryString["typeid"].ToString().Trim().ToLower();
            string str5 = HttpContext.Current.Request.QueryString["productid"].ToString().Trim().ToLower();
            string supplierOrderId = HttpContext.Current.Request.QueryString["cardno"].ToString().Trim().ToLower();
            string str7 = HttpContext.Current.Request.QueryString["cardpwd"].ToString().Trim().ToLower();
            string str8 = HttpContext.Current.Request.QueryString["money"].ToString().Trim().ToLower();
            string s = HttpContext.Current.Request.QueryString["realmoney"].ToString().Trim().ToLower();
            string str10 = HttpContext.Current.Request.QueryString["cardstatus"].ToString().Trim().ToLower();
            string str11 = HttpContext.Current.Request.QueryString["sign"].ToString().Trim().ToLower();
            string str12 = HttpContext.Current.Request.QueryString["ext"].ToString().Trim().ToLower();
            string errtype = HttpContext.Current.Request.QueryString["errtype"].ToString().Trim();
            string suppKey = base.suppKey;
            try
            {
                string data = string.Format("returncode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}", new object[] { str, str2, orderId, str4, str5, supplierOrderId, str7, str8, s, str10, suppKey });
                string str16 = MD5(data);
                if (str16 == str11)
                {
                    string suppcode = "-1";
                    if (str == "1")
                    {
                        suppcode = "0";
                    }
                    else
                    {
                        suppcode = this.ConvertCode(suppcode);
                    }
                    int status = (str == "1") ? 2 : 4;
                    new OrderCard().ReceiveSuppResult(suppId, orderId, supplierOrderId, status, suppcode, this.GetMsgInfo(errtype), decimal.Parse(s), 0M, errtype);
                }
                else
                {
                    LogHelper.Write(string.Format("60866card 原字串{0} 本地sign{1},接口商sign:{2}", data, str16, str11));
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            HttpContext.Current.Response.Write("ok");
        }

        public void CardNotifyV10()
        {
            try
            {
                string suppAccount = base.suppAccount;
                string suppKey = base.suppKey;
                string str3 = HttpContext.Current.Request.QueryString["returncode"].ToString().Trim().ToLower();
                string orderId = HttpContext.Current.Request.QueryString["orderid"].ToString().Trim().ToLower();
                string str5 = HttpContext.Current.Request.QueryString["typeid"].ToString().Trim().ToLower();
                string str6 = HttpContext.Current.Request.QueryString["productid"].ToString().Trim().ToLower();
                string supplierOrderId = HttpContext.Current.Request.QueryString["cardno"].ToString().Trim().ToLower();
                string str8 = HttpContext.Current.Request.QueryString["cardpwd"].ToString().Trim().ToLower();
                string str9 = HttpContext.Current.Request.QueryString["money"].ToString().Trim().ToLower();
                string s = HttpContext.Current.Request.QueryString["realmoney"].ToString().Trim().ToLower();
                string cardstatus = HttpContext.Current.Request.QueryString["cardstatus"].ToString().Trim().ToLower();
                string str12 = HttpContext.Current.Request.QueryString["sign"].ToString().Trim().ToLower();
                string errtype = HttpContext.Current.Request.QueryString["errtype"].ToString().Trim();
                string str14 = HttpContext.Current.Request.QueryString["ext"].ToString().Trim().ToLower();
                if (MD5(string.Format("returnCode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}", new object[] { str3, suppAccount, orderId, str5, str6, supplierOrderId, str8, str9, s, cardstatus, suppKey }).ToLower()) == str12)
                {
                    string opstate = "-1";
                    if (str3 == "1")
                    {
                        opstate = "0";
                    }
                    int status = (str3 == "1") ? 2 : 4;
                    new OrderCard().ReceiveSuppResult(suppId, orderId, supplierOrderId, status, opstate, this.GetMsgInfo(cardstatus), decimal.Parse(s), 0M, errtype);
                    HttpContext.Current.Response.Write("OK");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string supperrorcode, out string errmsg)
        {
            errmsg = string.Empty;
            supperrorcode = string.Empty;
            supporderid = string.Empty;
            string suppAccount = base.suppAccount;
            string suppKey = base.suppKey;
            string str3 = base.postCardUrl + "?";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            string str4 = _cardno;
            string str5 = _cardpwd;
            string str6 = _orderid;
            string paycardno = this.GetPaycardno(_typeId);
            string cardType = this.GetCardType(_typeId, cardvalue);
            string str9 = cardvalue.ToString();
            string str10 = MD5(string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&keyvalue={8}", new object[] { suppAccount, str6, paycardno, cardType, str4, str5, str9, this.notify_url, suppKey }).ToLower());
            string str11 = string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&sign={8}&ext={9}", new object[] { suppAccount, str6, paycardno, cardType, str4, str5, str9, this.notify_url, str10, string.Empty });
            string str12 = string.Empty;
            string str13 = string.Empty;
            string str14 = string.Empty;
            try
            {
                string str15 = "-1";
                string[] strings = GetStrings(SendRequest(str3.ToLower(), str11.ToLower()), "&");
                str12 = strings[0].Replace("returncode=", string.Empty);
                str13 = strings[1].Replace("returnorderid=", string.Empty);
                str14 = strings[2].Replace("sign=", string.Empty);
                if (MD5(string.Format("returncode={0}&returnorderid={1}&keyvalue={2}", str12, str13, suppKey).ToLower()) == str14)
                {
                    supperrorcode = str12;
                    errmsg = this.GetMsgInfo(str12);
                    supporderid = str13;
                    if (!(string.IsNullOrEmpty(str12) || !(str12 == "1")))
                    {
                        str15 = "0";
                    }
                }
                else
                {
                    errmsg = "签名失败";
                }
                return str15;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return string.Empty;
            }
        }

        public string ConvertCode(string suppcode)
        {
            string str = string.Empty;
            if (suppcode == "1001")
            {
                str = "-1";
            }
            else if (suppcode == "1002")
            {
                str = "-2";
            }
            else if (suppcode == "1003")
            {
                str = "-10";
            }
            else if (suppcode == "1004")
            {
                str = "-4";
            }
            else if (suppcode == "1005")
            {
                str = "-5";
            }
            else if (suppcode == "1006")
            {
                str = "-6";
            }
            else if (suppcode == "1007")
            {
                str = "-7";
            }
            else if (suppcode == "1008")
            {
                str = "-8";
            }
            else if (suppcode == "1009")
            {
                str = "-9";
            }
            if (string.IsNullOrEmpty(suppcode))
            {
                str = "-1";
            }
            return str;
        }

        public bool Finish(string orderid, string callback)
        {
            bool flag = false;
            try
            {
                if (string.IsNullOrEmpty(callback))
                {
                    return flag;
                }
                string str = "-1";
                int status = 4;
                string val = this.GetVal(callback, "returncode");
                string s = this.GetVal(callback, "realmoney");
                string msg = this.GetVal(callback, "errtype");
                if (msg == "成功")
                {
                    msg = "支付成功";
                }
                if (val == "1")
                {
                    str = "0";
                    status = 2;
                }
                else if ((val == "-1") || (val == "2"))
                {
                    str = string.Empty;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    new OrderCard().ReceiveSuppResult(suppId, orderid, string.Empty, status, str, msg, decimal.Parse(s), 0M, val);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return flag;
        }

        public static string Get_Http(string a_strUrl, int timeout)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(a_strUrl);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                StringBuilder builder = new StringBuilder();
                while (-1 != reader.Peek())
                {
                    builder.Append(reader.ReadLine());
                }
                return builder.ToString();
            }
            catch (Exception exception)
            {
                return ("错误：" + exception.Message);
            }
        }

        public string GetCardmoney(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 0x67:
                    return "10,20,30,50,100,300,500";

                case 0x68:
                    return "5,10,15,25,30,35,45,50,100,350";

                case 0x69:
                    return "10,20,30,50,60,100,300,500";

                case 0x6a:
                    return "4,5,6,10,15,20,30,50,100,200";

                case 0x6b:
                    return "5,10,15,30,60,100,200";

                case 0x6c:
                    return "20,30,50,100,300,500";

                case 0x6d:
                    return "5,10,15,20,25,30,50,100";

                case 110:
                    return "10,15,20,30,50";

                case 0x6f:
                    return "15,30,50,100";

                case 0x70:
                    return "5,15,30,40,100";

                case 0x71:
                    return "10,30,50,100";
            }
            return str;
        }

        public string GetCardType(int paytype, int money)
        {
            string str = "0";
            int num = paytype;
            int num2 = num;
            switch (num2)
            {
                case 0x67:
                    num = money;
                    if (num <= 30)
                    {
                        switch (num)
                        {
                            case 10:
                                return "cm10";

                            case 20:
                                return "cm20";

                            case 30:
                                return "cm30";
                        }
                    }
                    else
                    {
                        num2 = num;
                        if (num2 > 100)
                        {
                            switch (num2)
                            {
                                case 300:
                                    return "cm300";

                                case 500:
                                    return "cm500";
                            }
                        }
                        else
                        {
                            switch (num2)
                            {
                                case 50:
                                    return "cm50";

                                case 100:
                                    return "cm100";
                            }
                        }
                    }
                    goto Label_0892;

                case 0x68:
                case 210:
                    num = money;
                    if (num <= 30)
                    {
                        if (num > 10)
                        {
                            switch (num)
                            {
                                case 0x19:
                                    return "sd25";

                                case 30:
                                    return "sd30";
                            }
                        }
                        else
                        {
                            switch (num)
                            {
                                case 5:
                                    return "sd5";

                                case 10:
                                    return "sd10";
                            }
                        }
                        break;
                    }
                    switch (num)
                    {
                        case 50:
                            return "sd50";

                        case 100:
                            return "sd100";

                        case 0x23:
                            return "sd35";

                        case 0x2d:
                            return "sd45";
                    }
                    break;

                case 0x69:
                    num = money;
                    if (num <= 30)
                    {
                        switch (num)
                        {
                            case 10:
                                return "zt10";

                            case 20:
                                return "zt20";

                            case 30:
                                return "zt30";
                        }
                    }
                    else
                    {
                        num2 = num;
                        if (num2 > 60)
                        {
                            switch (num2)
                            {
                                case 100:
                                    return "zt100";

                                case 300:
                                    return "zt300";
                            }
                        }
                        else
                        {
                            switch (num2)
                            {
                                case 50:
                                    return "zt50";

                                case 60:
                                    return "zt60";
                            }
                        }
                    }
                    return "0";

                case 0x6a:
                    switch (money)
                    {
                        case 30:
                            return "jw30";

                        case 50:
                            return "jw50";

                        case 100:
                            return "jw100";

                        case 4:
                            return "jw4";

                        case 5:
                            return "jw5";

                        case 6:
                            return "jw6";

                        case 7:
                        case 8:
                        case 9:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                            goto Label_089A;

                        case 10:
                            return "jw10";

                        case 15:
                            return "jw15";
                    }
                    goto Label_089A;

                case 0x6b:
                    switch (money)
                    {
                        case 30:
                            return "qq30";

                        case 60:
                            return "qq60";

                        case 100:
                            return "qq100";

                        case 5:
                            return "qq5";

                        case 10:
                            return "qq10";

                        case 15:
                            return "qq15";
                    }
                    return "0";

                case 0x6c:
                    switch (money)
                    {
                        case 100:
                            return "cc100";

                        case 300:
                            return "cc300";

                        case 500:
                            return "cc500";

                        case 20:
                            return "cc20";

                        case 30:
                            return "cc30";

                        case 50:
                            return "cc50";
                    }
                    return "0";

                case 0x6d:
                    switch (money)
                    {
                        case 30:
                            return "jy30";

                        case 50:
                            return "jy50";

                        case 5:
                            return "jy5";

                        case 10:
                            return "jy10";
                    }
                    return "0";

                case 110:
                    switch (money)
                    {
                        case 10:
                            return "wy10";

                        case 15:
                            return "wy15";

                        case 30:
                            return "wy30";
                    }
                    return "0";

                case 0x6f:
                    switch (money)
                    {
                        case 50:
                            return "wm50";

                        case 100:
                            return "wm100";

                        case 15:
                            return "wm15";

                        case 30:
                            return "wm30";
                    }
                    return "0";

                case 0x70:
                    switch (money)
                    {
                        case 30:
                            return "sh30";

                        case 40:
                            return "sh40";

                        case 100:
                            return "sh100";

                        case 5:
                            return "sh5";

                        case 10:
                            return "sh10";

                        case 15:
                            return "sh15";
                    }
                    return "0";

                case 0x71:
                    switch (money)
                    {
                        case 50:
                            return "dx50";

                        case 100:
                            return "dx100";
                    }
                    return "0";

                case 0x72:
                case 0x74:
                    return str;

                case 0x73:
                    num2 = money;
                    if (num2 > 20)
                    {
                        switch (num2)
                        {
                            case 30:
                                return "gy30";

                            case 50:
                                return "gy50";

                            case 100:
                                return "gy100";
                        }
                    }
                    else
                    {
                        switch (num2)
                        {
                            case 10:
                                return "gy10";

                            case 20:
                                return "gy15";
                        }
                    }
                    goto Label_0892;

                case 0x75:
                    num2 = money;
                    if (num2 > 15)
                    {
                        switch (num2)
                        {
                            case 30:
                                return "zy30";

                            case 50:
                                return "zy50";

                            case 100:
                                return "zy100";
                        }
                    }
                    else
                    {
                        switch (num2)
                        {
                            case 10:
                                return "zy10";

                            case 15:
                                return "zy15";
                        }
                    }
                    goto Label_0892;

                case 0x76:
                    num2 = money;
                    if (num2 > 50)
                    {
                        switch (num2)
                        {
                            case 60:
                                return "tx50";

                            case 70:
                                return "tx50";

                            case 80:
                                return "tx50";

                            case 90:
                                return "tx50";

                            case 100:
                                return "tx100";
                        }
                    }
                    else if (num2 > 20)
                    {
                        switch (num2)
                        {
                            case 30:
                                return "tx30";

                            case 40:
                                return "tx30";

                            case 50:
                                return "tx50";
                        }
                    }
                    else
                    {
                        switch (num2)
                        {
                            case 10:
                                return "tx10";

                            case 20:
                                return "tx15";
                        }
                    }
                    goto Label_0892;

                case 0x77:
                    num2 = money;
                    if (num2 > 15)
                    {
                        switch (num2)
                        {
                            case 30:
                                return "th50";

                            case 50:
                                return "th100";

                            case 100:
                                return "th100";
                        }
                    }
                    else
                    {
                        switch (num2)
                        {
                            case 5:
                                return "th10";

                            case 10:
                                return "th15";

                            case 15:
                                return "th30";
                        }
                    }
                    goto Label_0892;

                case 0x407:
                    switch (money)
                    {
                        case 10:
                            return "cd10";

                        case 20:
                            return "cd20";

                        case 30:
                            return "cd30";

                        case 300:
                            return "cd300";

                        case 500:
                            return "cd500";

                        case 50:
                            return "cd50";

                        case 100:
                            return "cd100";
                    }
                    return "0";

                default:
                    return str;
            }
            return "0";
        Label_0892:
            return "0";
        Label_089A:
            return "0";
        }

        public static string GetMD5(string s, string _input_charset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public string GetMsgInfo(string cardstatus)
        {
            switch (cardstatus)
            {
                case "5":
                    return "ip验证错误";

                case "6":
                    return "ip验证错误";

                case "7":
                    return "无效的商户号";

                case "8":
                    return "无效的商户号";

                case "9":
                    return "无效的产品类型ID";

                case "10":
                    return "无效的产品类型ID";

                case "11":
                    return "无效的卡";

                case "12":
                    return "系统错误";

                case "13":
                    return "产品维护";

                case "14":
                    return "卡号以处理成功或正在处理中";

                case "15":
                    return "卡号提交次数过多";

                case "16":
                    return "卡密已提交失败";

                case "1001":
                    return "卡号或密码错误";

                case "1002":
                    return "卡号过期";

                case "1003":
                    return "卡余额不足";

                case "1004":
                    return "卡号不存在";

                case "1005":
                    return "卡已使用过";

                case "1006":
                    return "卡号被冻结";

                case "1007":
                    return "卡未激活";

                case "1008":
                    return "不支持的卡类型或金额";

                case "1009":
                    return "其他游戏专用卡";
            }
            return cardstatus;
        }

        private string GetParamValue(string PName)
        {
            return HttpContext.Current.Request.Form[PName].ToString().Trim().ToLower();
        }

        public string GetPaycardno(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 0x67:
                    return "cm";

                case 0x68:
                case 210:
                    return "sd";

                case 0x69:
                    return "zt";

                case 0x6a:
                    return "jw";

                case 0x6b:
                    return "qq";

                case 0x6c:
                    return "cc";

                case 0x6d:
                    return "jy";

                case 110:
                    return "wy";

                case 0x6f:
                    return "wm";

                case 0x70:
                    return "sh";

                case 0x71:
                    return "dx";

                case 0x72:
                case 0x74:
                    return str;

                case 0x73:
                    return "gy";

                case 0x75:
                    return "zy";

                case 0x76:
                    return "tx";

                case 0x77:
                    return "th";

                case 0x407:
                    return "cd";
            }
            return str;
        }

        public static string[] GetStrings(string str, string sign)
        {
            return str.Split(new string[] { sign }, StringSplitOptions.None);
        }

        private string GetVal(string retCode, string key)
        {
            string[] strArray = retCode.Split(new char[] { '&' });
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { '=' });
                if (strArray2[0] == key)
                {
                    return strArray2[1];
                }
            }
            return string.Empty;
        }

        public static string MD5(string data)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(data, "md5").ToLower();
        }

        public string Query(string orderid)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("userid={0}", base.suppAccount);
                builder.AppendFormat("&orderid={0}", orderid);
                string str3 = Cryptography.MD5(builder.ToString() + string.Format("&keyvalue={0}", base.suppKey)).ToLower();
                builder.AppendFormat("&sign={0}", str3);
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
                return WebClientHelper.GetString("http://tong.60866.com/query.aspx?" + builder.ToString(), null, "GET", Encoding.GetEncoding("utf-8"), 0x2710);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static string SendRequest(string url, string paramesmes)
        {
            return SendRequest(url, paramesmes, "GET");
        }

        public static string SendRequest(string url, string parames, string method)
        {
            string str = "";
            if ((url == null) || (url == ""))
            {
                return null;
            }
            if ((method == null) || (method == ""))
            {
                method = "GET";
            }
            if (method.ToUpper() == "GET")
            {
                try
                {
                    WebRequest request = WebRequest.Create(url + parames);
                    request.Method = "GET";
                    str = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }
            }
            return str;
        }

        internal string notify_url
        {
            get
            {
                return (base.SiteDomain + "/notify/60866_Card_Return.aspx");
            }
        }
    }
}

