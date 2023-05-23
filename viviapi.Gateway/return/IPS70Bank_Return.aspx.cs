namespace viviapi.Gateway.Return
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI;

    public class IPS70Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new IPS70().ReturnBank();
        }
    }
}

