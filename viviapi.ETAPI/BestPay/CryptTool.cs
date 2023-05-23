using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace viviapi.ETAPI.BestPay
{
    public class CryptTool
    {
        public static string md5Digest(string str, string charset)
        {
            string str1 = string.Empty;
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(charset).GetBytes(str))).Replace("-", "").ToUpper();
        }

        public static string getIpRemote(HttpRequest Request)
        {
            string str1 = string.Empty;
            string str2 = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(str2))
                str2 = str2.Split(',')[0].Trim();
            if (string.IsNullOrEmpty(str2))
                str2 = Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(str2))
                str2 = Request.UserHostAddress;
            return str2;
        }

        public static string getCurrentDate()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string getTodayDate2()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static long currentTimeMillis()
        {
            return (DateTime.Now.Ticks - Convert.ToDateTime("1970-1-1 0:0:0").Ticks) / 10000L;
        }
    }
}
