using System;
using System.Web.UI;

namespace viviapi.gateway.notify.HuiYuan
{
    public class Card : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new viviapi.ETAPI.huiyuan.Card().Notify();
        }
    }
}
