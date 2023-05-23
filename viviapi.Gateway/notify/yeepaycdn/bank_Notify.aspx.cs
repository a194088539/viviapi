namespace viviapi.Gateway.notify.yeepaycdn
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.yeepaycdn;

    public class bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}