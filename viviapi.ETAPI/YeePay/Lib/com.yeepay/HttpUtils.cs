namespace com.yeepay
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;

    public abstract class HttpUtils
    {
        protected HttpUtils()
        {
        }

        public static string SendRequest(string url, string para)
        {
            return SendRequest(url, para, "GET");
        }

        public static string SendRequest(string url, string para, string method)
        {
            Exception exception;
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
                    WebRequest request = WebRequest.Create(url + para);
                    request.Method = "GET";
                    str = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    return exception.Message;
                }
            }
            if (method.ToUpper() == "POST")
            {
                if ((para.Length > 0) && (para.IndexOf('?') == 0))
                {
                    para = para.Substring(1);
                }
                WebRequest request2 = WebRequest.Create(url);
                request2.Method = "POST";
                request2.ContentType = "application/x-www-form-urlencoded";
                StringBuilder builder = new StringBuilder();
                char[] anyOf = new char[] { '?', '=', '&' };
                byte[] bytes = null;
                if (para == null)
                {
                    request2.ContentLength = 0L;
                }
                else
                {
                    int num;
                    for (int i = 0; i < para.Length; i = num + 1)
                    {
                        num = para.IndexOfAny(anyOf, i);
                        if (num == -1)
                        {
                            builder.Append(HttpUtility.UrlEncode(para.Substring(i, para.Length - i), Encoding.GetEncoding("gb2312")));
                            break;
                        }
                        builder.Append(HttpUtility.UrlEncode(para.Substring(i, num - i), Encoding.GetEncoding("gb2312")));
                        builder.Append(para.Substring(num, 1));
                    }
                    bytes = Encoding.Default.GetBytes(builder.ToString());
                    request2.ContentLength = bytes.Length;
                    Stream requestStream = request2.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                try
                {
                    Stream responseStream = request2.GetResponse().GetResponseStream();
                    byte[] buffer = new byte[0x200];
                    for (int j = responseStream.Read(buffer, 0, 0x200); j > 0; j = responseStream.Read(buffer, 0, 0x200))
                    {
                        Encoding encoding = Encoding.GetEncoding("gb2312");
                        str = str + encoding.GetString(buffer, 0, j);
                    }
                    return str;
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                    return exception.Message;
                }
            }
            return str;
        }
    }
}

