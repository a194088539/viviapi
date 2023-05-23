namespace viviapi.WebUI.Userlogin
{
    using System;
    using System.Web;
    using System.Web.UI;
    using viviapi.BLL.User;

    public class logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserFactory.SignOut();
            string s = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\ntop.location.href=\"{0}\";\r\n//--></SCRIPT>", "/index.aspx");
            HttpContext.Current.Response.Write(s);
        }
    }
}

