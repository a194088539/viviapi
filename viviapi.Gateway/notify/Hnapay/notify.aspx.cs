using System;
using System.Web.UI;
using viviapi.ETAPI.Heepay;

namespace viviapi.gateway.notify.Hnapay
{
    public class notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
