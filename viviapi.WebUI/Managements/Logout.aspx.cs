namespace viviapi.WebUI.Managements
{
    using System;
    using System.Web;
    using System.Web.UI;
    using viviapi.BLL;
    using viviapi.SysConfig;

    public class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.SignOut();
            string str = string.Format("/{0}/Login.aspx", RuntimeSetting.ManagePagePath);
            string s = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\ntop.location.href=\"{0}\";\r\n//--></SCRIPT>", str);
            HttpContext.Current.Response.Write(s);
        }
    }
}

