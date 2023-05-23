using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI
{
    public class OfCard : ETAPIBase
    {
        private static int suppId = 80;
        public string notify_url = RuntimeSetting.SiteDomain + "/notify/ofcard_notify.aspx";

        public OfCard()
          : base(OfCard.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errorCode, out string errormsg)
        {
            errormsg = string.Empty;
            errorCode = string.Empty;
            string postCardUrl = this.postCardUrl;
            string str1 = "-1";
            string suppAccount = this.suppAccount;
            string str2 = _cardno;
            string str3 = _cardpwd;
            string str4 = this.GetCardType(_typeId) + cardvalue.ToString();
            string str5 = "r";
            string str6 = "1.0";
            string str7 = _orderid;
            string str8 = this.notify_url;
            string str9 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string str10 = "xml";
            string suppKey = this.suppKey;
            string str11 = OfCard.md5sign(suppAccount + str5 + str6 + str7 + str4 + str2 + str3 + str8 + str9 + str10 + suppKey).ToLower();
            string s = "usercode=" + suppAccount + "&mode=" + str5 + "&version=" + str6 + "&orderno=" + str7 + "&cardcode=" + str4 + "&cardno=" + str2 + "&cardpass=" + str3 + "&retaction=" + str8 + "&datetime=" + str9 + "&format=" + str10 + "&sign=" + str11;
            try
            {
                Encoding encoding = Encoding.GetEncoding("GBK");
                byte[] bytes = encoding.GetBytes(s);
                WebRequest webRequest = WebRequest.Create(postCardUrl);
                webRequest.Timeout = 50000;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = (long)bytes.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                    requestStream.Write(bytes, 0, bytes.Length);
                StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream(), encoding);
                string xml = streamReader.ReadToEnd();
                streamReader.Close();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);
                string innerText1 = xmlDocument.GetElementsByTagName("result")[0].InnerText;
                string innerText2 = xmlDocument.GetElementsByTagName("info")[0].InnerText;
                errorCode = innerText1;
                if (!string.IsNullOrEmpty(innerText1))
                {
                    if (innerText1 != "2001")
                    {
                        str1 = "-1";
                        errormsg = innerText2;
                    }
                    else
                        str1 = "0";
                }
                return str1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return string.Empty;
        }

        public void Notify()
        {
            try
            {
                string suppKey = this.suppKey;
                string str1 = HttpContext.Current.Request.Form["usercode"];
                string str2 = HttpContext.Current.Request.Form["mode"];
                string str3 = HttpContext.Current.Request.Form["version"];
                string orderId = HttpContext.Current.Request.Form["orderno"];
                string supplierOrderId = HttpContext.Current.Request.Form["billid"];
                string str4 = HttpContext.Current.Request.Form["result"];
                string msg = HttpContext.Current.Request.Form["info"];
                string s = HttpContext.Current.Request.Form["value"];
                string str5 = HttpContext.Current.Request.Form["accountvalue"];
                string str6 = HttpContext.Current.Request.Form["datetime"];
                string str7 = HttpContext.Current.Request.Form["sign"];
                if (!(OfCard.md5sign(str1 + str2 + str3 + orderId + supplierOrderId + str4 + msg + s + str5 + str6 + suppKey).ToLower() == str7.ToLower()))
                    return;
                int status = str4 == "2000" || str4 == "2011" ? 2 : 4;
                string opstate = status != 2 ? this.ConvertCode(str4) : "0";
                OrderCard orderCard = new OrderCard();
                string userviewmsg = msg;
                orderCard.ReceiveSuppResult(OfCard.suppId, orderId, supplierOrderId, status, opstate, msg, userviewmsg, Decimal.Parse(s), new Decimal(0), str4, (byte)1);
                HttpContext.Current.Response.Write("OK");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public string ConvertCode(string suppcode)
        {
            string str = string.Empty;
            if (suppcode == "2010" || suppcode == "2016")
                str = "-1";
            else if (suppcode == "2999" || suppcode == "2012" || suppcode == "2013")
                str = "-11";
            else if (suppcode == "2018")
                str = "-10";
            if (string.IsNullOrEmpty(suppcode))
                str = "-1";
            return str;
        }

        public string GetCardType(int _type)
        {
            switch (_type)
            {
                case 103:
                    return "000101";
                case 104:
                case 210:
                    return "000601";
                case 105:
                    return "000901";
                case 106:
                    return "000501";
                case 107:
                    return "000701";
                case 108:
                    return "000201";
                case 109:
                    return "001201";
                case 110:
                    return "001001";
                case 111:
                    return "000801";
                case 112:
                    return "001101";
                case 113:
                    return "000301";
                case 117:
                    return "001601";
                case 118:
                    return "001401";
                case 119:
                    return "001301";
                case 208:
                    return "000401";
                case 209:
                    return "002201";
                default:
                    return _type.ToString();
            }
        }

        public static string md5sign(string str)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("GBK").GetBytes(str))).Replace("-", "").ToLower();
        }

        public string Query(string orderid)
        {
            orderid = orderid.Trim();
            string str1 = string.Empty;
            string requestUriString = "http://card.pay.ofpay.com/querycard.do";
            string suppAccount = this.suppAccount;
            string str2 = "q";
            string str3 = "1.0";
            string str4 = orderid;
            string str5 = "xml";
            string suppKey = this.suppKey;
            string str6 = OfCard.md5sign(suppAccount + str2 + str3 + str4 + str5 + suppKey).ToLower();
            string s = "usercode=" + suppAccount + "&mode=" + str2 + "&version=" + str3 + "&orderno=" + str4 + "&format=" + str5 + "&sign=" + str6;
            string str7;
            try
            {
                Encoding encoding = Encoding.GetEncoding("GBK");
                byte[] bytes = encoding.GetBytes(s);
                WebRequest webRequest = WebRequest.Create(requestUriString);
                webRequest.Timeout = 50000;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = (long)bytes.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                    requestStream.Write(bytes, 0, bytes.Length);
                StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream(), encoding);
                string str8 = streamReader.ReadToEnd();
                streamReader.Close();
                str7 = str8;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                str7 = ex.Message;
            }
            return str7;
        }

        public bool Finish(string callback)
        {
            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(callback);
                    string innerText1 = xmlDocument.GetElementsByTagName("usercode")[0].InnerText;
                    string innerText2 = xmlDocument.GetElementsByTagName("mode")[0].InnerText;
                    string innerText3 = xmlDocument.GetElementsByTagName("version")[0].InnerText;
                    string innerText4 = xmlDocument.GetElementsByTagName("orderno")[0].InnerText;
                    string innerText5 = xmlDocument.GetElementsByTagName("billid")[0].InnerText;
                    string innerText6 = xmlDocument.GetElementsByTagName("result")[0].InnerText;
                    string innerText7 = xmlDocument.GetElementsByTagName("info")[0].InnerText;
                    string innerText8 = xmlDocument.GetElementsByTagName("value")[0].InnerText;
                    string innerText9 = xmlDocument.GetElementsByTagName("accountvalue")[0].InnerText;
                    string innerText10 = xmlDocument.GetElementsByTagName("datetime")[0].InnerText;
                    string innerText11 = xmlDocument.GetElementsByTagName("sign")[0].InnerText;
                    string str = OfCard.md5sign(innerText1 + innerText2 + innerText3 + innerText4 + innerText5 + innerText6 + innerText7 + innerText8 + innerText9 + innerText10 + this.suppKey);
                    string opstate = "-1";
                    int status = 4;
                    if (str.ToLower() == innerText11.ToLower())
                    {
                        if (innerText6 == "2000" || innerText6 == "2011")
                        {
                            opstate = "0";
                            status = 2;
                        }
                        if (innerText6 == "2014" || innerText6 == "2001")
                            opstate = string.Empty;
                        if (!string.IsNullOrEmpty(opstate))
                        {
                            new OrderCard().ReceiveSuppResult(OfCard.suppId, innerText4, innerText5, status, opstate, innerText7, Decimal.Parse(innerText8), new Decimal(0), innerText6);
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
