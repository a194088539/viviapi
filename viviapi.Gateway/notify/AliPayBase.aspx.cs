namespace viviapi.Gateway.notify
{
    using System;
    using System.Web.UI;

    public class AliPayBase : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new viviapi.ETAPI.AliPayBase().Notify();
        }
    }
}

