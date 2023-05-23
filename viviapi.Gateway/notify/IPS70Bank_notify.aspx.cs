namespace viviapi.Gateway.notify
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI;

    public class IPS70Bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new IPS70().Notify();
        }
    }
}

