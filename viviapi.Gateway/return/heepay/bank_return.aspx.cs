namespace viviapi.Gateway.ret.heepay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Heepay;

    public class bank_return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}

