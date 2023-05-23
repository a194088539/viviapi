namespace viviapi.Gateway.notify.shukenet
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.shukenet;

    public class bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}