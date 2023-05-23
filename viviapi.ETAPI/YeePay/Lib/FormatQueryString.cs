using System.Text;
using System.Web;

namespace com.yeepay.Utils
{
    public abstract class FormatQueryString
    {
        public static string GetQueryString(string strParaName)
        {
            return FormatQueryString.GetQueryString(strParaName, HttpContext.Current.Request.Url.Query, '&');
        }

        public static string GetQueryString(string strParaName, string strUrl)
        {
            return FormatQueryString.GetQueryString(strParaName, strUrl, '&');
        }

        public static string GetQueryString(string strParaName, string strUrl, char strSplitChar)
        {
            string[] strArray = strUrl.Split(strSplitChar);
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].IndexOf(strParaName) >= 0)
                    return HttpUtility.UrlDecode(strArray[index].Split('=')[1], Encoding.GetEncoding("gb2312"));
            }
            return "";
        }
    }
}
