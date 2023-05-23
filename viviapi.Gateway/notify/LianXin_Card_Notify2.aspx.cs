using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class LianXin_Card_Notify2 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new LianXinCard2().CardNotify();
        }
    }
}
