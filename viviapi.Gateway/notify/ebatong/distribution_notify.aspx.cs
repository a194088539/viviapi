namespace viviapi.Gateway.notify.ebatong
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Ebatong.distribution;

    public class distribution_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new gw().Notify();
        }
    }
}

