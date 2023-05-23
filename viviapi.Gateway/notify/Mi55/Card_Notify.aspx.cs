using System;
using System.Web.UI;
using viviapi.ETAPI.Mi55;

namespace viviapi.gateway.notify.Mi55
{
    public class Card_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().CardNotify();
        }
    }
}
