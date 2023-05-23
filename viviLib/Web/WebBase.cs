using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using viviLib.ExceptionHandling;
using viviLib.TimeControl;

namespace viviLib.Web
{
    public sealed class WebBase
    {
        private static HttpApplication _httpApplication;

        public static HttpApplicationState Application
        {
            get
            {
                if (WebBase.Context != null)
                    return WebBase.Context.Application;
                if (WebBase._httpApplication == null)
                    return (HttpApplicationState)null;
                return WebBase._httpApplication.Application;
            }
        }

        public static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static HttpApplication HttpApplication
        {
            set
            {
                WebBase._httpApplication = value;
            }
        }

        public static HttpRequest Request
        {
            get
            {
                if (WebBase.Context == null)
                    return (HttpRequest)null;
                return WebBase.Context.Request;
            }
        }

        public static HttpResponse Response
        {
            get
            {
                if (WebBase.Context == null)
                    return (HttpResponse)null;
                return WebBase.Context.Response;
            }
        }

        public static HttpServerUtility Server
        {
            get
            {
                if (WebBase.Context == null)
                    return (HttpServerUtility)null;
                return WebBase.Context.Server;
            }
        }

        public static HttpSessionState Session
        {
            get
            {
                if (WebBase.Context == null)
                    return (HttpSessionState)null;
                return WebBase.Context.Session;
            }
        }

        private WebBase()
        {
        }

        public static NameValueCollection BuildQueryString(NameValueCollection querystring, NameValueCollection list)
        {
            if ((querystring == null || querystring.Count == 0) && (list == null || list.Count == 0))
                return new NameValueCollection(0);
            if (querystring == null || querystring.Count == 0)
                return new NameValueCollection(list);
            if (list == null || list.Count == 0)
                return new NameValueCollection(querystring);
            NameValueCollection nameValueCollection = new NameValueCollection(querystring);
            for (int index = 0; index < list.AllKeys.Length; ++index)
                nameValueCollection[list.AllKeys[index]] = list[list.AllKeys[index]];
            return nameValueCollection;
        }

        public static string BuildQueryStringString(NameValueCollection querystring, NameValueCollection list)
        {
            NameValueCollection nameValueCollection = WebBase.BuildQueryString(querystring, list);
            if (nameValueCollection.Count == 0)
                return string.Empty;
            string str = string.Empty;
            string[] allKeys = nameValueCollection.AllKeys;
            if (allKeys != null)
            {
                for (int index1 = 0; index1 < allKeys.Length; ++index1)
                {
                    string[] values = nameValueCollection.GetValues(allKeys[index1]);
                    for (int index2 = 0; index2 < values.Length; ++index2)
                        str = str.Length != 0 ? str + string.Format("&{0}={1}", (object)allKeys[index1], (object)HttpUtility.UrlEncode(values[index2])) : str + string.Format("?{0}={1}", (object)allKeys[index1], (object)HttpUtility.UrlEncode(values[index2]));
                }
            }
            return str;
        }

        public static string GetPageUrl(string pagerParam, int page)
        {
            return WebBase.Request.Path + WebBase.BuildQueryStringString(WebBase.Request.QueryString, new NameValueCollection(1)
      {
        {
          pagerParam,
          string.Format("{0:d}", (object) page)
        }
      });
        }

        public static bool GetQueryStringBoolean(string param, bool defaultValue)
        {
            if (WebBase.Request.QueryString[param] != null)
            {
                bool flag = !defaultValue;
                if (string.Compare(WebBase.Request.QueryString[param], flag.ToString(), true) == 0 || string.Compare(WebBase.Request.QueryString[param], defaultValue ? "0" : "1", true) == 0)
                    return !defaultValue;
            }
            return defaultValue;
        }

        public static DateTime GetQueryStringDateTime(string param, DateTime defaultValue)
        {
            if (WebBase.Request.QueryString[param] != null && WebBase.Request.QueryString[param].Length != 0)
                return FormatConvertor.StringToDateTime(WebBase.Request.QueryString[param]);
            return defaultValue;
        }

        public static Decimal GetQueryStringDecimal(string param, Decimal defaultValue)
        {
            if (WebBase.Request.QueryString[param] != null && WebBase.Request.QueryString[param].Length != 0)
            {
                try
                {
                    return Convert.ToDecimal(WebBase.Request.QueryString[param]);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
            return defaultValue;
        }

        public static double GetQueryStringDouble(string param, double defaultValue)
        {
            if (WebBase.Request.QueryString[param] != null && WebBase.Request.QueryString[param].Length != 0)
            {
                try
                {
                    return Convert.ToDouble(WebBase.Request.QueryString[param]);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
            return defaultValue;
        }

        public static int GetQueryStringInt32(string param, int defaultValue)
        {
            if (WebBase.Request.QueryString[param] != null && WebBase.Request.QueryString[param].Length != 0)
            {
                try
                {
                    return Convert.ToInt32(WebBase.Request.QueryString[param], 10);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
            return defaultValue;
        }

        public static long GetQueryStringInt64(string param, long defaultValue)
        {
            if (WebBase.Request.QueryString[param] != null && WebBase.Request.QueryString[param].Length != 0)
            {
                try
                {
                    return Convert.ToInt64(WebBase.Request.QueryString[param], 10);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
            return defaultValue;
        }

        public static string GetQueryStringString(string param, string defaultValue)
        {
            if (WebBase.Request.QueryString[param] == null)
                return defaultValue;
            return WebBase.Request.QueryString[param];
        }

        public static string GetFormString(string param, string defaultValue)
        {
            if (WebBase.Request.Form[param] == null)
                return defaultValue;
            return WebBase.Request.Form[param];
        }
    }
}
