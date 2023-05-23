namespace viviapi.Gateway.notify.Mobaopay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Longbao;

    public class Bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

