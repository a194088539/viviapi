namespace viviapi.Gateway.notify.Alipay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Alipay;

    public class Alipay_Notify_mapi : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new AliPayMApi().Notify();
        }
    }
}

