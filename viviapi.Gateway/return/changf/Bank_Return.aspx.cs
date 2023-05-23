namespace viviapi.gateway.ret.changf
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Changf;

    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}

