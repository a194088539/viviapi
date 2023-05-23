using System;
using System.Web.UI;
using viviapi.ETAPI._91KA;

namespace viviapi.gateway.notify._91ka
{
    public class card_callback : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().Notify();
        }
    }
}
