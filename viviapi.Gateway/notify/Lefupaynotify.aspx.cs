using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class Lefupaynotify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Lefupayment().Notify();
        }
    }
}
