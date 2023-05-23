using CryptoKitLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace ProPalymentLib
{
    internal class ProPalyment : IProPalyment
    {
        private string _GatewayPublicKeyPath = "";
        private string _MER_ID = "";
        private string _MerCertPath = "";
        private string _MerCertPwd = "";
        private string _PostAddModel = "";
        private Dictionary<string, string> dic_Check;
        private StringBuilder sb_Check;

        public ProPalyment(string MerCertPath, string MerCertPwd, string GatewayPublicKeyPath, string PostAdd)
        {
            this._MerCertPath = MerCertPath;
            this._MerCertPwd = MerCertPwd;
            this._GatewayPublicKeyPath = GatewayPublicKeyPath;
            if (string.IsNullOrEmpty(PostAdd))
            {
                this._MerCertPath = "";
                this._MerCertPwd = "";
                this._GatewayPublicKeyPath = "";
                throw new ArgumentNullException("请求支付网关地址方式不正确");
            }
            this._PostAddModel = PostAdd;
            string[] strArray = new CryptoAgentClass().SelectSignCertificatebyPFX(this._MerCertPath, this._MerCertPwd).Split('|');
            if (strArray.Length > 2)
            {
                this._MER_ID = strArray[1];
            }
            else
            {
                this._MerCertPath = "";
                this._MerCertPwd = "";
                this._GatewayPublicKeyPath = "";
                throw new ArgumentNullException("商户ID查询不正确，请确认该证书的合法性。");
            }
        }

        private void CheckbyKey(string M_key, Dictionary<string, string> dictionary, string ErrorInfo, int Len_key, string ErrorLen)
        {
            KeyValuePair<string, string> keyValuePair1 = new KeyValuePair<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair2 in dictionary)
            {
                if (keyValuePair2.Key.Trim().ToLower() == M_key.Trim().ToLower())
                    keyValuePair1 = keyValuePair2;
            }
            if (string.IsNullOrEmpty(keyValuePair1.Value))
            {
                this.sb_Check.Append(ErrorInfo);
            }
            else
            {
                if (keyValuePair1.Value.Length <= Len_key)
                    return;
                this.sb_Check.Append(ErrorLen + Len_key.ToString() + "个字节;");
            }
        }

        private Dictionary<string, string> CheckUserDate(Dictionary<string, string> dictionary)
        {
            this.dic_Check = new Dictionary<string, string>();
            this.sb_Check = new StringBuilder();
            KeyValuePair<string, string> keyValuePair1 = new KeyValuePair<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair2 in dictionary)
            {
                if (keyValuePair2.Key.Trim().ToLower() == "code")
                    keyValuePair1 = keyValuePair2;
            }
            if (string.IsNullOrEmpty(keyValuePair1.Value))
                this.sb_Check.Append("支付交易代码 必须输入;");
            else if (keyValuePair1.Value.Trim() == "ORD001")
            {
                this.CheckbyKey("merOrderId", dictionary, "商品订单号 必须输入;", 20, "商品订单号 长度不能超过");
                this.CheckbyKey("returnUrl", dictionary, "同步返回URL 必须输入;", 120, "同步返回URL 长度不能超过");
                this.CheckbyKey("notifyUrl", dictionary, "异步通知URL 必须输入;", 120, "异步通知URL 长度不能超过");
                this.CheckbyKey("chkMethod", dictionary, "签名方式 必须输入;", 4, "签名方式 长度不能超过");
                this.CheckbyKey("merBusType", dictionary, "商户业务类型 必须输入;", 2, "商户业务类型 长度不能超过");
                this.CheckbyKey("payType", dictionary, "付款类型 必须输入;", 1, "付款类型 长度不能超过");
                this.CheckbyKey("merOrderAmt", dictionary, "订单总金额 必须输入;", 15, "订单总金额 长度不能超过");
            }
            else if (keyValuePair1.Value.Trim() == "ORD002")
            {
                this.CheckbyKey("merOrderId", dictionary, "商品订单号 必须输入;", 20, "商品订单号 长度不能超过");
                this.CheckbyKey("returnUrl", dictionary, "同步返回URL 必须输入;", 120, "同步返回URL 长度不能超过");
                this.CheckbyKey("notifyUrl", dictionary, "异步通知URL 必须输入;", 120, "异步通知URL 长度不能超过");
                this.CheckbyKey("chkMethod", dictionary, "签名方式 必须输入;", 4, "签名方式 长度不能超过");
                this.CheckbyKey("oldPayOrderId", dictionary, "原商品订单号 必须输入;", 20, "原商品订单号 长度不能超过");
                this.CheckbyKey("refundAmt", dictionary, "退款金额 必须输入;", 15, "退款金额 长度不能超过");
                this.CheckbyKey("refundReson", dictionary, "退款理由 必须输入;", 500, "退款理由 长度不能超过");
            }
            else if (keyValuePair1.Value.Trim() == "ORD003")
            {
                this.CheckbyKey("merOrderId", dictionary, "商品订单号 必须输入;", 20, "商品订单号 长度不能超过");
                this.CheckbyKey("merOrderDate", dictionary, "订单日期 必须输入;", 8, "订单日期 长度不能超过");
            }
            else if (keyValuePair1.Value.Trim() == "ORD004")
            {
                this.CheckbyKey("merOrder", dictionary, "商品订单号 必须输入;", 20, "商品订单号 长度不能超过");
                this.CheckbyKey("OrderDate", dictionary, "订单日期 必须输入;", 8, "订单日期 长度不能超过");
            }
            else if (keyValuePair1.Value.Trim() == "PAY001")
            {
                this.CheckbyKey("prdOrdNo", dictionary, "商品订单号 必须输入;", 20, "商品订单号 长度不能超过");
                this.CheckbyKey("ordAmt", dictionary, "订单总金额 必须输入;", 15, "订单总金额 长度不能超过");
                this.CheckbyKey("payerMobNo", dictionary, "买方手机号 必须输入;", 11, "买方手机号 长度不能超过");
                this.CheckbyKey("payerName", dictionary, "客户姓名 必须输入;", 20, "客户姓名 长度不能超过");
                this.CheckbyKey("certType", dictionary, "证件类型 必须输入;", 1, "证件类型 长度不能超过");
                this.CheckbyKey("certNo", dictionary, "证件号 必须输入;", 30, "证件号 长度不能超过");
                this.CheckbyKey("bankCardNo", dictionary, "银行卡号 必须输入;", 30, "银行卡号 长度不能超过");
                this.CheckbyKey("payType", dictionary, "支付类型 必须输入;", 2, "支付类型 长度不能超过");
            }
            if (this.sb_Check != null && this.sb_Check.ToString().Trim() != "")
                this.dic_Check.Add("CheckError", this.sb_Check.ToString().Trim());
            else
                this.dic_Check = (Dictionary<string, string>)null;
            return this.dic_Check;
        }

        private Dictionary<string, string> VNB_GetPage(string posturl, string postData, string str_charset)
        {
            this.VNB_SetCertificatePolicy();
            byte[] bytes1 = Encoding.GetEncoding(str_charset).GetBytes(postData);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)null;
            HttpWebRequest httpWebRequest = WebRequest.Create(posturl) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = (long)bytes1.Length;
            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(bytes1, 0, bytes1.Length);
            requestStream.Close();
            byte[] bytes2 = Convert.FromBase64String(new StreamReader((httpWebRequest.GetResponse() as HttpWebResponse).GetResponseStream(), Encoding.GetEncoding(str_charset)).ReadToEnd());
            string @string = Encoding.GetEncoding(str_charset).GetString(bytes2);
            if (!string.IsNullOrEmpty(@string))
            {
                XmlDocument xmlDocument = new XmlDocument();
                dictionary = new Dictionary<string, string>();
                xmlDocument.LoadXml(@string);
                XmlNode xmlNode1 = xmlDocument.SelectSingleNode("//data[@name='Signmessage']");
                if (xmlNode1 != null)
                {
                    string strSignedMessage = xmlNode1.Attributes["value"].Value;
                    string strSrcMessage = @string.Replace("<data name=\"Signmessage\" value=\"" + strSignedMessage + "\"/>", "");
                    if (!this.VNB_VerifySignedMessage(strSignedMessage, strSrcMessage))
                    {
                        dictionary.Add("ExcepError", "支付网关回发信息验签失败");
                        return dictionary;
                    }
                }
                foreach (XmlNode xmlNode2 in xmlDocument.SelectNodes("//data"))
                {
                    if (xmlNode2.Attributes["name"].Value != "Signmessage")
                        dictionary.Add(xmlNode2.Attributes["name"].Value, xmlNode2.Attributes["value"].Value);
                }
            }
            return dictionary;
        }

        private string VNB_PackageXML(Dictionary<string, string> dictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                if (keyValuePair.Key.Trim().ToLower() == "code")
                {
                    stringBuilder.Append("<business " + keyValuePair.Key + "=\"" + keyValuePair.Value + "\">");
                    stringBuilder.Append("<group>");
                    stringBuilder.Append("<data name=\"merNo\" value=\"" + this._MER_ID + "\"/>");
                }
            }
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                if (keyValuePair.Key.Trim().ToLower() != "code" || keyValuePair.Key.Trim().ToLower() != "merno")
                    stringBuilder.Append("<data name=\"" + keyValuePair.Key + "\" value=\"" + keyValuePair.Value + "\"/>");
            }
            stringBuilder.Append("</group>");
            stringBuilder.Append("</business>");
            return stringBuilder.ToString();
        }

        public Dictionary<string, string> VNB_ParseXML(string xmlbase64, string singmsg)
        {
            Dictionary<string, string> dictionary;
            try
            {
                dictionary = new Dictionary<string, string>();
                string @string = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(xmlbase64));
                if (this.VNB_VerifySignedMessage(singmsg, @string))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(@string);
                    XmlNode xmlNode1 = xmlDocument.SelectSingleNode("business");
                    dictionary.Add("code", xmlNode1.Attributes["code"].Value);
                    foreach (XmlNode xmlNode2 in xmlDocument.SelectNodes("//data"))
                    {
                        if (xmlNode2.Attributes["name"].Value != "Signmessage")
                            dictionary.Add(xmlNode2.Attributes["name"].Value, xmlNode2.Attributes["value"].Value);
                    }
                    return dictionary;
                }
                dictionary.Add("ExcepError", "签名信息验签失败");
            }
            catch (Exception ex)
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("ExcepError", ex.ToString());
            }
            return dictionary;
        }

        private bool VNB_RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        private void VNB_SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(this.VNB_RemoteCertificateValidate);
        }

        private string VNB_SignMessage(string strSrcMessage)
        {
            ICryptoAgent cryptoAgent = (ICryptoAgent)new CryptoAgentClass();
            cryptoAgent.SelectSignCertificatebyPFX(this._MerCertPath, this._MerCertPwd);
            return cryptoAgent.SignMessagePKCS1(strSrcMessage);
        }

        public Dictionary<string, string> VNB_SignMessageAndSendData2Gateway(Dictionary<string, string> dictionary)
        {
            try
            {
                this.CheckUserDate(dictionary);
                if (this.dic_Check != null)
                    return this.dic_Check;
                string str1 = "UTF-8";
                string str2 = HttpUtility.UrlEncode(this._MER_ID, Encoding.GetEncoding(str1));
                string str3 = this.VNB_PackageXML(dictionary);
                string str4 = HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.GetEncoding(str1).GetBytes(str3)), Encoding.GetEncoding(str1));
                string str5 = HttpUtility.UrlEncode(this.VNB_SignMessage(str3).Replace("\r\n", ""), Encoding.GetEncoding(str1));
                return this.VNB_GetPage(this._PostAddModel, "merId=" + str2 + "&xml=" + str4 + "&sign=" + str5 + "&charset=" + str1, str1);
            }
            catch (Exception ex)
            {
                this.dic_Check = new Dictionary<string, string>();
                this.dic_Check.Add("ExcepError", ex.Message);
                return this.dic_Check;
            }
        }

        private bool VNB_VerifySignedMessage(string strSignedMessage, string strSrcMessage)
        {
            ICryptoAgent cryptoAgent = (ICryptoAgent)new CryptoAgentClass();
            cryptoAgent.SelectVerifyCertbyCer(this._GatewayPublicKeyPath);
            try
            {
                return cryptoAgent.VerifyMessageSignaturePKCS1(strSignedMessage, strSrcMessage);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
