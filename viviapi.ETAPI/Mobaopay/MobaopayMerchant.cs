using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace viviapi.ETAPI.Mobaopay
{
    public sealed class MobaopayMerchant
    {
        private static readonly MobaopayMerchant instance = new MobaopayMerchant();

        public static MobaopayMerchant Instance
        {
            get
            {
                return MobaopayMerchant.instance;
            }
        }

        private MobaopayMerchant()
        {
        }

        private void checkResult(string result)
        {
            if (string.IsNullOrEmpty(result))
                throw new Exception("返回数据为空。");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);
            XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/moboAccount/respData");
            if (null == xmlNode1)
                throw new Exception("回复数据格式不正确，不存在/moboAccount/respData节点。");
            string outerXml = xmlNode1.OuterXml;
            XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/moboAccount/signMsg");
            if (null == xmlNode2)
                throw new Exception("回复数据格式不正确，不存在/moboAccount/signMsg节点。");
            string innerText = xmlNode2.InnerText;
            if (string.IsNullOrEmpty(outerXml) || string.IsNullOrEmpty(innerText))
                throw new Exception("回复数据格式不正确，源字符串或签名串不为空。");
            string srcData = outerXml.Replace(" />", "/>");
            if (!MobaopaySignUtil.Instance.verifyData(innerText, srcData))
                throw new Exception("回复数据签名验证失败。");
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public string transact(string requestStr, string serverUrl)
        {
            if (string.IsNullOrEmpty(requestStr))
                throw new Exception("请求数据不能为空。");
            if (string.IsNullOrEmpty(serverUrl))
                throw new Exception("请求地址不能为空。");
            string str = serverUrl.ToLower();
            bool flag = true;
            if (!str.StartsWith("https"))
                flag = false;
            requestStr = requestStr.Replace("\\+", "%2B");
            byte[] bytes = Encoding.UTF8.GetBytes(requestStr);
            HttpWebRequest httpWebRequest;
            if (flag)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                httpWebRequest = WebRequest.Create(serverUrl) as HttpWebRequest;
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            }
            else
                httpWebRequest = WebRequest.Create(serverUrl) as HttpWebRequest;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 60000;
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.1) Gecko/20061204 Firefox/2.0.0.3";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            httpWebRequest.KeepAlive = false;
            httpWebRequest.ContentLength = (long)bytes.Length;
            httpWebRequest.ServicePoint.Expect100Continue = false;
            using (Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Flush();
            }
            string result = string.Empty;
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                    result = streamReader.ReadToEnd();
            }
            if (string.IsNullOrEmpty(result))
                throw new Exception("返回参数错误！");
            this.checkResult(result);
            return result;
        }

        public string generateRefundRequest(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
                throw new Exception("apiName不能为空");
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
                throw new Exception("apiVersion不能为空");
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
                throw new Exception("platformID不能为空");
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
                throw new Exception("merchNo不能为空");
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
                throw new Exception("orderNo不能为空");
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
                throw new Exception("tradeDate不能为空");
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
                throw new Exception("amt不能为空");
            if (!sourceData.ContainsKey("tradeSummary") || string.IsNullOrEmpty(sourceData["tradeSummary"]))
                throw new Exception("tradeSummary不能为空");
            string str1 = sourceData["apiName"];
            string str2 = sourceData["apiVersion"];
            string str3 = sourceData["platformID"];
            string str4 = sourceData["merchNo"];
            string str5 = sourceData["orderNo"];
            string str6 = sourceData["tradeDate"];
            string str7 = sourceData["amt"];
            string str8 = sourceData["tradeSummary"];
            if (!str2.Equals("1.0.0.0"))
                throw new Exception("apiVersion错误！");
            return string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&tradeSummary={7}", (object)str1, (object)str2, (object)str3, (object)str4, (object)str5, (object)str6, (object)str7, (object)str8);
        }

        public string generateQueryRequest(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
                throw new Exception("apiName不能为空");
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
                throw new Exception("apiVersion不能为空");
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
                throw new Exception("platformID不能为空");
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
                throw new Exception("merchNo不能为空");
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
                throw new Exception("orderNo不能为空");
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
                throw new Exception("tradeDate不能为空");
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
                throw new Exception("amt不能为空");
            string str1 = sourceData["apiName"];
            string str2 = sourceData["apiVersion"];
            string str3 = sourceData["platformID"];
            string str4 = sourceData["merchNo"];
            string str5 = sourceData["orderNo"];
            string str6 = sourceData["tradeDate"];
            string str7 = sourceData["amt"];
            if (!str2.Equals("1.0.0.0"))
                throw new Exception("apiVersion错误！");
            return string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}", (object)str1, (object)str2, (object)str3, (object)str4, (object)str5, (object)str6, (object)str7);
        }

        public string generatePayRequest(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
                throw new Exception("apiName不能为空");
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
                throw new Exception("apiVersion不能为空");
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
                throw new Exception("platformID不能为空");
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
                throw new Exception("merchNo不能为空");
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
                throw new Exception("orderNo不能为空");
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
                throw new Exception("tradeDate不能为空");
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
                throw new Exception("amt不能为空");
            if (!sourceData.ContainsKey("merchUrl") || string.IsNullOrEmpty(sourceData["merchUrl"]))
                throw new Exception("merchUrl不能为空");
            if (!sourceData.ContainsKey("merchParam"))
                throw new Exception("merchParam可以为空，但必须存在！");
            if (!sourceData.ContainsKey("tradeSummary") || string.IsNullOrEmpty(sourceData["tradeSummary"]))
                throw new Exception("tradeSummary不能为空");
            string str1 = sourceData["apiName"];
            string str2 = sourceData["apiVersion"];
            string str3 = sourceData["platformID"];
            string str4 = sourceData["merchNo"];
            string str5 = sourceData["orderNo"];
            string str6 = sourceData["tradeDate"];
            string str7 = sourceData["amt"];
            string str8 = sourceData["merchUrl"];
            string str9 = sourceData["merchParam"];
            string str10 = sourceData["tradeSummary"];
            if (!str2.Equals("1.0.0.0"))
                throw new Exception("apiVersion错误！");
            return string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&merchUrl={7}&merchParam={8}&tradeSummary={9}", (object)str1, (object)str2, (object)str3, (object)str4, (object)str5, (object)str6, (object)str7, (object)str8, (object)str9, (object)str10);
        }
    }
}
