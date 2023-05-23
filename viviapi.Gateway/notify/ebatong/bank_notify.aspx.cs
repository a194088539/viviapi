namespace viviapi.Gateway.notify.ebatong
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Ebatong;

    public class bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

