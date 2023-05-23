using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.Alipay
{
    public class Submit
    {
        private static string GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?";
        private static string _key = "";
        private static string _input_charset = "";
        private static string _sign_type = "";

        static Submit()
        {
            Submit._key = Config.Key.Trim();
            Submit._input_charset = Config.Input_charset.Trim().ToLower();
            Submit._sign_type = Config.Sign_type.Trim().ToUpper();
        }

        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            string linkString = Core.CreateLinkString(sPara);
            string str;
            switch (Submit._sign_type)
            {
                case "MD5":
                    str = AlipayMD5.Sign(linkString, Submit._key, Submit._input_charset);
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Dictionary<string, string> sPara = Core.FilterPara(sParaTemp);
            string str = Submit.BuildRequestMysign(sPara);
            sPara.Add("sign", str);
            sPara.Add("sign_type", Submit._sign_type);
            return sPara;
        }

        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            return Core.CreateLinkStringUrlencode(Submit.BuildRequestPara(sParaTemp), code);
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            Dictionary<string, string> dictionary2 = Submit.BuildRequestPara(sParaTemp);
            StringBuilder stringBuilder = new StringBuilder();
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(101);
            string str = Submit.GATEWAY_NEW;
            if (!string.IsNullOrEmpty(cacheModel.jumpUrl))
                str = cacheModel.jumpUrl + "/switch/alipay.aspx?";
            else if (!string.IsNullOrEmpty(cacheModel.postBankUrl))
                str = cacheModel.postBankUrl;
            stringBuilder.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + str + "?_input_charset=" + Submit._input_charset + "' method='" + strMethod.ToLower().Trim() + "'>");
            foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
                stringBuilder.Append("<input type='hidden' name='" + keyValuePair.Key + "' value='" + keyValuePair.Value + "'/>");
            stringBuilder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            stringBuilder.Append("<script>document.forms['alipaysubmit'].submit();</script>");
            return ((object)stringBuilder).ToString();
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp)
        {
            Encoding encoding = Encoding.GetEncoding(Submit._input_charset);
            string s = Submit.BuildRequestParaToString(sParaTemp, encoding);
            byte[] bytes = encoding.GetBytes(s);
            string requestUriString = Submit.GATEWAY_NEW + "_input_charset=" + Submit._input_charset;
            string str1;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
                httpWebRequest.Method = "post";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = (long)bytes.Length;
                Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                StringBuilder stringBuilder = new StringBuilder();
                string str2;
                while ((str2 = streamReader.ReadLine()) != null)
                    stringBuilder.Append(str2);
                responseStream.Close();
                str1 = ((object)stringBuilder).ToString();
            }
            catch (Exception ex)
            {
                str1 = "报错：" + ex.Message;
            }
            return str1;
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string fileName, byte[] data, string contentType, int lengthFile)
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            Dictionary<string, string> dictionary2 = Submit.BuildRequestPara(sParaTemp);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Submit.GATEWAY_NEW + "_input_charset=" + Submit._input_charset);
            httpWebRequest.Method = strMethod;
            string str1 = DateTime.Now.Ticks.ToString("x");
            string str2 = "--" + str1;
            httpWebRequest.ContentType = "\r\nmultipart/form-data; boundary=" + str1;
            httpWebRequest.KeepAlive = true;
            StringBuilder stringBuilder1 = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
                stringBuilder1.Append(str2 + "\r\nContent-Disposition: form-data; name=\"" + keyValuePair.Key + "\"\r\n\r\n" + keyValuePair.Value + "\r\n");
            stringBuilder1.Append(str2 + "\r\nContent-Disposition: form-data; name=\"withhold_file\"; filename=\"");
            stringBuilder1.Append(fileName);
            stringBuilder1.Append("\"\r\nContent-Type: " + contentType + "\r\n\r\n");
            string s = ((object)stringBuilder1).ToString();
            Encoding encoding = Encoding.GetEncoding(Submit._input_charset);
            byte[] bytes1 = encoding.GetBytes(s);
            byte[] bytes2 = Encoding.ASCII.GetBytes("\r\n" + str2 + "--\r\n");
            long num = (long)(bytes1.Length + lengthFile + bytes2.Length);
            httpWebRequest.ContentLength = num;
            Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream();
            Stream responseStream;
            try
            {
                requestStream.Write(bytes1, 0, bytes1.Length);
                requestStream.Write(data, 0, lengthFile);
                requestStream.Write(bytes2, 0, bytes2.Length);
                responseStream = httpWebRequest.GetResponse().GetResponseStream();
            }
            catch (WebException ex)
            {
                return ((object)ex).ToString();
            }
            finally
            {
                if (requestStream != null)
                    requestStream.Close();
            }
            StreamReader streamReader = new StreamReader(responseStream, encoding);
            StringBuilder stringBuilder2 = new StringBuilder();
            string str3;
            while ((str3 = streamReader.ReadLine()) != null)
                stringBuilder2.Append(str3);
            responseStream.Close();
            return ((object)stringBuilder2).ToString();
        }

        public static string Query_timestamp()
        {
            XmlTextReader xmlTextReader = new XmlTextReader(Submit.GATEWAY_NEW + "service=query_timestamp&partner=" + Config.Partner);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load((XmlReader)xmlTextReader);
            return xmlDocument.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText;
        }
    }
}
