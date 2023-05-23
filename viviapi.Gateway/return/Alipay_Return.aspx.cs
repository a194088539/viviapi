namespace viviapi.Gateway.Return
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI;

    public class Alipay_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new AliPay().Return();
        }
    }
}

