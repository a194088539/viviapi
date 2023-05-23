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

namespace viviapi.ETAPI
{
    public class Cared70 : ETAPIBase
    {
        private static int suppId = 70;
        private string version = "v2.5";

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/yzch_card_return.aspx";
            }
        }

        public Cared70()
          : base(Cared70.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            supporderid = string.Empty;
            errmsg = string.Empty;
            if (this.version == "v1.0")
                return this.CardSendV10(_orderid, _cardno, _cardpwd, _typeId, cardvalue, out supporderid, out errmsg);
            if (this.version == "v2.5")
                return this.CardSendV25(_orderid, _cardno, _cardpwd, _typeId, cardvalue, out supporderid, out errmsg);
            else
                return "-1";
        }

        public string CardSendV10(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            string str1 = this.postCardUrl + "?";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            string str2 = _cardno;
            string str3 = _cardpwd;
            string str4 = _orderid;
            string paycardno = this.GetPaycardno(_typeId);
            string cardType = this.GetCardType(_typeId, cardvalue);
            string str5 = cardvalue.ToString();
            string str6 = Cared70.MD5(string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&keyvalue={8}", (object)suppAccount, (object)str4, (object)paycardno, (object)cardType, (object)str2, (object)str3, (object)str5, (object)this.notify_url, (object)suppKey).ToLower());
            string str7 = string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&sign={8}&ext={9}", (object)suppAccount, (object)str4, (object)paycardno, (object)cardType, (object)str2, (object)str3, (object)str5, (object)this.notify_url, (object)str6, (object)string.Empty);
            string str8 = string.Empty;
            string str9 = string.Empty;
            string str10 = string.Empty;
            try
            {
                string str11 = "-1";
                string[] strings = Cared70.GetStrings(Cared70.SendRequest(str1.ToLower(), str7.ToLower()), "&");
                string cardstatus = strings[0].Replace("returncode=", string.Empty);
                string str12 = strings[1].Replace("returnorderid=", string.Empty);
                string str13 = strings[2].Replace("sign=", string.Empty);
                if (Cared70.MD5(string.Format("returncode={0}&returnorderid={1}&keyvalue={2}", (object)cardstatus, (object)str12, (object)suppKey).ToLower()) == str13)
                {
                    errmsg = this.GetMsgInfo(cardstatus);
                    supporderid = str12;
                    if (!string.IsNullOrEmpty(cardstatus) && cardstatus == "1")
                        str11 = "0";
                }
                else
                    errmsg = "签名失败";
                return str11;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        public string CardSendV25(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("userid={0}", (object)this.suppAccount);
            stringBuilder.AppendFormat("&orderno={0}", (object)_orderid);
            stringBuilder.AppendFormat("&typeid={0}", (object)this.GetPaycardno(_typeId));
            stringBuilder.AppendFormat("&cardno={0}", (object)_cardno);
            stringBuilder.AppendFormat("&encpwd={0}", (object)0);
            stringBuilder.AppendFormat("&cardpwd={0}", (object)_cardpwd);
            stringBuilder.AppendFormat("&cardpwdenc={0}", (object)string.Empty);
            stringBuilder.AppendFormat("&money={0}", (object)cardvalue);
            stringBuilder.AppendFormat("&url={0}", (object)this.notify_url);
            string str1 = Cryptography.MD5(((object)stringBuilder).ToString() + string.Format("&keyvalue={0}", (object)this.suppKey)).ToLower();
            stringBuilder.AppendFormat("&sign={0}", (object)str1);
            stringBuilder.AppendFormat("&ext={0}", (object)string.Empty);
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            string[] strArray1 = WebClientHelper.GetString(this.postCardUrl + "?" + ((object)stringBuilder).ToString(), (string)null, "GET", Encoding.GetEncoding("utf-8"), 10000).Split('&');
            string str2 = "-1";
            if (strArray1.Length >= 2)
            {
                if (strArray1[0] == "returncode=1")
                {
                    str2 = "0";
                }
                else
                {
                    errmsg = strArray1[1].Replace("message=", "");
                    string[] strArray2 = errmsg.Split(',');
                    errmsg = strArray2[0];
                }
            }
            return str2;
        }

        public void CardNotify()
        {
            if (this.version == "v1.0")
            {
                this.CardNotifyV10();
            }
            else
            {
                if (!(this.version == "v2.5"))
                    return;
                this.CardNotifyV25();
            }
        }

        public void CardNotifyV10()
        {
            string str1 = ((object)HttpContext.Current.Request.QueryString["returncode"]).ToString().Trim().ToLower();
            string str2 = ((object)HttpContext.Current.Request.QueryString["userid"]).ToString().Trim().ToLower();
            string orderId = ((object)HttpContext.Current.Request.QueryString["orderid"]).ToString().Trim().ToLower();
            string str3 = ((object)HttpContext.Current.Request.QueryString["typeid"]).ToString().Trim().ToLower();
            string str4 = ((object)HttpContext.Current.Request.QueryString["productid"]).ToString().Trim().ToLower();
            string supplierOrderId = ((object)HttpContext.Current.Request.QueryString["cardno"]).ToString().Trim().ToLower();
            string str5 = ((object)HttpContext.Current.Request.QueryString["cardpwd"]).ToString().Trim().ToLower();
            string str6 = ((object)HttpContext.Current.Request.QueryString["money"]).ToString().Trim().ToLower();
            string s = ((object)HttpContext.Current.Request.QueryString["realmoney"]).ToString().Trim().ToLower();
            string cardstatus = ((object)HttpContext.Current.Request.QueryString["cardstatus"]).ToString().Trim().ToLower();
            string str7 = ((object)HttpContext.Current.Request.QueryString["sign"]).ToString().Trim().ToLower();
            ((object)HttpContext.Current.Request.QueryString["ext"]).ToString().Trim().ToLower();
            string errtype = ((object)HttpContext.Current.Request.QueryString["errtype"]).ToString().Trim();
            string suppKey = this.suppKey;
            try
            {
                string data = string.Format("returncode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}", (object)str1, (object)str2, (object)orderId, (object)str3, (object)str4, (object)supplierOrderId, (object)str5, (object)str6, (object)s, (object)cardstatus, (object)suppKey);
                string str8 = Cared70.MD5(data);
                if (str8 == str7)
                {
                    string suppcode = "-1";
                    string opstate = !(str1 == "1") ? this.ConvertCode(suppcode) : "0";
                    int status = str1 == "1" ? 2 : 4;
                    new OrderCard().ReceiveSuppResult(Cared70.suppId, orderId, supplierOrderId, status, opstate, this.GetMsgInfo(cardstatus), Decimal.Parse(s), new Decimal(0), errtype);
                }
                else
                    LogHelper.Write(string.Format("70card 原字串{0} 本地sign{1},接口商sign:{2}", (object)data, (object)str8, (object)str7));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            HttpContext.Current.Response.Write("ok");
        }

        public void CardNotifyV25()
        {
            HttpContext.Current.Request.ContentEncoding = Encoding.UTF8;
            string str1 = ((object)HttpContext.Current.Request.QueryString["returncode"]).ToString().Trim();
            string supplierOrderId = ((object)HttpContext.Current.Request.QueryString["yzchorderno"]).ToString().Trim();
            string str2 = ((object)HttpContext.Current.Request.QueryString["userid"]).ToString().Trim();
            string orderId = ((object)HttpContext.Current.Request.QueryString["orderno"]).ToString().Trim();
            string str3 = ((object)HttpContext.Current.Request.QueryString["money"]).ToString().Trim();
            string s = ((object)HttpContext.Current.Request.QueryString["realmoney"]).ToString().Trim();
            string str4 = ((object)HttpContext.Current.Request.QueryString["sign"]).ToString().Trim();
            ((object)HttpContext.Current.Request.QueryString["ext"]).ToString().Trim();
            string str5 = ((object)HttpContext.Current.Request.QueryString["message"]).ToString();
            string suppKey = this.suppKey;
            try
            {
                string data = string.Format("returncode={0}&yzchorderno={1}&userid={2}&orderno={3}&money={4}&realmoney={5}&keyvalue={6}", (object)str1, (object)supplierOrderId, (object)str2, (object)orderId, (object)str3, (object)s, (object)suppKey);
                string str6 = Cared70.MD5(data);
                if (str6 == str4)
                {
                    string opstate = "-1";
                    if (str1 == "1")
                        opstate = "0";
                    int status = str1 == "1" ? 2 : 4;
                    OrderCard orderCard = new OrderCard();
                    if (!string.IsNullOrEmpty(str5))
                        str5 = str5.Replace("message=", "").Split(',')[0];
                    if (str5 == "成功")
                        str5 = "支付成功";
                    orderCard.ReceiveSuppResult(Cared70.suppId, orderId, supplierOrderId, status, opstate, str5, Decimal.Parse(s), new Decimal(0), str5);
                }
                else
                    LogHelper.Write(string.Format("70card 原字串{0} 本地sign{1},接口商sign:{2}", (object)data, (object)str6, (object)str4));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            HttpContext.Current.Response.Write("ok");
            HttpContext.Current.Response.End();
        }

        public string ConvertCode(string suppcode)
        {
            string str = string.Empty;
            if (suppcode == "1001")
                str = "-1";
            else if (suppcode == "1002")
                str = "-2";
            else if (suppcode == "1003")
                str = "-10";
            else if (suppcode == "1004")
                str = "-4";
            else if (suppcode == "1005")
                str = "-5";
            else if (suppcode == "1006")
                str = "-6";
            else if (suppcode == "1007")
                str = "-7";
            else if (suppcode == "1008")
                str = "-8";
            else if (suppcode == "1009")
                str = "-9";
            if (string.IsNullOrEmpty(suppcode))
                str = "-1";
            return str;
        }

        private string GetParamValue(string PName)
        {
            return ((object)HttpContext.Current.Request.Form[PName]).ToString().Trim().ToLower();
        }

        public string GetMsgInfo(string cardstatus)
        {
            switch (cardstatus)
            {
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
                default:
                    return cardstatus;
            }
        }

        public static string Get_Http(string a_strUrl, int timeout)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(a_strUrl);
                httpWebRequest.Timeout = timeout;
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default);
                StringBuilder stringBuilder = new StringBuilder();
                while (-1 != streamReader.Peek())
                    stringBuilder.Append(streamReader.ReadLine());
                return ((object)stringBuilder).ToString();
            }
            catch (Exception ex)
            {
                return "错误：" + ex.Message;
            }
        }

        public string GetCardType(int paytype, int money)
        {
            switch (paytype)
            {
                case 103:
                    switch (money)
                    {
                        case 300:
                            return "cm300";
                        case 500:
                            return "cm500";
                        case 50:
                            return "cm50";
                        case 100:
                            return "cm100";
                        case 10:
                            return "cm10";
                        case 20:
                            return "cm20";
                        case 30:
                            return "cm30";
                        default:
                            return "cm" + money.ToString();
                    }
                case 104:
                    switch (money)
                    {
                        case 100:
                            return "sd100";
                        case 350:
                            return "sd350";
                        case 1000:
                            return "sd1000";
                        case 45:
                            return "sd45";
                        case 50:
                            return "sd50";
                        case 25:
                            return "sd25";
                        case 30:
                            return "sd30";
                        case 35:
                            return "sd35";
                        case 5:
                            return "sd5";
                        case 10:
                            return "sd10";
                        default:
                            return "sd" + money.ToString();
                    }
                case 105:
                    switch (money)
                    {
                        case 300:
                            return "zt300";
                        case 500:
                            return "zt500";
                        case 1000:
                            return "zt1000";
                        case 60:
                            return "zt60";
                        case 100:
                            return "zt100";
                        case 30:
                            return "zt30";
                        case 50:
                            return "zt50";
                        case 10:
                            return "zt10";
                        case 20:
                            return "zt20";
                        default:
                            return "zt" + money.ToString();
                    }
                case 106:
                    switch (money)
                    {
                        case 50:
                            return "jw50";
                        case 100:
                            return "jw100";
                        case 200:
                            return "jw200";
                        case 4:
                            return "jw4";
                        case 5:
                            return "jw5";
                        case 6:
                            return "jw6";
                        case 10:
                            return "jw10";
                        case 15:
                            return "jw15";
                        case 30:
                            return "jw30";
                        default:
                            return "jw" + money.ToString();
                    }
                case 107:
                    switch (money)
                    {
                        case 100:
                            return "qq100";
                        case 200:
                            return "qq200";
                        case 30:
                            return "qq30";
                        case 60:
                            return "qq60";
                        case 5:
                            return "qq5";
                        case 10:
                            return "qq10";
                        case 15:
                            return "qq15";
                        default:
                            return "qq" + money.ToString();
                    }
                case 108:
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
                        default:
                            return "cc" + money.ToString();
                    }
                case 109:
                    switch (money)
                    {
                        case 30:
                            return "jy30";
                        case 50:
                            return "jy50";
                        case 100:
                            return "jy100";
                        case 5:
                            return "jy5";
                        case 10:
                            return "jy10";
                        default:
                            return "jy" + money.ToString();
                    }
                case 110:
                    switch (money)
                    {
                        case 10:
                            return "wy10";
                        case 15:
                            return "wy15";
                        case 30:
                            return "wy30";
                        default:
                            return "wy" + money.ToString();
                    }
                case 111:
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
                        default:
                            return "wm" + money.ToString();
                    }
                case 112:
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
                        default:
                            return "sh" + money.ToString();
                    }
                case 113:
                    switch (money)
                    {
                        case 50:
                            return "dx50";
                        case 100:
                            return "dx100";
                        default:
                            return "dx" + money.ToString();
                    }
                case 115:
                    switch (money)
                    {
                        case 30:
                            return "gy30";
                        case 50:
                            return "gy50";
                        case 100:
                            return "gy100";
                        case 10:
                            return "gy10";
                        case 20:
                            return "gy15";
                        default:
                            return "gy" + money.ToString();
                    }
                case 117:
                    switch (money)
                    {
                        case 50:
                            return "zy50";
                        case 100:
                            return "zy100";
                        case 500:
                            return "zy500";
                        case 10:
                            return "zy10";
                        case 15:
                            return "zy15";
                        case 30:
                            return "zy30";
                        default:
                            return "zy" + money.ToString();
                    }
                case 118:
                    switch (money)
                    {
                        case 80:
                            return "tx80";
                        case 90:
                            return "tx90";
                        case 100:
                            return "tx100";
                        case 60:
                            return "tx60";
                        case 70:
                            return "tx70";
                        case 30:
                            return "tx30";
                        case 40:
                            return "tx40";
                        case 50:
                            return "tx50";
                        case 10:
                            return "tx10";
                        case 20:
                            return "tx15";
                        default:
                            return "tx" + money.ToString();
                    }
                case 119:
                    switch (money)
                    {
                        case 30:
                            return "th50";
                        case 50:
                            return "th100";
                        case 100:
                            return "th100";
                        case 5:
                            return "th10";
                        case 10:
                            return "th15";
                        case 15:
                            return "th30";
                        default:
                            return "th" + money.ToString();
                    }
                case 200:
                case 201:
                case 202:
                case 203:
                    switch (money)
                    {
                        case 50:
                            return "cd50";
                        case 100:
                            return "cd100";
                        case 300:
                            return "cd300";
                        case 10:
                            return "cd10";
                        case 20:
                            return "cd20";
                        case 30:
                            return "cd30";
                        default:
                            return "cd" + money.ToString();
                    }
                case 209:
                    switch (money)
                    {
                        case 80:
                            return "txzx80";
                        case 90:
                            return "txzx90";
                        case 100:
                            return "txzx100";
                        case 60:
                            return "txzx60";
                        case 70:
                            return "txzx70";
                        case 30:
                            return "txzx30";
                        case 40:
                            return "txzx40";
                        case 50:
                            return "txzx50";
                        case 10:
                            return "txzx10";
                        case 20:
                            return "txzx15";
                        default:
                            return "txzx" + money.ToString();
                    }
                default:
                    return "0";
            }
        }

        public static string GetMD5(string s, string _input_charset)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder stringBuilder = new StringBuilder(32);
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x").PadLeft(2, '0'));
            return ((object)stringBuilder).ToString();
        }

        public string GetPaycardno(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 103:
                    return "cm";
                case 104:
                    return "sd";
                case 105:
                    return "zt";
                case 106:
                    return "jw";
                case 107:
                    return "qq";
                case 108:
                    return "cc";
                case 109:
                    return "jy";
                case 110:
                    return "wy";
                case 111:
                    return "wm";
                case 112:
                    return "sh";
                case 113:
                    return "dx";
                case 115:
                    return "gy";
                case 117:
                    return "zy";
                case 118:
                    return "tx";
                case 119:
                    return "th";
                case 200:
                case 201:
                case 202:
                case 203:
                    return "cd";
                case 209:
                    return "txzx";
                default:
                    return str;
            }
        }

        public static string[] GetStrings(string str, string sign)
        {
            return str.Split(new string[1]
            {
        sign
            }, StringSplitOptions.None);
        }

        public static string MD5(string data)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(data, "md5").ToLower();
        }

        public static string SendRequest(string url, string paramesmes)
        {
            return Cared70.SendRequest(url, paramesmes, "GET");
        }

        public static string SendRequest(string url, string parames, string method)
        {
            string str = "";
            if (url == null || url == "")
                return (string)null;
            if (method == null || method == "")
                method = "GET";
            if (method.ToUpper() == "GET")
            {
                try
                {
                    WebRequest webRequest = WebRequest.Create(url + parames);
                    webRequest.Method = "GET";
                    str = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return str;
        }

        public static string[] BubbleSort(string[] r)
        {
            for (int index1 = 0; index1 < r.Length; ++index1)
            {
                bool flag = false;
                for (int index2 = r.Length - 2; index2 >= index1; --index2)
                {
                    if (string.CompareOrdinal(r[index2 + 1], r[index2]) < 0)
                    {
                        string str = r[index2 + 1];
                        r[index2 + 1] = r[index2];
                        r[index2] = str;
                        flag = true;
                    }
                }
                if (!flag)
                    return r;
            }
            return r;
        }

        public string Query(string orderid)
        {
            string str1 = string.Empty;
            string str2;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("userid={0}", (object)this.suppAccount);
                stringBuilder.AppendFormat("&orderno={0}", (object)orderid);
                string str3 = Cryptography.MD5(((object)stringBuilder).ToString() + string.Format("&keyvalue={0}", (object)this.suppKey)).ToLower();
                stringBuilder.AppendFormat("&sign={0}", (object)str3);
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
                str2 = WebClientHelper.GetString("http://tong.yzch.net/query.ashx?" + ((object)stringBuilder).ToString(), (string)null, "GET", Encoding.GetEncoding("utf-8"), 10000);
            }
            catch (Exception ex)
            {
                str2 = ex.Message;
            }
            return str2;
        }

        public bool Finish(string orderid, string callback)
        {
            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    string[] strArray = callback.Split('&');
                    if (strArray.Length == 3)
                    {
                        string opstate = "-1";
                        int status = 4;
                        string errtype = strArray[0].Replace("returncode=", "");
                        string s = strArray[1].Replace("realmoney=", "");
                        string msg = strArray[2].Replace("message=", "");
                        if (msg == "成功")
                            msg = "支付成功";
                        if (errtype == "1")
                        {
                            opstate = "0";
                            status = 2;
                        }
                        else if (errtype == "-1" || errtype == "2")
                            opstate = string.Empty;
                        if (!string.IsNullOrEmpty(opstate))
                        {
                            new OrderCard().ReceiveSuppResult(Cared70.suppId, orderid, string.Empty, status, opstate, msg, Decimal.Parse(s), new Decimal(0), errtype);
                            flag = true;
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
