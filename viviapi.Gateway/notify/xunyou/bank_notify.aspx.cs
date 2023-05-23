namespace viviapi.gateway.notify.xunyou
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.xunyou;

    public class bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}