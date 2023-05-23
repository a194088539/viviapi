using System;
using System.Web;
using viviLib.Security;

namespace viviLib.Web
{
    public class SafeCookie
    {
        private string keystr = "{F8CD3296-2524-43c9-A83B-F4E47B0A6B7D}";

        public static void DelCookie(string CookieName)
        {
            SafeCookie.SetCookie(CookieName, "", DateTime.Now.AddYears(-1));
        }

        public static string GetCookie(string CookieName)
        {
            string str = (string)null;
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
                str = Cryptography.RijndaelDecrypt(HttpContext.Current.Request.Cookies[CookieName].Value);
            return str;
        }

        public static void SetCookie(string CookieName, string CookieValue)
        {
            CookieValue = Cryptography.RijndaelEncrypt(CookieValue);
            HttpContext current = HttpContext.Current;
            if (current == null)
                return;
            current.Response.Cookies[CookieName].Value = CookieValue;
        }

        public static void SetCookie(string CookieName, string CookieValue, DateTime ExpireTime)
        {
            HttpContext.Current.Response.Cookies[CookieName].Expires = ExpireTime;
            SafeCookie.SetCookie(CookieName, CookieValue);
        }
    }
}
