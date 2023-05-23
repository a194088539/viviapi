using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;

namespace viviapi.ETAPI.WxPayAPI
{
    public class HttpService
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public static string Post(string xml, string url, bool isUseCert, int timeout)
        {
            GC.Collect();
            string str = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)null;
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpService.CheckValidationResult);
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = timeout * 10000;
                httpWebRequest.ContentType = "text/xml";
                byte[] bytes = Encoding.UTF8.GetBytes(xml);
                httpWebRequest.ContentLength = (long)bytes.Length;
                httpWebRequest.ServicePoint.Expect100Continue = false;
                if (isUseCert)
                {
                    X509Certificate2 x509Certificate2 = new X509Certificate2(HttpContext.Current.Request.PhysicalApplicationPath + "cert/apiclient_cert.p12", "1233410002");
                    httpWebRequest.ClientCertificates.Add((X509Certificate)x509Certificate2);
                    Log.Debug("WxPayApi", "PostXml used cert");
                }
                Stream requestStream = ((WebRequest)httpWebRequest).GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                str = streamReader.ReadToEnd().Trim();
                streamReader.Close();
            }
            catch (ThreadAbortException ex)
            {
                Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                Log.Error("Exception message: {0}", ex.Message);
                Thread.ResetAbort();
            }
            catch (WebException ex)
            {
                Log.Error("HttpService", ((object)ex).ToString());
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    Log.Error("HttpService", "StatusCode : " + (object)((HttpWebResponse)ex.Response).StatusCode);
                    Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)ex.Response).StatusDescription);
                }
                throw new WxPayException(((object)ex).ToString());
            }
            catch (Exception ex)
            {
                Log.Error("HttpService", ((object)ex).ToString());
                throw new WxPayException(((object)ex).ToString());
            }
            finally
            {
                if (httpWebResponse != null)
                    httpWebResponse.Close();
                if (httpWebRequest != null)
                    ((WebRequest)httpWebRequest).Abort();
            }
            return str;
        }

        public static string Get(string url)
        {
            GC.Collect();
            string str = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)null;
            HttpWebResponse httpWebResponse = (HttpWebResponse)null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpService.CheckValidationResult);
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                httpWebRequest.Proxy = (IWebProxy)new WebProxy()
                {
                    Address = new Uri("http://0.0.0.0:0")
                };
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                str = streamReader.ReadToEnd().Trim();
                streamReader.Close();
            }
            catch (ThreadAbortException ex)
            {
                Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                Log.Error("Exception message: {0}", ex.Message);
                Thread.ResetAbort();
            }
            catch (WebException ex)
            {
                Log.Error("HttpService", ((object)ex).ToString());
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    Log.Error("HttpService", "StatusCode : " + (object)((HttpWebResponse)ex.Response).StatusCode);
                    Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)ex.Response).StatusDescription);
                }
                throw new WxPayException(((object)ex).ToString());
            }
            catch (Exception ex)
            {
                Log.Error("HttpService", ((object)ex).ToString());
                throw new WxPayException(((object)ex).ToString());
            }
            finally
            {
                if (httpWebResponse != null)
                    httpWebResponse.Close();
                if (httpWebRequest != null)
                    ((WebRequest)httpWebRequest).Abort();
            }
            return str;
        }
    }
}
