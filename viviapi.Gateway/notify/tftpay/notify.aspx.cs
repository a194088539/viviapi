using System;
using System.Web.UI;
using viviapi.ETAPI.tftpay;

namespace viviapi.gateway.notify.tftpay
{
    public class notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new RMB().Notify();
        }
    }
}
