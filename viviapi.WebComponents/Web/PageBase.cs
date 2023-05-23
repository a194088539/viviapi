using System;
using System.Web;
using System.Web.UI;
using viviapi.BLL;
using viviapi.Model;
using viviapi.SysConfig;
using viviLib.Security;

namespace viviapi.WebComponents.Web
{
    public class PageBase : Page
    {
        private WebInfo _webinfo = (WebInfo)null;

        public string WebSiteTitleSuffix
        {
            get
            {
                return viviapi.BLL.SysConfig.WebSiteTitleSuffix;
            }
        }

        public string KeyWords
        {
            get
            {
                return "\"" + viviapi.BLL.SysConfig.WebSiteKey + "\"";
            }
        }

        public string Description
        {
            get
            {
                return "\"" + viviapi.BLL.SysConfig.WebSitedescription + "\"";
            }
        }

        public string firstPage
        {
            get
            {
                string str = RuntimeSetting.firstpage;
                if (string.IsNullOrEmpty(str))
                    str = "Login.aspx";
                return str;
            }
        }

        public string statJs
        {
            get
            {
                if (this.webInfo != null)
                    return HttpUtility.HtmlDecode(this.webInfo.Code);
                return string.Empty;
            }
        }

        public WebInfo webInfo
        {
            get
            {
                if (this._webinfo == null)
                    this._webinfo = WebInfoFactory.CurrentWebInfo;
                return this._webinfo;
            }
        }

        public string SiteName
        {
            get
            {
                if (this.webInfo == null)
                    return string.Empty;
                return this.webInfo.Name;
            }
        }

        public DateTime FirstDayOfMonth
        {
            get
            {
                return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
            }
        }

        public DateTime ToDayFirstTime
        {
            get
            {
                return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            }
        }

        public string getTitle(string subPageTitle)
        {
            return string.Format("{1} {0}-{2}", (object)this.SiteName, (object)subPageTitle, (object)viviapi.BLL.SysConfig.WebSiteTitleSuffix);
        }

        public void AlertAndRedirect(string msg, string url)
        {
            this.AlertAndRedirect((Page)this, msg, url);
        }

        public void AlertAndRedirect(string msg)
        {
            this.AlertAndRedirect((Page)this, msg, (string)null);
        }

        public void AlertAndRedirect(Page P, string msg)
        {
            this.AlertAndRedirect(P, msg, (string)null);
        }

        public void AlertAndRedirect(Page P, string msg, string url)
        {
            string str = string.Empty;
            string script = msg != null && msg.Length != 0 || url != null && url.Length != 0 ? (msg != null && msg.Length != 0 ? (url != null && url.Length != 0 ? string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=\"{1}\";\r\n//--></SCRIPT>\r\n", (object)AntiXss.JavaScriptEncode(msg), (object)url) : string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n", (object)AntiXss.JavaScriptEncode(msg))) : string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=\"{0}\";\r\n//--></SCRIPT>\r\n", (object)url)) : "\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n";
            P.ClientScript.RegisterStartupScript(P.GetType(), "AlertAndRedirect", script);
        }

        public string CutWord(object _str)
        {
            if (_str == null)
                return string.Empty;
            return this.CutWord(HttpUtility.HtmlEncode(_str.ToString()), 30);
        }

        public string CutWord(string _str)
        {
            return this.CutWord(_str, 30);
        }

        public string CutWord(string _str, int len)
        {
            if (string.IsNullOrEmpty(_str))
                return string.Empty;
            if (_str.Length > len)
                return _str.Substring(0, len) + "...";
            return _str;
        }
    }
}
