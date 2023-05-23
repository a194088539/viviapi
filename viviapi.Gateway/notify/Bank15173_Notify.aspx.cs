namespace viviapi.gateway.notify
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI;

    public class Bank15173_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

