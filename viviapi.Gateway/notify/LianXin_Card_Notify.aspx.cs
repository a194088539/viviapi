using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class LianXin_Card_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new LianXinCard().CardNotify();
        }
    }
}
