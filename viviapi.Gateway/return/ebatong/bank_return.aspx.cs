namespace viviapi.Gateway._return.ebatong
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using viviapi.ETAPI.Ebatong;

    public class bank_notify : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}

