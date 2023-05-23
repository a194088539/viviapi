namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web;
    using System.Web.UI;
    using viviapi.BLL.User;

    public class loginout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserFactory.SignOut();
            string s = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\ntop.location.href=\"{0}\";\r\n//--></SCRIPT>", "/login.aspx");
            HttpContext.Current.Response.Write(s);
        }
    }
}

