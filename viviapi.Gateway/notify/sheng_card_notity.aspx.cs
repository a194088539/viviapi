using System;
using System.Web.UI;
using viviapi.ETAPI.Shengpay;

namespace viviapi.gateway.notify
{
    public class sheng_card_notity : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Card().Notify();
        }
    }
}
