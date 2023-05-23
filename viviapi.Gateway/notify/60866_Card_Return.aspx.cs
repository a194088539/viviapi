namespace viviapi.Gateway.notify
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI;

    public class _0866_Card_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card60866().CardNotify();
        }
    }
}

