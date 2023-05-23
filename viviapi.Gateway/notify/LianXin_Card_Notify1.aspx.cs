using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class LianXin_Card_Notify1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new LianXinCard1().CardNotify();
        }
    }
}
