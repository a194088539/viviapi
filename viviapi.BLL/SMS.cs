using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace OKXR.Common
{
    public class SMS
    {
        public static bool SendSMS(string mobile, string content, string ext)
        {
            string str1 = ConfigurationManager.AppSettings["SMSSN"];
            string str2 = ConfigurationManager.AppSettings["SMSPWD"];
            string str3 = HttpUtility.UrlEncode(content, Encoding.GetEncoding("gb2312"));
            return SMS.Get_Http(string.Format("http://sdk2.entinfo.cn/z_send.aspx?sn={0}&pwd={1}&mobile={2}&content={3}&ext={4}", (object)str1, (object)str2, (object)mobile, (object)str3, (object)ext), 5000) == "1";
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
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                return "错误：" + ex.Message;
            }
        }

        public static bool MobileNumValidate(string input)
        {
            string pattern = "(86)*0*1\\d{10}";
            return Regex.IsMatch(input, pattern);
        }
    }
}
