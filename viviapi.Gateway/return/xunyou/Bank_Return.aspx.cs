namespace viviapi.gateway.ret.xunyou
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.xunyou;

    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}

