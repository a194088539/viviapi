namespace viviapi.Gateway.Ret.Alipay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Alipay;

    public class MApi_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new AliPayMApi().Return();
        }
    }
}

