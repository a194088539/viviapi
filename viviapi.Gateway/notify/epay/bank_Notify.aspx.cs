namespace viviapi.Gateway.notify.epay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.epay;

    public class bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}