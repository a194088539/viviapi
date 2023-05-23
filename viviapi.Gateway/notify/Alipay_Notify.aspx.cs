namespace viviapi.Gateway.notify
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI;

    public class Alipay_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new AliPay().Notify();
        }
    }
}

