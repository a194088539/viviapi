namespace viviapi.Gateway.notify.ddbill
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Ddbill;

    public class bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}