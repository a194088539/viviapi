using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify.LianXin
{
    public class Bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new LianXinRMB().Notify();
        }
    }
}
