namespace viviapi.Gateway.notify
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.YeePay;

    public class YeePay_Card_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().Return(this.Context);
        }
    }
}

