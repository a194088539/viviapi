using System;
using System.Web.UI;

namespace viviapi.gateway.notify
{
    public class YZCHBank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new viviapi.ETAPI.YZCHRMB().Notify();
        }
    }
}
