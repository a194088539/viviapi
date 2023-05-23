using System;
using System.Web.UI;
using viviapi.ETAPI.tongyi;

namespace viviapi.gateway.notify
{
    public class WuYou2_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new WuYou2API().Notify();
        }
    }
}