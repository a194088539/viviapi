using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using viviapi.SysConfig;

namespace viviapi.ETAPI.tongyi
{
    public class PayHttpClient
    {
        private Dictionary<string, string> reqContent;
        private string resContent;
        private string method;
        private string errInfo;
        private int timeOut;
        private int responseCode;

        public PayHttpClient()
        {
            this.reqContent = new Dictionary<string, string>();
            this.reqContent["url"] = "";
            this.reqContent["data"] = "";
            this.resContent = "";
            this.method = "POST";
            this.errInfo = "";
            this.timeOut = 60;
            this.responseCode = 0;
        }

        public void setReqContent(Dictionary<string, string> reqContent)
        {
            this.reqContent = reqContent;
        }

        public string getResContent()
        {
            return this.resContent;
        }

        public void setMethod(string method)
        {
            this.method = method;
        }

        public string getErrInfo()
        {
            return this.errInfo;
        }

        public void setTimeOut(int timeOut)
        {
            this.timeOut = timeOut;
        }

        public int getResponseCode()
        {
            return this.responseCode;
        }

        public bool call()
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.reqContent["url"]);
                string s = this.reqContent["data"];
                httpWebRequest.Referer = RuntimeSetting.SiteDomain;
                httpWebRequest.Timeout = this.timeOut * 1000;
                if (s != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(s);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = (long)bytes.Length;
                    Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                this.resContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                PayHttpClient payHttpClient = this;
                string str = payHttpClient.errInfo + ex.Message;
                payHttpClient.errInfo = str;
                if (httpWebResponse != null)
                    this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
                return false;
            }
            this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
            return true;
        }

        public bool call(string key)
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.reqContent["url"]);
                string s = this.reqContent["data"];
                httpWebRequest.Referer = RuntimeSetting.SiteDomain;
                httpWebRequest.Timeout = this.timeOut * 1000;
                if (s != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(s);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = (long)bytes.Length;
                    ((NameValueCollection)httpWebRequest.Headers).Add("api-key", key);
                    Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                this.resContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                PayHttpClient payHttpClient = this;
                string str = payHttpClient.errInfo + ex.Message;
                payHttpClient.errInfo = str;
                if (httpWebResponse != null)
                    this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
                return false;
            }
            this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
            return true;
        }

        public bool call1(string key)
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.reqContent["url"]);
                string s = this.reqContent["data"];
                httpWebRequest.Referer = RuntimeSetting.SiteDomain;
                httpWebRequest.Timeout = this.timeOut * 1000;
                if (s != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(s);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = (long)bytes.Length;
                    ((NameValueCollection)httpWebRequest.Headers).Add("token", key);
                    Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                this.resContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                PayHttpClient payHttpClient = this;
                string str = payHttpClient.errInfo + ex.Message;
                payHttpClient.errInfo = str;
                if (httpWebResponse != null)
                    this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
                return false;
            }
            this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
            return true;
        }

        public bool call1()
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.reqContent["url"]);
                string s = this.reqContent["data"];
                httpWebRequest.Referer = RuntimeSetting.SiteDomain;
                httpWebRequest.Timeout = this.timeOut * 1000;
                if (s != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(s);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.ContentLength = (long)bytes.Length;
                    Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                this.resContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                PayHttpClient payHttpClient = this;
                string str = payHttpClient.errInfo + ex.Message;
                payHttpClient.errInfo = str;
                if (httpWebResponse != null)
                    this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
                return false;
            }
            this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
            return true;
        }

        public bool call2()
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.reqContent["url"]);
                httpWebRequest.Referer = RuntimeSetting.SiteDomain;
                httpWebRequest.Timeout = this.timeOut * 1000;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                this.resContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                PayHttpClient payHttpClient = this;
                string str = payHttpClient.errInfo + ex.Message;
                payHttpClient.errInfo = str;
                if (httpWebResponse != null)
                    this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
                return false;
            }
            this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
            return true;
        }
    }
}

