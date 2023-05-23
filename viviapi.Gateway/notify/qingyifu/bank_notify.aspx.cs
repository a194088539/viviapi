namespace viviapi.Gateway.notify.qingyifu
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Qingyifu;

    public class bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}