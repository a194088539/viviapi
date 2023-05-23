namespace viviapi.Gateway.Ret.Baofoo
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Baofoo;

    public class Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}

