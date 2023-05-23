using System;
using System.Web;
using System.Web.Caching;
using viviapi.Cache;
using viviLib.ExceptionHandling;
using viviLib.IO;

namespace viviapi.WebComponents.Template
{
    public class Helper
    {
        public static string BaseDir
        {
            get
            {
                return HttpContext.Current.Request.PhysicalApplicationPath + "\\common\\template\\";
            }
        }

        public static string GetEmailTempCont(string cacheKey, string path)
        {
            try
            {
                string str1 = Helper.BaseDir + path;
                string str2 = string.Empty;
                object obj = DefaultCacheStrategy.GetWebCacheObj.Get(cacheKey);
                string str3;
                if (obj == null)
                {
                    str3 = File.ReadFile(str1);
                    DefaultCacheStrategy.GetWebCacheObj.Insert(cacheKey, (object)str3, new CacheDependency(str1));
                }
                else
                    str3 = obj.ToString();
                return str3;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        public static string GetEmailRegisterTemp()
        {
            return Helper.GetEmailTempCont("template_email_register", "email\\register.txt");
        }

        public static string GetEmailCheckTemp()
        {
            return Helper.GetEmailTempCont("template_email_checkmail", "email\\checkemail.txt");
        }
    }
}
