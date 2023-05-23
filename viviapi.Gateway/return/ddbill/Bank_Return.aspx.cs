namespace viviapi.Gateway.ret.ddbill
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Ddbill;

    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}