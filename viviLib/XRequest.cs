using System.Web;

namespace viviLib
{
    public class XRequest
    {
        public static string GetCheckListValue(int listlength, string listname)
        {
            string str = "";
            for (int index = 1; index < listlength + 1; ++index)
            {
                if (XRequest.GetString(listname + index.ToString()) != "")
                    str = str + XRequest.GetString(listname + index.ToString()) + ",";
            }
            if (str.Contains(","))
                str = str.Substring(0, str.Length - 1);
            return str;
        }

        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
                return string.Format("{0}:{1}", (object)request.Url.Host, (object)request.Url.Port.ToString());
            return request.Url.Host;
        }

        public static float GetFloat(string strName, float defValue)
        {
            if ((double)XRequest.GetQueryFloat(strName, defValue) == (double)defValue)
                return XRequest.GetFormFloat(strName, defValue);
            return XRequest.GetQueryFloat(strName, defValue);
        }

        public static float GetFormFloat(string strName, float defValue)
        {
            return Utility.StrToFloat((object)HttpContext.Current.Request.Form[strName], defValue);
        }

        public static int GetFormInt(string strName, int defValue)
        {
            return Utility.StrToInt((object)HttpContext.Current.Request.Form[strName], defValue);
        }

        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";
            return HttpContext.Current.Request.Form[strName];
        }

        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        public static int GetInt(string strName, int defValue)
        {
            if (XRequest.GetQueryInt(strName, defValue) == defValue)
                return XRequest.GetFormInt(strName, defValue);
            return XRequest.GetQueryInt(strName, defValue);
        }

        public static string GetIP()
        {
            string str = string.Empty;
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            switch (ip)
            {
                case "":
                case null:
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    break;
            }
            if (ip == null || ip == string.Empty)
                ip = HttpContext.Current.Request.UserHostAddress;
            if (ip == null || !(ip != string.Empty) || !Utility.IsIP(ip))
                return "0.0.0.0";
            return ip;
        }

        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return strArray[strArray.Length - 1].ToLower();
        }

        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }

        public static float GetQueryFloat(string strName, float defValue)
        {
            return Utility.StrToFloat((object)HttpContext.Current.Request.QueryString[strName], defValue);
        }

        public static int GetQueryInt(string strName, int defValue)
        {
            return Utility.StrToInt((object)HttpContext.Current.Request.QueryString[strName], defValue);
        }

        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";
            return HttpContext.Current.Request.QueryString[strName];
        }

        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
                return "";
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        public static string GetString(string strName)
        {
            if ("".Equals(XRequest.GetQueryString(strName)))
                return XRequest.GetFormString(strName);
            return XRequest.GetQueryString(strName);
        }

        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetUrlReferrer()
        {
            string str = (string)null;
            try
            {
                str = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            {
            }
            return str ?? "";
        }

        public static bool IsBrowserGet()
        {
            string[] strArray = new string[4]
            {
        "ie",
        "opera",
        "netscape",
        "mozilla"
            };
            string str = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (str.IndexOf(strArray[index]) >= 0)
                    return true;
            }
            return false;
        }

        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        public static bool IsSearchEnginesGet()
        {
            string[] strArray = new string[10]
            {
        "google",
        "yahoo",
        "msn",
        "baidu",
        "sogou",
        "sohu",
        "sina",
        "163",
        "lycos",
        "tom"
            };
            string str = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (str.IndexOf(strArray[index]) >= 0)
                    return true;
            }
            return false;
        }

        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count <= 0)
                return;
            HttpContext.Current.Request.Files[0].SaveAs(path);
        }
    }
}
