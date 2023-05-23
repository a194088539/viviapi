namespace viviapi.Gateway.notify.Baofoo
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using viviapi.ETAPI.Baofoo;

    public class Callback : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

