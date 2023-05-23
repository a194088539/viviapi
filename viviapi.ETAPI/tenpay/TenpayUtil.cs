using System;
using System.Text;
using System.Web;

namespace tenpay
{
    public class TenpayUtil
    {
        public static string UrlEncode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            string str;
            try
            {
                str = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                str = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
            }
            return str;
        }

        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            string str;
            try
            {
                str = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                str = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
            }
            return str;
        }

        public static uint UnixStamp()
        {
            return Convert.ToUInt32((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        public static string BuildRandomStr(int length)
        {
            string str = new Random().Next().ToString();
            if (str.Length > length)
                str = str.Substring(0, length);
            else if (str.Length < length)
            {
                for (int index = length - str.Length; index > 0; --index)
                    str.Insert(0, "0");
            }
            return str;
        }
    }
}
