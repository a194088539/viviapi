namespace viviapi.Gateway.Return
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI;

    public class Gopay_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Gopay().ReturnBank();
        }
    }
}

