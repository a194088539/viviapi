using System;
using System.Web.UI;
using viviapi.ETAPI.Card51esales;

namespace viviapi.gateway.notify
{
    public class Card51esales_Card_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().CardNotify();
        }
    }
}
