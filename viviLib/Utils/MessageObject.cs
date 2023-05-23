using System.Text;
using System.Web;
using System.Web.UI;

namespace viviLib.Utils
{
    public class MessageObject
    {
        public static void Location()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append("window.location.href=window.location.href;");
            stringBuilder.Append("</script>");
            HttpContext.Current.Response.Write(stringBuilder.ToString());
        }

        public static void RedirectPage(string url)
        {
            string str = "http://" + HttpContext.Current.Request.Url.Host + url;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append(string.Format("window.location.href='{0}';", (object)str));
            stringBuilder.Append("</script>");
            HttpContext.Current.Response.Write(stringBuilder.ToString());
        }

        public static void Show(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append("alert(\"" + str.Trim() + "\"); history.back(-1);\n");
            stringBuilder.Append("</script>");
            HttpContext.Current.Response.Write(stringBuilder.ToString());
        }

        public static void Show(Page page, string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append("alert(\"" + str.Trim() + "\");\n");
            stringBuilder.Append("</script>");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "show", stringBuilder.ToString());
        }

        public static void ShowClose(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\">\n");
            stringBuilder.Append("alert(\"" + str.Trim() + "\"); \n");
            stringBuilder.Append("window.close();\n");
            stringBuilder.Append("</script>\n");
            HttpContext.Current.Response.Write(stringBuilder.ToString());
        }

        public static void ShowJS(Page MyPage, string strCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append(strCode.Trim());
            stringBuilder.Append("</script>");
            MyPage.Response.Write(stringBuilder.ToString());
        }

        public static void ShowLocation(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append("alert(\"" + str.Trim() + "\"); \n");
            stringBuilder.Append("window.location.href=window.location.href;\n");
            stringBuilder.Append("</script>");
            HttpContext.Current.Response.Write(stringBuilder.ToString());
        }

        public static void ShowPre(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append("alert(\"" + str.Trim() + "\"); \n");
            stringBuilder.Append("var p=document.referrer; \n");
            stringBuilder.Append("window.location.href=p;\n");
            stringBuilder.Append("</script>");
            HttpContext.Current.Response.Write(stringBuilder.ToString());
        }

        public static void ShowRedirect(string str, string url)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\"> \n");
            stringBuilder.Append("alert(\"" + str.Trim() + "\"); \n");
            stringBuilder.Append("window.location.href=\"" + url.Trim() + "\";\n");
            stringBuilder.Append("</script>");
            HttpContext.Current.Response.Write(stringBuilder.ToString());
        }

        public static void Write(string str)
        {
            HttpContext.Current.Response.Write(str);
        }
    }
}
