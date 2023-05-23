using System;
using System.Web.UI;
using viviapi.ETAPI.Longbao;

namespace viviapi.gateway.notify.LongBao
{
    public class Card_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().CardNotify();
        }
    }
}
