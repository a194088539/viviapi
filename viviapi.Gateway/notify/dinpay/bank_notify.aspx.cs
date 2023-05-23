namespace viviapi.Gateway.notify.dinpay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Dinpay;

    public class bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}



