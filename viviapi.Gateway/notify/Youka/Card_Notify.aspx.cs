using System;
using System.Web.UI;
using viviapi.ETAPI.Youka;

namespace viviapi.gateway.notify.Youka
{
    public class Card_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().CardNotify();
        }
    }
}
