namespace viviapi.gateway.notify.Interface
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Longbao;

    public class Card_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().CardNotify();
        }
    }
}

