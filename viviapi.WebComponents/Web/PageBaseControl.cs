using System.Web.UI;
using viviapi.BLL;
using viviapi.Model;
using viviLib.Security;

namespace viviapi.WebComponents.Web
{
    public class PageBaseControl : UserControl
    {
        private WebInfo _webinfo = (WebInfo)null;

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

        public void AlertAndRedirect(string msg, string url)
        {
            this.AlertAndRedirect(this.Page, msg, url);
        }

        public void AlertAndRedirect(string msg)
        {
            this.AlertAndRedirect(this.Page, msg, (string)null);
        }

        public void AlertAndRedirect(Page P, string msg, string url)
        {
            string str = string.Empty;
            string script = msg != null && msg.Length != 0 || url != null && url.Length != 0 ? (msg != null && msg.Length != 0 ? (url != null && url.Length != 0 ? string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=\"{1}\";\r\n//--></SCRIPT>\r\n", (object)AntiXss.JavaScriptEncode(msg), (object)url) : string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n", (object)AntiXss.JavaScriptEncode(msg))) : string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=\"{0}\";\r\n//--></SCRIPT>\r\n", (object)url)) : "\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n";
            P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndRedirect", script);
        }
    }
}
