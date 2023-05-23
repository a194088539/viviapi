using System;
using System.Web.UI;
using viviapi.ETAPI.tongyi;

namespace viviapi.gateway.notify
{
    public class WuYou2_AgentNotify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new WuYou2gw().Notify();
        }
    }
}