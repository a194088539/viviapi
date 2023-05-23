namespace viviapi.gateway.Ret.weifutong
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.weifutong;

    public class BankReturn : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().BankReturn();
        }
    }
}

