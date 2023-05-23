namespace viviapi.Gateway.notify._51upay
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI._51upay;

    public class bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
