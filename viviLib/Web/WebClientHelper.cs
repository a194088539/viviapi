using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace viviLib.Web
{
    public sealed class WebClientHelper
    {
        private WebClientHelper()
        {
        }

        public static string FormatRequestData(NameValueCollection list, Encoding encoding)
        {
            if (list == null || list.Count == 0)
                return string.Empty;
            string[] strArray = new string[list.Count];
            for (int index = 0; index < list.Count; ++index)
                strArray[index] = string.Format("{0}={1}", (object)list.Keys[index], (object)HttpUtility.UrlEncode(list[index], encoding));
            return string.Join("&", strArray);
        }

        public static NameValueCollection GetRequestList(string str, Encoding encoding)
        {
            string[] strArray = str.Split('&');
            NameValueCollection nameValueCollection = new NameValueCollection(strArray.Length);
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray.Length > 0)
                {
                    int length = strArray[index].IndexOf("=");
                    if (length == -1)
                        nameValueCollection.Add("", strArray[index]);
                    else
                        nameValueCollection.Add(str.Substring(0, length), str.Length > length + 1 ? str.Substring(length + 1, 0) : "");
                }
            }
            return nameValueCollection;
        }

        public static string GetString(string url, NameValueCollection list, string method, Encoding encoding)
        {
            return WebClientHelper.GetString(url, WebClientHelper.FormatRequestData(list, encoding), method, encoding, 100000);
        }

        public static string GetString(string url, string postData, string method, Encoding encoding, int timeout)
        {
            string str = string.Empty;
            using (WebResponse webResponse = WebClientHelper.GetWebResponse(url, postData, method, encoding, timeout))
            {
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    str = viviLib.IO.File.ReadContent(responseStream, encoding);
                    responseStream.Close();
                }
                webResponse.Close();
            }
            return str;
        }
        public static string GetJsonString(string url, string postData, string method, Encoding encoding, int timeout)
        {
            string str = string.Empty;
            using (WebResponse webResponse = WebClientHelper.GetJsonWebResponse(url, postData, method, encoding, timeout))
            {
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    str = viviLib.IO.File.ReadContent(responseStream, encoding);
                    responseStream.Close();
                }
                webResponse.Close();
            }
            return str;
        }
        public static WebResponse GetJsonWebResponse(string url, string postData, string method, Encoding encoding, int timeout)
        {
            if (string.Compare(method, "get", true) == 0)
            {
                url = url.IndexOf("?") != -1 ? url + "&" + postData : url + "?" + postData;
                postData = string.Empty;
                method = "GET";
            }
            else
                method = "POST";
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            if (httpWebRequest == null)
                return (WebResponse)null;
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = timeout;
            if (string.Compare(method, "post", true) == 0)
            {
                byte[] bytes = encoding.GetBytes(postData);
                httpWebRequest.ContentLength = (long)bytes.Length;
                using (Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
            }
            return httpWebRequest.GetResponse();
        }
        public static WebResponse GetWebResponse(string url, NameValueCollection list, string method, Encoding encoding, int timeout)
        {
            return WebClientHelper.GetWebResponse(url, WebClientHelper.FormatRequestData(list, encoding), method, encoding, timeout);
        }

        public static WebResponse GetWebResponse(string url, string postData, string method, Encoding encoding, int timeout)
        {
            if (string.Compare(method, "get", true) == 0)
            {
                url = url.IndexOf("?") != -1 ? url + "&" + postData : url + "?" + postData;
                postData = string.Empty;
                method = "GET";
            }
            else
                method = "POST";
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            if (httpWebRequest == null)
                return (WebResponse)null;
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Timeout = timeout;
            if (string.Compare(method, "post", true) == 0)
            {
                byte[] bytes = encoding.GetBytes(postData);
                httpWebRequest.ContentLength = (long)bytes.Length;
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
            }
            return httpWebRequest.GetResponse();
        }

        public void GetAsyncWebResponse(string url, string postData, string method, Encoding encoding, int timeout, AsyncCallback callback, object state)
        {
            if (string.Compare(method, "get", true) == 0)
            {
                url = url.IndexOf("?") != -1 ? url + "&" + postData : url + "?" + postData;
                postData = string.Empty;
                method = "GET";
            }
            else
                method = "POST";
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Timeout = timeout;
            if (string.Compare(method, "post", true) == 0)
            {
                byte[] bytes = encoding.GetBytes(postData);
                httpWebRequest.ContentLength = (long)bytes.Length;
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
            }
            httpWebRequest.BeginGetResponse(callback, state);
        }
    }
}
