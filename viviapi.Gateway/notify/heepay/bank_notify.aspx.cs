namespace viviapi.Gateway.notify.heepay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Heepay;

    public class bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

