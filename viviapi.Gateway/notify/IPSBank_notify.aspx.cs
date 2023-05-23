using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class IPSBank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new IPS().Notify();
        }
    }
}
