using System;
using System.Web.UI;
using viviapi.ETAPI.ShenZhouFu;

namespace viviapi.gateway.notify.ShenZhouFu
{
    public class Card : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new card().Notify();
        }
    }
}
