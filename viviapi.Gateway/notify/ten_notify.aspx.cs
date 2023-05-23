using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class ten_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new TenPayRMB().Notify(this.Context);
        }
    }
}
