namespace viviapi.Gateway.notify.xunyou
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.xunyou;

    public class scan_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Scan().Notify();
        }
    }
}