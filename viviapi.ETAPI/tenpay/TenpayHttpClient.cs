using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace tenpay
{
    public class TenpayHttpClient
    {
        private string reqContent;
        private string resContent;
        private string method;
        private string errInfo;
        private string certFile;
        private string certPasswd;
        private string caFile;
        private int timeOut;
        private int responseCode;
        private string charset;

        public TenpayHttpClient()
        {
            this.caFile = "";
            this.certFile = "";
            this.certPasswd = "";
            this.reqContent = "";
            this.resContent = "";
            this.method = "POST";
            this.errInfo = "";
            this.timeOut = 60;
            this.responseCode = 0;
            this.charset = "gb2312";
        }

        public void setReqContent(string reqContent)
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

        public void setCertInfo(string certFile, string certPasswd)
        {
            this.certFile = certFile;
            this.certPasswd = certPasswd;
        }

        public void setCaInfo(string caFile)
        {
            this.caFile = caFile;
        }

        public void setTimeOut(int timeOut)
        {
            this.timeOut = timeOut;
        }

        public int getResponseCode()
        {
            return this.responseCode;
        }

        public void setCharset(string charset)
        {
            this.charset = charset;
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public bool call()
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                string s = (string)null;
                HttpWebRequest httpWebRequest;
                if (this.method.ToUpper() == "POST")
                {
                    string[] strArray = Regex.Split(this.reqContent, "\\?");
                    httpWebRequest = (HttpWebRequest)WebRequest.Create(strArray[0]);
                    if (strArray.Length >= 2)
                        s = strArray[1];
                }
                else
                    httpWebRequest = (HttpWebRequest)WebRequest.Create(this.reqContent);
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                if (this.certFile != "")
                    httpWebRequest.ClientCertificates.Add((X509Certificate)new X509Certificate2(this.certFile, this.certPasswd));
                httpWebRequest.Timeout = this.timeOut * 1000;
                Encoding encoding = Encoding.GetEncoding(this.charset);
                if (s != null)
                {
                    byte[] bytes = encoding.GetBytes(s);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = (long)bytes.Length;
                    Stream requestStream = httpWebRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), encoding);
                this.resContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                this.errInfo += ex.Message;
                if (httpWebResponse != null)
                    this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
                return false;
            }
            this.responseCode = Convert.ToInt32((object)httpWebResponse.StatusCode);
            return true;
        }
    }
}
