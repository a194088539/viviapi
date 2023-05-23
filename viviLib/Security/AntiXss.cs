namespace viviLib.Security
{
    public sealed class AntiXss
    {
        private AntiXss()
        {
        }

        public static string HtmlAttributeEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.HtmlAttributeEncode(s);
        }

        public static string HtmlEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.HtmlEncode(s);
        }

        public static string JavaScriptEncode(string s)
        {
            if (s != null && s.Length != 0)
                return Microsoft.Security.Application.AntiXss.JavaScriptEncode(s);
            return "''";
        }

        public static string UrlEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.UrlEncode(s);
        }

        public static string VisualBasicScriptEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.VisualBasicScriptEncode(s);
        }

        public static string XmlAttributeEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.XmlAttributeEncode(s);
        }

        public static string XmlEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.XmlEncode(s);
        }
    }
}
