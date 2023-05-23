namespace viviapi.Gateway.notify.suiszf
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.suiszf;

    public class bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}